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
        
        [HttpGet("getone/{id}")]
        public async Task<ActionResult<TagDTO>> GetOne(int id)
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
<<<<<<< HEAD
        //         }
        //     });
        //     return  (await model.PostTags.ForEachAsync(t => t.Id.Equals(id)).ToListAsync()).ToDTO();
        // }

        [HttpGet("getTagByName/{tgName}")]
        public async Task<ActionResult<TagDTO>> GetTagByName(string tgName){
            var tag = await model.Tags.SingleOrDefaultAsync(tg => tg.Name == tgName);
            if(tag == null)
                return NotFound();
            return tag.ToDTO();
        }


=======
        //         });
        //     return  (await model.PostTags.ForEachAsync(t => t.Id.Equals(id)).ToListAsync()).ToDTO();
        //     }
        // }
        
>>>>>>> c46003de33e59c7e9f8de78f5b8e8ff040893c7b
        [Authorized(Role.Admin)]
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
<<<<<<< HEAD
            return CreatedAtAction(nameof(GetOneTag), new {id = newTag.Id}, newTag.ToDTO());

        }

        // [Authorized(Role.Admin)]
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTag(int id)
        // {
        //     var tag = await model.Tags.FindAsync(id);

        //     if(tag == null){
        //         return NotFound();
        //     }

        //     model.Tags.Remove(tag);
        //     foreach (var p in model.Posts)
        //         if(p.Contains(tag))
        //             p.LsPostTags.Remove(tag);
        //     await model.SaveChangesAsyncWithValidation();
        //     return NoContent();
        // }


        // [Authorized(Role.Admin)]
        // [HttpPut("{id}")]
        // public async Task<IActionResult> EditTag(int id, TagDTO data)
        // {
        //     if(id != data.Id)
        //         return BadRequest();
        //     var tag = model.Tags.FindAsync(id);
        //     if(tag == null)
        //         return NotFound();
        //     tag.Name = data.Name;

        //     await model.SaveChangesAsyncWithValidation();
        //     return NoContent();

        // }
=======
            return CreatedAtAction(nameof(GetOne), new {id = newTag.Id}, newTag.ToDTO());

        }

        [Authorized(Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await model.Tags.FindAsync(id);

            if(tag == null){
                return NotFound();
            }
            model.Tags.Remove(tag);             
            await model.SaveChangesAsyncWithValidation();
            return NoContent();
        }

        [Authorized(Role.Admin)]
        [HttpPut("{id}")]

        public async Task<IActionResult> EditTag(int id, TagDTO data)
        {
            var tag = await model.Tags.FindAsync(id);

            if(id != data.Id)
                return BadRequest();
            
            if(tag == null)
                return NotFound();
            //tag.Name = data.Name;

            await model.SaveChangesAsyncWithValidation();
            return NoContent();

        }
       
>>>>>>> c46003de33e59c7e9f8de78f5b8e8ff040893c7b
    }
}