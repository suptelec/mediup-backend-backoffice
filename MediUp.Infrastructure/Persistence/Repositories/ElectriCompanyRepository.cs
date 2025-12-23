using MediUp.Domain.Entities;
using MediUp.Domain.Interfaces.Repositories;

namespace MediUp.Infrastructure.Persistence.Repositories;
public class ElectriCompanyRepository(AppDbContext context) : RepositoryBase<ElectriCompany, AppDbContext>(context), IElectriCompanyRepository
{
}
