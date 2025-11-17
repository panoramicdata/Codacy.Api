using Codacy.Api.Interfaces;

namespace Codacy.Api;

/// <summary>
/// Implementation of Repositories API operations
/// </summary>
internal class RepositoriesApi(HttpClient httpClient) : IRepositoriesApi
{
	private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

	// API methods will be implemented here
}
