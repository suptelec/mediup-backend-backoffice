using MediUp.Domain.Constants;
using MediUp.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace MediUp.Infrastructure.Authorization;
public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        List<Claim> permissions = context.User.Claims.Where(c => c.Type == requirement.ClaimType).ToList();
        foreach (Claim permission in permissions)
        {
            if (permission.Value.IsThisPermissionAllowed(requirement.ClaimType, requirement.ClaimValue))
                context.Succeed(requirement);
        }

        IEnumerable<string> groupedPermissionValues = context.User.Claims
            .Where(c => c.Type == AppConstants.PermissionsClaim)
            .Select(c => c.Value);
        if (groupedPermissionValues.Any())
        {
            string key = requirement.ClaimType
                .Replace("mup.", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace(".permissions", string.Empty, StringComparison.OrdinalIgnoreCase);

            List<string> permissionEntries = new();
            foreach (string value in groupedPermissionValues)
            {
                if (value.StartsWith("[", StringComparison.Ordinal))
                {
                    string[]? entries = JsonSerializer.Deserialize<string[]>(value);
                    if (entries is not null)
                    {
                        permissionEntries.AddRange(entries);
                    }
                }
                else
                {
                    permissionEntries.Add(value);
                }
            }

            foreach (string entry in permissionEntries)
            {
                string[] parts = entry.Split(':', 2, StringSplitOptions.TrimEntries);
                if (parts.Length != 2)
                {
                    continue;
                }

                if (!parts[0].Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (parts[1].IsThisPermissionAllowed(requirement.ClaimType, requirement.ClaimValue))
                {
                    context.Succeed(requirement);
                }
            }
        }

        return Task.CompletedTask;
    }
}