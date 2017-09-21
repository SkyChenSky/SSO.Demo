using Microsoft.EntityFrameworkCore;

namespace SSO.Demo.Service
{
    public class SkyChenContext : DbContext
    {
        public SkyChenContext(DbContextOptions<SkyChenContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
    }
}
