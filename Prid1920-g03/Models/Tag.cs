using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
namespace Prid1920_g03.Models {

    public class Tag {
       public int Id {get; set;} 
       public string Name {get; set;}
       public virtual IList<Post> Posts {get; set;} = new List<Post>();
    }
}