namespace MediUp.Domain.Entities;
public class LigtherMetricStatusHistory : BaseEntity
{
    public string Code { get; set; } = null!;
    public DateTime MeasuredAt { get; set; }
    public string Status { get; set; } = null!;
    public string ElectricCompanyName { get; set; } = null!;
}
