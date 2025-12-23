using MediUp.Domain.Dtos;
using MediUp.Domain.Dtos.Identity.User.Requests;
using MediUp.Domain.Dtos.Identity.User.Responses;
using MediUp.Domain.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Backoffice.Controllers;

public class IdentityUsersController(ILoggerFactory loggerFactory, IIdendityUserApiService idendityUserApiService)
    : BaseController(loggerFactory)
{
    private readonly IIdendityUserApiService _idendityUserApiService = idendityUserApiService;

    /// <summary>
    /// Creates a user in the identity service.
    /// </summary>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultDto<UserResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<UserResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestDto request)
    {
        Logger.LogInformation("Request received to create user with username {Username} and type {UserType}.", request.Username, request.Type);

        if (!ModelState.IsValid)
        {
            var validationMessage = new StringBuilder("The user payload is invalid.");
            foreach (var state in ModelState.Values.SelectMany(v => v.Errors))
            {
                validationMessage.Append(' ').Append(state.ErrorMessage);
            }

            Logger.LogWarning("Validation failed when creating user {Username}: {Errors}", request.Username, validationMessage.ToString());

            var invalidResult = Result.InvalidRequest<UserResponseDto>(validationMessage.ToString());
            return HandleResult(invalidResult);
        }

        var result = await _idendityUserApiService.CreateUser(request);

        if (result.Succeed)
        {
            Logger.LogInformation("User created successfully with id {Id}.", result.Result?.Id);
        }
        else
        {
            Logger.LogWarning("Failed to create user {Username}. Error: {Error}", request.Username, result.Message);
        }

        return HandleResult(result);
    }
}
