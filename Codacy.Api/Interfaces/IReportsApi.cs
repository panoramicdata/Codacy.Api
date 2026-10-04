using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for CSV report API operations. Each method returns the raw (chunked) CSV stream, which the caller must dispose.
/// </summary>
public interface IReportsApi
{
	/// <summary>
	/// Generate a CSV of all security and risk management items for an organization
	/// </summary>
	[Get("/api/v3/reports/organizations/{provider}/{organizationName}/security/items")]
	Task<Stream> GetReportSecurityItemsAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Generate a filtered CSV of security and risk management items
	/// </summary>
	/// <remarks>
	/// The body is non-nullable because Codacy rejects a null one. Pass an empty instance to export unfiltered.
	/// </remarks>
	[Post("/api/v3/reports/organizations/{provider}/{organizationName}/security/items/search")]
	Task<Stream> SearchReportSecurityItemsAsync(
		Provider provider,
		string organizationName,
		[Body] PostReportSecurityItemsBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Search the SBOM dependencies of an organization as CSV
	/// </summary>
	/// <remarks>
	/// The body is non-nullable because Codacy rejects a null one. Pass an empty instance to export unfiltered.
	/// </remarks>
	[Post("/api/v3/reports/organizations/{provider}/{organizationName}/sbom/dependencies/search")]
	Task<Stream> SearchReportSbomDependenciesAsync(
		Provider provider,
		string organizationName,
		[Body] SearchSbomDependenciesBody body,
		CancellationToken cancellationToken);
}
