namespace Codacy.Api.Models;

/// <summary>
/// Response containing a metric value
/// </summary>
public class MetricValueResponse
{
	/// <summary>Metric value</summary>
	public required MetricValue Data { get; set; }
}

/// <summary>
/// Value of a metric
/// </summary>
public class MetricValue
{
	/// <summary>Value</summary>
	public required double Value { get; set; }

	/// <summary>Latest value</summary>
	public double? LatestValue { get; set; }
}
