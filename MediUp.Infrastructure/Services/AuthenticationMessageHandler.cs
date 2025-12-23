using MediUp.Domain.Models;
using MediUp.Infrastructure.Interfaces.Apis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Services;
public class AuthenticationMessageHandler : DelegatingHandler
{
    private readonly IIdentityApi _identityService;
    private readonly AuthServerSettings _settings;

    public AuthenticationMessageHandler(IIdentityApi identityService, AuthServerSettings settings)
    {
        _identityService = identityService;
        _settings = settings;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var tokenRequest = new Dictionary<string, string>
        {
            ["grant_type"] = "client_credentials",
            ["client_id"] = _settings.ClientId,
            ["client_secret"] = _settings.ClientSecret,
            ["scope"] = string.Join(" ", _settings.Scope)
        };

        var token = await _identityService.GetTokenAsync(tokenRequest);

        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
