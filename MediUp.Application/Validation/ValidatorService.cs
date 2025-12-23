using FluentValidation;
using FluentValidation.Results;
using MediUp.Application.Interfaces;
using MediUp.Domain.Dtos;
using MediUp.Domain.Enums;
using MediUp.Domain.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace MediUp.Application.Validation;
public class ValidatorService(ILoggerFactory loggerFactory, IServiceProvider serviceProvider) : IValidatorService
{
    protected readonly ILogger<ValidatorService> Logger = loggerFactory.CreateLogger<ValidatorService>();
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public EmptyResultDto Validate<TRequest>(TRequest dto, string callerMemberName = "")
    {
        Check.NotNull(dto, nameof(dto));

        Type type = dto!.GetType();
        Logger.LogInformation($"{callerMemberName}: Validating request of type = {type}...");
        IValidator<TRequest>? validator = _serviceProvider.GetService<IValidator<TRequest>>() ?? throw new InvalidOperationException($"Did you forget to add a validator for = {type} ?");

        Logger.LogInformation($"{callerMemberName}: Validating request dto = {{@dto}}", dto);
        ValidationResult validationResult = validator.Validate(dto);
        if (validationResult.IsValid)
        {
            return EmptyResult.Success();
        }

        var errors = validationResult.Errors
        .GroupBy(e => e.PropertyName, e => new { e.ErrorMessage, e.ErrorCode })
        .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray())
        .SelectMany(e => e.Value)
        .ToArray();

        AppMessageType? errorCode = (AppMessageType?)errors.Where(s => s.ErrorCode.StartsWith("BBO_")).Select(s => Enum.Parse(typeof(AppMessageType), s.ErrorCode.Remove(0, 4))).FirstOrDefault();

        string errorMsg = $"{Environment.NewLine}Errors: {string.Join(Environment.NewLine, errors.Select(s => s.ErrorMessage))}";
        Logger.LogInformation($"{callerMemberName}: Validation failed with error = {errorMsg}");
        return errorCode.HasValue ? EmptyResult.InvalidRequest(errorCode.Value, errorMsg) : EmptyResult.InvalidRequest(errorMsg);
    }

    public async Task<EmptyResultDto> ValidateAsync<TRequest>(TRequest dto, string callerMemberName = "")
    {
        Check.NotNull(dto, nameof(dto));

        Type type = dto!.GetType();
        Logger.LogInformation($"{callerMemberName}: Validating request of type = {type}...");
        IValidator<TRequest>? validator = _serviceProvider.GetService<IValidator<TRequest>>()
            ?? throw new InvalidOperationException($"Did you forget to add a validator for = {type} ?");

        Logger.LogInformation($"{callerMemberName}: Validating request dto = {{@dto}}", dto);
        ValidationResult validationResult = await validator.ValidateAsync(dto);
        if (validationResult.IsValid)
        {
            return EmptyResult.Success();
        }

        var errors = validationResult.Errors
            .GroupBy(e => e.PropertyName, e => new { e.ErrorMessage, e.ErrorCode })
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray())
            .SelectMany(e => e.Value)
            .ToArray();

        AppMessageType? errorCode = (AppMessageType?)errors
            .Where(s => s.ErrorCode.StartsWith("BBO_"))
            .Select(s => Enum.Parse(typeof(AppMessageType), s.ErrorCode.Remove(0, 4)))
            .FirstOrDefault();

        string errorMsg = $"{Environment.NewLine}Errors: {string.Join(Environment.NewLine, errors.Select(s => s.ErrorMessage))}";
        Logger.LogInformation($"{callerMemberName}: Validation failed with error = {errorMsg}");
        return errorCode.HasValue ? EmptyResult.InvalidRequest(errorCode.Value, errorMsg) : EmptyResult.InvalidRequest(errorMsg);
    }
}
