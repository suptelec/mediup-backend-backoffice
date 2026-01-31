namespace MediUp.Domain.Entities;
public class MaintenanceFilesHistory: BaseEntity
{

    public string HtmlContent { get; set; } = null!;

    public string MaintenanceType { get; set; } = null!;

    public string? ElectricCompanyName { get; set; }


}
