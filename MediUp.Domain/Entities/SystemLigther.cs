namespace MediUp.Domain.Entities;
public class SystemLigther : BaseEntity
{
    public string Code { get; set; } = null!;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? UrlAuthorizationDocument { get; set; }
    public long ElectricCompanyId { get; set; }

    public virtual ElectriCompany ElectricCompany { get; set; } = null!;
    public virtual ICollection<LigtherMetric> LigtherMetrics { get; set; } = new List<LigtherMetric>();
    public virtual ICollection<LigtherTransformer> LigtherTransformers { get; set; } = new List<LigtherTransformer>();
}

