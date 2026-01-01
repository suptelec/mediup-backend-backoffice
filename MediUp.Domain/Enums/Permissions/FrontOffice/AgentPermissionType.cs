using System;

namespace MediUp.Domain.Enums.Permissions.FrontOffice;
[Flags]
public enum AgentPermissionType : long
{
    None = 0,
    ReadReports = 1 << 0,
    ManageReports = 1 << 1,
    ManageAgents = 1 << 2,
    All = ReadReports | ManageReports | ManageAgents
}
