namespace Codacy.Api.Models;

/// <summary>
/// Payload of a <c>quality.analysis.completed</c> webhook delivery, sent when a branch or pull
/// request analysis finishes. Read one with <see cref="CodacyWebhook.Deserialize"/>.
/// </summary>
public class WebhookAnalysisCompleted
{
	/// <summary>The event name, <see cref="CodacyWebhook.AnalysisCompletedEvent"/></summary>
	public required string Event { get; set; }

	/// <summary>The repository that was analyzed</summary>
	public required WebhookRepository Repository { get; set; }

	/// <summary>The organization that owns the repository</summary>
	public required WebhookOrganization Organization { get; set; }

	/// <summary>The branch or pull request that was analyzed</summary>
	public required WebhookTarget Target { get; set; }

	/// <summary>The commit that was analyzed. Use it to deduplicate reanalysis of the same commit.</summary>
	public required string CommitSha { get; set; }

	/// <summary>Outcome of the analysis</summary>
	public required WebhookAnalysisStatus Status { get; set; }

	/// <summary>When the analysis completed. Identical across delivery retries.</summary>
	public required DateTimeOffset Timestamp { get; set; }
}

/// <summary>Repository named in a webhook delivery</summary>
public class WebhookRepository
{
	/// <summary>Repository name</summary>
	public required string Name { get; set; }
}

/// <summary>Organization named in a webhook delivery</summary>
public class WebhookOrganization
{
	/// <summary>Codacy organization identifier</summary>
	public required long Id { get; set; }
}

/// <summary>Branch or pull request named in a webhook delivery</summary>
public class WebhookTarget
{
	/// <summary>Whether this is a branch or a pull request</summary>
	public required WebhookTargetType Type { get; set; }

	/// <summary>The branch name, or the pull request number as a string</summary>
	public required string Value { get; set; }
}
