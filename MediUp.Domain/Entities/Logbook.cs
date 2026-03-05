namespace MediUp.Domain.Entities;
public class Logbook : BaseEntity
{
    public int ReportNumber { get; set; }
    public int MonthNumber { get; set; }
    public DateOnly PeriodFrom { get; set; }
    public DateOnly PeriodTo { get; set; }
    public virtual ICollection<LogbookDetail> LogbookDetails { get; set; } = new List<LogbookDetail>();
}
