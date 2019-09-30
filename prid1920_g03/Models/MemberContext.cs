using Microsoft.EntityFrameworkCore;

namespace prid1920_g03.Models
{
    public class MemberContext : DbContext
    {
        public MemberContext(DbContextOptions<MemberContext> options): base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
    }
}