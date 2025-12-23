using MediUp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Dtos.Identity.User.Requests;
public class CreateUserRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName
        => $"{Name.Trim()} {LastName.Trim()}";
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public UserType Type { get; set; }
    public string IdentityDocument { get; set; } = string.Empty;

}
