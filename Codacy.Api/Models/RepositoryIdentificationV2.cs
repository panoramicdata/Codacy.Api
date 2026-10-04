namespace Codacy.Api.Models;

/// <summary>
/// Repository identification
/// </summary>
public class RepositoryIdentificationV2
{
	/// <summary>Identifier of the repository</summary>
	public required long RepositoryId { get; set; }

	/// <summary>Name of the repository</summary>
	public required string Name { get; set; }

	/// <summary>Git provider</summary>
	public required Provider Provider { get; set; }

	/// <summary>Name of the organization</summary>
	public required string OrganizationName { get; set; }
}
