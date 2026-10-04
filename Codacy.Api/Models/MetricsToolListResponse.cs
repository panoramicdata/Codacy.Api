namespace Codacy.Api.Models;

/// <summary>
/// List of metrics tools
/// </summary>
public class MetricsToolListResponse
{
	/// <summary>Metrics tools</summary>
	public required List<MetricsTool> Data { get; set; }
}
