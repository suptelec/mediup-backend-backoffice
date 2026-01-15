using MediUp.Domain.Enums;

namespace MediUp.Domain.Entities;
public class LigtherTransformer: BaseEntity
{
    public string Serial { get; set; } = null!;
    public string? Codigo { get; set; }
    public string? Model { get; set; }
    public string? Brand { get; set; }
    public string? Class { get; set; }
    public DateTime? LastCalibrationDate { get; set; }
    public DateTime? NextCalibrationDate { get; set; }
    public string? UrlPicture { get; set; }
    public TransformerType Type { get; set; }
    public double PrimaryCurrent { get; set; }
    public double SecondaryCurrent { get; set; }
    public double PrimaryVoltage { get; set; }
    public double SecondaryVoltage { get; set; }
    public long ElectricCompanyId { get; set; }
}
