using FluentValidation;
using MediUp.Domain.Dtos;
using MediUp.Domain.Enums.Permissions;

namespace MediUp.Application.Validation.Agents;

public class CreateAgentRequestValidator : AbstractValidator<CreateAgentRequestDto>
{
    public CreateAgentRequestValidator()
    {
        RuleFor(request => request.FirstName)
            .NotEmpty();

        RuleFor(request => request.LastName)
            .NotEmpty();

        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(request => request.Phone)
            .Matches(@"^\+?[0-9\s\-()]+$")
            .When(request => !string.IsNullOrWhiteSpace(request.Phone));
       

        RuleFor(request => request.ElectricCompanyId)
            .GreaterThan(0);
    }
}
