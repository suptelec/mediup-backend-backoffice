using System;

namespace MediUp.Domain.Entities;

public class EnergyMeasurementData : BaseEntity
{
    public int ExternalId { get; set; }

    public DateTime MeasuredAt { get; set; }

    public DateTime MeasuredAtIso { get; set; }

    public int QuarterHour { get; set; }

    public decimal ActiveEnergyDeliveredKwh { get; set; }

    public decimal ActiveEnergyReceivedKwh { get; set; }

    public decimal ReactiveEnergyDeliveredKvarh { get; set; }

    public decimal ReactiveEnergyReceivedKvarh { get; set; }

    public decimal ApparentEnergyDeliveredKvah { get; set; }

    public decimal IntegrationPeriodSeconds { get; set; }

    public decimal AverageVoltageKv { get; set; }

    public decimal Frequency { get; set; }

    public long EnergyMeasurementDownloadId { get; set; }

    public virtual EnergyMeasurementDownload EnergyMeasurementDownload { get; set; } = null!;
}
