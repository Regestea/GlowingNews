using IdentityServer.Shared.Client.GrpcServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlowingNews.IdentityServer.Protos;

namespace IdentityServer.Shared.Client
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddIdentityServerClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<AuthorizationService.AuthorizationServiceClient>(o =>
                o.Address = new Uri(configuration.GetSection("IdentityServer:GrpcUrl").Value));
            services.AddScoped<AuthorizationGrpcServices>();

            return services;
        }
    }
}
