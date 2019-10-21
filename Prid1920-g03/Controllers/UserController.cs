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
    [Authorize]
    [Route("api/[Controller]")]
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

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<User>> Authenticate(UserDTO data)
        {
            var user = await Authenticate(data.Pseudo, data.Password);
            if (user == null)
                return BadRequest(new ValidationErrors().Add("user not found", "Pseudo"));
            if (user.Token == null)
                return BadRequest(new ValidationErrors().Add("Incorrect password", "Password"));
            return Ok(user);
        }

        private async Task<User> Authenticate(string pseudo, string password)
        {
            var user = await _context.Users.FindAsync(pseudo);
            // return null if member not found
            if (user == null)
                return null;
            if (user.Password == password)
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
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
            }
            // remove password before returning
            user.Password = null;
            return user;
        }
    }

}
