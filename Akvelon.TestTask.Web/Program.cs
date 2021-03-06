using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Akvelon.TestTask.Web;
using Akvelon.TestTask.Web.Data.Apis;
using Akvelon.TestTask.Web.Data.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

var url = builder.Configuration["WebApiUrl"] 
          ?? throw new ArgumentException("Cannot find WebApiUrl in the Configuration file");

builder.Services.AddScoped(
    sp => new HttpClient
    {
        BaseAddress = new Uri(url)
    });

#region Apis

builder.Services.AddScoped<UserApi>();
builder.Services.AddScoped<ProjectApi>();
builder.Services.AddScoped<TaskApi>();

#endregion

#region Authentication

builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

builder.Services.AddScoped<AuthenticationStateProvider, UserAuthenticationStateProvider>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

#endregion

builder.Services.AddAntDesign();

await builder.Build().RunAsync();