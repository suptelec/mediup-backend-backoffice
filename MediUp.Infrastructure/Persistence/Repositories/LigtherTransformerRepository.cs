using MediUp.Domain.Entities;
using MediUp.Domain.Interfaces.Repositories;

namespace MediUp.Infrastructure.Persistence.Repositories;

public class LigtherTransformerRepository(AppDbContext context) : RepositoryBase<LigtherTransformer, AppDbContext>(context), ILigtherTransformerRepository
{
}
