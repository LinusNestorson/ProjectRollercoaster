using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectRollercoaster.Client;
using ProjectRollercoaster.Client.Services;

/// <summary>
/// Startup class as entry point and with added services.
/// </summary>
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IFeedService, FeedService>();
builder.Services.AddScoped<IFeedContentService, FeedContentService>();
builder.Services.AddScoped<ISpecificFeedService, SpecificFeedService>();
builder.Services.AddScoped<IRefreshService, RefreshService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPodcastService, PodcastService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();