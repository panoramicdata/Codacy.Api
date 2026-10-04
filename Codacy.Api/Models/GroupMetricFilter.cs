namespace Codacy.Api.Models;

/// <summary>
/// Request for the latest metric values grouped by dimension
/// </summary>
public class GroupMetricFilter
{
	/// <summary>Metric filter</summary>
	public required MetricFilter Filter { get; set; }

	/// <summary>Grouping options</summary>
	public required MetricGroupBy GroupBy { get; set; }
}
