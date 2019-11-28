using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Prid1920_g03.Models
{

    public class Vote
    {
        [Required(ErrorMessage = "Required")]
        public int UpDown { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime Timestamp { get; set; }

        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public virtual User User {get; set; }
        public virtual Post Post { get; set; }
        
    }
}