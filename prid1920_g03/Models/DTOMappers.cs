using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace prid1920_g03.Models {
    public static class DTOMappers {
        public static MemberDTO ToDTO(this Member member) {
            return new MemberDTO {
                Pseudo = member.Pseudo,
                // we don't put the password in the DTO for security reasons
                FullName = member.FullName,
                BirthDate = member.BirthDate,
            };
        }
        public static List<MemberDTO> ToDTO(this IEnumerable<Member> members) {
            return members.Select(m => m.ToDTO()).ToList();
        }
    }
}