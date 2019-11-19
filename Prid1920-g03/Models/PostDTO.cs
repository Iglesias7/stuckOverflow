using System;
using System.Collections.Generic;

namespace Prid1920_g03.Models
{

    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }

        public IList<CommentDTO> Comments { get; set; }

        public IList<VoteDTO> Votes { get; set; }

        // public IList<TagDTO> Tags { get; set; }
        public IList<PostDTO> Posts { get; set; }
    }
}