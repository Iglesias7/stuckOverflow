using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prid1920_g03.Models 
{
    public static class DTOMappers {
        public static UserDTO ToDTO(this User member) {
            return new UserDTO {
                Pseudo = member.Pseudo,
                // we don't put the password in the DTO for security reasons
                LastName = member.LastName,
                FirstName = member.FirstName,
                BirthDate = member.BirthDate,

            };
        }

        public static List<UserDTO> ToDTO(this IEnumerable<User> members) {
            return members.Select(m => m.ToDTO()).ToList();
        }
    }
}