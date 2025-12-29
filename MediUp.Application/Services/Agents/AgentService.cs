using AutoMapper;
using MediUp.Domain.Dtos;
using MediUp.Domain.Entities;
using MediUp.Domain.Enums;
using MediUp.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediUp.Application.Services.Agents;

public class AgentService(IAppDataService appDataService, ILogger<AgentService> logger, IMapper mapper) : IAgentService
{
    private readonly IAppDataService _appDataService = appDataService;
    private readonly ILogger<AgentService> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<ResultDto<AgentResponse>> CreateAsync(CreateAgentRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.FirstName)
            || string.IsNullOrWhiteSpace(request.LastName)
            || string.IsNullOrWhiteSpace(request.Email))
        {
            return Result.InvalidRequest<AgentResponse>("The agent must have first name, last name, and email.");
        }

        if (!await _appDataService.ElectriCompany.ExistsById(request.ElectricCompanyId))
        {
            return Result.NotFound<AgentResponse>("The electric company was not found.");
        }

        if (await _appDataService.Agent.ExistsAsync(agent => agent.Email == request.Email))
        {
            return Result.InvalidRequest<AgentResponse>("An agent with the provided email already exists.");
        }

        if (await _appDataService.Agent.ExistsAsync(agent => agent.IdentityUserId == request.IdentityUserId))
        {
            return Result.InvalidRequest<AgentResponse>("An agent with the provided identity user already exists.");
        }

        try
        {
            var entity = _mapper.Map<Agent>(request);

            _appDataService.Agent.Add(entity);
            await _appDataService.SaveChangesAsync();

            var response = _mapper.Map<AgentResponse>(entity);

            _logger.LogInformation("Agent {Email} created successfully with id {Id}.", entity.Email, entity.Id);

            return Result.Success(response);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unexpected error while creating agent {Email}.", request.Email);
            return Result.Fail<AgentResponse>(AppMessageType.UnknownError, "An unexpected error occurred while creating the agent.");
        }
    }
}
