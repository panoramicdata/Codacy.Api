namespace Codacy.Api.Models;

/// <summary>
/// Request body for creating a repository API token
/// </summary>
public class RepositoryApiTokenCreateRequest
{
	/// <summary>A name to identify the token: alphanumeric characters and dashes, maximum 100 characters</summary>
	public required string Name { get; set; }

	/// <summary>When the token expires: must be in the future and no more than one year from now</summary>
	public required DateTimeOffset ExpiresAt { get; set; }
}
