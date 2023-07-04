using AWS.Shared.Client;
using Configs.Shared;
using GlowingNews.Infrastructure;
using IdentityServer.Shared.Client;
using MassTransit;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
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

builder.Services.AddControllers();

builder.Services.AddSwagger(options =>
    {
        options.Title = "News";
        options.Version = "v1";
    }
);

// masstransit rabbit mq 
builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

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


