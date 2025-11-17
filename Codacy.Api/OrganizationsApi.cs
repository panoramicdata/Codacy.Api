using Codacy.Api.Interfaces;

namespace Codacy.Api;

/// <summary>
/// Implementation of Organizations API operations
/// </summary>
internal class OrganizationsApi(HttpClient httpClient) : IOrganizationsApi
{
	private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

	// API methods will be implemented here
}
