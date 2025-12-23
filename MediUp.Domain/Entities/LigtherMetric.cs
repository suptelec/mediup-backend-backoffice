using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Entities;
public class LigtherMetric : BaseEntity
{
    public string Serial { get; set; } = null!;

    public string? Model { get; set; }

    public string? FirmwareVersion { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Altitude { get; set; }

    public DateOnly? InstallationDate { get; set; }

    public DateTime? LastMaintenanceAt { get; set; }

    public string Status { get; set; } = null!;

    public long ElectricCompanyId { get; set; }

    public virtual ElectriCompany ElectricCompany { get; set; } = null!;
}
