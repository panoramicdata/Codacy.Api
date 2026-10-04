namespace Codacy.Api.Models;

/// <summary>
/// Request for the latest metric values grouped by dimension for an enterprise
/// </summary>
public class EnterpriseGroupMetricFilter
{
	/// <summary>Grouping options</summary>
	public required MetricGroupBy GroupBy { get; set; }

	/// <summary>Metric filter</summary>
	public required EnterpriseMetricFilter Filter { get; set; }
}
