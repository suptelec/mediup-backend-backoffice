namespace MediUp.Backoffice.Extensions;

public static class SwaggerAppBuilderExtensions
{
    public static IApplicationBuilder UseSwagger(
        this IApplicationBuilder app,
        string oAuthClientId,
        string oAuthClientSecret,
        string apiName,
        string version = "V1")
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{apiName} {version}");
            c.DocumentTitle = $"{apiName} API";

            c.OAuthClientId(oAuthClientId);
            c.OAuthClientSecret(oAuthClientSecret);
            c.OAuthRealm("realm");
            c.OAuthAppName(apiName);
            c.OAuthScopeSeparator(" ");
            c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            // c.OAuthUsePkce();
        });

        return app;
    }
}
