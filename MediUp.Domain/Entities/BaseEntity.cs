using MediUp.Domain.Enums;
using MediUp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Entities;
public abstract class BaseEntity : IBaseEntity
{

    public long Id { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual void MarkAsDeleted()
    {

        if (this is IEntityWithStatus entityWithStatus)
        {
            entityWithStatus.Status = EntityStatus.Deleted;
        }
    }
}