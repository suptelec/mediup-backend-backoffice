using MediUp.Domain.Dtos;
using MediUp.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MediUp.Application.Services.ElectriCompanies;
public interface IElectriCompanyService
{
    Task<ResultDto<ElectriCompany>> CreateAsync(CreateElectriCompanyRequest request, CancellationToken cancellationToken = default);
}
