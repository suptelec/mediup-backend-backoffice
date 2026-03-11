namespace MediUp.Domain.Entities;
public class MeasurementSystemMaintenanceSchedule : BaseEntity
{
    public string Code { get; set; } = null!; 
    public string InstallationLocation { get; set; } = null!; 
    public int Semester { get; set; }
    public int Spreadsheet { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public DateTime PeriodFrom { get; set; }
    public DateTime PeriodTo { get; set; }
    public long SystemLigtherId { get; set; }
    public virtual SystemLigther SystemLigther { get; set; } = null!;
}
