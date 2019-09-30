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
        private readonly prid1920_g03Context _context;

        public UserController(prid1920_g03Context context)
        {
            _context = context;

            if (_context.Users.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Members.Add(new User { Pseudo = "Iglesias", Password = "Tamo", FullName = "Palmiste" });
                _context.Members.Add(new User { Pseudo = "ben", Password = "ben", FullName = "Benoît Penelle" });
                _context.Members.Add(new User { Pseudo = "bruno", Password = "bruno", FullName = "Bruno Lacroix" });
                _context.SaveChanges();
            }
        }

         //DELETE: api/Todo/5
         [HttpDelete("{pseudo}")]
         public async Task<IActionResult> DeleteUser(string pseudo)
         {
             var user = await _context.Users.FindAsync(pseudo);

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

        [HttpGet("{pseudo}")]
        public async Task<ActionResult<UserDTO>> GetOne(string pseudo)
        {
            var user = await _context.Users.FindAsync(pseudo);
            if (user == null)
                return NotFound();
            return user.ToDTO();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO data)
        {
            var user = await _context.Users.FindAsync(data.Pseudo);
            if (user != null)
            {
                var err = new ValidationErrors().Add("Pseudo already in use", nameof(user.Pseudo));
                return BadRequest(err);
            }
            var newUser = new User()
            {
                Pseudo = data.Pseudo,
                Password = data.Password,
                FullName = data.FullName,
                BirthDate = data.BirthDate,
            };
            _context.Users.Add(newUser);
            var res = await _context.SaveChangesAsyncWithValidation();
            if (!res.IsEmpty)
                return BadRequest(res);
            return CreatedAtAction(nameof(GetOne), new { pseudo = newUser.Pseudo }, newUser.ToDTO());
        }

        [HttpPut("{pseudo}")]
        public async Task<IActionResult> PutUser(string pseudo, UserDTO userDTO)
        {
            if (pseudo != userDTO.Pseudo)
                return BadRequest();
            var user = await _context.Members.FindAsync(pseudo);
            if (user == null)
                return NotFound();
            user.FullName = userDTO.FullName;
            user.BirthDate = userDTO.BirthDate;
            var res = await _context.SaveChangesAsyncWithValidation();
            if (!res.IsEmpty)
                return BadRequest(res);
            return NoContent();
        }
    }
}