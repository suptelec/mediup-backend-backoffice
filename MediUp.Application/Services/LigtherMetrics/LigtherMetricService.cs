using AutoMapper;
using MediUp.Application.Interfaces;
using MediUp.Domain.Dtos;
using MediUp.Domain.Entities;
using MediUp.Domain.Enums;
using MediUp.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace MediUp.Application.Services.LigtherMetrics;

public class LigtherMetricService(
    IAppDataService appDataService,
    ILogger<LigtherMetricService> logger,
    IMapper mapper,
    IValidatorService validatorService) : ILigtherMetricService
{
    private readonly IAppDataService _appDataService = appDataService;
    private readonly ILogger<LigtherMetricService> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IValidatorService _validatorService = validatorService;

    public async Task<ResultDto<LigtherMetricResponse>> CreateAsync(CreateLigtherMetricRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("CreateLigtherMetric: request received for serial {Serial}", request.Serial);

        var validationResult = await _validatorService.ValidateAsync(request);
        if (!validationResult.Succeed)
        {
            _logger.LogWarning(
                "CreateLigtherMetric: validation failed for serial {Serial}: {Message}",
                request.Serial,
                validationResult.Message);
            return Result.FromOther<LigtherMetricResponse>(validationResult);
        }

        bool companyExists = await _appDataService.ElectriCompany.ExistsById(request.ElectricCompanyId);
        if (!companyExists)
        {
            _logger.LogWarning(
                "CreateLigtherMetric: electric company {ElectricCompanyId} not found for serial {Serial}",
                request.ElectricCompanyId,
                request.Serial);
            return Result.NotFound<LigtherMetricResponse>($"Electric company with id = {request.ElectricCompanyId} was not found");
        }

        bool serialExists = await _appDataService.LigtherMetric.ExistsAsync(metric =>
            metric.Serial == request.Serial && metric.ElectricCompanyId == request.ElectricCompanyId);
        if (serialExists)
        {
            _logger.LogWarning(
                "CreateLigtherMetric: serial {Serial} already exists for company {ElectricCompanyId}",
                request.Serial,
                request.ElectricCompanyId);
            return Result.Fail<LigtherMetricResponse>(AppMessageType.ISResourceAlreadyExists, $"A ligthmeter with serial '{request.Serial}' already exists.");
        }

        var entity = _mapper.Map<LigtherMetric>(request);

        _logger.LogInformation("CreateLigtherMetric: persisting ligthmeter for serial {Serial}", request.Serial);
        _appDataService.LigtherMetric.Add(entity);
        await _appDataService.SaveChangesAsync();

        var response = _mapper.Map<LigtherMetricResponse>(entity);

        _logger.LogInformation("CreateLigtherMetric: ligthmeter {Id} created successfully", entity.Id);

        return Result.Success(response);
    }

    public async Task<ResultDto<IEnumerable<LigtherMetricResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var metrics = await _appDataService.LigtherMetric.GetAllAsync();
        var response = metrics.Select(_mapper.Map<LigtherMetricResponse>).ToList();

        _logger.LogInformation("Retrieved {Count} ligthmeters.", response.Count);

        return Result.Success<IEnumerable<LigtherMetricResponse>>(response);
    }

    public async Task<ResultDto<LigtherMetricResponse>> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            _logger.LogWarning("GetLigtherMetricById: invalid id {Id}.", id);
            return Result.InvalidId<LigtherMetricResponse>(id);
        }

        _logger.LogInformation("GetLigtherMetricById: retrieving ligthmeter {Id}.", id);

        var metric = await _appDataService.LigtherMetric.GetByIdAsync(id);

        if (metric is null)
        {
            _logger.LogWarning("GetLigtherMetricById: ligthmeter {Id} not found.", id);
            return Result.NotFound<LigtherMetricResponse>($"Ligthmeter with id = {id} was not found");
        }

        var response = _mapper.Map<LigtherMetricResponse>(metric);

        _logger.LogInformation("GetLigtherMetricById: ligthmeter {Id} retrieved successfully.", id);
        return Result.Success(response);
    }
}
