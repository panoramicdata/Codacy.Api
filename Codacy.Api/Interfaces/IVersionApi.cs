using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Version API operations
/// </summary>
public interface IVersionApi
{
	/// <summary>
	/// Get the version of the Codacy installation
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Version information</returns>
	[Get("/api/v3/version")]
	Task<VersionResponse> GetVersionAsync(CancellationToken cancellationToken = default);
}
