using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for analysis action API operations (recovery, autoconfig, reanalysis, AI review, quick fixes)
/// </summary>
public interface IAnalysisActionsApi
{
	/// <summary>
	/// Recover a stuck repository
	/// </summary>
	/// <remarks>
	/// Triggers a recovery action when the repository is stuck on its first analysis.
	/// Codacy returns 422 when the repository is not stuck.
	/// </remarks>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/recover")]
	Task RecoverRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch,
		CancellationToken cancellationToken);

	/// <summary>
	/// Add a repository autoconfiguration run
	/// </summary>
	/// <remarks>
	/// Experimental autoconfig enqueuing endpoint.
	/// </remarks>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/autoconfig")]
	Task<AddAutoconfigResponse> AddAutoconfigAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the latest autoconfig run summary
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/autoconfig/runs")]
	Task<AutoconfigRunSummaryResponse> GetLatestAutoconfigRunSummaryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the autoconfig run status
	/// </summary>
	/// <remarks>
	/// Experimental. Fetches the latest autoconfig run state (queued, running, successful or failed).
	/// </remarks>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/autoconfig/status")]
	Task<AutoconfigStatusResponse> GetAutoconfigStatusAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Reanalyze the coverage for a commit
	/// </summary>
	/// <remarks>
	/// Triggers the reanalysis of the latest coverage report uploaded for the commit.
	/// Has no effect if the commit does not have any coverage report.
	/// </remarks>
	[Post("/api/v3/coverage/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}/reanalyze")]
	Task ReanalyzeCommitCoverageAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string commitUuid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Trigger an AI review for a pull request
	/// </summary>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/ai-reviewer/trigger")]
	Task TriggerPullRequestAiReviewAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		CancellationToken cancellationToken);

	/// <summary>
	/// Ignore the false positive result in an issue
	/// </summary>
	[Patch("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/{issueId}/false-positive/ignore")]
	Task IgnoreFalsePositiveAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string issueId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Reanalyze a specific commit in a repository
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/reanalyzeCommit")]
	Task ReanalyzeCommitByIdAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] CommitUuidRequest body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Check if the repository has quick fix suggestions for a branch
	/// </summary>
	/// <remarks>
	/// Experimental. If branch is not provided, the default branch is used.
	/// </remarks>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/hasSuggestions")]
	Task<HasQuickfixSuggestionsResponse> HasQuickfixSuggestionsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get quick fixes for issues in patch format
	/// </summary>
	/// <remarks>
	/// Experimental. If branch is not provided, the default branch is used.
	/// </remarks>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/patch")]
	Task<QuickfixPatchResponse> GetQuickfixesPatchAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get quick fixes for pull request issues in patch format
	/// </summary>
	/// <remarks>
	/// Experimental.
	/// </remarks>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/issues/patch")]
	Task<QuickfixPatchResponse> GetPullRequestQuickfixesPatchAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the patterns overview for a coding standard tool
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/coding-standards/{codingStandardId}/tools/{toolUuid}/patterns/overview")]
	Task<ToolPatternsOverviewResponse> GetCodingStandardToolPatternsOverviewAsync(
		Provider provider,
		string organizationName,
		long codingStandardId,
		string toolUuid,
		[Query] string? languages,
		[Query] string? categories,
		[Query] string? severityLevels,
		[Query] string? tags,
		[Query] string? search,
		[Query] bool? enabled,
		[Query] bool? recommended,
		CancellationToken cancellationToken);
}
