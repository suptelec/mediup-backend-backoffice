namespace MediUp.Domain.Entities;

public class LigtherTransformerMetric : BaseEntity
{
    public long LigtherTransformerId { get; set; }
    public long LigtherMetricId { get; set; }

    public virtual LigtherTransformer LigtherTransformer { get; set; } = null!;
    public virtual LigtherMetric LigtherMetric { get; set; } = null!;
}
