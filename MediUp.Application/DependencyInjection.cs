using FluentValidation;
using MediUp.Application.Interfaces;
using MediUp.Application.Validation;
using MediUp.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;
namespace MediUp.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IValidationSettings settings)
    {
        services.AddValidation(settings).AddMapper();
        return services;
    }

    


    #region Validation
    private static IServiceCollection AddValidation(this IServiceCollection services, IValidationSettings settings)
    {
        services.TryAddSingleton(_ => settings);
        services.AddScoped<IValidatorService, ValidatorService>();
        services.AddValidators<ValidatorService>();
        return services;
    }

    private static IServiceCollection AddValidators<TAssembly>(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssemblyContaining<TAssembly>(ServiceLifetime.Scoped, f => !f.ValidatorType.IsNestedPrivate, true);
    }
    #endregion


    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(c => c.AddProfile(new MappingProfile()));
        return services;
    }


    #region Services

    public static IServiceCollection AddCustomerService(this IServiceCollection services)
    {
        services.TryAddScoped<ICustomerService, CustomerService>();
        return services;
    }
    public static IServiceCollection AddLoanService(this IServiceCollection services)
    {
        services.TryAddScoped<ILoanService, LoanService>();
        return services;
    }

    public static IServiceCollection AddBillService(this IServiceCollection services)
    {
        services.TryAddScoped<IBillService, BillService>();
        return services;
    }

    public static IServiceCollection AddCsvExportService(this IServiceCollection services)
    {
        services.TryAddScoped<ICsvExportService, CsvExportService>();
        return services;
    }
    public static IServiceCollection AddAwsS3Service(this IServiceCollection services)
    {
        services.TryAddAWSService<IAmazonS3>();
        services.TryAddScoped<IAwsS3Service, AwsS3Service>();
        return services;
    }
    public static IServiceCollection AddAwsService(this IServiceCollection services, IConfiguration configuration)
    {
        var awsSesSettings = configuration.GetSection(nameof(AwsSesSettings)).Get<AwsSesSettings>();

        if (awsSesSettings != null)
        {
            awsSesSettings.CheckSettings();
            services.AddSingleton(awsSesSettings);
            if (awsSesSettings.UseAssumeRole)
            {
                var baseCredentials = DefaultAWSCredentialsIdentityResolver.GetCredentials();
                var assumeCreds = new AssumeRoleAWSCredentials(baseCredentials, awsSesSettings.RoleArn, awsSesSettings.RoleSessionName);

                var region = RegionEndpoint.GetBySystemName(awsSesSettings.Region);
                var sesOptions = new AWSOptions
                {
                    Region = region,
                    Credentials = assumeCreds
                };
                services.TryAddAWSService<IAmazonSimpleEmailServiceV2>(sesOptions);
            }
            else
                services.TryAddAWSService<IAmazonSimpleEmailServiceV2>();

            services.TryAddScoped<IAwsSESService, AwsSESService>();
        }

        var awsSnsSettings = configuration.GetSection(nameof(AwsSnsSettings)).Get<AwsSnsSettings>();

        if (awsSnsSettings != null)
        {
            awsSnsSettings.CheckSettings();
            services.AddSingleton(awsSnsSettings);
            if (awsSnsSettings.UseAssumeRole)
            {
                var baseCredentials = DefaultAWSCredentialsIdentityResolver.GetCredentials();
                var assumeCreds = new AssumeRoleAWSCredentials(baseCredentials, awsSnsSettings.RoleArn, awsSnsSettings.RoleSessionName);

                var region = RegionEndpoint.GetBySystemName(awsSnsSettings.Region);
                var snsOptions = new AWSOptions
                {
                    Region = region,
                    Credentials = assumeCreds
                };
                services.TryAddAWSService<IAmazonSimpleNotificationService>(snsOptions);
            }
            else
                services.TryAddAWSService<IAmazonSimpleNotificationService>();

            services.TryAddScoped<IAwsSnsService, AwsSnsService>();
        }

        services.TryAddScoped<IAwsSESService, AwsSESService>();

        return services;
    }
    public static IServiceCollection AddCountryService(this IServiceCollection services)
    {
        services.TryAddScoped<ICountryService, CountryService>();
        return services;
    }
    public static IServiceCollection AddCityService(this IServiceCollection services)
    {
        services.TryAddScoped<ICityService, CityService>();
        return services;
    }
    public static IServiceCollection AddProvinceService(this IServiceCollection services)
    {
        services.TryAddScoped<IProvinceService, ProvinceService>();
        return services;
    }
    public static IServiceCollection AddCatalogService(this IServiceCollection services)
    {
        services.TryAddScoped<ICatalogService, CatalogService>();
        return services;
    }
    public static IServiceCollection AddDashboardService(this IServiceCollection services)
    {
        services.TryAddScoped<IDashboardService, DashboardService>();
        return services;
    }

    public static IServiceCollection AddGlobalLoanConfigService(this IServiceCollection services)
    {
        services.TryAddScoped<IGlobalLoanConfigService, GlobalLoanConfigService>();
        return services;
    }
    public static IServiceCollection AddSharedService(this IServiceCollection services, AppSettings settings, AppAssets appAssets)
    {

        services.AddSingleton(settings);

        services.AddSingleton(appAssets);
        // Logging
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        // AWS SDK
        services.AddAWSService<IAmazonS3>();
        // Shared services

        services.AddScoped<IAwsS3Service, AwsS3Service>();
        services.AddScoped<IPdfService, PdfService>();
        services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));
        services.AddScoped<ISharedService, SharedService>();

        return services;
    }



    #endregion
}