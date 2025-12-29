using MediUp.Domain.Entities;
using MediUp.Domain.Interfaces.Repositories;

namespace MediUp.Infrastructure.Persistence.Repositories;

public class AgentRepository(AppDbContext context) : RepositoryBase<Agent, AppDbContext>(context), IAgentRepository
{
}
