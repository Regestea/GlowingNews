using GlowingNews.Application.Common.Interfaces;
using GlowingNews.Infrastructure.Persistence;
using GlowingNews.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlowingNews.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<NewsContext>(
                o => o.UseNpgsql(configuration.GetSection("DatabaseSettings:ConnectionString").Value));

            services.AddScoped<INewsRepository, NewsRepository>();
            return services;
        }
    }
}
