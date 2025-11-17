using Codacy.Api.Interfaces;

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
	/// Initializes a new instance of the CodacyClient
	/// </summary>
	/// <param name="options">Configuration options for the client</param>
	public CodacyClient(CodacyClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);
		options.Validate();

		_options = options;
		_httpClient = CreateHttpClient();

		// Initialize API modules
		Organizations = new OrganizationsApi(_httpClient);
		Repositories = new RepositoriesApi(_httpClient);
		Analysis = new AnalysisApi(_httpClient);
		Issues = new IssuesApi(_httpClient);
		Commits = new CommitsApi(_httpClient);
		PullRequests = new PullRequestsApi(_httpClient);
	}

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
