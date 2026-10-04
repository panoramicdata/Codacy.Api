namespace Codacy.Api.Models;

/// <summary>
/// Selects the metrics to start collecting
/// </summary>
public class MetricsFilter
{
	/// <summary>Names of the metrics</summary>
	public required List<string> Metrics { get; set; }
}
