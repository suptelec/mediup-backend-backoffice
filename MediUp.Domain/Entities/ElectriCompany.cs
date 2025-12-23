using MediUp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Entities;
public class ElectriCompany : BaseEntity
{

    public string Name { get; set; } = null!;

    public string TaxId { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public virtual ICollection<LigtherMetric> MupLigthermetrics { get; set; } = new List<LigtherMetric>();
}
