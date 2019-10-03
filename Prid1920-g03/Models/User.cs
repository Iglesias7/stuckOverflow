using System;
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
            var currContext = validationContext.GetService(typeof(DbContext));
            Debug.Assert(currContext != null);
            if (Password == "abc")
                yield return new ValidationResult("The password may not be equal to 'abc'", new[] { nameof(Password) });
            if (BirthDate.HasValue && BirthDate.Value.Date > DateTime.Today)
                yield return new ValidationResult("Can't be born in the future in this reality", new[] { nameof(BirthDate) });
            else if (Age.HasValue && Age < 18)
                yield return new ValidationResult("Must be 18 years old", new[] { nameof(BirthDate) });
            else if (FirstName == null && LastName == null )
                yield return new ValidationResult("FirstName and LastName is not null", new[] { nameof(FirstName) });
        
        }
    }
}