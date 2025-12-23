using MediUp.Domain.Entities;
using MediUp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Persistence.Repositories;
internal class ElectriCompanyRepository(AppDbContext context) : RepositoryBase<ElectriCompany, AppDbContext>(context), IElectriCompanyRepository
{
}
