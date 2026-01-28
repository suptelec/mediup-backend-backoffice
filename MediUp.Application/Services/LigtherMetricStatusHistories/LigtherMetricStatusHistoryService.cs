using AutoMapper;
using MediUp.Application.Interfaces;
using MediUp.Domain.Dtos;
using MediUp.Domain.Entities;
using MediUp.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace MediUp.Application.Services.LigtherMetricStatusHistories;

public class LigtherMetricStatusHistoryService(
    IAppDataService appDataService,
    ILogger<LigtherMetricStatusHistoryService> logger,
    IMapper mapper,
    IValidatorService validatorService) : ILigtherMetricStatusHistoryService
{
    private readonly IAppDataService _appDataService = appDataService;
    private readonly ILogger<LigtherMetricStatusHistoryService> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IValidatorService _validatorService = validatorService;

    public async Task<ResultDto<IEnumerable<LigtherMetricStatusHistoryResponse>>> CreateAsync(
        CreateLigtherMetricStatusHistoryRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "CreateLigtherMetricStatusHistory: request received for user {User} at {Date}",
            request.User,
            request.Date);

        var validationResult = await _validatorService.ValidateAsync(request);
        if (!validationResult.Succeed)
        {
            _logger.LogWarning(
                "CreateLigtherMetricStatusHistory: validation failed for user {User}: {Message}",
                request.User,
                validationResult.Message);
            return Result.FromOther<IEnumerable<LigtherMetricStatusHistoryResponse>>(validationResult);
        }

        if (!TryParseMeasuredAt(request.Date, out var measuredAt))
        {
            _logger.LogWarning(
                "CreateLigtherMetricStatusHistory: invalid date {Date} for user {User}",
                request.Date,
                request.User);
            return Result.InvalidRequest<IEnumerable<LigtherMetricStatusHistoryResponse>>(
                $"Date '{request.Date}' is invalid. Expected format: yyyy-dd-MM HH:mm:ss.");
        }

        _logger.LogInformation(
            "CreateLigtherMetricStatusHistory: persisting {Count} status history entries for user {User}",
            request.Status.Count,
            request.User);

        var entities = request.Status.Select(entry => new LigtherMetricStatusHistory
        {
            Code = entry.Key,
            Status = entry.Value,
            ElectricCompanyName = request.User,
            MeasuredAt = measuredAt
        }).ToList();

        _appDataService.LigtherMetricStatusHistory.AddRange(entities);
        await _appDataService.SaveChangesAsync();

        var response = entities.Select(_mapper.Map<LigtherMetricStatusHistoryResponse>).ToList();

        _logger.LogInformation(
            "CreateLigtherMetricStatusHistory: created {Count} status history entries successfully",
            response.Count);

        return Result.Success(response);
    }

    private static bool TryParseMeasuredAt(string dateValue, out DateTime measuredAt)
    {
        if (DateTime.TryParseExact(
                dateValue,
                "yyyy-dd-MM HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out measuredAt))
        {
            return true;
        }

        return DateTime.TryParse(dateValue, out measuredAt);
    }
}
