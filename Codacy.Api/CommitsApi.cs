using Codacy.Api.Interfaces;

namespace Codacy.Api;

/// <summary>
/// Implementation of Commits API operations
/// </summary>
internal class CommitsApi(HttpClient httpClient) : ICommitsApi
{
	private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

	// API methods will be implemented here
}
