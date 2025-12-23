using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Enums;
public enum AppMessageType
{
    //To map identity errors
    ISInvalidRequest = 1,
    UnknownErrorOccurred = 2,
    ISNotFound = 3,
    ISResourceAlreadyExists = 4,
    UserIsLockedOut = 5,
    AccesCodeNotValid = 6,
    AccesCodeExpired = 7,
    DNIExists = 8,
    HasPreviousSession = 9,
    InvalidPassword = 10,
    OnlyOneAdminRemaining = 11,
    InvalidUserlogin = 12,
    UserEmailExists = 13,
    CannotDeleteLastAdmin = 14,
    //FrontOffice 100 - 999

    //1000 - 1999 backoffice reservado
    InvalidRequest = 1000,
    NotFound = 1001,
    InvalidStatusTransition = 1002,
    UserNotActivate = 1003,
    UserHasActiveLoans = 1004,
    CustomerIncompleteOnboarding = 1005,
    UnknownError = 1999,

    BoUnauthorizedUser = 3001,

    IdentityError = 4000,
}

