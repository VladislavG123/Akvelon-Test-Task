using Akvelon.TestTask.DAL;
using Akvelon.TestTask.DAL.Options;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Akvelon.TestTask.DAL.Providers.Mongo;

namespace Akvelon.TestTask.AppStart.ConfigureServices;

public class ConfigureServicesMongoProviders
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOption>(configuration.GetSection("MongoDb"));
        services.AddSingleton<MongoDbContext>();
        
        services.AddScoped<ITaskProvider, MongoTaskProvider>();
        services.AddScoped<IProjectProvider, MongoProjectProvider>();
        services.AddScoped<IUserProvider, MongoUserProvider>();
    }
}