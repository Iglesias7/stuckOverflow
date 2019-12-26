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

    public class FilterController : ControllerBase {

        private readonly Prid1920_g03Context model;

        public FilterController(Prid1920_g03Context _model){
            this.model = _model;
        }

        [HttpGet("newest")]
        [HttpGet("newest/{filter}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtNewest(string filter = "") {
            var itemList = from p in model.Posts
                        where p.Title != null && (p.Title.Contains(filter) || p.User.Pseudo.Contains(filter)
                        // || (from t in p.Tags where t.Name.Contains(filter) select t).SingleOrDefault() 
                        // || (from c in p.Comments where c.Body.Contains(filter) select c)
                        )
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("tagfilter")]
        [HttpGet("tagfilter/{filter}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtTagFilter(string filter = "") {
            var itemList = from p in model.Posts
                        where p.Title != null && p.PostTags.Count() > 0  && (p.Title.Contains(filter) || p.User.Pseudo.Contains(filter))
                        // (from i in p.PostTags where i.PostId == p.Id  select i).Count() != 0 
                        
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("unanswered")]
        [HttpGet("unanswered/{filter}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtUnanswered (string filter = "") {

            var itemList = from p in model.Posts
                        where p.Title != (null) && (from r in p.Responses where r.AcceptedAnswerId == null select r).Count() == (from r in p.Responses select r).Count()
                         && (p.Title.Contains(filter) || p.User.Pseudo.Contains(filter)) 
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("getall")]
        [HttpGet("getall/{filter}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtAll (string filter = "") {

            var itemList = from p in model.Posts
                        where p.Title != (null) && (p.Title.Contains(filter) || p.User.Pseudo.Contains(filter)) 
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("votefilter")]
        [HttpGet("votefilter/{filter}")]
        public ActionResult<IEnumerable<PostDTO>> GEtVotefilter (string filter = "") {
            var query = model.Posts.Where(p => p.ParentId == null && (p.Title.Contains(filter) || p.User.Pseudo.Contains(filter))).AsEnumerable().OrderByDescending(p => p.HightVote).ToList();
                  
            return query.ToDTO();
        }

    }
}
