using MediUp.Domain.Constants;
using MediUp.Domain.Enums.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Authorization;
public class AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        string enumTypeName = policyName.Split("_").First();
        string enumValue = policyName.Split("_").Last();

        string claimType = enumTypeName switch
        {
           
            nameof(GlobalConfigPermissionType) => AppConstants.GlobalPermissionsClaim,
            _ => throw new ArgumentOutOfRangeException(nameof(policyName), policyName, null)
        };

        return await base.GetPolicyAsync(policyName)
               ?? new AuthorizationPolicyBuilder()
                   .RequireClaim(claimType)
                   .AddRequirements(new PermissionRequirement(claimType, enumValue))
                   .Build();
    }
}
