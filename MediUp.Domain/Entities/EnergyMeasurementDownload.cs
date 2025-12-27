using System;
using System.Collections.Generic;

namespace MediUp.Domain.Entities;

public class EnergyMeasurementDownload : BaseEntity
{
    public string Username { get; set; } = null!;

    public string Meter { get; set; } = null!;

    public DateOnly MeasurementDate { get; set; }

    public string IntegrationStatus { get; set; } = null!;

    public virtual ICollection<EnergyMeasurementEvent> Events { get; set; } = new List<EnergyMeasurementEvent>();

    public virtual ICollection<EnergyMeasurementData> Data { get; set; } = new List<EnergyMeasurementData>();
}
