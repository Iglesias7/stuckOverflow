using System;
using System.Collections.Generic;

namespace Prid1920_g03.Models
{

    public class VoteDTO
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int UpDown { get; set; }
        public DateTime Timestamp { get; set; }
    }
}