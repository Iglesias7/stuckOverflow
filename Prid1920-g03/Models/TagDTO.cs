using System;
using System.Collections.Generic;

namespace Prid1920_g03.Models
{

    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NbXPosts {get; set; }
        public string Body {get; set; }
        public DateTime Timestamp {get; set; }

    }
}