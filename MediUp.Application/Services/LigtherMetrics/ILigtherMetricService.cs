using MediUp.Domain.Dtos;

namespace MediUp.Application.Services.LigtherMetrics;

public interface ILigtherMetricService
{
    Task<ResultDto<LigtherMetricResponse>> CreateAsync(CreateLigtherMetricRequest request, CancellationToken cancellationToken = default);
    Task<ResultDto<IEnumerable<LigtherMetricResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ResultDto<LigtherMetricResponse>> GetByIdAsync(long id, CancellationToken cancellationToken = default);
}
