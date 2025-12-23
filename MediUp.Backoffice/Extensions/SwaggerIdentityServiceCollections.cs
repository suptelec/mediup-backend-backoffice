using MediUp.Backoffice.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace MediUp.Backoffice.Extensions;

public static class SwaggerIdentityServiceCollections
{
    public static IServiceCollection AddSwagger(
        this IServiceCollection services,
        string oAuthAuthority,
        List<string> swaggerScopes,
        string apiName,
        string xmlFileName,
        string version = "V1")
    {
        var (oAuthScheme, oAuthSecReq, oAuthSecScheme) = BuildForOauth(oAuthAuthority, swaggerScopes);

        var (bearerScheme, bearerSecReq, bearerSecScheme) = BuildForBearerToken();

        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwaggerIgnoreParameterFilter>();
            c.OperationFilter<ODataQueryOptionsFilter>();
            c.SwaggerDoc(version, new OpenApiInfo
            {
                Version = version,
                Title = $"{apiName} API",
                Description = $"{apiName} API"
            });
            c.AddSecurityDefinition(oAuthScheme, oAuthSecScheme);
            c.AddSecurityRequirement(oAuthSecReq);
            c.AddSecurityDefinition(bearerScheme, bearerSecScheme);
            c.AddSecurityRequirement(bearerSecReq);

            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
            c.IncludeXmlComments(xmlPath);
            c.UseAllOfToExtendReferenceSchemas();

        });
        return services;
    }

    private static (string, OpenApiSecurityRequirement, OpenApiSecurityScheme) BuildForBearerToken()
    {
        string scheme = "Bearer";
        var securityScheme = new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization,
            Type = SecuritySchemeType.ApiKey,
            Scheme = scheme
        };

        var securityReq = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = scheme
                    }
                }, Array.Empty<string>()
            }
        };

        return (scheme, securityReq, securityScheme);
    }

    private static (string, OpenApiSecurityRequirement, OpenApiSecurityScheme) BuildForOauth(
        string oAuthAuthority,
        IEnumerable<string> swaggerScopes)
    {
        string scheme = "oauth2";
        var scopeDictionary = swaggerScopes.ToDictionary(x => x, x => "");
        var securityScheme = new OpenApiSecurityScheme
        {
            Description = "Oauth flow",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization,
            Type = SecuritySchemeType.OAuth2,
            Scheme = scheme,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{oAuthAuthority}/connect/authorize"),
                    TokenUrl = new Uri($"{oAuthAuthority}/connect/token"),
                    Scopes = scopeDictionary
                }
            },
            OpenIdConnectUrl = new Uri($"{oAuthAuthority}/connect/authorize")
        };

        var securityReq = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = scheme
                    }
                }, Enumerable.Empty<string>().ToList()
            }
        };

        return (scheme, securityReq, securityScheme);
    }
}
