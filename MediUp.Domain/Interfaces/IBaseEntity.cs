using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Interfaces;
public interface IBaseEntity
{
    long Id { get; set; }
    string CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }

    string? UpdatedBy { get; set; }
    DateTime? UpdatedAt { get; set; }
}
