using MediUp.Domain.Dtos;
using MediUp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System.Net.Mime;


namespace MediUp.Backoffice.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(typeof(EmptyResultDto), StatusCodes.Status500InternalServerError)]
public abstract class BaseController : ControllerBase
{

    protected readonly ILogger Logger;

    protected BaseController(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }

    protected IActionResult HandleResult<T>(T result)
    where T : EmptyResultDto
    {
        if (result.Succeed)
        {
            return Ok(result);
        }

        return result.MessageType switch
        {
            AppMessageType.UnknownError => StatusCode(StatusCodes.Status500InternalServerError, result),
            AppMessageType.InvalidRequest or
            AppMessageType.NotFound => NotFound(result),
            AppMessageType.InvalidStatusTransition => BadRequest(result),
            AppMessageType.UserNotActivate => BadRequest(result),
            AppMessageType.BoUnauthorizedUser => BadRequest(result),
            AppMessageType.IdentityError => BadRequest(result),
            AppMessageType.UserHasActiveLoans => BadRequest(result),
            AppMessageType.CustomerIncompleteOnboarding => BadRequest(result),

            //Identity map errors
            AppMessageType.AccesCodeNotValid => BadRequest(result),
            AppMessageType.AccesCodeExpired => BadRequest(result),
            AppMessageType.UserIsLockedOut => Unauthorized(result),
            AppMessageType.DNIExists => BadRequest(result),
            AppMessageType.ISInvalidRequest => BadRequest(result),
            AppMessageType.ISNotFound => NotFound(result),
            AppMessageType.ISResourceAlreadyExists => BadRequest(result),
            AppMessageType.HasPreviousSession => BadRequest(result),
            AppMessageType.InvalidPassword => BadRequest(result),
            AppMessageType.OnlyOneAdminRemaining => BadRequest(result),
            AppMessageType.InvalidUserlogin => BadRequest(result),
            AppMessageType.UserEmailExists => BadRequest(result),
            AppMessageType.CannotDeleteLastAdmin => BadRequest(result),
            null => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
