using FluentValidation;
using MediUp.Domain.Dtos;

namespace MediUp.Application.Validation.LigtherTransformers;

public class CreateLigtherTransformerRequestValidator : AbstractValidator<CreateLigtherTransformerRequest>
{
    public CreateLigtherTransformerRequestValidator()
    {
        RuleFor(request => request.Serial)
            .NotEmpty();

        RuleFor(request => request.ElectricCompanyId)
            .GreaterThan(0);

        RuleFor(request => request.PrimaryCurrent)
            .GreaterThan(0);

        RuleFor(request => request.SecondaryCurrent)
            .GreaterThan(0);

        RuleFor(request => request.PrimaryVoltage)
            .GreaterThan(0);

        RuleFor(request => request.SecondaryVoltage)
            .GreaterThan(0);

        RuleFor(request => request.NextCalibrationDate)
            .GreaterThanOrEqualTo(request => request.LastCalibrationDate)
            .When(request => request.LastCalibrationDate.HasValue && request.NextCalibrationDate.HasValue)
            .WithMessage("The next calibration date must be after the last calibration date.");
    }
}
