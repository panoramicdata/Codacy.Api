using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for repository tool pattern API operations
/// </summary>
public interface IRepositoryToolPatternsApi
{
	/// <summary>
	/// List the patterns configuration for a repository tool
	/// </summary>
	/// <remarks>
	/// Uses the coding standard if applied, repository settings otherwise.
	/// </remarks>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tools/{toolUuid}/patterns")]
	Task<ToolConfiguredPatternsListResponse> ListRepositoryToolPatternsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string toolUuid,
		[Query] string? languages,
		[Query] string? categories,
		[Query] string? severityLevels,
		[Query] string? tags,
		[Query] string? search,
		[Query] bool? enabled,
		[Query] bool? recommended,
		[Query] bool? matchesStack,
		[Query] string? sort,
		[Query] string? direction,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Bulk update the code patterns of a tool in a repository
	/// </summary>
	/// <remarks>
	/// Use the filters to specify the code patterns to update, or omit them to update all code patterns.
	/// </remarks>
	[Patch("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tools/{toolUuid}/patterns")]
	Task UpdateRepositoryToolPatternsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string toolUuid,
		[Body] UpdateToolPatternsBody body,
		[Query] string? languages,
		[Query] string? categories,
		[Query] string? severityLevels,
		[Query] string? tags,
		[Query] string? search,
		[Query] bool? recommended,
		[Query] bool? matchesStack,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the pattern configuration for a repository tool pattern
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tools/{toolUuid}/patterns/{patternId}")]
	Task<ToolConfiguredPatternResponse> GetRepositoryToolPatternAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string toolUuid,
		string patternId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the patterns overview for a repository tool
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tools/{toolUuid}/patterns/overview")]
	Task<ToolPatternsOverviewResponse> GetToolPatternsOverviewAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string toolUuid,
		[Query] string? languages,
		[Query] string? categories,
		[Query] string? severityLevels,
		[Query] string? tags,
		[Query] string? search,
		[Query] bool? enabled,
		[Query] bool? recommended,
		[Query] bool? matchesStack,
		CancellationToken cancellationToken);

	/// <summary>
	/// List the repository tool patterns that conflict with coding standards
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tools/{toolUuid}/conflicts")]
	Task<RepositoryToolConflictsResponse> ListRepositoryToolPatternConflictsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string toolUuid,
		CancellationToken cancellationToken);
}
