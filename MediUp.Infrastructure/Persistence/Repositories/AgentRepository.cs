using MediUp.Domain.Entities;
using MediUp.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MediUp.Infrastructure.Persistence.Repositories;

public class AgentRepository(AppDbContext context) : RepositoryBase<Agent, AppDbContext>(context), IAgentRepository
{
    public Task<bool> EmailExistsAsync(string email, long electricCompanyId)
    {
        return _dbSet.AnyAsync(agent => agent.Email == email && agent.ElectricCompanyId == electricCompanyId);
    }
}
