using AutoMapper;
using MediUp.Domain.Dtos;
using MediUp.Domain.Entities;
using MediUp.Domain.Enums;
using MediUp.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;


namespace MediUp.Application.Services.ElectriCompanies;
public class ElectriCompanyService(IAppDataService appDataService, ILogger<ElectriCompanyService> logger, IMapper mapper) : IElectriCompanyService
{
    private readonly IAppDataService _appDataService = appDataService;
    private readonly ILogger<ElectriCompanyService> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<ResultDto<ElectriCompany>> CreateAsync(CreateElectriCompanyRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.TaxId))
        {
            return Result.InvalidRequest<ElectriCompany>("The electric company must have a name and tax id.");
        }

        if (await _appDataService.ElectriCompany.ExistsAsync(company => company.TaxId == request.TaxId))
        {
            return Result.InvalidRequest<ElectriCompany>("An electric company with the provided tax id already exists.");
        }

        try
        {
            var electriCompany = _mapper.Map<ElectriCompany>(request);

            electriCompany.CreatedAt = DateTime.UtcNow;
            electriCompany.CreatedBy = string.IsNullOrWhiteSpace(electriCompany.CreatedBy) ? "backoffice" : electriCompany.CreatedBy;

            _appDataService.ElectriCompany.Add(electriCompany);
            await _appDataService.SaveChangesAsync();

            _logger.LogInformation("Electric company {Name} ({TaxId}) created successfully with id {Id}.", electriCompany.Name, electriCompany.TaxId, electriCompany.Id);

            return Result.Success(electriCompany);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unexpected error while creating electric company {Name} ({TaxId}).", request.Name, request.TaxId);
            return Result.Fail<ElectriCompany>(AppMessageType.UnknownError, "An unexpected error occurred while creating the electric company.");
        }
    }
}
