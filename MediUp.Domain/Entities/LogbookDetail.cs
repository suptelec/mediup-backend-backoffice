using MediUp.Domain.Enums;

namespace MediUp.Domain.Entities;
public class LogbookDetail: BaseEntity
{
    public int ReportNumber { get; set; }
    public int MonthNumber { get; set; }
    public long? SystemLigtherId { get; set; }
    public long? LigtherMetricId { get; set; }
    public string? Code { get; set; }
    public LogbookDetailType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public DateTime OccuredAt { get; set; }
    public long ElectricCompanyId { get; set; }
    public virtual ElectriCompany ElectricCompany { get; set; } = null!;

}
