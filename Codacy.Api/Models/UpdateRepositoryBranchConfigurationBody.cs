namespace Codacy.Api.Models;

/// <summary>
/// Update repository branch configuration body
/// </summary>
public class UpdateRepositoryBranchConfigurationBody
{
	/// <summary>Is enabled for analysis</summary>
	public bool? IsEnabled { get; set; }
}
