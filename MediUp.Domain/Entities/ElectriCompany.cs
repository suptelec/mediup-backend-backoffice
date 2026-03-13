namespace MediUp.Domain.Entities;
public class ElectriCompany : BaseEntity
{

    public string Name { get; set; } = null!;
    public string TaxId { get; set; } = null!;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContractNumber { get; set; }
    public virtual ICollection<SystemLigther> SystemLigthers { get; set; } = new List<SystemLigther>();
    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
    public virtual ICollection<LogbookDetail> LogbookDetails { get; set; } = new List<LogbookDetail>();
}
