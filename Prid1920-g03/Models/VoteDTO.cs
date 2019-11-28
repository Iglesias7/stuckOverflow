using System;
using System.Collections.Generic;

namespace Prid1920_g03.Models
{

    public class VoteDTO
    {
        public int UpDown { get; set; }
        public DateTime Timestamp { get; set; }

        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public User User {get; set; }
        public Post Post { get; set; }
    }
}