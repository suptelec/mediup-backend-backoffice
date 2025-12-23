using MediUp.Domain.Interfaces.Identity;
using MediUp.Domain.Interfaces.Services;
using MediUp.Domain.Models;
using MediUp.Domain.Utils;
using MediUp.Infrastructure.Authorization;
using MediUp.Infrastructure.Interfaces.Apis;
using MediUp.Infrastructure.Mapping;
using MediUp.Infrastructure.Persistence;
using MediUp.Infrastructure.Persistence.Interceptors;
using MediUp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using OpenIddict.Validation.AspNetCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Refit;

using System.Text.Json;
using System.Text.Json.Serialization;


namespace MediUp.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddAppInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var readOnlyConnectionString = configuration.GetConnectionString("ReadOnlyConnection");
        var idsSettings = configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>()!;

        services.AddScoped<IAppDataService, AppDataService>();
        services.AddAppDbContext(connectionString!);
        services.AddOpenIdDictAuthentication(idsSettings!);

        services.AddAutoMapper(c => c.AddProfile(new MappingProfile()));
        return services;
    }
    public static IServiceCollection AddAuthHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        return services;
    }


    private static IServiceCollection AddAppDbContext(this IServiceCollection services, string connectionString)
    {
        Check.NotEmpty(connectionString, nameof(connectionString));
#if DEBUG
        IdentityModelEventSource.ShowPII = true;
#endif
        services.AddScoped<AuditInterceptor>();
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
#if DEBUG
            options.UseLoggerFactory(LoggerFactory.Create(c => c.AddDebug().AddConsole()));
            options.EnableSensitiveDataLogging();
#endif
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), o =>
            {
                o.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                o.SchemaBehavior(MySqlSchemaBehavior.Translate, DbConstants.GenerateTableSchema);
                o.MigrationsHistoryTable($"{DbConstants.Scheme}{DbConstants.MigrationsTableName}");
            });
            options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
        });

        return services;
    }

    private static IServiceCollection AddOpenIdDictAuthentication(this IServiceCollection services, IdentityServerSettings appIdentityServerSettings)
    {
        services.AddOpenIddict()
        .AddValidation(options =>
        {
            options.SetIssuer(new Uri(appIdentityServerSettings.Issuer));
            options.UseSystemNetHttp();
            options.UseAspNetCore();

        });
        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
        .AddJwtBearer(o =>
        {
            o.Authority = appIdentityServerSettings.Authority;
            o.RequireHttpsMetadata = appIdentityServerSettings.RequireHttpsMetadata;
        });

        services.AddAuthorization();
        return services;
    }

    public static IServiceCollection AddRepos(this IServiceCollection services)
    {
        return services;
    }
    public static IServiceCollection AddIdentityApis(this IServiceCollection services, AuthServerSettings settings)
    {
        var jsonSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            })
        };
        services.AddSingleton(_ => settings);
        services.AddTransient<AuthenticationMessageHandler>();
        services.AddTransient<AuthenticatedHttpClientHandler>();
        services.AddRefitClient<IIdentityApi>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(settings.Endpoint));
        services.AddSingleton<IIdendityApiService, IdendityApiService>();
        services.AddRefitClient<IIdentityUsersApi>(jsonSettings)
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(settings.Endpoint))
        .AddHttpMessageHandler<AuthenticationMessageHandler>();
        services.AddRefitClient<IIdentityApiWithAuth>(jsonSettings)
        .ConfigureHttpClient(c => c.BaseAddress = new Uri(settings.Endpoint))
        .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();
        services.AddSingleton<IIdendityUserApiService, IdendityUserApiService>();
        return services;
    }

    
}
