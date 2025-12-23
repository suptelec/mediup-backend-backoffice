using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MediUp.Backoffice.Configuration;

public class SwaggerIgnoreParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            return;

        var ignoredParams = context.MethodInfo.GetParameters()
            .Where(p => p.GetCustomAttributes(typeof(SwaggerIgnoreAttribute), false).Length != 0)
            .Select(p => p.Name)
            .ToList();

        operation.Parameters = [.. operation.Parameters.Where(p => !ignoredParams.Contains(p.Name, StringComparer.OrdinalIgnoreCase))];
    }
}
