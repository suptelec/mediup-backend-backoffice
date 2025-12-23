using MediUp.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Authorization;
public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        List<Claim> permissions = [.. context.User.Claims.Where(c => c.Type == requirement.ClaimType)];
        foreach (Claim permission in permissions)
        {
            if (permission.Value.IsThisPermissionAllowed(requirement.ClaimType, requirement.ClaimValue))
                context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
