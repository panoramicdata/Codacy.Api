namespace Codacy.Api.Models;

/// <summary>
/// Admin entity action response
/// </summary>
public class AdminEntityActionResponse
{
	/// <summary>Action result</summary>
	public required AdminEntityActionResult Data { get; set; }
}
