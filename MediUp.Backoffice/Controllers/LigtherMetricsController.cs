using MediUp.Application.Services.LigtherMetrics;
using MediUp.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace MediUp.Backoffice.Controllers;

public class LigtherMetricsController(ILoggerFactory loggerFactory, ILigtherMetricService ligtherMetricService) : BaseController(loggerFactory)
{
    private readonly ILigtherMetricService _ligtherMetricService = ligtherMetricService;

    /// <summary>
    /// Creates a new ligthmeter by delegating work to the service and repository layer.
    /// </summary>
    /// <param name="request">Payload containing the ligthmeter details.</param>
    /// <returns>A <see cref="ResultDto{T}"/> indicating the operation result.</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultDto<LigtherMetricResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<LigtherMetricResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateLigtherMetricRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Request received to create ligthmeter with serial {Serial}.", request.Serial);

        var result = await _ligtherMetricService.CreateAsync(request, cancellationToken);

        if (result.Succeed)
        {
            Logger.LogInformation("Ligthmeter created successfully with id {Id}.", result.Result?.Id);
        }
        else
        {
            Logger.LogWarning("Failed to create ligthmeter with serial {Serial}. Error: {Error}", request.Serial, result.Message);
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves all ligthmeters.
    /// </summary>
    /// <returns>A <see cref="ResultDto{T}"/> containing the list of ligthmeters.</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultDto<IEnumerable<LigtherMetricResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<IEnumerable<LigtherMetricResponse>>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Request received to retrieve all ligthmeters.");

        var result = await _ligtherMetricService.GetAllAsync(cancellationToken);

        if (result.Succeed)
        {
            Logger.LogInformation("Retrieved {Count} ligthmeters successfully.", result.Result?.Count() ?? 0);
        }
        else
        {
            Logger.LogWarning("Failed to retrieve ligthmeters. Error: {Error}", result.Message);
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves a ligthmeter by id.
    /// </summary>
    /// <param name="id">Ligthmeter id.</param>
    /// <returns>A <see cref="ResultDto{T}"/> containing the ligthmeter.</returns>
    [HttpGet("{id:long}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultDto<LigtherMetricResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<LigtherMetricResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<LigtherMetricResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Request received to retrieve ligthmeter {Id}.", id);

        var result = await _ligtherMetricService.GetByIdAsync(id, cancellationToken);

        if (result.Succeed)
        {
            Logger.LogInformation("Retrieved ligthmeter {Id} successfully.", id);
        }
        else
        {
            Logger.LogWarning("Failed to retrieve ligthmeter {Id}. Error: {Error}", id, result.Message);
        }

        return HandleResult(result);
    }
}
