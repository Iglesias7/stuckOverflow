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

    [Route("api/[controller]")]
    [ApiController]

    public class VoteController : ControllerBase {

        private readonly Prid1920_g03Context model;

        public VoteController(Prid1920_g03Context _model){
            this.model = _model;
        }
        

        [HttpPost]
        public async Task<IActionResult> AddVote(VoteDTO data)
        {
            var post = await model.Posts.FindAsync(data.PostId);
            if(post == null)
                return NotFound();

            var vote = await model.Votes.SingleOrDefaultAsync(p => p.AuthorId == data.AuthorId && p.PostId == data.PostId);

            Vote newVote = new Vote()
            {
                UpDown = data.UpDown,
                AuthorId = data.AuthorId,
                PostId = data.PostId
            };

            if(vote != null){
                model.Votes.Remove(vote);
            }

            model.Votes.Add(newVote);

            await model.SaveChangesAsyncWithValidation();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVote(int id)
        {
            var user = await model.Users.SingleOrDefaultAsync(u => u.Pseudo == User.Identity.Name);

            var vote = await model.Votes.SingleOrDefaultAsync(p => p.AuthorId == user.Id && p.PostId == id);

            if(vote != null){
                model.Votes.Remove(vote);
            }else{
                return BadRequest();
            }

            await model.SaveChangesAsyncWithValidation();

            return NoContent();
        }
    }
}
