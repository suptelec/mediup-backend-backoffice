namespace MediUp.Domain.Entities;
public class CenaceMeasurementDetail: BaseEntity
{
    public string Hour { get; set; } = string.Empty;
    public double Value { get; set; }
    public long CenaceMeasurementId { get; set; }
    public virtual CenaceMeasurement CenaceMeasurement { get; set; } = null!;
}
