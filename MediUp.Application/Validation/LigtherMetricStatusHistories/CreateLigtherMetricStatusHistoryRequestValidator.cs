using FluentValidation;
using MediUp.Domain.Dtos;

namespace MediUp.Application.Validation.LigtherMetricStatusHistories;

public class CreateLigtherMetricStatusHistoryRequestValidator : AbstractValidator<CreateLigtherMetricStatusHistoryRequest>
{
    public CreateLigtherMetricStatusHistoryRequestValidator()
    {
        RuleFor(request => request.User)
            .NotEmpty();

        RuleFor(request => request.Status)
            .NotEmpty();

        RuleFor(request => request.Date)
            .NotEmpty();
    }
}
