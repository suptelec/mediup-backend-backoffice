using FluentValidation;
using MediUp.Domain.Dtos;

namespace MediUp.Application.Validation.LigtherMetrics;

public class CreateLigtherMetricRequestValidator : AbstractValidator<CreateLigtherMetricRequest>
{
    public CreateLigtherMetricRequestValidator()
    {
        RuleFor(request => request.Serial)
            .NotEmpty();

        RuleFor(request => request.Codigo)
            .NotEmpty();

        RuleFor(request => request.ElectricCompanyId)
            .GreaterThan(0);

        RuleFor(request => request.ManufacturingYear)
            .GreaterThan(0);

        RuleFor(request => request.NextCalibrationDate)
            .GreaterThanOrEqualTo(request => request.LastCalibrationDate)
            .When(request => request.LastCalibrationDate.HasValue && request.NextCalibrationDate.HasValue)
            .WithMessage("The next calibration date must be after the last calibration date.");

        RuleFor(x => x.PrincipalCode)
          .NotEmpty()
          .When(x => !x.IsPrincipal)
          .WithMessage("PrincipalCode is required when IsPrincipal is false.");
    }
}
