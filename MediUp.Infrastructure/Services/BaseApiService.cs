using MediUp.Domain.Dtos;
using MediUp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using MediUp.Domain.Extensions;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Services;
public class BaseApiService
{
    protected readonly ILogger Logger;
    public BaseApiService(ILogger logger)
    {
        Logger = logger;
    }

    protected async Task HandleApiException<T>(
          ApiException ex,
          List<T> responses,
          AppMessageType defaultError = AppMessageType.UnknownError)
          where T : EmptyResultDto
    {
        foreach (var response in responses)
        {
            await HandleApiException(ex, response, defaultError);
        }
    }

    protected async Task HandleApiException<T>(
       ApiException ex,
       T response,
       AppMessageType defaultError = AppMessageType.UnknownError)
       where T : EmptyResultDto
    {
        var error = await ex.GetContentAsAsync<EmptyResultDto>();
        var statusCodeList = new List<System.Net.HttpStatusCode>() { System.Net.HttpStatusCode.Unauthorized, System.Net.HttpStatusCode.Forbidden };

        if (statusCodeList.Contains(ex.StatusCode))
        {
            Logger.LogError(ex,
                $"{nameof(HandleApiException)}: Response StatusCode is {ex.StatusCode}");

            var newResponse = new EmptyResultDto(AppMessageType.BoUnauthorizedUser);
            response.Message = newResponse.Message;
            response.MessageId = newResponse.MessageId;
            response.MessageType = newResponse.MessageType;
        }

        //If for some reasone, we cant get an error response, lets set a default one
        else if (error is null)
        {
            Logger.LogError(ex,
                $"{nameof(HandleApiException)}: Response doesnt have a body, " +
                $"so this may be an error produced by this app");
            HandleUnknownException(response, defaultError);
        }
        else
        {
            Logger.LogError(ex,
                $"{nameof(HandleApiException)}: Response does have a body, " +
                $"Error = {error.Message} - {error.MessageId}");
            response.Message = error.Message;
            response.MessageId = error.MessageId;
            response.MessageType = error.MessageType;
        }

    }

    protected void HandleUnknownException<T>(
        T response,
        AppMessageType defaultError = AppMessageType.UnknownError)
        where T : EmptyResultDto
    {
        response.MessageId = defaultError.GetErrorCode();
        response.Message = defaultError.GetErrorMsg();
        response.MessageType = defaultError;
        response.MessageId = StatusCodes.Status500InternalServerError.ToString();
    }

}
