using Microsoft.EntityFrameworkCore;
using SSO.Demo.Service.Entity;

namespace SSO.Demo.Service.Context
{
    public class SkyChenContext : DbContext
    {
        public SkyChenContext(DbContextOptions<SkyChenContext> options) : base(options)
        {

        }
        public DbSet<SysUser> SysUser { get; set; }

        public DbSet<SysMenu> SysMenu { get; set; }

        public DbSet<SysRole> SysRole { get; set; }

        public DbSet<SysLog> SysLog { get; set; }
    }
}
