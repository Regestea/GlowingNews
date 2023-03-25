
using IdentityServer.Shared.Client;
using NewsLike.Application;
using NewsLike.Infrastructure;

namespace NewsLike.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentityServerClientServices(options =>
            {
                options.IdentityServerGrpcUrl = builder.Configuration.GetSection("IdentityServer:GrpcUrl").Value ?? throw new NullReferenceException();
                options.RedisConnectionString = builder.Configuration.GetSection("Redis:ConnectionString").Value ?? throw new NullReferenceException();
                options.RedisInstanceName = builder.Configuration.GetSection("Redis:InstanceName").Value ?? throw new NullReferenceException();
                options.IssuerUrl = builder.Configuration.GetSection("Issuer:Url").Value ?? throw new NullReferenceException();
            });

            // Add services to the container.
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}