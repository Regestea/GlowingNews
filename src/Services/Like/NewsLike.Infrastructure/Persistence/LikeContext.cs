using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NewsLike.Domain.Entities;

namespace NewsLike.Infrastructure.Persistence
{
    public class LikeContext : DbContext
    {
        public LikeContext(DbContextOptions<LikeContext> options) : base(options)
        {

        }
        public DbSet<Like> Likes { get; set; } 

    }
}
