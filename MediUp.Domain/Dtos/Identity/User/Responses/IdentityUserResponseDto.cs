using MediUp.Domain.Enums;
using MediUp.Domain.Enums.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Dtos.Identity.User.Responses;
public class IdentityUserResponseDto
{
    public long Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public UserStatusType Status { get; set; }
    public UserType Type { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string? ProfileImagePath { get; set; }

    public GlobalConfigPermissionType GlobalPermission { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool EmailConfirmed { get; set; }
}
