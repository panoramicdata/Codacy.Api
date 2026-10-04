namespace Codacy.Api.Models;

/// <summary>
/// Actor of the audit event
/// </summary>
public class AuditActor
{
	/// <summary>Email of the audit actor</summary>
	public required string Email { get; set; }

	/// <summary>Role of the audit actor</summary>
	public AuditActorRole? Role { get; set; }
}
