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

    public const string MeterPermissionsClaim = "mup.fo.meters.permissions";
    public const string EnergyMeasurementDownloadPermissionsClaim = "mup.fo.energymeasurementdownloads.permissions";
    public const string DashboardPermissionsClaim = "mup.fo.dashboard.permissions";
    public const string AgentPermissionsClaim = "mup.agent.permissions";

    public const string PermissionsClaim = "permissions";

    public const int RoleAgentId = 2;





}
