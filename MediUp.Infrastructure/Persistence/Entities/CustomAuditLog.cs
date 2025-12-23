using MediUp.Infrastructure.Smarter.EntityFramework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Persistence.Entities;
public class CustomAuditLog : BaseAuditLog
{
    public string? ModuleId { get; set; }
    public string? IpAddress { get; set; }
    public void SetCustomValues(string ipAddress)
    {
        IpAddress = ipAddress;
    }
}
