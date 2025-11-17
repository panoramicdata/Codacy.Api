using Codacy.Api.Interfaces;

namespace Codacy.Api;

/// <summary>
/// Implementation of Pull Requests API operations
/// </summary>
internal class PullRequestsApi(HttpClient httpClient) : IPullRequestsApi
{
	private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

	// API methods will be implemented here
}
