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

    public class CommentController : ControllerBase {

        private readonly Prid1920_g03Context model;

        public CommentController(Prid1920_g03Context _model){
            this.model = _model;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetOneComment(int id)
        {
            var comment =await model.Comments.FindAsync(id);
            if(comment == null)
                return NotFound();
            return comment.ToDTO();
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> AddComment(CommentDTO data)
        {

            var user = await model.Users.FindAsync(data.AuthorId);
            if(user == null)
                return BadRequest();
            var post = await model.Posts.FindAsync(data.PostId);
            if(post == null )
                return BadRequest();
            var newComment = new Comment()
            {
               Body = data.Body,
               Timestamp = data.Timestamp,
               AuthorId = data.AuthorId,
               PostId = data.PostId,
               Post = post,
               User = user

            };
            post.Comments.Add(newComment);
            model.Comments.Add(newComment);
            var res = await model.SaveChangesAsyncWithValidation();
            if(!res.IsEmpty)
                return BadRequest(res);
            return CreatedAtAction(nameof(GetOneComment), new {id = newComment.Id}, newComment.ToDTO());
         }
   

        /*Only the owner of a post or an administrator
        can execute this action */

        [HttpPut("{id}")]
        public async Task<IActionResult> EditComment(int id, CommentDTO data)
        {
            var user = await model.Users.FindAsync(data.AuthorId);

            if(id != data.Id)
                return BadRequest();
            var comment = await model.Comments.FindAsync(id);
            if(comment == null)
                return NotFound();
            if(user == null)
                return BadRequest();
            if(user.Id != comment.AuthorId || !!User.IsInRole(Role.Admin.ToString()) )
            comment.Body = data.Body;

            await model.SaveChangesAsyncWithValidation();
            return NoContent();
        }

        /*Only the owner of a post or an administrator
        can execute this action */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id, CommentDTO data)
        {
            if(id != data.Id)
                return BadRequest();
            var com = await model.Comments.FindAsync(id);
            var user = await model.Users.FindAsync(com.AuthorId);
            if(user == null)
                return BadRequest();
            if(com.AuthorId != data.AuthorId || !User.IsInRole(Role.Admin.ToString()) )
                return BadRequest();

            model.Comments.Remove(com);

            await model.SaveChangesAsyncWithValidation();

            return NoContent();
        }

    }
}
