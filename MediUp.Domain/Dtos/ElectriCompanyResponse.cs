namespace MediUp.Domain.Dtos;

public class ElectriCompanyResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string TaxId { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }
}
