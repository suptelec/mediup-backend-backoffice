using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Enums;
public enum EntityStatus
{
    /// <summary>
    /// Entidad activa
    /// </summary>
    Active = 1,

    /// <summary>
    /// Entidad inactiva
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Entidad eliminada (borrado lógico)
    /// </summary>
    Deleted = 3
}
