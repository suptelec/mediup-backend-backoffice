using System;

namespace MediUp.Domain.Entities;

public class EnergyMeasurementEvent : BaseEntity
{
    public DateTime OccurredAt { get; set; }

    public long EnergyMeasurementDownloadId { get; set; }

    public virtual EnergyMeasurementDownload EnergyMeasurementDownload { get; set; } = null!;
}
