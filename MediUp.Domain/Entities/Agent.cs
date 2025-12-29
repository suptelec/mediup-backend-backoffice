using MediUp.Domain.Enums.Permissions;
using System;

namespace MediUp.Domain.Entities;
public class Agent : BaseEntity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public AgentPermissionType Permission { get; set; }

    public long ElectricCompanyId { get; set; }

    public virtual ElectriCompany ElectricCompany { get; set; } = null!;
}
