using System;
namespace prid1920_g03.Models {
    public class UserDTO {
        public string Pseudo { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}