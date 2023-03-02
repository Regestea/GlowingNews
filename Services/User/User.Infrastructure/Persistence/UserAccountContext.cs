using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserAccount.Domain.Entities;

namespace UserAccount.Infrastructure.Persistence
{
    public class UserAccountContext : DbContext
    {
        public UserAccountContext(DbContextOptions<UserAccountContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Follow> Follows { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
