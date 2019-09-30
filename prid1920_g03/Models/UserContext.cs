using Microsoft.EntityFrameworkCore;

namespace prid1920_g03.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<prid1920_g03Context> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}