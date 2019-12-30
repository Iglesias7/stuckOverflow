using System;
using System.Collections.Generic;

namespace Prid1920_g03.Models
{

    public class CommentDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }

        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public PostUser CommentUser {get; set;}
    }
}