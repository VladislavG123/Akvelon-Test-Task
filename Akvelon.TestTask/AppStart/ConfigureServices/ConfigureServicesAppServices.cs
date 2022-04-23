using Akvelon.TestTask.DAL;
using Akvelon.TestTask.DAL.Providers.Abstract;
using Akvelon.TestTask.DAL.Providers.EntityFramework;
using Akvelon.TestTask.LogicLevel;
using Akvelon.TestTask.LogicLevel.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Akvelon.TestTask.AppStart.ConfigureServices;

public class ConfigureServicesAppServices
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITaskBllService, TaskBllService>();
        services.AddScoped<IProjectBllService, ProjectBllService>();
        services.AddScoped<IUserBllService, UserBllService>();
    }
}