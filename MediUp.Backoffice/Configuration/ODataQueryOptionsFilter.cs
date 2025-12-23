using Microsoft.AspNetCore.OData.Query;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MediUp.Backoffice.Configuration;

public class ODataQueryOptionsFilter : IOperationFilter
{
    /// <summary>
    /// Applies the OData query options to the Swagger operation.
    /// </summary>
    /// <param name="operation">The Swagger operation.</param>
    /// <param name="context">The operation filter context.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        bool hasODataQueryOptions = context.MethodInfo
           .GetParameters()
           .Any(p => p.ParameterType.IsGenericType &&
                     p.ParameterType.GetGenericTypeDefinition() == typeof(ODataQueryOptions<>));

        if (!hasODataQueryOptions)
            return;

        // Agregar parámetros OData solo si el endpoint usa ODataQueryOptions<T>
        operation.Parameters ??= [];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$filter",
            In = ParameterLocation.Query,
            Description = "Filter the results. Example: createdAt ge 2025-04-31T00:00:00Z",
            Schema = new OpenApiSchema { Type = "string" },
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$orderby",
            In = ParameterLocation.Query,
            Description = "Order the results. Example: createdAt desc",
            Schema = new OpenApiSchema { Type = "string" },
            Example = new OpenApiString("createdAt desc")
        });


    }
}