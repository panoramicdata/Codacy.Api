namespace Codacy.Api.Models;

// ===== People Models =====

/// <summary>
/// List people response
/// </summary>
public class ListPeopleResponse
{
	/// <summary>People data</summary>
	public required List<Person> Data { get; set; }

	/// <summary>Pagination</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Person
/// </summary>
public class Person
{
	/// <summary>User ID</summary>
	public required long UserId { get; set; }

	/// <summary>Username</summary>
	public required string Username { get; set; }

	/// <summary>Email</summary>
	public required string Email { get; set; }

	/// <summary>Name</summary>
	public string? Name { get; set; }

	/// <summary>Role</summary>
	public required UserRole Role { get; set; }

	/// <summary>Joined date</summary>
	public required DateTimeOffset Joined { get; set; }

	/// <summary>Is pending</summary>
	public required bool IsPending { get; set; }
}

/// <summary>
/// Remove people body
/// </summary>
public class RemovePeopleBody
{
	/// <summary>User IDs to remove</summary>
	public required List<long> UserIds { get; set; }
}

/// <summary>
/// Remove people response
/// </summary>
public class RemovePeopleResponse
{
	/// <summary>Number of people removed</summary>
	public required int Removed { get; set; }
}

/// <summary>
/// Suggested authors response
/// </summary>
public class SuggestedAuthorsResponse
{
	/// <summary>Suggested authors</summary>
	public required List<SuggestedAuthor> Data { get; set; }

	/// <summary>Pagination</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Repository suggested authors response
/// </summary>
public class RepositorySuggestedAuthorsResponse
{
	/// <summary>Suggested authors</summary>
	public required List<SuggestedAuthor> Data { get; set; }

	/// <summary>Pagination</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Suggested author
/// </summary>
public class SuggestedAuthor
{
	/// <summary>Author name</summary>
	public string? Name { get; set; }

	/// <summary>Author email</summary>
	public string? Email { get; set; }

	/// <summary>Commit count</summary>
	public int? CommitCount { get; set; }
}
