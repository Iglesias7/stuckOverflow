using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace prid1920_g03.Models {
    public static class DTOMappers {
        public static UserDTO ToDTO(this User user) {
            return new UserDTO {
                Pseudo = user.Pseudo,
                // we don't put the password in the DTO for security reasons
                FullName = user.FullName,
                BirthDate = user.BirthDate,
            };
        }
        public static List<UserDTO> ToDTO(this IEnumerable<User> users) {
            return users.Select(m => m.ToDTO()).ToList();
        }
    }
}