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
        public int? AcceptedAnswerId { get; set; }

        public int AuthorId { get; set; }
        public int? ParentId { get; set; }
        public User User { get; set; }
        public Post Parent { get; set; }

        public IList<CommentDTO> Comments { get; set; }
        public IList<VoteDTO> Votes { get; set; }
        public IList<PostDTO> Replies { get; set; }
        public IEnumerable<string> LsTags { get; set; }
    }
}