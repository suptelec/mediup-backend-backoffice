using System.ComponentModel.DataAnnotations;

namespace MediUp.Domain.Dtos;

public class CreateElectriCompanyRequest
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string TaxId { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    [EmailAddress]
    public string? ContactEmail { get; set; }

    [Phone]
    public string? ContactPhone { get; set; }

    public string? CreatedBy { get; set; }
}
