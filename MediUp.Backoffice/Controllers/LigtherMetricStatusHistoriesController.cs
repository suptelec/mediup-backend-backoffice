using MediUp.Application.Services.LigtherMetricStatusHistories;
using MediUp.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Mime;

namespace MediUp.Backoffice.Controllers;

public class LigtherMetricStatusHistoriesController(
    ILoggerFactory loggerFactory,
    ILigtherMetricStatusHistoryService ligtherMetricStatusHistoryService) : BaseController(loggerFactory)
{
    private readonly ILigtherMetricStatusHistoryService _ligtherMetricStatusHistoryService = ligtherMetricStatusHistoryService;

    /// <summary>
    /// Creates a new ligthmeter status history record.
    /// </summary>
    /// <param name="request">Payload containing the status history details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="ResultDto{T}"/> indicating the operation result.</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultDto<IEnumerable<LigtherMetricStatusHistoryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<IEnumerable<LigtherMetricStatusHistoryResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateLigtherMetricStatusHistoryRequest request,
        CancellationToken cancellationToken = default)
    {
        Logger.LogInformation(
            "Request received to create ligthmeter status history for user {User} at {Date}.",
            request.User,
            request.Date);

        var result = await _ligtherMetricStatusHistoryService.CreateAsync(request, cancellationToken);

        if (result.Succeed)
        {
            Logger.LogInformation(
                "Ligthmeter status history created successfully with {Count} entries.",
                result.Result?.Count() ?? 0);
        }
        else
        {
            Logger.LogWarning(
                "Failed to create ligthmeter status history for user {User}. Error: {Error}",
                request.User,
                result.Message);
        }

        return HandleResult(result);
    }
}
