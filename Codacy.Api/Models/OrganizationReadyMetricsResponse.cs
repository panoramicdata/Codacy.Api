namespace Codacy.Api.Models;

/// <summary>
/// Metrics that are ready for an organization
/// </summary>
public class OrganizationReadyMetricsResponse
{
	/// <summary>Ready metrics</summary>
	public required OrganizationReadyMetrics Data { get; set; }
}

/// <summary>
/// Ready metrics for an organization
/// </summary>
public class OrganizationReadyMetrics
{
	/// <summary>Organization identifier</summary>
	public required long OrganizationId { get; set; }

	/// <summary>Git provider</summary>
	public required Provider Provider { get; set; }

	/// <summary>The name of the organization to which the results belong</summary>
	public required string OrganizationName { get; set; }

	/// <summary>Names of the metrics that are ready</summary>
	public required List<string> ReadyMetrics { get; set; }

	/// <summary>When data collection started</summary>
	public DateTimeOffset? StartedAt { get; set; }
}
