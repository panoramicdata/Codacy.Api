using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for tool and pattern catalogue API operations
/// </summary>
public interface IToolsApi
{
	/// <summary>
	/// List the languages supported by available tools
	/// </summary>
	[Get("/api/v3/languages/tools")]
	Task<LanguageListResponse> ListLanguagesWithToolsAsync(
		CancellationToken cancellationToken);

	/// <summary>
	/// List the tools
	/// </summary>
	[Get("/api/v3/tools")]
	Task<ToolListResponse> ListToolsAsync(
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List the patterns of a tool
	/// </summary>
	[Get("/api/v3/tools/{toolUuid}/patterns")]
	Task<PatternListResponse> ListPatternsAsync(
		string toolUuid,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] bool? enabled,
		CancellationToken cancellationToken);

	/// <summary>
	/// Add feedback relating to a tool pattern
	/// </summary>
	[Post("/api/v3/tools/{toolUuid}/patterns/{patternId}/organizations/{provider}/{organizationName}/feedback")]
	Task AddPatternFeedbackAsync(
		string toolUuid,
		string patternId,
		Provider provider,
		string organizationName,
		[Body] AddEnrichedPatternFeedbackBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get a tool pattern
	/// </summary>
	[Get("/api/v3/tools/{toolUuid}/patterns/{patternId}")]
	Task<PatternResponse> GetPatternAsync(
		string toolUuid,
		string patternId,
		CancellationToken cancellationToken);

	/// <summary>
	/// List the duplication tools
	/// </summary>
	[Get("/api/v3/duplicationTools")]
	Task<DuplicationToolListResponse> ListDuplicationToolsAsync(
		CancellationToken cancellationToken);

	/// <summary>
	/// List the metrics tools
	/// </summary>
	[Get("/api/v3/metricsTools")]
	Task<MetricsToolListResponse> ListMetricsToolsAsync(
		CancellationToken cancellationToken);
}
