using Microsoft.EntityFrameworkCore;
using System.Reflection;
using GlowingNews.IdentityServer.Entities;

namespace GlowingNews.IdentityServer.Context
{
    public class IdentityServerContext:DbContext
    {
        public IdentityServerContext(DbContextOptions<IdentityServerContext> options) : base(options)
        {
            
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
