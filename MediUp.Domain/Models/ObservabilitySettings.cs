using MediUp.Domain.Utils;

namespace MediUp.Domain.Models;
public class ObservabilitySettings
{
    public string JaegerEndpoint { get; set; } = string.Empty;
    public string LokiEndpoint { get; set; } = string.Empty;
    public string LokiUser { get; set; } = string.Empty;
    public string LokiApiKey { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public string OtlpHeaders { get; set; } = string.Empty;


    public void CheckSettings()
    {
        Check.NotEmpty(JaegerEndpoint, nameof(JaegerEndpoint));
        Check.NotEmpty(LokiEndpoint, nameof(LokiEndpoint));
        Check.NotEmpty(ServiceName, nameof(ServiceName));
        Check.NotEmpty(Environment, nameof(Environment));

    }
}