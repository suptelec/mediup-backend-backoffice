using Logging;
using MediUp.Application;
using MediUp.Backoffice.Controllers;
using MediUp.Backoffice.Extensions;
using MediUp.Backoffice.Middleware;
using MediUp.Backoffice.Models;
using MediUp.Domain.Dtos.Common;
using MediUp.Domain.Interfaces;
using MediUp.Domain.Models;
using MediUp.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.OData;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

bool logToFiles = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") != "true";
string? logsPath = logToFiles ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs") : null;

var observabilityConfig = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .AddEnvironmentVariables()
    .Build()
    .GetSection(nameof(ObservabilitySettings))
    .Get<ObservabilitySettings>()!;

observabilityConfig.CheckSettings();

Log.Logger = Logging.DependencyInjection.CreateBootstrapperLogger(observabilityConfig,new ToLog(typeof(Program)), logsPath);

try
{
    Log.Information("Creating builder...");
    Log.Information(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "NO_ENVIRONMENT");

    Log.Information("Creating builder...");
    var builder = WebApplication.CreateBuilder(args);

    var settings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!;
    settings.CheckSettings();
    builder.Services.Configure<MailOptions>(builder.Configuration.GetSection(nameof(MailOptions)));
    var swaggerSettings = builder.Configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>()!;
    var authServerSettings = builder.Configuration.GetSection(nameof(AuthServerSettings)).Get<AuthServerSettings>()!;
    

    var appAssets = new AppAssets(builder.Environment.WebRootPath);
    IServiceCollection services = builder.Services;

    services.AddSingleton(appAssets);
    
    #region Services
    // Add services to the container.
    Log.Information("Configuring services...");

    string[] excludedKeyWords =
    [
        "DbContext"
    ];
    var apiLogs = ToLog.From<BaseController>();
    var middlewareLogs = new List<ToLog>
    {
        new(typeof(AppStatusMiddleware)),
        new(typeof(ExceptionHandlerMiddleware))
    };
    var appLogs = ToLog.From(typeof(MediUp.Application.DependencyInjection));
    var infrastructureLogs = ToLog.From(typeof(MediUp.Infrastructure.DependencyInjection));
    var logs = apiLogs
        .Concat(middlewareLogs)
        .Concat(appLogs)
        .Concat(infrastructureLogs)
        .Where(log => !excludedKeyWords.Any(keyword => log.Source?.Contains(keyword) ?? false))
        .ToArray();

    builder.Host.ConfigureAppLogging(observabilityConfig,logsPath, false, logs: logs);
    builder.Services.AddControllers()
        .AddOData(opt =>
        {
            opt.EnableQueryFeatures();
            opt.TimeZone = TimeZoneInfo.Utc;
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });

    builder.Services.AddCors();
    builder.Services.AddAutoMapper(s => AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddAuthorization();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddAppSettings();
    builder.Services.AddAppInfrastructure(builder.Configuration);
    builder.Services.AddApplication(new ValidationSettings(settings));
    builder.Services.AddRepos();
    builder.Services.AddSwagger(swaggerSettings.Authority, swaggerSettings.SwaggerScopes, "CashOn Backoffice", "MediUp.Backoffice.xml");
    builder.Services.AddHealthChecks();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddAuthHandlers();
    builder.Services.AddTransient<ICurrentLoggedUser, CurrentLoggedUser>();
    builder.Services.AddMemoryCache();

    builder.Services.AddSingleton(settings);
    builder.Services.AddElectriCompanyService()
        .AddAgentService()
        .AddLigtherTransformerService()
        .AddLigtherMetricService();




    builder.Services.AddIdentityApis(authServerSettings);


    #endregion

    builder.Services.AddOpenTelemetry()
                    .ConfigureResource(resource => resource
                        .AddService(observabilityConfig!.ServiceName))
                    .WithTracing(tracing => tracing
                        .AddAspNetCoreInstrumentation(options =>
                        {
                            options.Filter = httpContext =>
                                !httpContext.Request.Path.StartsWithSegments("/metrics") &&
                                !httpContext.Request.Path.StartsWithSegments("/healthchecks");
                        })
                        .AddHttpClientInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation()
                         .AddOtlpExporter(o =>
                         {
                             o.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                             o.TimeoutMilliseconds = 5000;
                         }))
                    .WithMetrics(metrics => metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddOtlpExporter(o =>
                        {
                            o.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                            o.TimeoutMilliseconds = 5000;
                        }));


    Log.Information("Building app...");
    var app = builder.Build();
    app.ApplyAppMigrations(app.Configuration);

    if (app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }

    builder.Host.ConfigureHostBuilder();


    app.UseSwagger(swaggerSettings.SwaggerClientId, swaggerSettings.SwaggerClientSecret, "MediUp Bo");
    //app.UseHttpsRedirection();
    var forwardedHeaderOptions = new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost,
    };

    forwardedHeaderOptions.KnownNetworks.Clear();
    forwardedHeaderOptions.KnownProxies.Clear();

    app.UseForwardedHeaders(forwardedHeaderOptions);

    app.UseCors(options => options.WithOrigins(settings.BaseDomain).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

    app.MapPrometheusScrapingEndpoint();
    app.UseHealthChecks("/healthchecks");

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.UseAuthentication();
    app.UseMiddleware<AppStatusMiddleware>();
    app.UseAuthorization();

    app.MapControllers();
    Log.Information("Running app...");
    await app.RunAsync();

}
catch (Exception e)
{
    Log.Fatal(e, "Application terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
