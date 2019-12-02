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


    public class PostController : ControllerBase {

        private readonly Prid1920_g03Context model;

        public PostController(Prid1920_g03Context _model){
            this.model = _model;
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

        // [HttpPost]
        // public async Task<ActionResult<PostDTO>> AddPost(PostDTO data){

        //     var post = await model.Posts.SingleOrDefaultAsync(p => p.Title == data.Title);
        //     var user = await model.Users.FindAsync(data.AuthorId);
        //     if(post != null){
        //         var error = new ValidationErrors().Add("Change the title, this one is already used", nameof(post.Title));
        //         return BadRequest(error);
        //     }
        //     if(user == null){
        //         return BadRequest("The author of the post doesn't exist !");
        //     }
        //     var newPost = new Post(){
        //         Title = data.Title,
        //         Body = data.Body,
        //         Timestamp = data.Timestamp,
        //         User = user,
        //         AuthorId = authorId
        //     };
        //     model.Posts.Add(newPost);
        //     var res = await model.SaveChangesAsyncWithValidation();
        //     if(!res.IsEmpty)
        //         return BadRequest(res);
        //     return CreatedAtAction(nameof(GetOnePost), new {id = newPost.Id }, newPost.ToDTO());
        // }

        //Only the owner of a post can delete it 

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeletePost(int id, int authorId)
        // {
        //    var post = await model.Posts.FindAsync(id);
        //    var user = await model.Users.FindAsync(authorId);

        //    if(post == null){
        //        return NotFound();
        //    } 
        //    if(user.AuthorId != authorId || user.Role != Role.Admin)
        //         return NotFound();
        //     var comments = (from c in model.Comments where c.Post.Id == post.Id 
        //     select c);
        //     var votes = (from v in model.Votes where v.Post.Id == post.Id 
        //     select v);
        //     foreach(var c in comments)
        //         if(c != null)
        //             model.Comments.Remove(c);
        //     foreach(var v in votes )
        //         if(v != null)
        //             model.Votes.Remove(v);
        //     model.Posts.Remove(post);  

        //     await model.SaveChangesAsync();

        //     return NoContent();
            
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> EditPost(int id, PostDTO data)
        // {
        //     var user = await model.Users.FindAsync(data.AuthorId);
        //     if(id != data.Id)
        //         return BadRequest();
        //     var post = model.Posts.FindAsync(id);
        //     if(post == null)
        //         return NotFound();
        //     if(user == null  )
        //         return NotFound(); 
        //     if(user.Id != post.AuthorId || user.Role != Role.Admin )
        //         return NotFound("You are not the owner of this post !"); 
  
        // //     post.Title = data.Title;
        // //     post.Body = data.Body;

        // //     await model.SaveChangesAsyncWithValidation();
            
        //     return NoContent();

        // }

        // [HttpGet]
        // public async Task<ActionResult<CommentDTO>> GetAllComments()
        // {
        //     return(await model.Comments.ToListAsync()).ToDTO();
        // }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<CommentDTO>> GetOneComment(int id)
        // {
        //     var comment =await model.Comments.FindAsync(id);
        //     if(comment == null)
        //         return NotFound();
        //     return comment.CommentDTO();
        // }

        // [HttpPost]
        // public async Task<ActionResult<PostDTO>> AddComment(CommentDTO data)
        // {
        //     var com = await model.Comments.FindAsync(data.Id);
        //     if(com != null)
        //         return BadRequest('Error');
        //     var user = await model.Users.FindAsync(data.AuthorId);
        //     if(user == null)
        //         return BadRequest("Error! The author of the comment doesn't exists in our db");
        //     var post = await model.Posts.FindAsync(data.PostId);
        //     if(post == null ) 
        //         return BadRequest("Error! Really weird the post witch the comment is based doesn't exists in our db");  
        //     var newComment = new Comment()
        //     {
        //        Body = data.Body,
        //        Timestamp = data.Timestamp,
        //        AuthorId = data.AuthorId,
        //        PostId = data,
        //        Post = post,
        //        User = user


        //     };
        //     model.Comments.Add(newComment);
        //     var res = await model.SaveChangesAsyncWithValidation();
        //     if(!res.IsEmpty)
        //         return BadRequest(res);
        //     return CreatedAtAction(nameof(GetOneComment), new {id = newComment.Id}, newComment.ToDTO());
            
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> EditComment(int id, CommentDTO data)
        // {
        //     var user = await model.Users.FindAsync(data.AuthorId);
            
        //     if(id != data.Id)
        //         return BadRequest();
        //     var comment = await model.Comments.FindAsync(id);
        //     if(comment == null)
        //         return NotFound();
        //     if(user == null)
        //         return BadRequest("Error! The author of the comment doesn't exists in our db");
        //     if(user.id != comment.AuthorId || user.Role != Role.Admin)
        //     comment.Body = data.Body;

        //     await model.SaveChangesAsyncWithValidation();
        //     return NoContent();
            
                  
        // }

        // [HttpPost("{id}")]
        // public async Task<IActionResult> DeleteComment(int id, CommentDTO data)
        // {
        //     if(id != data.Id)
        //         return BadRequest();
        //     var comment = await model.Comments.FindAsync(id);
        //     var user = await model.Users.FindAsync(comment.AuthorId);
        //     if(user == null)
        //         return BadRequest("Error! The author of the comment doesn't exists in our db");
        //     if(comment.AuthorId != data.AuthorId || user.Role != Role.Admin )
        //         return BadRequest("Only the author or the admin can execute this action!");
            
        //     await model.Comments.Remove(comment);

        //     await model.SaveChangesAsync();

        //     return NoContent();
        // }

        
    }
}