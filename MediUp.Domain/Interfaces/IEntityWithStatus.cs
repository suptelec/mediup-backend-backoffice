using MediUp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Interfaces;
public interface IEntityWithStatus : IBaseEntity
{
    /// <summary>
    /// Estado de la entidad
    /// </summary>
    EntityStatus Status { get; set; }
}
