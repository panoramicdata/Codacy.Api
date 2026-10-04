namespace Codacy.Api.Models;

/// <summary>
/// Metrics that are ready for each organization in an enterprise
/// </summary>
public class ReadyMetricsForEnterpriseResponse
{
	/// <summary>Ready metrics per organization</summary>
	public required List<EnterpriseReadyMetrics> Data { get; set; }
}

/// <summary>
/// Ready metrics for an organization in an enterprise
/// </summary>
public class EnterpriseReadyMetrics
{
	/// <summary>Organization identifier</summary>
	public required long OrganizationId { get; set; }

	/// <summary>Organization name</summary>
	public required string OrganizationName { get; set; }

	/// <summary>Names of the metrics that are ready</summary>
	public required List<string> ReadyMetrics { get; set; }

	/// <summary>When data collection started</summary>
	public DateTimeOffset? StartedAt { get; set; }
}
