using System.Text.Json;

namespace Codacy.Api.Models;

/// <summary>
/// Audit log of an event performed by a Codacy user who is part of an organization
/// </summary>
public class AuditLog
{
	/// <summary>Actor of the event</summary>
	public required AuditActor Actor { get; set; }

	/// <summary>Action performed in the event</summary>
	public required string Action { get; set; }

	/// <summary>Result of the audit action</summary>
	public required AuditResultType Result { get; set; }

	/// <summary>Timestamp when the event occurred</summary>
	public required DateTimeOffset Timestamp { get; set; }

	/// <summary>Source of the event: UI (Codacy app) or API (Codacy API)</summary>
	public string? Source { get; set; }

	/// <summary>Name of the repository, if the scope of the event action is a repository</summary>
	public string? RepositoryName { get; set; }

	/// <summary>Description of the event</summary>
	public string? Description { get; set; }

	/// <summary>Free-form details specific to the performed event action (JsonElement because the spec leaves the shape open)</summary>
	public JsonElement? Details { get; set; }

	/// <summary>Identifier of the entity involved in the event</summary>
	public string? EntityId { get; set; }
}
