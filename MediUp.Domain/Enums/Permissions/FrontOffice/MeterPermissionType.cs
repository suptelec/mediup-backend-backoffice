using System;

namespace MediUp.Domain.Enums.Permissions.FrontOffice;
[Flags]
public enum MeterPermissionType : long
{
    None = 0,
    Read = 1 << 0,
    Cud = 1 << 1,
    All = Read | Cud,
}
