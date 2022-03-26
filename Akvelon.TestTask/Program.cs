using Akvelon.TestTask.AppStart.Configures;
using Akvelon.TestTask.AppStart.ConfigureServices;

var builder = WebApplication.CreateBuilder(args);

ConfigureServicesAppServices.ConfigureServices(builder.Services, builder.Configuration);
ConfigureServicesBase.ConfigureServices(builder.Services, builder.Configuration);
ConfigureServicesSwagger.ConfigureServices(builder.Services, builder.Configuration);
ConfigureServicesCors.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureCommon.Configure(app, app.Environment);
ConfigureEndpoints.Configure(app, app.Environment);

app.Run();