namespace Codacy.Api.Models;

/// <summary>
/// Repository summary information
/// </summary>
public class RepositorySummary
{
	/// <summary>Codacy repository identifier</summary>
	public long? RepositoryId { get; set; }

	/// <summary>Git provider</summary>
	public Provider? Provider { get; set; }

	/// <summary>Organization owner name</summary>
	public string? Owner { get; set; }

	/// <summary>Repository name</summary>
	public string? Name { get; set; }
}

/// <summary>
/// Full repository information
/// </summary>
public class Repository : RepositorySummary
{
	/// <summary>Full repository path</summary>
	public string? FullPath { get; set; }

	/// <summary>Repository visibility</summary>
	public Visibility? Visibility { get; set; }

	/// <summary>Remote identifier on Git provider</summary>
	public string? RemoteIdentifier { get; set; }

	/// <summary>Last updated timestamp</summary>
	public DateTimeOffset? LastUpdated { get; set; }

	/// <summary>User permission level</summary>
	public Permission? Permission { get; set; }

	/// <summary>Repository problems</summary>
	public List<RepositoryProblem>? Problems { get; set; }

	/// <summary>Programming languages</summary>
	public List<string>? Languages { get; set; }

	/// <summary>Default branch</summary>
	public Branch? DefaultBranch { get; set; }

	/// <summary>Repository badges</summary>
	public Badges? Badges { get; set; }

	/// <summary>Coding standards</summary>
	public List<CodingStandardInfo>? Standards { get; set; }

	/// <summary>Added state</summary>
	public AddedState? AddedState { get; set; }

	/// <summary>Gate policy ID</summary>
	public long? GatePolicyId { get; set; }

	/// <summary>Gate policy name</summary>
	public string? GatePolicyName { get; set; }
}

/// <summary>
/// Repository problem
/// </summary>
public class RepositoryProblem
{
	/// <summary>Problem message</summary>
	public required string Message { get; set; }

	/// <summary>Problem actions</summary>
	public List<ProblemLink>? Actions { get; set; }

	/// <summary>Problem code</summary>
	public required string Code { get; set; }

	/// <summary>Problem severity</summary>
	public required string Severity { get; set; }
}

/// <summary>
/// Problem link
/// </summary>
public class ProblemLink
{
	/// <summary>Link name</summary>
	public required string Name { get; set; }

	/// <summary>Link URL</summary>
	public required string Url { get; set; }
}

/// <summary>
/// Branch information
/// </summary>
public class Branch
{
	/// <summary>Branch ID</summary>
	public required long Id { get; set; }

	/// <summary>Branch name</summary>
	public required string Name { get; set; }

	/// <summary>Is default branch</summary>
	public required bool IsDefault { get; set; }

	/// <summary>Is enabled for analysis</summary>
	public required bool IsEnabled { get; set; }

	/// <summary>Last updated</summary>
	public DateTimeOffset? LastUpdated { get; set; }

	/// <summary>Branch type</summary>
	public required string BranchType { get; set; }

	/// <summary>Last commit SHA</summary>
	public string? LastCommit { get; set; }
}

/// <summary>
/// Repository badges
/// </summary>
public class Badges
{
	/// <summary>Grade badge URL</summary>
	public required string Grade { get; set; }

	/// <summary>Coverage badge URL</summary>
	public required string Coverage { get; set; }
}

/// <summary>
/// Repository added state
/// </summary>
public enum AddedState
{
	/// <summary>Repository not added</summary>
	NotAdded,
	/// <summary>Repository added</summary>
	Added,
	/// <summary>Repository being followed</summary>
	Following
}

/// <summary>
/// Repository list response
/// </summary>
public class RepositoryListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>List of repositories</summary>
	public required List<Repository> Data { get; set; }
}
