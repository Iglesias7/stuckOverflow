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
                        where p.Title != null && (p.User.Pseudo.Contains(filter) || 
                        (from r in p.Comments where r.Body.Contains(filter) select r).Count() > 0 || 
                        (from r in p.PostTags where r.Tag.Name.Contains(filter) select r).Count() > 0
                        //  || p.Tags.Any(t => t.Name == filter) || p.Comments.Any(t => t.Body == filter)
                        )
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("newestbytag/{tagname}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtNewestByTag(string tagname) {
           var tag = (from t in model.Tags where t.Name == tagname select t).FirstOrDefault();
           var itemList = from p in model.Posts
                         join ptg in model.PostTags on p.Id equals ptg.PostId
                        where ptg.TagId == tag.Id && p.Title != (null) 
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("tagfilter")]
        [HttpGet("tagfilter/{filter}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtTagFilter(string filter = "") {
            var itemList = from p in model.Posts
                        where p.Title != null && p.PostTags.Count() > 0  && (p.User.Pseudo.Contains(filter) || 
                        (from r in p.Comments where r.Body.Contains(filter) select r).Count() > 0 || 
                        (from t in p.PostTags where t.Tag.Name.Contains(filter) select t).Count() > 0
                        )
                        // (from i in p.PostTags where i.PostId == p.Id  select i).Count() != 0 
                        
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("unanswered")]
        [HttpGet("unanswered/{filter}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtUnanswered (string filter = "") {

            var itemList = from p in model.Posts
                        where p.Title != (null) && p.AcceptedAnswerId == null 
                         && (p.User.Pseudo.Contains(filter) || 
                        (from r in p.Comments where r.Body.Contains(filter) select r).Count() > 0 || 
                        (from r in p.PostTags where r.Tag.Name.Contains(filter) select r).Count() > 0) 
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("unansweredbytag/{tagname}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtUnansweredByTag (string tagname) {
            var tag = (from t in model.Tags where t.Name == tagname select t).FirstOrDefault();

            var itemList = from p in model.Posts
                         join ptg in model.PostTags on p.Id equals ptg.PostId
                        where ptg.TagId == tag.Id && (p.Title != (null) && p.AcceptedAnswerId == null )
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }




        [HttpGet("getall")]
        [HttpGet("getall/{filter}")]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GEtAll (string filter = "") {

            var itemList = from p in model.Posts
                        where p.Title != (null) && (p.User.Pseudo.Contains(filter) || 
                        (from r in p.Comments where r.Body.Contains(filter) select r).Count() > 0 || 
                        (from r in p.PostTags where r.Tag.Name.Contains(filter) select r).Count() > 0) 
                        orderby p.Timestamp descending
                        select p;

            return (await itemList.ToListAsync()).ToDTO();
        }

        [HttpGet("votefilter")]
        [HttpGet("votefilter/{filter}")]
        public ActionResult<IEnumerable<PostDTO>> GEtVotefilter (string filter = "") {
            var query = model.Posts.Where(p => p.ParentId == null && (p.User.Pseudo.Contains(filter) || 
                        (from r in p.Comments where r.Body.Contains(filter) select r).Count() > 0 || 
                        (from r in p.PostTags where r.Tag.Name.Contains(filter) select r).Count() > 0)).AsEnumerable().OrderByDescending(p => p.HightVote).ToList();
                  
            return query.ToDTO();
        }


        [HttpGet("votefilterbytag/{tagname}")]
        public ActionResult<IEnumerable<PostDTO>> GEtVotefilterByTag (string tagname) {
            

            var tag = (from t in model.Tags where t.Name == tagname select t).FirstOrDefault();
            var itemList = from p in model.Posts
                         join ptg in model.PostTags on p.Id equals ptg.PostId
                          where ptg.TagId == tag.Id && ( p.ParentId == null)
                          select p;
            var items = itemList.AsEnumerable().OrderByDescending(p => p.HightVote).ToList();

            return items.ToDTO();
        }




    }
}
