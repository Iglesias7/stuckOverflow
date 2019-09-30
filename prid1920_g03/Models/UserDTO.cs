using System;
namespace prid1920_g03.Models {
    public class UserDTO {

        public string Id { get; set; }

        public string Pseudo { get; set; }

        public string email { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Password { get; set; }

        public DateTime? BirthDate { get; set; }

        public int Reputation { get; set; }
    }
}