namespace Codacy.Api.Models;

/// <summary>
/// Heartbeat response
/// </summary>
public class HeartbeatResponse
{
	/// <summary>Server time of the last activity of the user</summary>
	public required DateTimeOffset LastActivity { get; set; }

	/// <summary>Time in milliseconds until the session expires due to inactivity</summary>
	public required long IdleExpiresIn { get; set; }

	/// <summary>Time in milliseconds until the session absolute timeout expires</summary>
	public required long AbsoluteExpiresIn { get; set; }
}
