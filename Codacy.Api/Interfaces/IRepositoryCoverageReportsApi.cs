using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for repository coverage report API operations
/// </summary>
public interface IRepositoryCoverageReportsApi
{
	/// <summary>
	/// List the latest coverage reports of a repository
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/coverage/status")]
	Task<CoverageReportResponse> ListCoverageReportsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List the coverage reports of a commit
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}/coverage/reports")]
	Task<ListResponse<CoverageReportEntry>> ListCommitCoverageReportsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string commitUuid,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get a coverage report of a commit
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}/coverage/reports/{reportUuid}")]
	Task<CoverageReportContentResponse> GetCoverageReportAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string commitUuid,
		string reportUuid,
		CancellationToken cancellationToken);
}
