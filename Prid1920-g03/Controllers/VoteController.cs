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
    [Route("api/[controller]")]
    [ApiController]

    public class VoteController : ControllerBase {

        private readonly Prid1920_g03Context model;

        public VoteController(Prid1920_g03Context _model){
            this.model = _model;
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddVote(VoteDTO data)
        {
            var post = await model.Posts.FindAsync(data.PostId);
            if(post == null)
                return NotFound();

            var vote = await model.Votes.SingleOrDefaultAsync(p => p.AuthorId == data.AuthorId && p.PostId == data.PostId);
            
            var user = await model.Users.SingleOrDefaultAsync(u => u.Pseudo == User.Identity.Name);
            if(user == null)
                return NotFound();

            Vote newVote = new Vote()
            {
                UpDown = data.UpDown,
                AuthorId = data.AuthorId,
                PostId = data.PostId
            };
            
            if(vote != null){
                model.Votes.Remove(vote);
                if(vote.UpDown == 1){
                    post.User.Reputation -= 10; 
                }else{
                    post.User.Reputation += 2; 
                    user.Reputation += 1;
                }
            }

            model.Votes.Add(newVote);
            if(data.UpDown == 1){
                post.User.Reputation += 10; 
            }else{
                post.User.Reputation -= 2; 
                user.Reputation -= 1;
            }
            
            await model.SaveChangesAsyncWithValidation();

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVote(int id)
        {
            var user = await model.Users.SingleOrDefaultAsync(u => u.Pseudo == User.Identity.Name);
            var post = await model.Posts.FindAsync(id);
            if(post == null)
                return NotFound();
            var vote = await model.Votes.SingleOrDefaultAsync(p => p.AuthorId == user.Id && p.PostId == id);

            if(vote != null){
                model.Votes.Remove(vote);
                if(vote.UpDown == 1){
                    post.User.Reputation -= 10; 
                }else{
                    post.User.Reputation += 2; 
                    user.Reputation += 1;
                }
            }else{
                return BadRequest();
            }

            await model.SaveChangesAsyncWithValidation();

            return NoContent();
        }
    }
}
