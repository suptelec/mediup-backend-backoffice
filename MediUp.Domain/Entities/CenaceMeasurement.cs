namespace MediUp.Domain.Entities;
public class CenaceMeasurement: BaseEntity
{
    public DateOnly MeasurementDate { get; set; }
    public DateTime PublicationDate { get; set; }
    public string CenaceCode { get; set; } = null!;
    public string TPLCode { get; set; } = null!;
    public string Code { get; set; } = null!;
    public virtual ICollection<CenaceMeasurementDetail> CenaceMeasurementDetails { get; set; } = new List<CenaceMeasurementDetail>();


}
