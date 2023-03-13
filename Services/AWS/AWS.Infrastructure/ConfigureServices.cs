using AWS.Application.Common.Interfaces;
using AWS.Infrastructure.Persistence.AWS;
using AWS.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AWS.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AwsIndexDbContext>(
                o => o.UseNpgsql(configuration.GetSection("DatabaseSettings:ConnectionString").Value));
            services.AddScoped<IAwsFileRepository, AwsFileRepository>();

            services.AddSingleton<IAwsClientContext, AwsClientContext>();

            return services;
        }
    }
}
