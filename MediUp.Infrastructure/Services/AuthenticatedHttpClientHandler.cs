using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Services;
public class AuthenticatedHttpClientHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedHttpClientHandler(
       IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //TODO: THIS MAY CHANGE IN THE FUTURE
        if (request.Headers.Authorization!.Scheme == JwtBearerDefaults.AuthenticationScheme)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            string token = httpContext!.Request.Headers["Authorization"].ToString()?.Split($"{JwtBearerDefaults.AuthenticationScheme} ")[1]!;

            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
