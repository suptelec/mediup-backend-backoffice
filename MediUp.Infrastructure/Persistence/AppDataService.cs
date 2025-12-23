using MediUp.Domain.Interfaces.Repositories;
using MediUp.Domain.Interfaces.Services;

namespace MediUp.Infrastructure.Persistence;

public class AppDataService : IAppDataService
{
    private readonly AppDbContext _context;

    public AppDataService(AppDbContext context, IElectriCompanyRepository electriCompanyRepository)
    {
        _context = context;
        ElectriCompany = electriCompanyRepository;
    }

    public IElectriCompanyRepository ElectriCompany { get; }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();

    public void Dispose()
    {
        _context.Dispose();
    }
}
