using MediUp.Domain.Dtos;

namespace MediUp.Application.Services.LigtherMetricStatusHistories;

public interface ILigtherMetricStatusHistoryService
{
    Task<ResultDto<IEnumerable<LigtherMetricStatusHistoryResponse>>> CreateAsync(
        CreateLigtherMetricStatusHistoryRequest request,
        CancellationToken cancellationToken = default);
}
