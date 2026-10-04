namespace Codacy.Api.Models;

/// <summary>
/// Request body to update the join mode
/// </summary>
public class JoinModeRequest
{
	/// <summary>Join mode</summary>
	public required JoinMode JoinMode { get; set; }
}
