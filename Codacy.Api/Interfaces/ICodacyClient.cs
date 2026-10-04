namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for the Codacy API client
/// </summary>
public interface ICodacyClient : IDisposable
{
	/// <summary>
	/// Gets the Version API module
	/// </summary>
	IVersionApi Version { get; }

	/// <summary>
	/// Gets the Account API module
	/// </summary>
	IAccountApi Account { get; }

	/// <summary>
	/// Gets the Organizations API module
	/// </summary>
	IOrganizationsApi Organizations { get; }

	/// <summary>
	/// Gets the Repositories API module
	/// </summary>
	IRepositoriesApi Repositories { get; }

	/// <summary>
	/// Gets the Analysis API module
	/// </summary>
	IAnalysisApi Analysis { get; }

	/// <summary>
	/// Gets the Issues API module
	/// </summary>
	IIssuesApi Issues { get; }

	/// <summary>
	/// Gets the Commits API module
	/// </summary>
	ICommitsApi Commits { get; }

	/// <summary>
	/// Gets the Pull Requests API module
	/// </summary>
	IPullRequestsApi PullRequests { get; }

	/// <summary>
	/// Gets the People API module
	/// </summary>
	IPeopleApi People { get; }

	/// <summary>
	/// Gets the Coverage API module
	/// </summary>
	ICoverageApi Coverage { get; }

	/// <summary>
	/// Gets the Coding Standards API module
	/// </summary>
	ICodingStandardsApi CodingStandards { get; }

	/// <summary>
	/// Gets the Security API module
	/// </summary>
	ISecurityApi Security { get; }

	/// <summary>
	/// Gets the Sbom API module
	/// </summary>
	ISbomApi Sbom { get; }

	/// <summary>
	/// Gets the Images API module
	/// </summary>
	IImagesApi Images { get; }

	/// <summary>
	/// Gets the Reports API module
	/// </summary>
	IReportsApi Reports { get; }

	/// <summary>
	/// Gets the AiInventory API module
	/// </summary>
	IAiInventoryApi AiInventory { get; }

	/// <summary>
	/// Gets the Billing API module
	/// </summary>
	IBillingApi Billing { get; }

	/// <summary>
	/// Gets the OrganizationSettings API module
	/// </summary>
	IOrganizationSettingsApi OrganizationSettings { get; }

	/// <summary>
	/// Gets the Enterprises API module
	/// </summary>
	IEnterprisesApi Enterprises { get; }

	/// <summary>
	/// Gets the Admin API module
	/// </summary>
	IAdminApi Admin { get; }

	/// <summary>
	/// Gets the Platform API module
	/// </summary>
	IPlatformApi Platform { get; }

	/// <summary>
	/// Gets the RepositorySettings API module
	/// </summary>
	IRepositorySettingsApi RepositorySettings { get; }

	/// <summary>
	/// Gets the RepositoryApiTokens API module
	/// </summary>
	IRepositoryApiTokensApi RepositoryApiTokens { get; }

	/// <summary>
	/// Gets the RepositoryFiles API module
	/// </summary>
	IRepositoryFilesApi RepositoryFiles { get; }

	/// <summary>
	/// Gets the Diffs API module
	/// </summary>
	IDiffsApi Diffs { get; }

	/// <summary>
	/// Gets the RepositoryCoverageReports API module
	/// </summary>
	IRepositoryCoverageReportsApi RepositoryCoverageReports { get; }

	/// <summary>
	/// Gets the GatePolicies API module
	/// </summary>
	IGatePoliciesApi GatePolicies { get; }

	/// <summary>
	/// Gets the Segments API module
	/// </summary>
	ISegmentsApi Segments { get; }

	/// <summary>
	/// Gets the Jira API module
	/// </summary>
	IJiraApi Jira { get; }

	/// <summary>
	/// Gets the Slack API module
	/// </summary>
	ISlackApi Slack { get; }

	/// <summary>
	/// Gets the Dast API module
	/// </summary>
	IDastApi Dast { get; }

	/// <summary>
	/// Gets the RepositoryToolPatterns API module
	/// </summary>
	IRepositoryToolPatternsApi RepositoryToolPatterns { get; }

	/// <summary>
	/// Gets the AnalysisActions API module
	/// </summary>
	IAnalysisActionsApi AnalysisActions { get; }

	/// <summary>
	/// Gets the Tools API module
	/// </summary>
	IToolsApi Tools { get; }

	/// <summary>
	/// Gets the Metrics API module
	/// </summary>
	IMetricsApi Metrics { get; }
}
