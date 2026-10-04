using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Slack integration API operations
/// </summary>
public interface ISlackApi
{
	/// <summary>
	/// Get the Slack integration of the organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/integrations/slack")]
	Task<SlackIntegrationResponse> GetSlackIntegrationAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create or update the Slack integration of the organization
	/// </summary>
	[Put("/api/v3/organizations/{provider}/{organizationName}/integrations/slack")]
	Task<SlackIntegrationResponse> CreateOrUpdateSlackIntegrationAsync(
		Provider provider,
		string organizationName,
		[Body] SlackIntegrationRequest body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete the Slack integration of the organization and associated resources
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/integrations/slack")]
	Task DeleteSlackIntegrationAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);
}
