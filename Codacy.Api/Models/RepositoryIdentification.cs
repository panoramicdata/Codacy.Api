namespace Codacy.Api.Models;

/// <summary>
/// Identifier and name of a repository
/// </summary>
public class RepositoryIdentification
{
	/// <summary>Identifier of the repository</summary>
	public required long RepositoryId { get; set; }

	/// <summary>Name of the repository</summary>
	public required string Name { get; set; }
}
