using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Prid1920_g03.Models
{
     
    public class User : IValidatableObject
    {
        [Key]
        public int Id {get; set;}
        
        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters")]
        [MaxLength(10, ErrorMessage = "Maximum 10 characters")]
        [RegularExpression("^[A-Za-z][A-Za-z0-9_]{2,9}$")]
        public string Pseudo {get; set;}

        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters")]
        [MaxLength(10, ErrorMessage = "Maximum 10 characters")]
        public string Password {get; set;}

        [Required(ErrorMessage = "Required")]
        [RegularExpression("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$")]
        public string Email {get; set;}

        [MinLength(3, ErrorMessage = "Minimum 3 characters")]
        [MaxLength(50, ErrorMessage = "Maximum 10 characters")]
        public string LastName {get; set;}

        [MinLength(3, ErrorMessage = "Minimum 3 characters")]
        [MaxLength(50, ErrorMessage = "Maximum 10 characters")]
        public string FirstName {get; set;}
        public DateTime? BirthDate {get; set;}

        [Required(ErrorMessage = "Required")]
        public int Reputation {get; set;}

        [NotMapped]
        public int? Age {get {
            if(!BirthDate.HasValue)
                return null;
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Value.Year;
            if(BirthDate.Value.Date > today.AddYears(-age))
                -- age;
            
            return age;
                        }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var currContext = validationContext.GetService(typeof(DbContext)) as Prid1920_g03Context;
            var user_pseudo = (from p in currContext.Users where p.Pseudo == Pseudo select  p).FirstOrDefault() ;
            var user_email =  (from e in currContext.Users where e.Email == Email select e).FirstOrDefault() ;
            Debug.Assert(currContext != null);
            if (BirthDate.HasValue && BirthDate.Value.Date > DateTime.Today)
                yield return new ValidationResult("Can't be born in the future in this reality", new[] { nameof(BirthDate) });
            else if (Age.HasValue && Age < 18)
                yield return new ValidationResult("Must be 18 years old", new[] { nameof(BirthDate) });
            if(FirstName == null && LastName != null)
                yield return new ValidationResult("The FirstName cannot be null, if the LastName isn't", new[] {nameof(FirstName) });
            if(LastName == null && FirstName != null)
                yield return new ValidationResult("The LastName cannot be null, if the FirstName isn't", new[] {nameof(LastName) });
            if(Reputation < 0)
                yield return new ValidationResult("The Reputation must be >= 0 ", new[] {nameof(Reputation) });
            if(user_pseudo != null )
                yield return new ValidationResult("This pseudo is already used ", new[] {nameof(Pseudo) });
            else if(user_email != null)
                yield return new ValidationResult("this email is already used ", new[]{nameof(Email)} );
        }
    }
}