using System.Reflection;
using System.Security.Claims;
using System.Text;
using Configs.Shared;
using GlowingNews.IdentityServer.Context;
using GlowingNews.IdentityServer.Entities;
using GlowingNews.IdentityServer.GrpcServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Security.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGrpc();
builder.Services.AddControllers();

#region DatabaseConfig

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<IdentityServerContext>(
    o => o.UseNpgsql(builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value));

#endregion

builder.Services.AddSwagger(options =>
    {
        options.Title = "Identity Server";
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

#region JwtBearer

//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7126",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetValue<string>("JWT:SecretKey") ?? throw new InvalidOperationException()))
        };
    });

#endregion

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region Authorization

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Roles.Admin, policy =>
        policy.RequireClaim(ClaimTypes.Role, Roles.Admin));
    options.AddPolicy(Roles.User, policy =>
        policy.RequireClaim(ClaimTypes.Role, Roles.User));
});

#endregion

var app = builder.Build();
app.UseCors("AllowAllOrigins");
//MigrateDatabase
app.MigrateDatabaseServices();


app.MapGrpcService<AuthorizationGrpcService>();
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