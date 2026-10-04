namespace Codacy.Api.Models;

/// <summary>
/// Dormant account information
/// </summary>
public class DormantAccountInfo
{
	/// <summary>Email address of the deleted account</summary>
	public required string Email { get; set; }
}
