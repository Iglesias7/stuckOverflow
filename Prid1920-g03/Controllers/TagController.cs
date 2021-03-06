using PRID_Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prid1920_g03.Models;
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
            return (await this.model.Tags.ToListAsync()).ToDTO();
        }

        [HttpGet("tagsbynbposts")]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetByNbPosts(){
            var tags = from tg in model.Tags
            orderby tg.PostTags.Count() descending
            select tg;
            return  (await tags.ToListAsync()).ToDTO();

        }

        [HttpGet("getbytimestamp")]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetByTimestamp(){
            var tags = from tg in model.Tags
            orderby tg.Timestamp descending
            select tg;
            return  (await tags.ToListAsync()).ToDTO();
        }
        
        [HttpGet("getbyname")]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetByName(){
            var tags = from tg in model.Tags
            orderby tg.Name ascending
            select tg;
            return  (await tags.ToListAsync()).ToDTO();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetOne(int id)
        {
            var tag = await model.Tags.FindAsync(id);
            if(tag == null)
                return NotFound();
            return tag.ToDTO();
        }

        [HttpGet("getTagByName/{name}")]
        public async Task<ActionResult<bool>> GetTagByName(string  name)
        {
            var tag = await model.Tags.SingleOrDefaultAsync(tg => tg.Name == name);
            return tag == null;
        }
     
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
            
            if(tag == null)
                return NotFound();
            tag.Name = data.Name;

            await model.SaveChangesAsyncWithValidation();
            return NoContent();

        }
       
    }
}