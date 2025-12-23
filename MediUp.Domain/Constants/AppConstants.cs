using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Constants;
public static class AppConstants
{
    public static string DeletedSuffix(long id) => $"-del{id}";
    public const string Na = "N/A";
    public const string ContentId = "logoCID";

    public const string CustomerPermissionsClaim = "mup.customer.permissions";
    public const string LoanPermissionsClaim = "mup.loan.permissions";
    public const string GlobalPermissionsClaim = "mup.global.permissions";
    public const string BillBatchPermissionClaim = "mup.billbatch.permissions";
    public const string CacheKey = "dashboard_sumary_cache";

    public const string LoanOverdueNotification = "Tu préstamo {0} requiere tu atención";

   


}
