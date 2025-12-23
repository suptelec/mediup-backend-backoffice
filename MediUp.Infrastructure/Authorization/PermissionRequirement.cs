using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Authorization;
public class PermissionRequirement(string claimType, string claimValue) : IAuthorizationRequirement
{
    public string ClaimType { get; } = claimType;
    public string ClaimValue { get; } = claimValue;
}
