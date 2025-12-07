namespace Codacy.Api.Models;

/// <summary>
/// Issues overview response
/// </summary>
public class IssuesOverviewResponse
{
	/// <summary>Overview data</summary>
	public required IssuesOverview Data { get; set; }
}
