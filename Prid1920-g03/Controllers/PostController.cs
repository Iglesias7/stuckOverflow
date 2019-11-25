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

        
    }
}