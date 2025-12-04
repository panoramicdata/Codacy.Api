namespace Codacy.Api.Models;

/// <summary>
/// Join mode for organizations
/// </summary>
public enum JoinMode
{
	/// <summary>Automatic join</summary>
	auto,
	/// <summary>Admin automatic approval</summary>
	adminAuto,
	/// <summary>Request to join</summary>
	request
}
