using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAccount.Application.Common.Interfaces;
using UserAccount.Infrastructure.Persistence;
using UserAccount.Infrastructure.Repositories;

namespace UserAccount.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<UserAccountContext>(
                o => o.UseNpgsql(configuration.GetSection("DatabaseSettings:ConnectionString").Value));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();

            return services;
        }
    }
}
