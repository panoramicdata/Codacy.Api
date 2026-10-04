namespace Codacy.Api.Models;

/// <summary>
/// Informs if the user can leave the organization and if not, why
/// </summary>
public class LeaveOrgCheckResult
{
	/// <summary>True if user can leave the organization</summary>
	public required bool CanLeave { get; set; }

	/// <summary>Message</summary>
	public required string Message { get; set; }

	/// <summary>Reason the user cannot leave</summary>
	public LeaveOrgProblem? Reason { get; set; }
}
