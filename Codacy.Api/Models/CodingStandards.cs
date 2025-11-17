namespace Codacy.Api.Models;

// ===== Coding Standards Models =====

/// <summary>
/// Coding standards list response
/// </summary>
public class CodingStandardsListResponse
{
	/// <summary>Coding standards</summary>
	public required List<CodingStandard> Data { get; set; }
}

/// <summary>
/// Coding standard response
/// </summary>
public class CodingStandardResponse
{
	/// <summary>Coding standard data</summary>
	public required CodingStandard Data { get; set; }
}

/// <summary>
/// Coding standard
/// </summary>
public class CodingStandard
{
	/// <summary>Coding standard ID</summary>
	public required long Id { get; set; }

	/// <summary>Name</summary>
	public required string Name { get; set; }

	/// <summary>Description</summary>
	public string? Description { get; set; }

	/// <summary>Is default</summary>
	public bool? IsDefault { get; set; }

	/// <summary>Is draft</summary>
	public bool? IsDraft { get; set; }

	/// <summary>Created at</summary>
	public DateTimeOffset? CreatedAt { get; set; }

	/// <summary>Updated at</summary>
	public DateTimeOffset? UpdatedAt { get; set; }

	/// <summary>Created by</summary>
	public string? CreatedBy { get; set; }

	/// <summary>Number of repositories using this standard</summary>
	public int? RepositoryCount { get; set; }

	/// <summary>Languages covered</summary>
	public List<string>? Languages { get; set; }
}

/// <summary>
/// Create coding standard body
/// </summary>
public class CreateCodingStandardBody
{
	/// <summary>Coding standard name</summary>
	public required string Name { get; set; }

	/// <summary>Description</summary>
	public string? Description { get; set; }

	/// <summary>Create as draft</summary>
	public bool? IsDraft { get; set; }
}

/// <summary>
/// Create coding standard from preset body
/// </summary>
public class CreateCodingStandardPresetBody
{
	/// <summary>Coding standard name</summary>
	public required string Name { get; set; }

	/// <summary>Description</summary>
	public string? Description { get; set; }

	/// <summary>Preset name (e.g., "recommended", "strict")</summary>
	public required string PresetName { get; set; }

	/// <summary>Languages to apply preset to</summary>
	public required List<string> Languages { get; set; }
}

/// <summary>
/// Set default coding standard body
/// </summary>
public class SetDefaultCodingStandardBody
{
	/// <summary>Apply to all repositories</summary>
	public bool? ApplyToAllRepositories { get; set; }

	/// <summary>Specific repository IDs to apply to</summary>
	public List<long>? RepositoryIds { get; set; }
}

/// <summary>
/// Coding standard tools list response
/// </summary>
public class CodingStandardToolsListResponse
{
	/// <summary>Tools</summary>
	public required List<CodingStandardTool> Data { get; set; }
}

/// <summary>
/// Coding standard tool
/// </summary>
public class CodingStandardTool
{
	/// <summary>Tool UUID</summary>
	public required string Uuid { get; set; }

	/// <summary>Tool name</summary>
	public required string Name { get; set; }

	/// <summary>Is enabled</summary>
	public required bool IsEnabled { get; set; }

	/// <summary>Language</summary>
	public required string Language { get; set; }

	/// <summary>Total patterns</summary>
	public required int TotalPatterns { get; set; }

	/// <summary>Enabled patterns count</summary>
	public required int EnabledPatterns { get; set; }
}

/// <summary>
/// Configured patterns list response
/// </summary>
public class ConfiguredPatternsListResponse
{
	/// <summary>Patterns</summary>
	public required List<ConfiguredPattern> Data { get; set; }

	/// <summary>Pagination</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Configured pattern
/// </summary>
public class ConfiguredPattern
{
	/// <summary>Pattern ID</summary>
	public required string PatternId { get; set; }

	/// <summary>Title</summary>
	public required string Title { get; set; }

	/// <summary>Description</summary>
	public string? Description { get; set; }

	/// <summary>Category</summary>
	public required string Category { get; set; }

	/// <summary>Severity level</summary>
	public required SeverityLevel Level { get; set; }

	/// <summary>Languages</summary>
	public required List<string> Languages { get; set; }

	/// <summary>Is enabled</summary>
	public required bool IsEnabled { get; set; }

	/// <summary>Is recommended</summary>
	public required bool IsRecommended { get; set; }

	/// <summary>Parameters</summary>
	public Dictionary<string, object>? Parameters { get; set; }

	/// <summary>Tags</summary>
	public List<string>? Tags { get; set; }
}

/// <summary>
/// Update patterns body
/// </summary>
public class UpdatePatternsBody
{
	/// <summary>Action (enable/disable)</summary>
	public required string Action { get; set; }

	/// <summary>Pattern IDs to update</summary>
	public List<string>? PatternIds { get; set; }

	/// <summary>Update all patterns matching filters</summary>
	public bool? UpdateAllMatchingFilters { get; set; }
}

/// <summary>
/// Tool configuration
/// </summary>
public class ToolConfiguration
{
	/// <summary>Is enabled</summary>
	public bool? IsEnabled { get; set; }

	/// <summary>Use configuration file</summary>
	public bool? UseConfigurationFile { get; set; }
}

/// <summary>
/// Coding standard repositories list response
/// </summary>
public class CodingStandardRepositoriesListResponse
{
	/// <summary>Repositories</summary>
	public required List<CodingStandardRepository> Data { get; set; }

	/// <summary>Pagination</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Coding standard repository
/// </summary>
public class CodingStandardRepository
{
	/// <summary>Repository ID</summary>
	public required long RepositoryId { get; set; }

	/// <summary>Repository name</summary>
	public required string RepositoryName { get; set; }

	/// <summary>Provider</summary>
	public required Provider Provider { get; set; }

	/// <summary>Organization name</summary>
	public required string OrganizationName { get; set; }

	/// <summary>Applied at</summary>
	public required DateTimeOffset AppliedAt { get; set; }
}

/// <summary>
/// Apply coding standard to repositories body
/// </summary>
public class ApplyCodingStandardToRepositoriesBody
{
	/// <summary>Repository IDs to apply standard to</summary>
	public required List<long> RepositoryIds { get; set; }

	/// <summary>Force apply (override existing standards)</summary>
	public bool? ForceApply { get; set; }
}

/// <summary>
/// Apply coding standard to repositories result response
/// </summary>
public class ApplyCodingStandardToRepositoriesResultResponse
{
	/// <summary>Result data</summary>
	public required ApplyCodingStandardResult Data { get; set; }
}

/// <summary>
/// Apply coding standard result
/// </summary>
public class ApplyCodingStandardResult
{
	/// <summary>Total repositories</summary>
	public required int TotalRepositories { get; set; }

	/// <summary>Successfully applied count</summary>
	public required int SuccessCount { get; set; }

	/// <summary>Failed count</summary>
	public required int FailedCount { get; set; }

	/// <summary>Skipped count</summary>
	public required int SkippedCount { get; set; }

	/// <summary>Detailed results</summary>
	public required List<RepositoryApplicationResult> Results { get; set; }
}

/// <summary>
/// Repository application result
/// </summary>
public class RepositoryApplicationResult
{
	/// <summary>Repository ID</summary>
	public required long RepositoryId { get; set; }

	/// <summary>Repository name</summary>
	public required string RepositoryName { get; set; }

	/// <summary>Status (success/failed/skipped)</summary>
	public required string Status { get; set; }

	/// <summary>Error message (if failed)</summary>
	public string? ErrorMessage { get; set; }
}
