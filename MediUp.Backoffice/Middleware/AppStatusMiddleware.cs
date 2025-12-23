using Microsoft.Net.Http.Headers;

namespace MediUp.Backoffice.Middleware;

public class AppStatusMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context, ILogger<AppStatusMiddleware> logger)
    {
        var headers = context.Request.Headers
            .Where(kvp => kvp.Key != HeaderNames.Authorization)
            .ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.ToString());
        string url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
        logger.LogInformation(
            "Request started for username = {Username} url = {Url} with headers = {Headers}",
            context.User.Identity?.Name ?? "NA",
            url,
            headers);

        return next(context);
    }
}
