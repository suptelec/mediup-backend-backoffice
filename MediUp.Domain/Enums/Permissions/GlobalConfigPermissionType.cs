using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Enums.Permissions;
[Flags]
public enum GlobalConfigPermissionType : long
{
    None = 0, //0
    ReadConfiguration = 1 << 0, //1
    CudConfiguration = 1 << 1, //2
    Dashboard = 1 << 2,

    All = ReadConfiguration | CudConfiguration //7
}
