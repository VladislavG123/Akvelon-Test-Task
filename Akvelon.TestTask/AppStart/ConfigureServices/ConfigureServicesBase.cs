using System.Net;
using System.Text;

namespace Akvelon.TestTask.AppStart.ConfigureServices;

public class ConfigureServicesBase
{
    /// <summary>
    /// ConfigureServices Services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddControllers();
        services.AddMemoryCache();
        services.AddRouting();
        services.AddHttpContextAccessor();
        services.AddResponseCaching();
    }
}