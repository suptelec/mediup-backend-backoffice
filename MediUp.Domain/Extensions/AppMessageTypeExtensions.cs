using MediUp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Extensions;
public static class AppMessageTypeExtensions
{
    public static string GetErrorMsg(this AppMessageType msg)
    {
        return msg switch
        {
            AppMessageType.InvalidRequest => "Invalid request",
            AppMessageType.UnknownError => "Unknown error occurred",
            AppMessageType.NotFound => "The resource was not found",
            AppMessageType.InvalidStatusTransition => "The requested loan status transition is not allowed based on the current status.",
            AppMessageType.UserNotActivate => "The user is not active or does not exist.",
            AppMessageType.BoUnauthorizedUser => "The user is not authorized.",
            AppMessageType.IdentityError => "The identity server returned an error.",
            AppMessageType.UserHasActiveLoans => "Action not allowed: the user has active loans.",
            AppMessageType.CustomerIncompleteOnboarding => "Action not allowed: the user has not completed the onboarding process.",

            //map identity errors
            AppMessageType.ISInvalidRequest => "Invalid request",
            AppMessageType.UnknownErrorOccurred => "Unknown error occurred",
            AppMessageType.ISNotFound => "The resource you were looking for was not found",
            AppMessageType.ISResourceAlreadyExists => "Resource already exists",
            AppMessageType.UserIsLockedOut => "User is locked out",
            AppMessageType.AccesCodeNotValid => "Access code is not valid",
            AppMessageType.AccesCodeExpired => "Access code is expired",
            AppMessageType.DNIExists => "DNI already exists",
            AppMessageType.HasPreviousSession => "An active session already exists for this user.",
            AppMessageType.InvalidPassword => "Invalid password",
            AppMessageType.OnlyOneAdminRemaining => "Operation denied: this user is the only active administrator and cannot be locked.",
            AppMessageType.InvalidUserlogin => "The user login is invalid.",
            AppMessageType.UserEmailExists => "the user email already exists.",
            AppMessageType.CannotDeleteLastAdmin => "Cannot delete the last admin user.",
            _ => throw new ArgumentOutOfRangeException(nameof(msg), msg, null)
        };
    }

    public static string GetErrorCodeValue(this AppMessageType msg) => $"{(int)msg}";

    public static string GetErrorCode(this AppMessageType msg)
    {
        int msgId = (int)msg;
        return $"CBO_{msgId}";
    }
}
