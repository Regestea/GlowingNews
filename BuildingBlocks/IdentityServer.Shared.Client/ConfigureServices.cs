using IdentityServer.Shared.Client.GrpcServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GlowingNews.IdentityServer.Protos;
using IdentityServer.Shared.Client.Repositories;
using IdentityServer.Shared.Client.Repositories.Interfaces;
using StackExchange.Redis;

namespace IdentityServer.Shared.Client
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddIdentityServerClientServices(this IServiceCollection services, Action<ConfigureOptions> options)
        {
            var configureOptions = new ConfigureOptions();
            options.Invoke(configureOptions);

            services.AddGrpcClient<AuthorizationService.AuthorizationServiceClient>(o =>
                o.Address = new Uri(configureOptions.IdentityServerGrpcUrl));

            services.AddScoped<AuthorizationGrpcServices>();

            services.AddScoped<ICacheRepository, CacheRepository>();

            #region Redis Cache

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configureOptions.RedisConnectionString;
                options.InstanceName = configureOptions.RedisInstanceName;

            });

            #endregion

            #region Redis Multiplexer

            services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect("localhost:9191,password=123456"));

            #endregion

            return services;
        }
    }
}
