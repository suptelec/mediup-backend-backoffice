using MediUp.Domain.Constants;
using MediUp.Domain.Enums.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Extensions;
public static class PermissionTypeExtensions
{
    public static bool IsThisPermissionAllowed(this string permission, string claimType, string permissionName)
    {
        if (!long.TryParse(permission, out long currentPermission))
        {
            throw new ArgumentOutOfRangeException(nameof(permission), permission, "The current permission couldn't be converted to a valid long value.");
        }

        if (!long.TryParse(permissionName, out long requiredPermission))
        {
            throw new ArgumentOutOfRangeException(nameof(permissionName), permissionName, "The required permission couldn't be converted to a valid integer value.");
        }

        Type enumType = claimType switch
        {

            AppConstants.GlobalPermissionsClaim => typeof(GlobalConfigPermissionType),
            _ => throw new ArgumentOutOfRangeException(nameof(claimType), claimType, null)
        };

        bool exists = new List<long>(Enum.GetValues(enumType).Cast<long>())
            .Any(enumValue =>
                enumValue != 0 &&
                (enumValue & requiredPermission) == enumValue &&
                (requiredPermission & currentPermission) == requiredPermission);

        return exists;
    }
}
