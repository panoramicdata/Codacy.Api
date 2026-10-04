namespace Codacy.Api.Models;

/// <summary>
/// Request for an enterprise metric value for a specific period
/// </summary>
public class EnterprisePeriodMetricFilterBody
{
	/// <summary>Metric filter</summary>
	public required EnterpriseMetricFilter Filter { get; set; }

	/// <summary>Start date of the period</summary>
	public required DateTimeOffset Date { get; set; }

	/// <summary>Period granularity</summary>
	public required MetricPeriod Period { get; set; }
}
