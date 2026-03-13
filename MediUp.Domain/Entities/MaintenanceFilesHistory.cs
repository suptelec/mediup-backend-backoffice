namespace MediUp.Domain.Entities;
public class MaintenanceFilesHistory: BaseEntity
{
    public int MonthNumber { get; set; }
    public int WorkOrderhNumber { get; set; }
    public int Year { get; set; }
    public string SystemLigtherName { get; set; } = null!;
    public string HtmlContent { get; set; } = null!;
    public string MaintenanceType { get; set; } = null!;
    public string? ElectricCompanyName { get; set; }


}
