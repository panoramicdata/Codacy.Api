namespace Codacy.Api.Models;

/// <summary>
/// Request body for deleting repository API tokens by ids
/// </summary>
public class RepositoryApiTokensDeleteRequest
{
	/// <summary>Ids of the repository API tokens to delete</summary>
	public required List<long> Ids { get; set; }
}
