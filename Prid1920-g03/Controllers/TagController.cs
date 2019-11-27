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
        
        [HttpGet]
        public async Task<ActionResult<TagDTO>> GetOneTag(int id)
        {
            var tag = await model.Tags.FindAsync(id);
            if(tag == null)
                return NotFound();
            return tag.ToDTO();
        }



    }
}