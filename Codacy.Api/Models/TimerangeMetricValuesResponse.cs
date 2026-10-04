namespace Codacy.Api.Models;

/// <summary>
/// Response containing metric values over a time range
/// </summary>
public class TimerangeMetricValuesResponse
{
	/// <summary>Metric values per period</summary>
	public required List<TimerangeMetricValue> Data { get; set; }
}

/// <summary>
/// Value of a metric for a period
/// </summary>
public class TimerangeMetricValue
{
	/// <summary>Start date of the period</summary>
	public required DateTimeOffset Date { get; set; }

	/// <summary>Group, when grouping was requested</summary>
	public MetricGroup? Group { get; set; }

	/// <summary>Value</summary>
	public required double Value { get; set; }

	/// <summary>Latest value</summary>
	public double? LatestValue { get; set; }
}
