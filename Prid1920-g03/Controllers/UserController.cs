using System;
using PRID_Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prid1920_g03.Models;

namespace Prid1920_g03.Controllers
{
    [Route("api/users")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly Prid1920_g03Context _context;

        public UserController(Prid1920_g03Context context)
        {
            _context = context;
            
            // The system must always have an admin

            if(_context.Users.Count() == 0){
                _context.Users.Add(new User{ Pseudo = "admin", Password = "admin", Email = "administrator@gmail.com"});
                _context.SaveChanges();
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            return (await _context.Users.ToListAsync()).ToDTO();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetOne(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            return user.ToDTO();
        }

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
            return CreatedAtAction(nameof(GetOne), new { id = newUser.Id }, newUser.ToDTO() );
        }

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
    }

}
