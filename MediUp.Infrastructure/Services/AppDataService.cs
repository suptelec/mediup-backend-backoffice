using MediUp.Domain.Interfaces.Repositories;
using MediUp.Domain.Interfaces.Services;
using MediUp.Infrastructure.Persistence;
using MediUp.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Services;
public class AppDataService(AppDbContext dbContext) : IAppDataService, IDisposable
{
    private bool _disposed = false;

    public IElectriCompanyRepository ElectriCompany { get; } = new ElectriCompanyRepository(dbContext);






    #region Properties


    #endregion

    public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }

        _disposed = true;
    }

}
