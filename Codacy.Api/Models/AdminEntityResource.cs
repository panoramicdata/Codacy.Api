namespace Codacy.Api.Models;

/// <summary>
/// Admin entity resource
/// </summary>
public class AdminEntityResource
{
	/// <summary>Resource details</summary>
	public required Dictionary<string, string> Details { get; set; }

	/// <summary>Entity identification</summary>
	public AdminEntityIdentification? EntityIdentification { get; set; }
}
