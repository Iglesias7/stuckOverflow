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

        [Required(ErrorMessage = "Required")]
        public string Body { get; set; }

        [Required(ErrorMessage = "Required")]
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
        public IEnumerable<Tag> Tags { get => LsPostTags.Select(t => t.Tag);}
        
    }
}