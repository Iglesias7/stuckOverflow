using System;
using System.Collections.Generic;

namespace Prid1920_g03.Models {
    
    public class PostUser {
        public int Id {get; set;}
        public string Pseudo {get; set;}
        public int Reputation {get; set;}
        public string PicturePath { get; set; }
        public Role Role { get; set; }
    }
}