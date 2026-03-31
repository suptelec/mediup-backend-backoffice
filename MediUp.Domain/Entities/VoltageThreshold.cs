namespace MediUp.Domain.Entities;
public class VoltageThreshold : BaseEntity
{
    public decimal NominalKv { get; set; }
    public decimal LowerNormal { get; set; }
    public decimal LowerEmergent { get; set; }
    public decimal UpperNormal { get; set; }
    public decimal UpperEmergent { get; set; }
}