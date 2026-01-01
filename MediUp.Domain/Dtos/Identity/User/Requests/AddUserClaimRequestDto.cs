namespace MediUp.Domain.Dtos.Identity.User.Requests;
public class AddUserClaimRequestDto
{
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}

