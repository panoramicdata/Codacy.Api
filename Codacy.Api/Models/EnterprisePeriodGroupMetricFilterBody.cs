namespace Codacy.Api.Models;

/// <summary>
/// Request for enterprise metric values for a specific period grouped by dimension
/// </summary>
public class EnterprisePeriodGroupMetricFilterBody
{
	/// <summary>Metric filter</summary>
	public required EnterpriseMetricFilter Filter { get; set; }

	/// <summary>Grouping options</summary>
	public required MetricGroupBy GroupBy { get; set; }

	/// <summary>Start date of the period</summary>
	public required DateTimeOffset Date { get; set; }

	/// <summary>Period granularity</summary>
	public required MetricPeriod Period { get; set; }
}
