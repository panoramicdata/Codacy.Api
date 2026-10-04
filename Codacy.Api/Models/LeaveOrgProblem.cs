namespace Codacy.Api.Models;

/// <summary>
/// Problem preventing a user from leaving an organization
/// </summary>
public class LeaveOrgProblem
{
	/// <summary>Suggested actions</summary>
	public required List<ProblemLink> Actions { get; set; }

	/// <summary>A stable identifier for a problem</summary>
	public required string Code { get; set; }
}
