using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for enterprise API operations
/// </summary>
public interface IEnterprisesApi
{
	/// <summary>
	/// List user enterprises
	/// </summary>
	[Get("/api/v3/enterprises/{provider}")]
	Task<ListResponse<EnterpriseEntity>> ListEnterprisesAsync(
		Provider provider,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get an enterprise
	/// </summary>
	[Get("/api/v3/enterprises/{provider}/{enterpriseName}")]
	Task<GetEnterpriseResponse> GetEnterpriseAsync(
		Provider provider,
		string enterpriseName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the organizations of an enterprise
	/// </summary>
	[Get("/api/v3/enterprises/{provider}/{enterpriseName}/organizations")]
	Task<ListResponse<EnterpriseOrganization>> ListEnterpriseOrganizationsAsync(
		Provider provider,
		string enterpriseName,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get enterprise seats
	/// </summary>
	[Get("/api/v3/enterprises/{provider}/{enterpriseName}/seats")]
	Task<ListResponse<Seat>> ListEnterpriseSeatsAsync(
		Provider provider,
		string enterpriseName,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? search,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get enterprise seats as a CSV file
	/// </summary>
	[Get("/api/v3/reports/enterprises/{provider}/{enterpriseName}/seats-csv")]
	Task<string> ListEnterpriseSeatsCsvAsync(
		Provider provider,
		string enterpriseName,
		CancellationToken cancellationToken);

	/// <summary>
	/// List user configured enterprise provider account tokens
	/// </summary>
	[Get("/api/v3/user/enterprise/integrations")]
	Task<ListResponse<EnterpriseAccountToken>> ListUserEnterpriseProviderTokensAsync(
		CancellationToken cancellationToken);

	/// <summary>
	/// Add an Enterprise account token
	/// </summary>
	[Post("/api/v3/user/enterprise/integrations")]
	Task AddEnterpriseTokenAsync(
		[Body] AddEnterpriseAccountTokenBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete an Enterprise account token
	/// </summary>
	[Delete("/api/v3/user/enterprise/integrations/{provider}")]
	Task DeleteEnterpriseTokenAsync(
		Provider provider,
		CancellationToken cancellationToken);
}
