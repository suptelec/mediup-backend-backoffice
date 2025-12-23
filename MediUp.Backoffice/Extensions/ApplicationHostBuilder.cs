using MediUp.Infrastructure;
using MediUp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using Serilog;

namespace MediUp.Backoffice.Extensions;

public static class ApplicationHostBuilder
{
    public static IHostBuilder ConfigureHostBuilder(this IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            var env = hostingContext.HostingEnvironment;

            config.SetBasePath(env.ContentRootPath);
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            config.AddEnvironmentVariables();
        });

        return builder;
    }

    public static void ApplyAppMigrations(this IApplicationBuilder builder, IConfiguration config)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        Log.Information("Trying to apply any pending migrations to the db...");
        appContext.Database.Migrate();
        Log.Information("Successfully applied any pending migration.");

        if (config.GetValue<bool>("SeedDb"))
        {
            Seed.SeedApp(appContext).GetAwaiter().GetResult();
            throw new Exception("config migration completed, disable that thing");
        }
    }
}
