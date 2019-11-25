using System;

namespace Prid1920_g03.Models {

    public class Tag {
       public int Id {get; set;} 
       public string Name {get; set;}
       public virtual IList<Post> Posts {get; set;} = new List<Post>();
    }
}