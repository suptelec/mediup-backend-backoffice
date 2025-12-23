using MediUp.Domain.Constants;
using MediUp.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;


namespace MediUp.Backoffice.Models;

public class CurrentLoggedUser : ICurrentLoggedUser
{
    public long Id { get; }
    public string UserName { get; }
    public string Email { get; }
    public string FullName { get; }
    public string? IpAddress { get; }

    public CurrentLoggedUser(IHttpContextAccessor context)
    {
        var httpContext = context.HttpContext;
        Id = long.Parse(GetClaimValue(httpContext, JwtRegisteredClaimNames.NameId) ?? "0");
        UserName = GetClaimValue(httpContext, JwtRegisteredClaimNames.Sub)
                   ?? GetClaimValueOrNa(httpContext, JwtRegisteredClaimNames.UniqueName);
        Email = GetClaimValueOrNa(httpContext, JwtRegisteredClaimNames.Email);
        FullName = GetClaimValueOrNa(httpContext, JwtRegisteredClaimNames.Name);
        IpAddress = httpContext?.Connection?.RemoteIpAddress?.ToString();
        //     if (Enum.TryParse(GetClaimValue(httpContext, AppConstants.GlobalPermissionsClaim), out GlobalConfigPermissionType permission))
        //     {
        //         GlobalConfigPermission = permission;
        //     }
    }

    protected static string GetClaimValueOrNa(HttpContext? context, params string[] keys)
    {
        return GetClaimValue(context, keys) ?? AppConstants.Na;
    }

    protected static string? GetClaimValue(HttpContext? context, params string[] keys)
    {
        return keys
            .Select(key => context?.User.Claims.FirstOrDefault(c => c.Type == key)?.Value)
            .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
    }
}