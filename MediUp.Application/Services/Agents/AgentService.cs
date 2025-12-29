using AutoMapper;
using MediUp.Application.Interfaces;
using MediUp.Domain.Dtos;
using MediUp.Domain.Dtos.Identity.User.Requests;
using MediUp.Domain.Entities;
using MediUp.Domain.Enums;
using MediUp.Domain.Interfaces;
using MediUp.Domain.Interfaces.Identity;
using MediUp.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediUp.Application.Services.Agents;

public class AgentService(
    IAppDataService appDataService,
    IIdendityUserApiService identityUserApiService,
    IValidatorService validatorService,
    ICurrentLoggedUser currentLoggedUser,
    ILogger<AgentService> logger,
    IMapper mapper) : IAgentService
{
    private readonly IAppDataService _appDataService = appDataService;
    private readonly IIdendityUserApiService _identityUserApiService = identityUserApiService;
    private readonly IValidatorService _validatorService = validatorService;
    private readonly ICurrentLoggedUser _currentLoggedUser = currentLoggedUser;
    private readonly ILogger<AgentService> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<ResultDto<AgentResponse>> CreateAsync(CreateAgentRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        _logger.LogInformation("CreateAgent: request received for email {Email}", request.Email);

        var validationResult = _validatorService.Validate(request);
        if (!validationResult.Succeed)
        {
            _logger.LogWarning("CreateAgent: validation failed for email {Email}: {Message}", request.Email, validationResult.Message);
            return Result.FromOther<AgentResponse>(validationResult);
        }

        if (_currentLoggedUser.ElectricCompanyId <= 0)
        {
            _logger.LogWarning("CreateAgent: missing electric company for user {User}", _currentLoggedUser.UserName);
            return Result.InvalidRequest<AgentResponse>("Electric company id is required for the current user.");
        }

        bool companyExists = await _appDataService.ElectriCompany.ExistsById(_currentLoggedUser.ElectricCompanyId);
        if (!companyExists)
        {
            _logger.LogWarning(
                "CreateAgent: electric company {ElectricCompanyId} not found for email {Email}",
                _currentLoggedUser.ElectricCompanyId,
                request.Email);
            return Result.NotFound<AgentResponse>($"Electric company with id = {_currentLoggedUser.ElectricCompanyId} was not found");
        }

        bool emailExists = await _appDataService.Agent.EmailExistsAsync(request.Email, _currentLoggedUser.ElectricCompanyId);
        if (emailExists)
        {
            _logger.LogWarning(
                "CreateAgent: email {Email} already exists for company {ElectricCompanyId}",
                request.Email,
                _currentLoggedUser.ElectricCompanyId);
            return Result.InvalidRequest<AgentResponse>($"An agent with email '{request.Email}' already exists");
        }

        try
        {
            var identityRequest = new CreateUserRequestDto
            {
                Username = request.Email,
                Email = request.Email,
                Name = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.Phone,
                Password = "Dev#2017",
                IdentityDocument = request.Email,
                Type = UserType.BackOffice
            };

            _logger.LogInformation("CreateAgent: creating identity user for email {Email}", request.Email);
            var identityResult = await _identityUserApiService.CreateUser(identityRequest);
            if (!identityResult.Succeed || identityResult.Result is null)
            {
                _logger.LogWarning("CreateAgent: failed to create identity user for email {Email}", request.Email);
                return Result.FromOther<AgentResponse>(identityResult);
            }

            var entity = _mapper.Map<Agent>(request);
            entity.ElectricCompanyId = _currentLoggedUser.ElectricCompanyId;
            entity.IdentityUserId = identityResult.Result.Id;

            _logger.LogInformation("CreateAgent: persisting agent for email {Email}", request.Email);
            _appDataService.Agent.Add(entity);
            await _appDataService.SaveChangesAsync();

            var response = _mapper.Map<AgentResponse>(entity);

            _logger.LogInformation("CreateAgent: agent {Id} created successfully", entity.Id);

            return Result.Success(response);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unexpected error while creating agent {Email}.", request.Email);
            return Result.Fail<AgentResponse>(AppMessageType.UnknownError, "An unexpected error occurred while creating the agent.");
        }
    }
}
