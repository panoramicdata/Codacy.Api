namespace Codacy.Api.Models;

/// <summary>
/// Repository gate policy
/// </summary>
public class RepositoryGatePolicy
{
	/// <summary>Gate policy ID</summary>
	public required long Id { get; set; }

	/// <summary>Gate policy name</summary>
	public required string Name { get; set; }
}
