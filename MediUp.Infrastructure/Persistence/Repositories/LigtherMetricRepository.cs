using MediUp.Domain.Entities;
using MediUp.Domain.Interfaces.Repositories;

namespace MediUp.Infrastructure.Persistence.Repositories;

public class LigtherMetricRepository(AppDbContext context) : RepositoryBase<LigtherMetric, AppDbContext>(context), ILigtherMetricRepository
{
}
