using MediUp.Domain.Dtos;
using MediUp.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MediUp.Application.Services.Agents;

public interface IAgentService
{
    Task<ResultDto<AgentResponse>> CreateAsync(CreateAgentRequestDto request, CancellationToken cancellationToken = default);
}
