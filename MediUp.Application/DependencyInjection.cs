using FluentValidation;
using MediUp.Application.Interfaces;
using MediUp.Application.Services.ElectriCompanies;
using MediUp.Application.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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

    public static IServiceCollection AddElectriCompanyService(this IServiceCollection services)
    {
        services.TryAddScoped<IElectriCompanyService, ElectriCompanyService>();
        return services;
    }

   



    #endregion
}