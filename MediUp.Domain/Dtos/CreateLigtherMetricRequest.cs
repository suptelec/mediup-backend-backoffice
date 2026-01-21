namespace MediUp.Domain.Dtos;

public class CreateLigtherMetricRequest
{
    public string Serial { get; set; } = null!;
    public string Codigo { get; set; } = null!;
    public string? Model { get; set; }
    public string? Brand { get; set; }
    public string? Class { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int ManufacturingYear { get; set; }
    public string Province { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public DateTime? LastCalibrationDate { get; set; }
    public DateTime? NextCalibrationDate { get; set; }
    public string? Status { get; set; }
    public string? UrlPicture { get; set; }
    public string? Address { get; set; }
    public string? Reference { get; set; }
    public long ElectricCompanyId { get; set; }
    public bool IsPrincipal { get; set; }
    public string? PrincipalCode { get; set; }
    public string? UrlCertificate { get; set; }

}
