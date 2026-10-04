using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for platform-level API operations (login, configuration, health and session)
/// </summary>
public interface IPlatformApi
{
	/// <summary>
	/// List configured login providers on Codacy's platform
	/// </summary>
	[Get("/api/v3/login/integrations")]
	Task<ListResponse<ConfiguredLoginIntegration>> ListConfiguredLoginIntegrationsAsync(
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List provider integrations existing on Codacy's platform
	/// </summary>
	[Get("/api/v3/provider/integrations")]
	Task<ListResponse<ProviderIntegration>> ListProviderIntegrationsAsync(
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get configuration status
	/// </summary>
	[Get("/api/v3/configuration/status")]
	Task<ConfigurationStatusResponse> GetConfigurationStatusAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Health check endpoint
	/// </summary>
	[Get("/api/v3/health")]
	Task<HealthCheckResponse> HealthAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Send a heartbeat to keep the session alive
	/// </summary>
	[Post("/api/v3/session/heartbeat")]
	Task<HeartbeatResponse> HeartbeatAsync(
		[Body] HeartbeatRequest body,
		CancellationToken cancellationToken);
}
