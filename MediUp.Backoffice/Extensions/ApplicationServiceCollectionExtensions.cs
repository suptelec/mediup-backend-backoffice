using MediUp.Domain.Models;

namespace MediUp.Backoffice.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddAppSettings(this IServiceCollection services)
    {
        services.AddOptions<AppSettings>().Configure<IConfiguration>((settings, configuration) =>
        {
            configuration.GetSection(nameof(AppSettings)).Bind(settings);
        });
        return services;
    }
}
