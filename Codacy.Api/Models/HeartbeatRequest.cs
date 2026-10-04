namespace Codacy.Api.Models;

/// <summary>
/// Request body to send a heartbeat
/// </summary>
public class HeartbeatRequest
{
	/// <summary>True if the user was active in the last heartbeat interval</summary>
	public required bool WasActive { get; set; }
}
