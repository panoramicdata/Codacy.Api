namespace Codacy.Api.Models;

/// <summary>
/// Default settings for Git provider integrations
/// </summary>
public class ProviderIntegrationSettingsPatchBody
{
	/// <summary>Toggle the feature "Status checks"</summary>
	public bool? CommitStatus { get; set; }

	/// <summary>Toggle the feature "Issue annotations"</summary>
	public bool? PullRequestComment { get; set; }

	/// <summary>Toggle the feature "Issue summaries"</summary>
	public bool? PullRequestSummary { get; set; }

	/// <summary>Toggle the feature "Coverage summary" (GitHub only)</summary>
	public bool? CoverageSummary { get; set; }

	/// <summary>Toggle the feature "Suggested fixes" (GitHub only)</summary>
	public bool? Suggestions { get; set; }

	/// <summary>Toggle the feature "AI-enhanced comments"</summary>
	public bool? AiEnhancedComments { get; set; }

	/// <summary>Toggle the feature "AI Pull Request Reviewer" (GitHub only)</summary>
	public bool? AiPullRequestReviewer { get; set; }

	/// <summary>Toggle the feature "AI Pull Request Reviewer Automatic" (GitHub only)</summary>
	public bool? AiPullRequestReviewerAutomatic { get; set; }

	/// <summary>Toggle the feature "Pull Request Unified Summary" (GitHub only)</summary>
	public bool? PullRequestUnifiedSummary { get; set; }
}
