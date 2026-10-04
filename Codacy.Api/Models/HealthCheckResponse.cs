namespace Codacy.Api.Models;

/// <summary>
/// Health check response
/// </summary>
public class HealthCheckResponse
{
	/// <summary>Health check</summary>
	public required HealthCheck Data { get; set; }
}
