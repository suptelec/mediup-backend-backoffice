using System;

namespace MediUp.Domain.Enums.Permissions.FrontOffice;
[Flags]
public enum EnergyMeasurementDownloadPermissionType : long
{
    None = 0,
    Read = 1 << 0,
    Cud = 1 << 1,
    All = Read | Cud,
}
