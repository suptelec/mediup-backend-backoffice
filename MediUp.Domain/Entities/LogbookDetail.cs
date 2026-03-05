using MediUp.Domain.Enums;

namespace MediUp.Domain.Entities;
public class LogbookDetail: BaseEntity
{
    public long LogbookId { get; set; }
    public virtual Logbook Logbook { get; set; } = null!;
    public LogbookDetailType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public int SortOrder { get; set; }

}
