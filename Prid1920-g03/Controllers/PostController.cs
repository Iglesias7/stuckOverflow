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

        private readonly Prid1920_g03Context model ;

        public PostController(Prid1920_g03Context _model){

            this.model = _model;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts() {

            return (await model.Posts.ToListAsync()).ToDTO();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetOnePost(int postId) {

           var post = await model.Posts.FindAsync(postId);
           if(post == null){
               return NotFound();
           }
            return post.ToDTO();
        
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> AddPost(PostDTO data){

            var post = await model.Posts.SingleOrDefaultAsync(p => p.Title == data.Title);
            if(post != null){
                var error = new ValidationErrors().Add("Change the title, this one is already used", nameof(post.Title));
                return BadRequest(error);
            }

            var newPost = new Post(){
                Title = data.Title,
                Body = data.Body,
                Timestamp = data.Timestamp
            };
            model.Posts.Add(newPost);
            var res = await model.SaveChangesAsyncWithValidation();
            if(!res.IsEmpty)
                return BadRequest(res);
            return CreatedAtAction(nameof(GetOnePost), new {id = newPost.Id }, newPost.ToDTO());
        }

        //Only the owner of a post can delete it 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
           var post = await model.Posts.FindAsync(id);

           if(post == null){
               return NotFound();
           } 
        // Supression en cascade des relations par composition
            foreach(var p in post.LsPosts){
                var com = (from c in model.Comments where c.Post.Id == p.Id 
                select c).FirstOrDefault();
                var vt = (from v in model.Votes where v.Post.Id == p.Id 
                select v).FirstOrDefault();
                if(com != null)
                    model.Comments.Remove(com);
                if(vt != null)
                     model.Votes.Remove(vt);             
                model.Posts.Remove(p);
            }
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

        
    }
}