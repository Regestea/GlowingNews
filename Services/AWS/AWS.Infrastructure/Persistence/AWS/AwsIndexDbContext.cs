using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AWS.Infrastructure.Persistence.AWS
{
    public class AwsIndexDbContext:DbContext
    {
        public AwsIndexDbContext(DbContextOptions<AwsIndexDbContext> options) : base(options)
        {

        }

        public DbSet<AwsFile> AwsFiles { get; set; } = null!;
    }
}
