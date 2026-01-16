using MediUp.Application.Services.LigtherTransformers;
using MediUp.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace MediUp.Backoffice.Controllers;

public class LigtherTransformersController(ILoggerFactory loggerFactory, ILigtherTransformerService ligtherTransformerService) : BaseController(loggerFactory)
{
    private readonly ILigtherTransformerService _ligtherTransformerService = ligtherTransformerService;

    /// <summary>
    /// Creates a new transformer by delegating work to the service and repository layer.
    /// </summary>
    /// <param name="request">Payload containing the transformer details.</param>
    /// <returns>A <see cref="ResultDto{T}"/> indicating the operation result.</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultDto<LigtherTransformerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<LigtherTransformerResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateLigtherTransformerRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Request received to create transformer with serial {Serial}.", request.Serial);

        var result = await _ligtherTransformerService.CreateAsync(request, cancellationToken);

        if (result.Succeed)
        {
            Logger.LogInformation("Transformer created successfully with id {Id}.", result.Result?.Id);
        }
        else
        {
            Logger.LogWarning("Failed to create transformer with serial {Serial}. Error: {Error}", request.Serial, result.Message);
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves all transformers.
    /// </summary>
    /// <returns>A <see cref="ResultDto{T}"/> containing the list of transformers.</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultDto<IEnumerable<LigtherTransformerResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<IEnumerable<LigtherTransformerResponse>>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Request received to retrieve all transformers.");

        var result = await _ligtherTransformerService.GetAllAsync(cancellationToken);

        if (result.Succeed)
        {
            Logger.LogInformation("Retrieved {Count} transformers successfully.", result.Result?.Count() ?? 0);
        }
        else
        {
            Logger.LogWarning("Failed to retrieve transformers. Error: {Error}", result.Message);
        }

        return HandleResult(result);
    }
}
