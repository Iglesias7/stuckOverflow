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

    public class TagController : ControllerBase
    {
        private readonly Prid1920_g03Context model;

        public TagController(Prid1920_g03Context _model){

            this.model = _model;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetAllTags()
        {
            return (await model.Tags.ToListAsync()).ToDTO();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetOneTag(int id)
        {
            var tag = await model.Tags.FindAsync(id);
            if(tag == null)
                return NotFound();
            return tag.ToDTO();
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<TagDTO>> GetTagsByPost(int id)
        // {
        //     var tagrs = null;
        //     await model.PostTags.ForEachAsync(pt => {
        //         if(pt.PostId.Equals(id)){
                    
        //             this.GetAllTags.ForEachAsync(t => {
        //                 if(t.Id.Equals(pt)){
                            
        //                 }
        //             });
        //     });
        //     return  (await model.PostTags.ForEachAsync(t => t.Id.Equals(id)).ToListAsync()).ToDTO();
        // }

        [HttpPost]

        public async Task<ActionResult<TagDTO>> AddTag(TagDTO data){

            var tag = await model.Tags.SingleOrDefaultAsync(tg => tg.Name == data.Name);
            if(tag != null) {
                var error = new ValidationErrors().Add("This tag already exists !", nameof(tag.Name));
                return BadRequest(error);
            }
            var newTag = new Tag()
            {
                Name = data.Name,
            };

            model.Tags.Add(newTag);
            var res = await model.SaveChangesAsyncWithValidation();
            if(!res.IsEmpty)
                return BadRequest(res);
            return CreatedAtAction(nameof(GetOneTag), new {id = newTag.Id}, newTag.TagDTO());

        }



       



    }
}