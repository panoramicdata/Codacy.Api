namespace Codacy.Api.Models;

/// <summary>
/// Response containing metric values grouped by dimension
/// </summary>
public class PeriodGroupedMetricValuesResponse
{
	/// <summary>Grouped metric values</summary>
	public required List<GroupedMetricValue> Data { get; set; }
}

/// <summary>
/// Value of a metric for a group
/// </summary>
public class GroupedMetricValue
{
	/// <summary>Group</summary>
	public required MetricGroup Group { get; set; }

	/// <summary>Value</summary>
	public required double Value { get; set; }

	/// <summary>Latest value</summary>
	public double? LatestValue { get; set; }
}
