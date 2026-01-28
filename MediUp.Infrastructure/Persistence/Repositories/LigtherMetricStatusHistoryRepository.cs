using MediUp.Domain.Entities;
using MediUp.Domain.Interfaces.Repositories;

namespace MediUp.Infrastructure.Persistence.Repositories;

public class LigtherMetricStatusHistoryRepository(AppDbContext context) : RepositoryBase<LigtherMetricStatusHistory, AppDbContext>(context), ILigtherMetricStatusHistoryRepository
{
}
