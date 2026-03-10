using MediUp.Domain.Enums;

namespace MediUp.Domain.Entities;
public class LogbookDetail: BaseEntity
{
    public long SystemLigtherId { get; set; }
    public long LigtherMetricId { get; set; }
    public string Code { get; set; } = string.Empty;
    public LogbookDetailType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public DateTime OccuredAt { get; set; }
    public long LogbookId { get; set; }
    public virtual Logbook Logbook { get; set; } = null!;

}
