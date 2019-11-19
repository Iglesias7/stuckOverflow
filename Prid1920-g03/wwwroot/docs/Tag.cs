using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Prid1920_g03.Models
{

    public class Tag : IValidatableObject
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        public virtual IList<Post> Posts { get; set; } = new List<Post>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var currContext = validationContext.GetService(typeof(DbContext)) as Prid1920_g03Context;
            var tag_name = (from t in currContext.Tags where t.Name == Name select t).FirstOrDefault();
            
            if (tag_name != null)
                yield return new ValidationResult("This tagName is already used ", new[] { nameof(Name) });
        }
    }
}