namespace Codacy.Api.Models;

/// <summary>
/// Check submodules response
/// </summary>
public class CheckSubmodulesResponse
{
	/// <summary>True if the submodules option is enabled for the organization</summary>
	public required bool Data { get; set; }
}
