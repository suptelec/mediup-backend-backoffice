using MediUp.Infrastructure.Models.Identity;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Interfaces.Apis;
public interface IIdentityApi
{
    [Post("/connect/token")]
    Task<TokenResponse> GetTokenAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> request);

}

