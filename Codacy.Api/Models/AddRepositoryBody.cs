namespace Codacy.Api.Models;

/// <summary>
/// Add repository body
/// </summary>
public class AddRepositoryBody
{
	/// <summary>Repository full path</summary>
	public required string RepositoryFullPath { get; set; }

	/// <summary>Provider</summary>
	public required Provider Provider { get; set; }
}
