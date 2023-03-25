using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsLike.Application.Common.Interfaces;
using NewsLike.Infrastructure.Persistence;
using NewsLike.Infrastructure.Repositories;

namespace NewsLike.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<LikeContext>(
                o => o.UseNpgsql(configuration.GetSection("DatabaseSettings:ConnectionString").Value));

            services.AddScoped<ILikeRepository, LikeRepository>();
            return services;
        }
    }
}
