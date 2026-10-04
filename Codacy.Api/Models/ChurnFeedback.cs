namespace Codacy.Api.Models;

/// <summary>
/// Feedback when deleting a subscription
/// </summary>
public class ChurnFeedback
{
	/// <summary>Reason for joining</summary>
	public required Reason JoinReason { get; set; }

	/// <summary>Reason for cancelling</summary>
	public required Reason CancelReason { get; set; }
}
