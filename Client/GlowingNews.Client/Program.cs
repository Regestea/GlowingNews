using Blazored.LocalStorage;
using GlowingNews.Client;
using GlowingNews.Client.Auth;
using GlowingNews.Client.Enums;
using GlowingNews.Client.Services;
using GlowingNews.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped< AuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUploadService, UploadService>();


builder.Services.AddHttpClient(nameof(HttpClients.Aws),
    client => { client.BaseAddress = new Uri("https://localhost:7268/api/"); });

builder.Services.AddHttpClient(nameof(HttpClients.IdentityServer),
    client => { client.BaseAddress = new Uri("https://localhost:7126/"); });

builder.Services.AddHttpClient(nameof(HttpClients.Like),
    client => { client.BaseAddress = new Uri("https://localhost:7012/"); });

builder.Services.AddHttpClient(nameof(HttpClients.News),
    client => { client.BaseAddress = new Uri("https://localhost:7043/"); });

builder.Services.AddHttpClient(nameof(HttpClients.Search),
    client => { client.BaseAddress = new Uri("https://localhost:7092/"); });

builder.Services.AddHttpClient(nameof(HttpClients.User),
    client => { client.BaseAddress = new Uri("https://localhost:7050/api/"); });

await builder.Build().RunAsync();