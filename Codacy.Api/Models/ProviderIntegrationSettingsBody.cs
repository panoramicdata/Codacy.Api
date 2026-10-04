namespace Codacy.Api.Models;

/// <summary>
/// Default settings for Git provider integrations with a list of available settings
/// </summary>
public class ProviderIntegrationSettingsBody
{
	/// <summary>Toggle the feature "Status checks"</summary>
	public required bool CommitStatus { get; set; }

	/// <summary>Toggle the feature "Issue annotations"</summary>
	public required bool PullRequestComment { get; set; }

	/// <summary>Toggle the feature "Issue summaries"</summary>
	public required bool PullRequestSummary { get; set; }

	/// <summary>Toggle the feature "Coverage summary" (GitHub only)</summary>
	public bool? CoverageSummary { get; set; }

	/// <summary>Toggle the feature "Suggested fixes" (GitHub only)</summary>
	public bool? Suggestions { get; set; }

	/// <summary>Toggle the feature "AI-enhanced comments"</summary>
	public required bool AiEnhancedComments { get; set; }

	/// <summary>Toggle the feature "AI Pull Request Reviewer" (GitHub only)</summary>
	public bool? AiPullRequestReviewer { get; set; }

	/// <summary>Toggle the feature "AI Pull Request Reviewer Automatic" (GitHub only)</summary>
	public bool? AiPullRequestReviewerAutomatic { get; set; }

	/// <summary>Toggle the feature "Pull Request Unified Summary" (GitHub only)</summary>
	public bool? PullRequestUnifiedSummary { get; set; }

	/// <summary>List of available settings for the Git provider integration</summary>
	public required List<string> AvailableSettings { get; set; }
}
