using MediUp.Domain.Enums.Permissions;

namespace MediUp.Domain.Dtos;

public class CreateAgentRequest
{
    public long IdentityUserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public AgentPermissionType Permission { get; set; }

    public long ElectricCompanyId { get; set; }
}
