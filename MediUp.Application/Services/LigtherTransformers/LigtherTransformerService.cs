using AutoMapper;
using MediUp.Application.Interfaces;
using MediUp.Domain.Dtos;
using MediUp.Domain.Entities;
using MediUp.Domain.Enums;
using MediUp.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace MediUp.Application.Services.LigtherTransformers;

public class LigtherTransformerService(
    IAppDataService appDataService,
    ILogger<LigtherTransformerService> logger,
    IMapper mapper,
    IValidatorService validatorService) : ILigtherTransformerService
{
    private readonly IAppDataService _appDataService = appDataService;
    private readonly ILogger<LigtherTransformerService> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IValidatorService _validatorService = validatorService;

    public async Task<ResultDto<LigtherTransformerResponse>> CreateAsync(CreateLigtherTransformerRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("CreateLigtherTransformer: request received for serial {Serial}", request.Serial);

        var validationResult = await _validatorService.ValidateAsync(request);
        if (!validationResult.Succeed)
        {
            _logger.LogWarning(
                "CreateLigtherTransformer: validation failed for serial {Serial}: {Message}",
                request.Serial,
                validationResult.Message);
            return Result.FromOther<LigtherTransformerResponse>(validationResult);
        }

        bool companyExists = await _appDataService.ElectriCompany.ExistsById(request.ElectricCompanyId);
        if (!companyExists)
        {
            _logger.LogWarning(
                "CreateLigtherTransformer: electric company {ElectricCompanyId} not found for serial {Serial}",
                request.ElectricCompanyId,
                request.Serial);
            return Result.NotFound<LigtherTransformerResponse>($"Electric company with id = {request.ElectricCompanyId} was not found");
        }

        bool serialExists = await _appDataService.LigtherTransformer.ExistsAsync(transformer =>
            transformer.Serial == request.Serial && transformer.ElectricCompanyId == request.ElectricCompanyId);
        if (serialExists)
        {
            _logger.LogWarning(
                "CreateLigtherTransformer: serial {Serial} already exists for company {ElectricCompanyId}",
                request.Serial,
                request.ElectricCompanyId);
            return Result.Fail<LigtherTransformerResponse>(AppMessageType.ISResourceAlreadyExists, $"A transformer with serial '{request.Serial}' already exists.");
        }

        var entity = _mapper.Map<LigtherTransformer>(request);

        _logger.LogInformation("CreateLigtherTransformer: persisting transformer for serial {Serial}", request.Serial);
        _appDataService.LigtherTransformer.Add(entity);
        await _appDataService.SaveChangesAsync();

        var response = _mapper.Map<LigtherTransformerResponse>(entity);

        _logger.LogInformation("CreateLigtherTransformer: transformer {Id} created successfully", entity.Id);

        return Result.Success(response);
    }

    public async Task<ResultDto<IEnumerable<LigtherTransformerResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var transformers = await _appDataService.LigtherTransformer.GetAllAsync();
        var response = transformers.Select(_mapper.Map<LigtherTransformerResponse>).ToList();

        _logger.LogInformation("Retrieved {Count} transformers.", response.Count);

        return Result.Success<IEnumerable<LigtherTransformerResponse>>(response);
    }
}
