namespace MediUp.Domain.Entities;
public class PowerFactorThreshold: BaseEntity
{
    public string Level { get; set; } = string.Empty;
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public string Description { get; set; } = string.Empty;
}