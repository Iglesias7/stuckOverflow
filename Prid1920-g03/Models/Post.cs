using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Prid1920_g03.Models
{

    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Timestamp { get; set; }

               
        public int? ParentId { get; set; }
        public int AuthorId { get; set; } 
        public int? AcceptedAnswerId { get; set; }
        
        public virtual Post PostParent { get; set; }
        public virtual User User { get; set; }
        public virtual IList<Post> Responses { get; set; } = new List<Post>();
        public virtual IList<Comment> Comments { get; set; } = new List<Comment>();
        public virtual IList<Vote> Votes { get; set; } = new List<Vote>();
       


        public virtual IList<PostTag> LsPostTags { get; set; } = new List<PostTag>();

        [NotMapped]
        public IEnumerable<Tag> Tags { get => LsPostTags.Select(t => t.Tag); }

        [NotMapped]
        public int NumResponse
        {
            get
            {
                return(
                    from c in Responses
                    select c
                ).Count();
            }
        }

        [NotMapped]
        public int VoteState
        {
            get
            {
                return Votes.Sum(r => r.UpDown);

                // int nb = 0;
                // foreach(Vote v in Votes)
                // {
                //     nb += v.UpDown; 
                // }
                // return nb;
            }
        }

        [NotMapped]
        public int HightVote
        {
            get
            {
                int nb = 0;
                foreach(Post r in Responses)
                {
                    if(r.VoteState > nb)
                        nb = r.VoteState; 
                }

                if(this.VoteState > nb)
                        nb = this.VoteState;
                return nb;

                // return Responses.Max(x => x.VoteState);
            }        
        }

        [NotMapped]
        public int NumVote
        {
            get
            {
                return(
                    from v in Votes
                    select v
                ).Count();
            }
        }

        [NotMapped]
        public int NumComment
        {
            get
            {
                return(
                    from c in Comments
                    select c
                ).Count();
            }
        }

        [NotMapped]
        public Boolean AcceptedAnswerIdExist
        {
            get
            {
               Boolean q = false;
               foreach(Post r in Responses)
                {
                    if(r.AcceptedAnswerId != null)
                        q = true;
                }
                return q;
            }
        }
        
    }
}