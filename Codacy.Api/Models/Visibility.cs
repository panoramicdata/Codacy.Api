namespace Codacy.Api.Models;

/// <summary>
/// Repository visibility
/// </summary>
public enum Visibility
{
	/// <summary>Public repository</summary>
	Public,
	/// <summary>Private repository</summary>
	Private,
	/// <summary>Public for logged in users</summary>
	LoginPublic
}
