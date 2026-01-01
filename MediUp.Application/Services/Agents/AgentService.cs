using AutoMapper;
using MediUp.Application.Interfaces;
using MediUp.Domain.Constants;
using MediUp.Domain.Dtos;
using MediUp.Domain.Dtos.Identity.User.Requests;
using MediUp.Domain.Entities;
using MediUp.Domain.Enums;
using MediUp.Domain.Enums.Permissions.FrontOffice;
using MediUp.Domain.Interfaces.Identity;
using MediUp.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace MediUp.Application.Services.Agents;

public class AgentService(
    IAppDataService appDataService,
    ILogger<AgentService> logger,
    IMapper mapper,
    IValidatorService validatorService,
    IIdendityUserApiService identityUserApiService) : IAgentService
{
    private readonly IAppDataService _appDataService = appDataService;
    private readonly ILogger<AgentService> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IValidatorService _validatorService = validatorService;
    private readonly IIdendityUserApiService _identityUserApiService = identityUserApiService;

    public async Task<ResultDto<AgentResponseDto>> CreateAsync(CreateAgentRequestDto request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        _logger.LogInformation("CreateAgent: request received for email {Email}", request.Email);

        var validationResult = _validatorService.Validate(request);
        if (!validationResult.Succeed)
        {
            _logger.LogWarning(
                "CreateAgent: validation failed for email {Email}: {Message}",
                request.Email,
                validationResult.Message);
            return Result.FromOther<AgentResponseDto>(validationResult);
        }

        if (request.ElectricCompanyId <= 0)
        {
            _logger.LogWarning("CreateAgent: missing electric company for email {Email}", request.Email);
            return Result.InvalidRequest<AgentResponseDto>("Electric company id is required for the agent.");
        }

        bool companyExists = await _appDataService.ElectriCompany.ExistsById(request.ElectricCompanyId);
        if (!companyExists)
        {
            _logger.LogWarning(
                "CreateAgent: electric company {ElectricCompanyId} not found for email {Email}",
                request.ElectricCompanyId,
                request.Email);
            return Result.NotFound<AgentResponseDto>($"Electric company with id = {request.ElectricCompanyId} was not found");
        }

        bool emailExists = await _appDataService.Agent.ExistsAsync(agent =>
            agent.Email == request.Email && agent.ElectricCompanyId == request.ElectricCompanyId);
        if (emailExists)
        {
            _logger.LogWarning(
                "CreateAgent: email {Email} already exists for company {ElectricCompanyId}",
                request.Email,
                request.ElectricCompanyId);
            return Result.Fail<AgentResponseDto>(AppMessageType.ISResourceAlreadyExists, $"An agent with email '{request.Email}' already exists.");
        }

        var identityRequest = new CreateUserRequestDto
        {
            Username = request.Email,
            Email = request.Email,
            Name = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.Phone,
            Password = "Dev#2017",
            IdentityDocument = request.Email,
            AgentPermission = AgentPermissionType.All,
            RoleId = AppConstants.RoleAgentId
        };

        _logger.LogInformation("CreateAgent: creating identity user for email {Email}", request.Email);
        var identityResult = await _identityUserApiService.CreateUser(identityRequest);
        if (!identityResult.Succeed || identityResult.Result is null)
        {
            _logger.LogWarning("CreateAgent: failed to create identity user for email {Email}", request.Email);
            return Result.FromOther<AgentResponseDto>(identityResult);
        }

        var claimResult = await AddFrontOfficeClaimsAsync(identityResult.Result.Id);
        if (!claimResult.Succeed)
        {
            return Result.FromOther<AgentResponseDto>(claimResult);
        }

        var entity = _mapper.Map<Agent>(request);
        entity.ElectricCompanyId = request.ElectricCompanyId;
        entity.IdentityUserId = identityResult.Result.Id;

        _logger.LogInformation("CreateAgent: persisting agent for email {Email}", request.Email);
        _appDataService.Agent.Add(entity);
        await _appDataService.SaveChangesAsync();

        var response = _mapper.Map<AgentResponseDto>(entity);

        _logger.LogInformation("CreateAgent: agent {Id} created successfully", entity.Id);

        return Result.Success(response);
    }

    private async Task<EmptyResultDto> AddFrontOfficeClaimsAsync(long userId)
    {
        var frontOfficeClaims = new List<AddUserClaimRequestDto>
        {
            new()
            {
                ClaimType = AppConstants.MeterPermissionsClaim,
                ClaimValue = ((long)MeterPermissionType.All).ToString()
            },
            new()
            {
                ClaimType = AppConstants.EnergyMeasurementDownloadPermissionsClaim,
                ClaimValue = ((long)EnergyMeasurementDownloadPermissionType.All).ToString()
            },
            new()
            {
                ClaimType = AppConstants.DashboardPermissionsClaim,
                ClaimValue = ((long)DashboardPermissionType.All).ToString()
            },
            new()
            {
                ClaimType = AppConstants.AgentPermissionsClaim,
                ClaimValue = ((long)AgentPermissionType.All).ToString()
            }
        };

        foreach (var claim in frontOfficeClaims)
        {
            _logger.LogInformation(
                "CreateAgent: adding claim {ClaimType} for identity user {UserId}",
                claim.ClaimType,
                userId);
            var claimResult = await _identityUserApiService.AddUserClaim(userId, claim);
            if (!claimResult.Succeed)
            {
                _logger.LogWarning(
                    "CreateAgent: failed to add claim {ClaimType} for identity user {UserId}",
                    claim.ClaimType,
                    userId);
                return EmptyResult.FromOther(claimResult);
            }
        }

        return EmptyResult.Success();
    }
}
