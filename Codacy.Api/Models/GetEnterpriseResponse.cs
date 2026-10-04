namespace Codacy.Api.Models;

/// <summary>
/// Get enterprise response
/// </summary>
public class GetEnterpriseResponse
{
	/// <summary>Enterprise</summary>
	public required EnterpriseEntity Data { get; set; }
}
