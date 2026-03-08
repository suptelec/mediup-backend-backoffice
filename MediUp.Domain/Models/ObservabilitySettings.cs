namespace MediUp.Domain.Models;
public class ObservabilitySettings
{
    public string JaegerEndpoint { get; set; } = string.Empty;
    public string LokiEndpoint { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public string OtlpHeaders { get; set; } = string.Empty;
}