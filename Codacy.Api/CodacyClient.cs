using System.Text.Json;
using System.Text.Json.Serialization;
using Codacy.Api.Interfaces;
using Refit;

namespace Codacy.Api;

/// <summary>
/// Client for interacting with the Codacy API
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "S1699:Calling virtual method in constructor", Justification = "CreateApiClient is intentionally virtual to allow testing; the method does not rely on any derived class state")]
public class CodacyClient : ICodacyClient, IDisposable
{
	private readonly CodacyClientOptions _options;
	private readonly HttpClient _httpClient;
	private readonly RefitSettings _refitSettings;
	private bool _disposed;

	/// <summary>
	/// The JSON configuration every request and response goes through. Exposed so that model
	/// tests deserialize captured API responses exactly as the client does, rather than against
	/// a second set of options that could drift away from these.
	/// </summary>
	internal static JsonSerializerOptions JsonSerializerOptions { get; } = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		PropertyNameCaseInsensitive = true,
		Converters = { new JsonStringEnumConverter() }
	};

	/// <summary>
	/// Initializes a new instance of the CodacyClient for testing
	/// </summary>
	/// <param name="options">Configuration options for the client</param>
	public CodacyClient(CodacyClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);
		options.Validate();

		_options = options;
		_httpClient = options.HttpClientFactory?.CreateClient()
			?? options.HttpClient
			?? CreateHttpClient();

		// Configure JSON serialization with camelCase naming policy
		_refitSettings = new RefitSettings
		{
			ContentSerializer = new SystemTextJsonContentSerializer(JsonSerializerOptions),
			// Without this, Provider reaches the URL as "Github" rather than "gh", and bools as
			// "True" rather than "true".
			UrlParameterFormatter = new CodacyUrlParameterFormatter()
		};

		// Initialize API modules using Refit
		Version = CreateApiClient<IVersionApi>();
		Account = CreateApiClient<IAccountApi>();
		Organizations = CreateApiClient<IOrganizationsApi>();
		Repositories = CreateApiClient<IRepositoriesApi>();
		Analysis = CreateApiClient<IAnalysisApi>();
		Issues = CreateApiClient<IIssuesApi>();
		Commits = CreateApiClient<ICommitsApi>();
		PullRequests = CreateApiClient<IPullRequestsApi>();
		People = CreateApiClient<IPeopleApi>();
		Coverage = CreateApiClient<ICoverageApi>();
		CodingStandards = CreateApiClient<ICodingStandardsApi>();
		Security = CreateApiClient<ISecurityApi>();
		Sbom = CreateApiClient<ISbomApi>();
		Images = CreateApiClient<IImagesApi>();
		Reports = CreateApiClient<IReportsApi>();
		AiInventory = CreateApiClient<IAiInventoryApi>();
		Billing = CreateApiClient<IBillingApi>();
		OrganizationSettings = CreateApiClient<IOrganizationSettingsApi>();
		Enterprises = CreateApiClient<IEnterprisesApi>();
		Admin = CreateApiClient<IAdminApi>();
		Platform = CreateApiClient<IPlatformApi>();
		RepositorySettings = CreateApiClient<IRepositorySettingsApi>();
		RepositoryApiTokens = CreateApiClient<IRepositoryApiTokensApi>();
		RepositoryFiles = CreateApiClient<IRepositoryFilesApi>();
		Diffs = CreateApiClient<IDiffsApi>();
		RepositoryCoverageReports = CreateApiClient<IRepositoryCoverageReportsApi>();
		GatePolicies = CreateApiClient<IGatePoliciesApi>();
		Segments = CreateApiClient<ISegmentsApi>();
		Jira = CreateApiClient<IJiraApi>();
		Slack = CreateApiClient<ISlackApi>();
		Dast = CreateApiClient<IDastApi>();
		RepositoryToolPatterns = CreateApiClient<IRepositoryToolPatternsApi>();
		AnalysisActions = CreateApiClient<IAnalysisActionsApi>();
		Tools = CreateApiClient<IToolsApi>();
		Metrics = CreateApiClient<IMetricsApi>();
	}

	/// <summary>
	/// Gets the Version API module
	/// </summary>
	public IVersionApi Version { get; }

	/// <summary>
	/// Gets the Account API module
	/// </summary>
	public IAccountApi Account { get; }

	/// <summary>
	/// Gets the Organizations API module
	/// </summary>
	public IOrganizationsApi Organizations { get; }

	/// <summary>
	/// Gets the Repositories API module
	/// </summary>
	public IRepositoriesApi Repositories { get; }

	/// <summary>
	/// Gets the Analysis API module
	/// </summary>
	public IAnalysisApi Analysis { get; }

	/// <summary>
	/// Gets the Issues API module
	/// </summary>
	public IIssuesApi Issues { get; }

	/// <summary>
	/// Gets the Commits API module
	/// </summary>
	public ICommitsApi Commits { get; }

	/// <summary>
	/// Gets the Pull Requests API module
	/// </summary>
	public IPullRequestsApi PullRequests { get; }

	/// <summary>
	/// Gets the People API module
	/// </summary>
	public IPeopleApi People { get; }

	/// <summary>
	/// Gets the Coverage API module
	/// </summary>
	public ICoverageApi Coverage { get; }

	/// <summary>
	/// Gets the Coding Standards API module
	/// </summary>
	public ICodingStandardsApi CodingStandards { get; }

	/// <summary>
	/// Gets the Security API module
	/// </summary>
	public ISecurityApi Security { get; }

	/// <summary>
	/// Gets the Sbom API module
	/// </summary>
	public ISbomApi Sbom { get; }

	/// <summary>
	/// Gets the Images API module
	/// </summary>
	public IImagesApi Images { get; }

	/// <summary>
	/// Gets the Reports API module
	/// </summary>
	public IReportsApi Reports { get; }

	/// <summary>
	/// Gets the AiInventory API module
	/// </summary>
	public IAiInventoryApi AiInventory { get; }

	/// <summary>
	/// Gets the Billing API module
	/// </summary>
	public IBillingApi Billing { get; }

	/// <summary>
	/// Gets the OrganizationSettings API module
	/// </summary>
	public IOrganizationSettingsApi OrganizationSettings { get; }

	/// <summary>
	/// Gets the Enterprises API module
	/// </summary>
	public IEnterprisesApi Enterprises { get; }

	/// <summary>
	/// Gets the Admin API module
	/// </summary>
	public IAdminApi Admin { get; }

	/// <summary>
	/// Gets the Platform API module
	/// </summary>
	public IPlatformApi Platform { get; }

	/// <summary>
	/// Gets the RepositorySettings API module
	/// </summary>
	public IRepositorySettingsApi RepositorySettings { get; }

	/// <summary>
	/// Gets the RepositoryApiTokens API module
	/// </summary>
	public IRepositoryApiTokensApi RepositoryApiTokens { get; }

	/// <summary>
	/// Gets the RepositoryFiles API module
	/// </summary>
	public IRepositoryFilesApi RepositoryFiles { get; }

	/// <summary>
	/// Gets the Diffs API module
	/// </summary>
	public IDiffsApi Diffs { get; }

	/// <summary>
	/// Gets the RepositoryCoverageReports API module
	/// </summary>
	public IRepositoryCoverageReportsApi RepositoryCoverageReports { get; }

	/// <summary>
	/// Gets the GatePolicies API module
	/// </summary>
	public IGatePoliciesApi GatePolicies { get; }

	/// <summary>
	/// Gets the Segments API module
	/// </summary>
	public ISegmentsApi Segments { get; }

	/// <summary>
	/// Gets the Jira API module
	/// </summary>
	public IJiraApi Jira { get; }

	/// <summary>
	/// Gets the Slack API module
	/// </summary>
	public ISlackApi Slack { get; }

	/// <summary>
	/// Gets the Dast API module
	/// </summary>
	public IDastApi Dast { get; }

	/// <summary>
	/// Gets the RepositoryToolPatterns API module
	/// </summary>
	public IRepositoryToolPatternsApi RepositoryToolPatterns { get; }

	/// <summary>
	/// Gets the AnalysisActions API module
	/// </summary>
	public IAnalysisActionsApi AnalysisActions { get; }

	/// <summary>
	/// Gets the Tools API module
	/// </summary>
	public IToolsApi Tools { get; }

	/// <summary>
	/// Gets the Metrics API module
	/// </summary>
	public IMetricsApi Metrics { get; }

	/// <summary>
	/// Creates an API client using Refit
	/// </summary>
	protected virtual T CreateApiClient<T>() where T : class
		=> RestService.For<T>(_httpClient, _refitSettings);

	private HttpClient CreateHttpClient()
	{
		var handler = new LoggingHttpClientHandler(_options);
		
		var httpClient = new HttpClient(handler)
		{
			BaseAddress = new Uri(_options.BaseUrl),
			Timeout = _options.RequestTimeout
		};

		return httpClient;
	}

	/// <summary>
	/// Releases all resources used by the CodacyClient
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Releases the unmanaged resources used by the CodacyClient and optionally releases the managed resources
	/// </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				_httpClient?.Dispose();
			}

			_disposed = true;
		}
	}
}
