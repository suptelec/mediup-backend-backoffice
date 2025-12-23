using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Models;
public class IdentityServerSettings
{
    public string Issuer { get; set; } = null!;
    public string Authority { get; set; } = null!;
    public string[] Audiences { get; set; } = null!;
    public bool RequireHttpsMetadata { get; set; }
    public string SwaggerClientId { get; set; } = null!;
    public string SwaggerClientSecret { get; set; } = null!;
    public List<string> SwaggerScopes { get; set; } = [];
}
