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

        public PostController(Prid1920_g03Context _model){
            this.model = _model;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts() {
            var itemList = from p in model.Posts
                        where p.Title != (null)
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetOnePost(int id) {

           var post = await model.Posts.FindAsync(id);
           if(post == null){
               return NotFound();
           }
            return post.ToDTO();
        }

        [HttpGet("getbytagname/{name}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetByTagName(string name){
            var tag = (from t in model.Tags where t.Name == name select t).FirstOrDefault();
            if(tag == null)
                return NotFound();
            var posts = (from p in model.Posts
                         where p.Id ==
                         (from t in model.PostTags where t.TagId == tag.Id select t.PostId).FirstOrDefault()
                         select p);
            if(posts == null)
                return NotFound();
            return (await posts.ToListAsync()).ToDTO();
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> AddPost(PostDTO data){

            var user = await model.Users.FindAsync(data.AuthorId);
            if(user == null){
                return BadRequest();
            }

            var newPost = new Post(){
                Title = data.Title,
                Body = data.Body,
                Timestamp = DateTime.Now,
                User = user,
                AuthorId = data.AuthorId,
                ParentId = data.ParentId,
            };

            var newDateTime = newPost.Timestamp;

            model.Posts.Add(newPost);
         await model.SaveChangesAsyncWithValidation();


            if(data.LsTags != null){
                foreach(var t in data.LsTags){
                    var tag = await model.Tags.SingleOrDefaultAsync(p => p.Name == t);
                    var post = await model.Posts.SingleOrDefaultAsync(p => p.Title == data.Title && p.Body == data.Body && p.Timestamp == newDateTime);
                    var newPostTag = new PostTag(){
                        PostId = post.Id,
                        TagId = tag.Id,
                        Post = post,
                        Tag = tag
                    };

                    model.PostTags.Add(newPostTag);
                }
            }

            await model.SaveChangesAsyncWithValidation();

            return CreatedAtAction(nameof(GetOnePost), new {id = newPost.Id }, newPost.ToDTO());
        }


        [HttpPut("accept/{id}")]
        public async Task<ActionResult<PostDTO>> AcceptPost(int id, PostDTO data){

            var user = await model.Users.FindAsync(data.AuthorId);
            if(user == null){
                return BadRequest();
            }

            var post = await model.Posts.FindAsync(id);
            if(post == null)
                return NotFound();
           
            post.AcceptedAnswerId = data.AcceptedAnswerId;

            await model.SaveChangesAsyncWithValidation();

            return NoContent();
        }

        /*Only the owner of a post or an administrator
        can execute this action */

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {

           var post = await model.Posts.FindAsync(id);
           var user = await model.Users.FindAsync(post.AuthorId);

        //    if(post == null){
        //        return NotFound();
        //    }

        //    if(post.AuthorId != user.Id || !User.IsInRole(Role.Admin.ToString()))
        //         return NotFound();
            var comments = (from c in model.Comments where c.PostId == post.Id select c);
            var votes = (from v in model.Votes where v.PostId == post.Id select v);
            var responses = (from r in model.Posts where r.ParentId == post.Id select r);
            var postTags = (from pt in model.PostTags where pt.PostId == post.Id select pt);

            foreach(var c in comments)
                if(c != null)
                    model.Comments.Remove(c);
            foreach(var v in votes )
                if(v != null)
                    model.Votes.Remove(v);

            foreach(var r in responses )
                if(r != null)
                    model.Posts.Remove(r);
            foreach(var pt in postTags )
                if(pt != null)
                    model.PostTags.Remove(pt);

            model.Posts.Remove(post);

            await model.SaveChangesAsync();

            return NoContent();
        }

        /*Only the owner of a post or an administrator
        can execute this action */

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPost(int id, PostDTO data)
        {
            var user = await model.Users.FindAsync(data.AuthorId);
           
            var post = await model.Posts.FindAsync(id);
            if(post == null)
                return NotFound();
            if(user == null  )
                return NotFound();

            // if(user.Id != post.AuthorId || !User.IsInRole(Role.Admin.ToString()) )
            //     return NotFound("You are not the owner of this post !");

            post.Title = data.Title;
            post.Body = data.Body;
            // var postTgs = [];
             var  postTgs = new List<PostTag>();

            if(data.LsTags != null){
                foreach(var t in data.LsTags){
                    var tag = await model.Tags.SingleOrDefaultAsync(p => p.Name == t);
                    var postTag = await model.PostTags.SingleOrDefaultAsync(p => p.PostId == id && p.TagId == tag.Id);
                    if(postTag != null)
                        model.PostTags.Remove(postTag);
                    var newPostTag = new PostTag(){
                        PostId = id,
                        TagId = tag.Id
                    };

                    postTgs.Add(newPostTag);
                }

                post.LsPostTags = postTgs;
            }

            await model.SaveChangesAsyncWithValidation();

            return NoContent();
        }
    }
}
