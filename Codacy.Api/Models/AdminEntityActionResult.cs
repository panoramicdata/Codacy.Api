namespace Codacy.Api.Models;

/// <summary>
/// Admin entity action result
/// </summary>
public class AdminEntityActionResult
{
	/// <summary>Result type (success, updated, failed)</summary>
	public required string ResultType { get; set; }

	/// <summary>Map of field names to their new values (for updated results)</summary>
	public Dictionary<string, string>? Changes { get; set; }

	/// <summary>Error message (for failed results)</summary>
	public string? ErrorReason { get; set; }

	/// <summary>Entity identification</summary>
	public AdminEntityIdentification? EntityIdentification { get; set; }
}
