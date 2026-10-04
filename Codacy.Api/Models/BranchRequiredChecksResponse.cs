namespace Codacy.Api.Models;

/// <summary>
/// Branch required checks response
/// </summary>
public class BranchRequiredChecksResponse
{
	/// <summary>Required checks</summary>
	public required BranchRequiredChecks Data { get; set; }
}
