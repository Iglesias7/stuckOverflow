using PRID_Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prid1920_g03.Models;


namespace prid1920_g03.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly prid1920_g03 _context;

        public UserController(prid1920_g03 context)
        {
            _context = context;

            if (_context.Users.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Users.Add(new User { Pseudo = "Iglesias", Password = "Tamo", FirstName = "Palmiste" });
                _context.Users.Add(new User { Pseudo = "ben", Password = "ben", FirstName = "Benoît Penelle" });
                _context.Users.Add(new User { Pseudo = "bruno", Password = "bruno", FirstName = "Bruno Lacroix" });
                _context.SaveChanges();
            }
        }

         //DELETE: api/Todo/5
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
        //new
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
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
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                BirthDate = data.BirthDate,
                Reputation = data.Reputation,
            };
            _context.Users.Add(newUser);
            var res = await _context.SaveChangesAsyncWithValidation();
            if (!res.IsEmpty)
                return BadRequest(res);
            return CreatedAtAction(nameof(GetOne), new { id = newUser.Id }, newUser.ToDTO());
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
            user.BirthDate = userDTO.BirthDate;
            var res = await _context.SaveChangesAsyncWithValidation();
            if (!res.IsEmpty)
                return BadRequest(res);
            return NoContent();
        }
    }
}