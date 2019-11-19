using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prid1920_g03.Models 
{
    public static class DTOMappers {
        public static UserDTO ToDTO(this User user) {
            return new UserDTO {
                Id = user.Id,
                Pseudo = user.Pseudo,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Reputation = user.Reputation,
                PicturePath = user.PicturePath,
                Role = user.Role
            };
        }
        public static List<UserDTO> ToDTO(this IEnumerable<User> users) {
            return users.Select(m => m.ToDTO()).ToList();
        }
    }
}