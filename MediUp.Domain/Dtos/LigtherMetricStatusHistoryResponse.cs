namespace MediUp.Domain.Dtos;

public class LigtherMetricStatusHistoryResponse
{
    public long Id { get; set; }
    public string Code { get; set; } = null!;
    public DateTime MeasuredAt { get; set; }
    public string Status { get; set; } = null!;
    public string ElectricCompanyName { get; set; } = null!;
}
