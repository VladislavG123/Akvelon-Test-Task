using Akvelon.TestTask.DAL;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Akvelon.TestTask.DAL.Providers.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TestTask.AppStart.ConfigureServices;

public class ConfigureServicesEntityProviders
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresDb")));

        services.AddScoped<ITaskProvider, EntityTaskProvider>();
        services.AddScoped<IProjectProvider, EntityProjectProvider>();
        services.AddScoped<IUserProvider, EntityUserProvider>();
    }
}