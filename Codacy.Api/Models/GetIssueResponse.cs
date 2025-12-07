namespace Codacy.Api.Models;

/// <summary>
/// Get issue response
/// </summary>
public class GetIssueResponse
{
	/// <summary>Issue data</summary>
	public required Issue Data { get; set; }
}
