using Codacy.Api.Interfaces;
using Refit;

namespace Codacy.Api;

/// <summary>
/// Client for interacting with the Codacy API
/// </summary>
public class CodacyClient : ICodacyClient, IDisposable
{
	private readonly CodacyClientOptions _options;
	private readonly HttpClient _httpClient;
	private bool _disposed;

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
	/// Creates an API client using Refit
	/// </summary>
	protected virtual T CreateApiClient<T>() where T : class
		=> RestService.For<T>(_httpClient);

	private HttpClient CreateHttpClient()
	{
		var httpClient = new HttpClient
		{
			BaseAddress = new Uri(_options.BaseUrl),
			Timeout = _options.RequestTimeout
		};

		// Add authentication header
		httpClient.DefaultRequestHeaders.Add("api-token", _options.ApiToken);

		// Add any custom default headers
		foreach (var header in _options.DefaultHeaders)
		{
			httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
		}

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
