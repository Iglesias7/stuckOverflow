using System;

namespace Prid1920_g03.Models {

    public class Tag {
       public int Id {get; set;} 
       public string Name {get; set;}

       public Post Question {get; set;}

       public Post Response {get; set;}
    }
}