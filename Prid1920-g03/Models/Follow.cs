using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Prid1920_g03.Models
{

    public class Follow {
        public int FollowerPseudo { get; set; }
        public int FolloweePseudo { get; set; }

        public virtual User Follower { get; set; }
        public virtual User Followee { get; set; }
    }
}