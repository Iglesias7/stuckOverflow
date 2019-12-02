using System;
using PRID_Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prid1920_g03.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Security.Claims;
using Prid1920_g03.Helpers;

namespace Prid1920_g03.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly Prid1920_g03Context _context;

        public UserController(Prid1920_g03Context context)
        {
            _context = context;
        }

        [Authorized(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            return (await _context.Users.ToListAsync()).ToDTO();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetOne(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            return user.ToDTO();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO data)
        {
            var user = await _context.Users.FindAsync(data.Id);
            if (user != null)
            {
                var err = new ValidationErrors().Add("Pseudo already in use", nameof(user.Id));
                return BadRequest(err);
            }
            var newUser = new User()
            {
                Pseudo = data.Pseudo,
                Password = data.Password,
                LastName = data.LastName,
                FirstName = data.FirstName,
                BirthDate = data.BirthDate,
                Email = data.Email,
                Reputation = data.Reputation,
                Role = data.Role,
                PicturePath = data.PicturePath
            };
            _context.Users.Add(newUser);

            var res = await _context.SaveChangesAsyncWithValidation();
            if (!res.IsEmpty)
                return BadRequest(res);
            return CreatedAtAction(nameof(GetOne), new { id = newUser.Id }, newUser.ToDTO());
        }

        [Authorized(Role.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
                return BadRequest();
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.BirthDate = userDTO.BirthDate;
            var res = await _context.SaveChangesAsyncWithValidation();
            if (!res.IsEmpty)
                return BadRequest(res);

            if (!string.IsNullOrWhiteSpace(userDTO.PicturePath))
                // On ajoute un timestamp à la fin de l'url pour générer un URL différent quand on change d'image
                // car sinon l'image ne se rafraîchit pas parce que l'url ne change pas et le browser la prend dans la cache.
                user.PicturePath = userDTO.PicturePath + "?" + DateTime.Now.Ticks;
            else
                user.PicturePath = null;

            if (userDTO.Password != null)
                user.Password = userDTO.Password;

            return NoContent();
        }

        [Authorized(Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Suppression en cascade des relations avec ce membre
            var comments = (from c in _context.Comments where c.User.Id == user.Id select c);
            var votes = (from v in _context.Votes where v.User.Id == user.Id select v);
            var posts = (from p in _context.Posts where p.Id == user.Id select p);
            if(comments != null)
                foreach(var c in comments)
                    _context.Remove(c);    
            if(votes != null)
                foreach(var v in votes)
                    _context.Votes.Remove(v);
            if(posts != null)
                foreach(var p in posts)
                    _context.Posts.Remove(p);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDTO>> Authenticate(UserDTO data)
        {
            var user = await Authenticate(data.Pseudo, data.Password);
            if (user == null)
                return BadRequest(new ValidationErrors().Add("user not found", "Pseudo"));
            if (user.Token == null)
                return BadRequest(new ValidationErrors().Add("Incorrect password", "Password"));
            return user.ToDTO();
        }

        private async Task<User> Authenticate(string pseudo, string password)
        {

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Pseudo == pseudo);

            // return null if member not found
            if (user == null)
                return null;
            if ( user.Password == TokenHelper.GetPasswordHash(password))
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("my-super-secret-key");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                                                 {
                                             new Claim(ClaimTypes.Name, user.Pseudo),
                                             new Claim(ClaimTypes.Role, user.Role.ToString())
                                                 }),
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
            }
            // remove password before returning
            user.Password = null;
            return user;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<ActionResult<UserDTO>> Signup(UserDTO data)
        {

            return await this.PostUser(data);
        }



        [AllowAnonymous]
        [HttpGet("availablePseudo/{pseudo}")]
        public async Task<ActionResult<bool>> getByPseudo(string pseudo)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Pseudo == pseudo);
            return user == null;
        }

        [AllowAnonymous]
        [HttpGet("availableEmail/{email}")]
        public async Task<ActionResult<bool>> getByEmail(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user == null;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] string pseudo, [FromForm]IFormFile picture)
        {
            if (picture != null && picture.Length > 0)
            {
                //var fileName = Path.GetFileName(picture.FileName);
                var fileName = pseudo + "-" + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".jpg";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", fileName);
                using (var fileSrteam = new FileStream(filePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileSrteam);
                }
                return Ok($"\"uploads/{fileName}\"");
            }
            return Ok();
        }

        [HttpPost("cancel")]
        public IActionResult Cancel([FromBody] dynamic data)
        {
            string picturePath = data.picturePath;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", picturePath);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            return Ok();
        }

        [HttpPost("confirm")]
        public IActionResult Confirm([FromBody] dynamic data)
        {
            string pseudo = data.pseudo;
            string picturePath = data.picturePath;
            string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", pseudo + ".jpg");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", picturePath);
            if (System.IO.File.Exists(path))
            {
                if (System.IO.File.Exists(newPath))
                    System.IO.File.Delete(newPath);
                System.IO.File.Move(path, newPath);
            }
            return Ok();
        }


        [HttpGet("rels/{pseudo}")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersWithRelationship(string pseudo)
        {
            var friends = await _context.Users.ToListAsync();
            var friendsDTO = new List<object>();
            foreach (var friend in friends)
            {
                var isFollower = friend.Followees.Where(f => f.Pseudo == pseudo).Count() > 0;
                var isFollowee = friend.Followers.Where(f => f.Pseudo == pseudo).Count() > 0;
                var isMutual = isFollower && isFollowee;
                var rel = "none";
                if (friend.Pseudo == pseudo)
                    rel = "self";
                else if (isMutual)
                    rel = "mutual";
                else if (isFollower)
                    rel = "follower";
                else if (isFollowee)
                    rel = "followee";

                friendsDTO.Add(new
                {
                    Pseudo = friend.Pseudo,
                    LastName = friend.LastName,
                    FirstName = friend.FirstName,
                    Email = friend.Email,
                    Reputation = friend.Reputation,
                    Role = friend.Role,
                    PicturePath = friend.PicturePath,
                    Relationship = rel
                });
            }
            return Ok(friendsDTO);
        }

        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromBody] dynamic body) {
            var follower = (int)body.follower;
            var followee = (int)body.followee;
            var rel = await _context.Follows.Where(f => f.Follower.Id == follower && f.Followee.Id == followee).SingleOrDefaultAsync();
            
            if (rel == null) {
                _context.Add(new Follow { FollowerPseudo = follower, FolloweePseudo = followee });
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost("unfollow")]
        public async Task<IActionResult> UnFollow([FromBody] dynamic body) {
            var follower = (int)body.follower;
            var followee = (int)body.followee;
            var rel = await _context.Follows.Where(f => f.Follower.Id == follower && f.Followee.Id == followee).SingleOrDefaultAsync();
            
            if (rel != null) {
                _context.Remove(rel);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

    }

}
