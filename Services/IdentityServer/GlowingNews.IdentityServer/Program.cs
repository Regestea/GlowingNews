using System.Security.Claims;
using System.Text;
using GlowingNews.IdentityServer.Context;
using GlowingNews.IdentityServer.Entities;
using GlowingNews.IdentityServer.GrpcServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGrpc();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region DatabaseConfig

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<IdentityServerContext>(
    o => o.UseNpgsql(builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value));

#endregion


#region SwaggerConfig

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyProject", Version = "v1.0.0" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    };

    c.AddSecurityRequirement(securityRequirement);

});


#endregion


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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:SecretKey") ?? throw new InvalidOperationException()))
        };
    });

#endregion

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region Authorization

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Roles.Admin.ToString(), policy =>
        policy.RequireClaim(ClaimTypes.Role, Roles.Admin.ToString()));
    options.AddPolicy(Roles.User.ToString(), policy =>
        policy.RequireClaim(ClaimTypes.Role, Roles.User.ToString()));
});

#endregion

var app = builder.Build();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

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
