using MediUp.Domain.Dtos;
using MediUp.Domain.Interfaces.Identity;
using MediUp.Domain.Models;
using MediUp.Infrastructure.Interfaces.Apis;
using MediUp.Infrastructure.Models.Identity;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Services;
public class IdendityApiService : BaseApiService, IIdendityApiService
{
    private readonly IIdentityApi _identityApi;
    private readonly AuthServerSettings _settings;
    private TokenResponse? _currentToken;
    public IdendityApiService(
        IIdentityApi identityApi,
        AuthServerSettings settings,
        ILogger<IdendityApiService> logger)
        : base(logger)
    {
        _identityApi = identityApi;
        _settings = settings;
    }
    public async Task<string> GetToken()
    {
        var response = new EmptyResultDto();
        try
        {
            var tokenRequest = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _settings.ClientId,
                ["client_secret"] = _settings.ClientSecret,
                ["scope"] = string.Join(" ", _settings.Scope)
            };

            _currentToken = await _identityApi.GetTokenAsync(tokenRequest);
        }
        catch (ApiException apiEx)
        {
            Logger.LogError(apiEx, $"{nameof(GetToken)}: Api exception occurred trying to connect token");
            await HandleApiException(apiEx, response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{nameof(GetToken)}: Unknown error occurred trying to connect token");
            HandleUnknownException(response);
        }

        return _currentToken!.AccessToken;
    }
}