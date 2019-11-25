using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
namespace Prid1920_g03.Models {

    public class PostTag {
       public int PostId {get; set;} 
       public int TagId {get; set;}
       public virtual Post Post {get; set;}

       public virtual Tag Tag {get; set;}

    }
}