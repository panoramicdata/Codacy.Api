using Codacy.Api.Interfaces;

namespace Codacy.Api;

/// <summary>
/// Implementation of Analysis API operations
/// </summary>
internal class AnalysisApi(HttpClient httpClient) : IAnalysisApi
{
	private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

	// API methods will be implemented here
}
