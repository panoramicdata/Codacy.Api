using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

// ===== Coding Standards Models =====

/// <summary>
/// Coding standards list response
/// </summary>
public class CodingStandardsListResponse
{
	/// <summary>Coding standards</summary>
	[JsonPropertyName("data")]
	public required List<CodingStandard> Data { get; set; }
}

/// <summary>
/// Coding standard response
/// </summary>
public class CodingStandardResponse
{
	/// <summary>Coding standard data</summary>
	[JsonPropertyName("data")]
	public required CodingStandard Data { get; set; }
}

/// <summary>
/// Coding standard
/// </summary>
public class CodingStandard
{
	/// <summary>Coding standard ID</summary>
	[JsonPropertyName("id")]
	public required long Id { get; set; }

	/// <summary>Name</summary>
	[JsonPropertyName("name")]
	public required string Name { get; set; }

	/// <summary>Description</summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>Is default</summary>
	[JsonPropertyName("isDefault")]
	public bool? IsDefault { get; set; }

	/// <summary>Is draft</summary>
	[JsonPropertyName("isDraft")]
	public bool? IsDraft { get; set; }

	/// <summary>Created at</summary>
	[JsonPropertyName("createdAt")]
	public DateTimeOffset? CreatedAt { get; set; }

	/// <summary>Updated at</summary>
	[JsonPropertyName("updatedAt")]
	public DateTimeOffset? UpdatedAt { get; set; }

	/// <summary>Created by</summary>
	[JsonPropertyName("createdBy")]
	public string? CreatedBy { get; set; }

	/// <summary>Number of repositories using this standard</summary>
	[JsonPropertyName("repositoryCount")]
	public int? RepositoryCount { get; set; }

	/// <summary>Languages covered</summary>
	[JsonPropertyName("languages")]
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
	[JsonPropertyName("data")]
	public required List<CodingStandardTool> Data { get; set; }
}

/// <summary>
/// Coding standard tool
/// </summary>
public class CodingStandardTool
{
	/// <summary>Tool UUID</summary>
	[JsonPropertyName("uuid")]
	public string? Uuid { get; set; }

	/// <summary>Tool name</summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>Is enabled</summary>
	[JsonPropertyName("isEnabled")]
	public bool? IsEnabled { get; set; }

	/// <summary>Language</summary>
	[JsonPropertyName("language")]
	public string? Language { get; set; }

	/// <summary>Total patterns (camelCase)</summary>
	[JsonPropertyName("totalPatterns")]
	public int? TotalPatterns { get; set; }

	/// <summary>Enabled patterns count (camelCase)</summary>
	[JsonPropertyName("enabledPatterns")]
	public int? EnabledPatterns { get; set; }

	// Additional properties that might be returned by the API
	// Based on Codacy API patterns, tools might have nested structure

	/// <summary>Tool information (nested object)</summary>
	[JsonPropertyName("tool")]
	public CodingStandardToolInfo? Tool { get; set; }

	/// <summary>Number of patterns</summary>
	[JsonPropertyName("numberOfPatterns")]
	public int? NumberOfPatterns { get; set; }

	/// <summary>Number of enabled patterns</summary>
	[JsonPropertyName("numberOfEnabledPatterns")]
	public int? NumberOfEnabledPatterns { get; set; }
}

/// <summary>
/// Tool information (for nested structure)
/// </summary>
public class CodingStandardToolInfo
{
	/// <summary>Tool UUID</summary>
	[JsonPropertyName("uuid")]
	public string? Uuid { get; set; }

	/// <summary>Tool name</summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }
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
	[JsonPropertyName("patternId")]
	public string? PatternId { get; set; }

	/// <summary>Title</summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>Description</summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>Category</summary>
	[JsonPropertyName("category")]
	public string? Category { get; set; }

	/// <summary>Severity level</summary>
	[JsonPropertyName("level")]
	public SeverityLevel? Level { get; set; }

	/// <summary>Languages</summary>
	[JsonPropertyName("languages")]
	public List<string>? Languages { get; set; }

	/// <summary>Is enabled</summary>
	[JsonPropertyName("isEnabled")]
	public bool? IsEnabled { get; set; }

	/// <summary>Is recommended</summary>
	[JsonPropertyName("isRecommended")]
	public bool? IsRecommended { get; set; }

	/// <summary>Parameters - using JsonElement for flexible JSON structure</summary>
	[JsonPropertyName("parameters")]
	public JsonElement? Parameters { get; set; }

	/// <summary>Tags</summary>
	[JsonPropertyName("tags")]
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
	[JsonPropertyName("data")]
	public required List<CodingStandardRepository> Data { get; set; }

	/// <summary>Pagination</summary>
	[JsonPropertyName("pagination")]
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Coding standard repository
/// </summary>
public class CodingStandardRepository
{
	/// <summary>Repository ID</summary>
	[JsonPropertyName("repositoryId")]
	public required long RepositoryId { get; set; }

	/// <summary>Repository name</summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }
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
