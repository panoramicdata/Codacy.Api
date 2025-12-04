namespace Codacy.Api.Models;

/// <summary>
/// User permission level
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix")]
public enum Permission
{
	/// <summary>Administrator permission</summary>
	admin,
	/// <summary>Write permission</summary>
	write,
	/// <summary>Read permission</summary>
	read
}
