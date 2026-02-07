namespace MediUp.Domain.Entities;
public class LigtherMetric : BaseEntity
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
    public bool IsPrincipal { get; set; }
    public string? PrincipalCode { get; set; }
    public string? UrlCertificate { get; set; }
    public long? SystemLigtherId { get; set; }
    public string? IpAddress { get; set; }         
    public string? Gateway { get; set; }           
    public string? SubnetMask { get; set; }        
    public string? MacAddress { get; set; }        
    public string? TpRatio { get; set; }          
    public string? TcRatio { get; set; }          
    public string? BatteryLevelPercent { get; set; } 
    public string? SwitchLanPort { get; set; }
    public virtual SystemLigther? SystemLigther { get; set; } = null!;
    public virtual ICollection<LigtherTransformerMetric> LigtherTransformerMetrics { get; set; } = new List<LigtherTransformerMetric>();
}
