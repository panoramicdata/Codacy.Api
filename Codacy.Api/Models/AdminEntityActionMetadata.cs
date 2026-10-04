namespace Codacy.Api.Models;

/// <summary>
/// Admin entity action metadata
/// </summary>
public class AdminEntityActionMetadata
{
	/// <summary>Unique identifier for the action, used in URLs</summary>
	public required string Slug { get; set; }

	/// <summary>Human-friendly name for the action</summary>
	public required string DisplayName { get; set; }

	/// <summary>Description of what the action does</summary>
	public string? Description { get; set; }

	/// <summary>Fields of the action</summary>
	public required List<AdminEntityActionField> Fields { get; set; }

	/// <summary>Whether the action requires user confirmation before execution</summary>
	public required bool RequiresConfirmation { get; set; }
}
