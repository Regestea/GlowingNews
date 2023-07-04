using EventBus.Messages.Common;
using MassTransit;
using Search.API.EventBusConsumer;
using HealthChecks.UI.Client;
using Search.API.Extensions;
using Nest;
using Search.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = new ConnectionSettings();
settings.DefaultMappingFor<News>(x => x.IndexName("news"));
builder.Services.AddSingleton<IElasticClient>(new ElasticClient(settings));


builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<AddIndexNewsConsumer>();
    config.AddConsumer<UpdateIndexNewsConsumer>();
    config.AddConsumer<DeleteIndexNewsConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.NewsQueue, c =>
        {
            c.ConfigureConsumer<AddIndexNewsConsumer>(ctx);
            c.ConfigureConsumer<UpdateIndexNewsConsumer>(ctx);
            c.ConfigureConsumer<DeleteIndexNewsConsumer>(ctx);
        });
    });
});

builder.Services.AddElasticSearch(builder.Configuration);

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