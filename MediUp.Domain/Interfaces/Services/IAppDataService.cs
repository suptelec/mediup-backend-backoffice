using MediUp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Interfaces.Services;
public interface IAppDataService : IDisposable
{
    IElectriCompanyRepository ElectriCompany { get; }
  

    Task SaveChangesAsync();
}
