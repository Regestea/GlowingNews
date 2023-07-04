using AWS.Application.Common.Interfaces;
using AWS.Infrastructure.Persistence.AWS;
using AWS.Infrastructure.Persistence.Repositories;
using AWS.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AWS.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AwsIndexDbContext>(
                o => o.UseNpgsql(configuration.GetSection("DatabaseSettings:ConnectionString").Value));
            services.AddScoped<IAwsFileRepository, AwsFileRepository>();

            services.AddScoped<IAwsClientContext, AwsClientContext>();

            //services.AddSingleton<AwsCleanerService>();

            services.AddHostedService<AwsCleanerService>();

            return services;
        }
    }
}
