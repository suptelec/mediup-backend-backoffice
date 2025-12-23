using MediUp.Application.Services.ElectriCompanies;
using MediUp.Domain.Dtos;
using MediUp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediUp.Backoffice.Controllers;

public class ElectriCompanyController(ILoggerFactory loggerFactory, IElectriCompanyService electriCompanyService) : BaseController(loggerFactory)
{
    private readonly IElectriCompanyService _electriCompanyService = electriCompanyService;

    /// <summary>
    /// Creates a new electric company by delegating work to the service and repository layer.
    /// </summary>
    /// <param name="request">Payload containing the electric company details.</param>
    /// <returns>A <see cref="ResultDto{T}"/> indicating the operation result.</returns>
    /// <remarks>
    /// This endpoint logs the flow so we can trace the request through the controller.
    /// It also keeps the controller thin by delegating validation and persistence to the service layer.
    /// </remarks>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultDto<ElectriCompany>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<ElectriCompany>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateElectriCompanyRequest request, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Request received to create electric company with tax id {TaxId}.", request.TaxId);

        // Validate incoming payload to provide an informative error before hitting the service.
        if (!ModelState.IsValid)
        {
            var validationMessage = new StringBuilder("The electric company payload is invalid.");
            foreach (var state in ModelState.Values.SelectMany(v => v.Errors))
            {
                validationMessage.Append(' ').Append(state.ErrorMessage);
            }

            Logger.LogWarning("Validation failed when creating electric company with tax id {TaxId}: {Errors}", request.TaxId, validationMessage.ToString());

            var invalidResult = Result.InvalidRequest<ElectriCompany>(validationMessage.ToString());
            return HandleResult(invalidResult);
        }

        var result = await _electriCompanyService.CreateAsync(request, cancellationToken);

        if (result.Succeed)
        {
            Logger.LogInformation("Electric company created successfully with id {Id}.", result.Result?.Id);
        }
        else
        {
            Logger.LogWarning("Failed to create electric company with tax id {TaxId}. Error: {Error}", request.TaxId, result.Message);
        }

        return HandleResult(result);
    }

    /// <summary>
    /// Retrieves all electric companies.
    /// </summary>
    /// <returns>A <see cref="ResultDto{T}"/> containing the list of electric companies.</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ResultDto<IEnumerable<ElectriCompanyResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto<IEnumerable<ElectriCompanyResponse>>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Request received to retrieve all electric companies.");

        var result = await _electriCompanyService.GetAllAsync(cancellationToken);

        if (result.Succeed)
        {
            Logger.LogInformation("Retrieved {Count} electric companies successfully.", result.Result?.Count() ?? 0);
        }
        else
        {
            Logger.LogWarning("Failed to retrieve electric companies. Error: {Error}", result.Message);
        }

        return HandleResult(result);
    }
}
