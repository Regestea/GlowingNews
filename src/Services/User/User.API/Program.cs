using IdentityServer.Shared.Client;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AWS.Shared.Client;
using Configs.Shared;
using UserAccount.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityServerClientServices(options =>
{
    options.IdentityServerGrpcUrl = builder.Configuration.GetSection("IdentityServer:GrpcUrl").Value ?? throw new NullReferenceException();
    options.RedisConnectionString = builder.Configuration.GetSection("Redis:ConnectionString").Value ?? throw new NullReferenceException();
    options.RedisInstanceName = builder.Configuration.GetSection("Redis:InstanceName").Value ?? throw new NullReferenceException();
    options.IssuerUrl = builder.Configuration.GetSection("Issuer:Url").Value ?? throw new NullReferenceException();
});
builder.Services.AddAwsClientServices(options =>
{
    options.AwsGrpcUrl = builder.Configuration.GetSection("AwsServer:GrpcUrl").Value ?? throw new NullReferenceException();
});

builder.Services.AddControllers();


builder.Services.AddSwagger(options =>
    {
        options.Title = "User Account";
        options.Version = "v1";
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAllOrigins");

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
