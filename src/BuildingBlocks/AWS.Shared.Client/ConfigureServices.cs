using AWS.API.Protos;
using AWS.Shared.Client.GrpcServices;
using Microsoft.Extensions.DependencyInjection;

namespace AWS.Shared.Client
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAwsClientServices(this IServiceCollection services, Action<ConfigureOptions> options)
        {
            var configureOptions = new ConfigureOptions();
            options.Invoke(configureOptions);

            services.AddGrpcClient<AwsService.AwsServiceClient>(o =>
                o.Address = new Uri(configureOptions.AwsGrpcUrl));
            services.AddScoped<AwsGrpcService>();

            return services;
        }
    }
}
