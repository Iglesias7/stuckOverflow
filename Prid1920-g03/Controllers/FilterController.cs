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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class FilterController : ControllerBase {

        private readonly Prid1920_g03Context model;

        public FilterController(Prid1920_g03Context _model){
            this.model = _model;
        }

        [HttpGet("newest")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtNewest() {
            var itemList = from p in model.Posts
                        where p.Title != null 
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("tagfilter")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtTagFilter() {
            var itemList = from p in model.Posts
                        where p.Title != null && p.PostTags.Count() > 0  
                        // (from i in p.PostTags where i.PostId == p.Id  select i).Count() != 0 
                        
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("unanswered")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtUnanswered () {

            var itemList = from p in model.Posts
                        where p.Title != (null) && (from r in p.Responses where r.AcceptedAnswerId == null select r).Count() == (from r in p.Responses select r).Count() 
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("votefilter")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtVotefilter () {

            var querry = model.Posts.Where(p => p.ParentId == null).AsEnumerable().OrderByDescending(p => p.HightVote).ToList();

            return (querry).ToDTO();
        }

    }
}
