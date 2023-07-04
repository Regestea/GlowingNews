using System.Collections.Generic;
using System.Reflection;
using GlowingNews.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlowingNews.Infrastructure.Persistence
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {

        }
        public DbSet<New> News { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
