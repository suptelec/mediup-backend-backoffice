namespace MediUp.Domain.Dtos;

public class CreateLigtherMetricStatusHistoryRequest
{
    public string User { get; set; } = null!;
    public Dictionary<string, string> Status { get; set; } = new();
    public string Date { get; set; } = null!;
}
