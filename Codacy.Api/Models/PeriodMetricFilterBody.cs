namespace Codacy.Api.Models;

/// <summary>
/// Request for a metric value for a specific period
/// </summary>
public class PeriodMetricFilterBody
{
	/// <summary>Metric filter</summary>
	public required MetricFilter Filter { get; set; }

	/// <summary>Start date of the period</summary>
	public required DateTimeOffset Date { get; set; }

	/// <summary>Period granularity</summary>
	public required MetricPeriod Period { get; set; }
}
