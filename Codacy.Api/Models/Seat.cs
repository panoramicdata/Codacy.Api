namespace Codacy.Api.Models;

/// <summary>
/// Enterprise seat
/// </summary>
public class Seat
{
	/// <summary>Identifiers of the organizations</summary>
	public required List<long> OrganizationsIds { get; set; }

	/// <summary>Emails</summary>
	public required List<string> Emails { get; set; }

	/// <summary>Last analysis</summary>
	public DateTimeOffset? LastAnalysis { get; set; }

	/// <summary>Creation date</summary>
	public required DateTimeOffset CreatedAt { get; set; }

	/// <summary>Last commit identifier</summary>
	public long? LastCommitId { get; set; }

	/// <summary>Provider identifier</summary>
	public string? ProviderId { get; set; }

	/// <summary>Provider login</summary>
	public string? ProviderLogin { get; set; }

	/// <summary>Whether the seat is active</summary>
	public required bool IsActive { get; set; }
}
