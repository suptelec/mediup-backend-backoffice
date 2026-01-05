using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Entities;
public class LigtherMetric : BaseEntity
{
    public string Serial { get; set; } = null!;
    public string Codigo { get; set; } = null!;

    public string? Model { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
    public int ManufacturingYear { get; set; }

    public string Province { get; set; } = string.Empty;

    public string Sector { get; set; } = string.Empty;

    public DateTime? LastCalibrationDate { get; set; }
    public DateTime? NextCalibrationDate  { get; set; }

    public string? Status { get; set; } 

    public long ElectricCompanyId { get; set; }

    public virtual ElectriCompany ElectricCompany { get; set; } = null!;
}
