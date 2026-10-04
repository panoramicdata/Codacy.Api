using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Jira integration API operations
/// </summary>
public interface IJiraApi
{
	/// <summary>
	/// Get the Jira integration of the organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/integrations/jira")]
	Task<JiraIntegrationResponse> GetJiraIntegrationAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create or update the Jira integration of the organization
	/// </summary>
	[Put("/api/v3/organizations/{provider}/{organizationName}/integrations/jira")]
	Task<JiraIntegrationResponse> CreateOrUpdateJiraIntegrationAsync(
		Provider provider,
		string organizationName,
		[Query] string oauthCode,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete the Jira integration of the organization and associated resources
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/integrations/jira")]
	Task DeleteJiraIntegrationAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get Jira tickets for a Codacy element
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/integrations/jira/tickets")]
	Task<GetJiraTicketsResponse> GetJiraTicketsAsync(
		Provider provider,
		string organizationName,
		[Query] ElementType elementType,
		[Query] string elementId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create a Jira ticket
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/integrations/jira/tickets")]
	Task<CreateJiraTicketResponse> CreateJiraTicketAsync(
		Provider provider,
		string organizationName,
		[Body] CreateJiraTicketBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Unlink a Jira ticket from a repository
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/integrations/jira/tickets/{jiraTicketIdentifier}")]
	Task UnlinkRepositoryJiraTicketAsync(
		Provider provider,
		string organizationName,
		long jiraTicketIdentifier,
		[Body] UnlinkRepositoryJiraTicketBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get available Jira projects for the organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/integrations/jira/projects")]
	Task<AvailableJiraProjectsResponse> GetAvailableJiraProjectsAsync(
		Provider provider,
		string organizationName,
		[Query] string? search,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get available issue types for a Jira project
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/integrations/jira/projects/{jiraProjectId}/issueTypes")]
	Task<JiraProjectIssueTypesResponse> GetJiraProjectIssueTypesAsync(
		Provider provider,
		string organizationName,
		long jiraProjectId,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get available fields by issue type for a Jira project
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/integrations/jira/projects/{jiraProjectId}/issueTypes/{jiraIssueTypeId}/fields")]
	Task<JiraProjectIssueFieldsResponse> GetJiraProjectIssueFieldsAsync(
		Provider provider,
		string organizationName,
		long jiraProjectId,
		string jiraIssueTypeId,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);
}
