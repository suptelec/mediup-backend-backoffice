using MediUp.Domain.Entities;

namespace MediUp.Domain.Interfaces.Repositories;

public interface IAgentRepository : IRepositoryBase<Agent>
{
    Task<bool> EmailExistsAsync(string email, long electricCompanyId);
}
