namespace Codacy.Api.Models;

/// <summary>
/// Request to join an organization
/// </summary>
public class RequestToJoin
{
	/// <summary>Email</summary>
	public required string Email { get; set; }

	/// <summary>Name</summary>
	public required string Name { get; set; }

	/// <summary>Number of commits</summary>
	public int? NumberOfCommits { get; set; }

	/// <summary>Number of repositories</summary>
	public int? NumberOfRepositories { get; set; }

	/// <summary>Last activity</summary>
	public DateTimeOffset? LastActivity { get; set; }

	/// <summary>Creation date</summary>
	public required DateTimeOffset CreationDate { get; set; }
}
