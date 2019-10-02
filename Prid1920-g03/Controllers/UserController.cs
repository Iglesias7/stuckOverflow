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
    [Route("api/user")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly Prid1920_g03Context _context;

        public UserController(Prid1920_g03Context context)
        {
            _context = context;
            
            if (_context.Users.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Users.Add(new User { Pseudo = "ben", Password = "ben", FirstName = "Benoît Penelle" });
                _context.Users.Add(new User { Pseudo = "bruno", Password = "bruno", FirstName = "Bruno Lacroix" });
                _context.SaveChanges();
            }



        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            return (await _context.Users.ToListAsync()).ToDTO();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetOne(int UserId)
        {
            var member = await _context.Users.FindAsync(UserId);
            if (member == null)
                return NotFound();
            return member.ToDTO();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostMember(UserDTO data)
        {
            var member = await _context.Users.FindAsync(data.Pseudo);
            if (member != null)
            {
                var err = new ValidationErrors().Add("Pseudo already in use", nameof(member.Pseudo));
                return BadRequest(err);
            }
            var newMember = new User()
            {
                Pseudo = data.Pseudo,
                Password = data.Password,
                LastName = data.LastName,
                FirstName = data.FirstName,
                BirthDate = data.BirthDate,
                Email = data.Email,
                Reputation = data.Reputation,
            };
            _context.Users.Add(newMember);
            var res = await _context.SaveChangesAsyncWithValidation();
            if (!res.IsEmpty)
                return BadRequest(res);
            return CreatedAtAction(nameof(GetOne), new { pseudo = newMember.Pseudo }, newMember.ToDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(string pseudo, UserDTO memberDTO)
        {
            if (pseudo != memberDTO.Pseudo)
                return BadRequest();
            var member = await _context.Users.FindAsync(pseudo);
            if (member == null)
                return NotFound();
            member.FirstName = memberDTO.FirstName;
            member.LastName = memberDTO.LastName;
            member.BirthDate = memberDTO.BirthDate;
            var res = await _context.SaveChangesAsyncWithValidation();
            if (!res.IsEmpty)
                return BadRequest(res);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int UserId)
        {
            var member = await _context.Users.FindAsync(UserId);

            if (member == null)
            {
                return NotFound();
            }

            _context.Users.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
