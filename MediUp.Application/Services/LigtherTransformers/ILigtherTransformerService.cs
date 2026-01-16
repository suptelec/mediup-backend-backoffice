using MediUp.Domain.Dtos;

namespace MediUp.Application.Services.LigtherTransformers;

public interface ILigtherTransformerService
{
    Task<ResultDto<LigtherTransformerResponse>> CreateAsync(CreateLigtherTransformerRequest request, CancellationToken cancellationToken = default);

    Task<ResultDto<IEnumerable<LigtherTransformerResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
}
