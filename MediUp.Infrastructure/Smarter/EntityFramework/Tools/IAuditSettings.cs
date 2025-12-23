using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Smarter.EntityFramework.Tools;
public interface IAuditSettings
{
    long Node { get; }

    string Assembly { get; }

    string ModelNameSpace { get; }

    string CreatedBy { get; }

    string UpdatedBy { get; }

    Action<BaseAuditLog, object> SetCustomProperties { get; set; }

    Dictionary<string, List<string>> IgnoredProps { get; }
}
