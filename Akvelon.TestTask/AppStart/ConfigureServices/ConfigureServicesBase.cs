using System.Net;
using System.Text;
using Akvelon.TestTask.LogicLevel.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
        services.Configure<SecretOption>(configuration.GetSection("Secrets"));
            
        // configure jwt authentication
        var secrets = configuration.GetSection("Secrets");
        var key = Encoding.ASCII.GetBytes(secrets.GetValue<string>("JWTSecret"));
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddControllers();
        services.AddMemoryCache();
        services.AddRouting();
        services.AddHttpContextAccessor();
        services.AddResponseCaching();
    }
}