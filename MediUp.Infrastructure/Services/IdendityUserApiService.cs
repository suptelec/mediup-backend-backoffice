using MediUp.Domain.Dtos;
using MediUp.Domain.Dtos.Identity.User.Requests;
using MediUp.Domain.Dtos.Identity.User.Responses;
using MediUp.Domain.Enums;
using MediUp.Domain.Interfaces.Identity;
using MediUp.Infrastructure.Interfaces.Apis;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Services;
public class IdendityUserApiService : BaseApiService, IIdendityUserApiService
{
    private readonly IIdentityUsersApi _user;
    private readonly IIdentityApiWithAuth _identityApi;

    public IdendityUserApiService(
        IIdentityUsersApi user,
        IIdentityApiWithAuth identityApi,
        ILogger<IdendityApiService> logger)
        : base(logger)
    {
        _user = user;
        _identityApi = identityApi;
    }

    public async Task<ResultDto<UserResponseDto>> CreateUser([Body] CreateUserRequestDto dto)
    {
        var response = new ResultDto<UserResponseDto>();
        try
        {
            Logger.LogInformation($"{nameof(CreateUser)}: Trying to create user...");
            response = await _user.CreateUser(dto);
        }
        catch (ApiException apiEx)
        {
            Logger.LogError(apiEx, $"{nameof(CreateUser)}: Api exception occurred trying to create user");
            await HandleApiException(apiEx, response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{nameof(CreateUser)}: Unknown error occurred trying to create user");
            HandleUnknownException(response);
        }
        return response;
    }

    public async Task<ResultDto<IdentityUserResponseDto>> GetUser()
    {
        var response = new ResultDto<IdentityUserResponseDto>();
        try
        {
            Logger.LogInformation($"{nameof(GetUser)}: Trying to create user...");
            response = await _identityApi.GetUser();
        }
        catch (ApiException apiEx)
        {
            Logger.LogError(apiEx, $"{nameof(GetUser)}: Api exception occurred trying to create user");
            await HandleApiException(apiEx, response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{nameof(GetUser)}: Unknown error occurred trying to create user");
            HandleUnknownException(response);
        }
        return response;
    }

    public async Task<EmptyResultDto> ActivateUser(long id, UserStatusType? userStatus = null)
    {
        var response = new EmptyResultDto();
        try
        {
            Logger.LogInformation($"{nameof(ActivateUser)}: Trying to activate user with id = {id}...");
            response = await _identityApi.ActivateUserAsync(id, userStatus);
        }
        catch (ApiException apiEx)
        {
            Logger.LogError(apiEx, $"{nameof(ActivateUser)}: Api exception occurred trying to activate user");
            await HandleApiException(apiEx, response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{nameof(ActivateUser)}: Unknown error occurred trying to activate user");
            HandleUnknownException(response);
        }

        return response;
    }
    public async Task<EmptyResultDto> InactivateUser(long id)
    {
        var response = new EmptyResultDto();
        try
        {
            Logger.LogInformation($"{nameof(InactivateUser)}: Trying to inactivate user...");
            response = await _identityApi.InactivateUser(id);
        }
        catch (ApiException apiEx)
        {
            Logger.LogError(apiEx, $"{nameof(InactivateUser)}: Api exception occurred trying to inactivate user ...");
            await HandleApiException(apiEx, response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{nameof(InactivateUser)}: Unknown error occurred trying to inactivate user ...");
            HandleUnknownException(response);
        }
        return response;
    }

    public async Task<EmptyResultDto> DeleteUser(long id)
    {
        var response = new EmptyResultDto();
        try
        {
            Logger.LogInformation($"{nameof(DeleteUser)}: Trying to delete user ...");
            response = await _identityApi.DeleteUser(id);
        }
        catch (ApiException apiEx)
        {
            Logger.LogError(apiEx, $"{nameof(DeleteUser)}: Api exception occurred trying to delete user ...");
            await HandleApiException(apiEx, response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{nameof(DeleteUser)}: Unknown error occurred trying to delete user ...");
            HandleUnknownException(response);
        }
        return response;
    }

}
