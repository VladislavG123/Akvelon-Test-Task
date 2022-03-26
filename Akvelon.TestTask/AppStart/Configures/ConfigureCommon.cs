using Akvelon.TestTask.AppStart.ConfigureServices;
using Microsoft.AspNetCore.Builder;

namespace Akvelon.TestTask.AppStart.Configures;

public class ConfigureCommon
{
    /// <summary>
    /// Configure pipeline
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseResponseCaching();
        app.UseSwagger();
        app.UseSwaggerUI(ConfigureServicesSwagger.SwaggerSettings);

    }

}