namespace Codacy.Api.Models;

/// <summary>
/// Health check
/// </summary>
public class HealthCheck
{
	/// <summary>Message</summary>
	public required string Message { get; set; }
}
