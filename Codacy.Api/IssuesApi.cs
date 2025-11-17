using Codacy.Api.Interfaces;

namespace Codacy.Api;

/// <summary>
/// Implementation of Issues API operations
/// </summary>
internal class IssuesApi(HttpClient httpClient) : IIssuesApi
{
	private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

	// API methods will be implemented here
}
