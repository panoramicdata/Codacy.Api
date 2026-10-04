namespace Codacy.Api.Models;

/// <summary>
/// Request for metric values over a time range
/// </summary>
public class TimerangeMetricFilterBody
{
	/// <summary>Metric filter</summary>
	public required MetricFilter Filter { get; set; }

	/// <summary>Grouping options</summary>
	public required MetricGroupBy GroupBy { get; set; }

	/// <summary>Start of the time range</summary>
	public required DateTimeOffset From { get; set; }

	/// <summary>End of the time range</summary>
	public required DateTimeOffset To { get; set; }

	/// <summary>Time granularity; if omitted, the backend chooses a default</summary>
	public MetricPeriod? Period { get; set; }
}
