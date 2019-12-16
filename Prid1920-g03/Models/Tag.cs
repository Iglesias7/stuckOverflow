using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
namespace Prid1920_g03.Models {

    public class Tag {
        [Key]
       public int Id {get; set;} 
       public string Name {get; set;}

        public virtual IList<PostTag> PostTags { get; set; } = new List<PostTag>();

        [NotMapped]
        public int NbXPosts {
            get{
                var nb = 0;
                foreach (var item in PostTags)
                {
                    nb++;
                }
                return nb;
            }
        }
       
    }
}