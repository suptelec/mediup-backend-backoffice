namespace MediUp.Domain.Entities;
public class EnergyMeasurementSummary : BaseEntity
{
    public long IdEmpresa { get; set; }
    public long IdSystemaMedida { get; set; }
    public string Codigo { get; set; } = null!;
    public string TPlCode { get; set; } = null!;
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal TotalActiveEnergyDeliveredKwh { get; set; }
    public decimal TotalActiveEnergyReceivedKwh { get; set; }
    public decimal TotalReactiveEnergyDeliveredKvarh { get; set; }
    public decimal TotalReactiveEnergyReceivedKvarh { get; set; }
    public decimal TotalApparentEnergyDeliveredKvah { get; set; }
}
