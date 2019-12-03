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

    /***********************TO GET CURRENT USER NAME************ */
    // var user = User.Identity.Name
    /***********************TO VERiFY IF CURRENT USER ROLE IS ADMIN *****************/
    //  User.IsInRole(Role.Admin.ToString())

    public class PostController : ControllerBase {

        private readonly Prid1920_g03Context model;
        private User currentUser;

        public PostController(Prid1920_g03Context _model){
            this.model = _model;
            var userName = User.Identity.Name;
            var user = (from u in model.Users where u.Pseudo == userName select u).FirstOrDefault();
            if(user != null)
                this.currentUser = user;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts() {
             return (await model.Posts.ToListAsync()).ToDTO();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetOnePost(int id) {

           var post = await model.Posts.FindAsync(id);
           if(post == null){
               return NotFound();
           }
            return post.ToDTO();
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> AddPost(PostDTO data){

            var post = await model.Posts.SingleOrDefaultAsync(p => p.Title == data.Title);
            var user = await model.Users.FindAsync(data.AuthorId);
            if(post != null){
                var error = new ValidationErrors().Add("Change the title, this one is already used", nameof(post.Title));
                return BadRequest(error);
            }
            if(user == null){
                return BadRequest();
            }
            var newPost = new Post(){
                Title = data.Title,
                Body = data.Body,
                Timestamp = data.Timestamp,
                User = user,
                AuthorId = data.AuthorId
            };
            model.Posts.Add(newPost);
            var res = await model.SaveChangesAsyncWithValidation();
            if(!res.IsEmpty)
                return BadRequest(res);
            return CreatedAtAction(nameof(GetOnePost), new {id = newPost.Id }, newPost.ToDTO());
        }

        /*Only the owner of a post or an administrator 
        can execute this action */

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id, PostDTO data)
        {
           
           var post = await model.Posts.FindAsync(id);
           var user = await model.Users.FindAsync(data.AuthorId);

           if(post == null){
               return NotFound();
           } 
           if(post.AuthorId != user.Id || currentUser.Role != Role.Admin)
                return NotFound();
            var comments = (from c in model.Comments where c.Post.Id == post.Id 
            select c);
            var votes = (from v in model.Votes where v.Post.Id == post.Id 
            select v);
            foreach(var c in comments)
                if(c != null)
                    model.Comments.Remove(c);
            foreach(var v in votes )
                if(v != null)
                    model.Votes.Remove(v);
            model.Posts.Remove(post);  

            await model.SaveChangesAsync();

            return NoContent();
            
        }

        /*Only the owner of a post or an administrator 
        can execute this action */

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPost(int id, PostDTO data)
        {
            var user = await model.Users.FindAsync(data.AuthorId);
            if(id != data.Id)
                return BadRequest();
            var post = await model.Posts.FindAsync(id);
            if(post == null)
                return NotFound();
            if(user == null  )
                return NotFound(); 
            if(user.Id != post.AuthorId || currentUser.Role != Role.Admin )
                return NotFound("You are not the owner of this post !"); 
  
        // //     post.Title = data.Title;
        // //     post.Body = data.Body;

        // //     await model.SaveChangesAsyncWithValidation();
            
            return NoContent();

        }

        [Authorize]
        [HttpGet("getallcomments")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllComments()
        {
             return(await model.Comments.ToListAsync()).ToDTO();
        }

        [HttpGet("getonecomment/{id}")]
        public async Task<ActionResult<CommentDTO>> GetOneComment(int id)
        {
            var comment =await model.Comments.FindAsync(id);
            if(comment == null)
                return NotFound();
            return comment.ToDTO();
        }

        [HttpPost("addcomment")]
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

        [HttpPut("editcomment/{id}")]
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
            if(user.Id != comment.AuthorId || currentUser.Role != Role.Admin )
            comment.Body = data.Body;

            await model.SaveChangesAsyncWithValidation();
            return NoContent();
            
                  
        }

        /*Only the owner of a post or an administrator 
        can execute this action */
        [HttpPost("deletecomment/{id}")]
        public async Task<IActionResult> DeleteComment(int id, CommentDTO data)
        {
            if(id != data.Id)
                return BadRequest();
            var com = await model.Comments.FindAsync(id);
            var user = await model.Users.FindAsync(com.AuthorId);
            if(user == null)
                return BadRequest();
            if(com.AuthorId != data.AuthorId || currentUser.Role != Role.Admin )
                return BadRequest();
            
            model.Comments.Remove(com);

            await model.SaveChangesAsyncWithValidation();

            return NoContent();
        }

        
    }
}