using MediUp.Application.Interfaces;
using MediUp.Application.Services.Agents;
using MediUp.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace MediUp.Backoffice.Controllers;

public class AgentsController(ILoggerFactory loggerFactory, IAgentService agentService, IValidatorService validatorService) : BaseController(loggerFactory)
{
    private readonly IAgentService _agentService = agentService;
    private readonly IValidatorService _validatorService = validatorService;

    /// <summary>
    /// Creates a new agent and assigns them to an electric company.
    /// </summary>
    /// <param name="request">Payload containing the agent details.</param>
    /// <returns>A <see cref="ResultDto{T}"/> indicating the operation result.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultDto<AgentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<AgentResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAgentRequestDto request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Request received to create agent with email {Email}.", request.Email);

        var validationResult = await _validatorService.ValidateAsync(request);
        if (!validationResult.Succeed)
        {
            Logger.LogWarning("Validation failed when creating agent with email {Email}: {Errors}", request.Email, validationResult.Message);
            return HandleResult(Result.FromOther<AgentResponse>(validationResult));
        }

        var result = await _agentService.CreateAsync(request, cancellationToken);

        if (result.Succeed)
        {
            Logger.LogInformation("Agent created successfully with id {Id}.", result.Result?.Id);
        }
        else
        {
            Logger.LogWarning("Failed to create agent with email {Email}. Error: {Error}", request.Email, result.Message);
        }

        return HandleResult(result);
    }
}
