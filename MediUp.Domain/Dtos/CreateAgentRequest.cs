using MediUp.Domain.Enums.Permissions;

namespace MediUp.Domain.Dtos;

public class CreateAgentRequestDto
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public long ElectricCompanyId { get; set; }
}
