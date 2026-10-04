using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for billing and payment plan API operations
/// </summary>
public interface IBillingApi
{
	/// <summary>
	/// Update the information about organization billing
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/billing")]
	Task UpdateOrganizationDetailedBillingAsync(
		Provider provider,
		string organizationName,
		[Body] BillingDetailsUpdate body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get card information about organization billing
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/billing/card")]
	Task<CardResponse> GetOrganizationBillingCardAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Add a card to the organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/billing/card")]
	Task AddOrganizationBillingCardAsync(
		Provider provider,
		string organizationName,
		[Body] CardCreation body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get a billing estimation
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/billing/estimation")]
	Task<BillingEstimationResponse> GetOrganizationBillingEstimationAsync(
		Provider provider,
		string organizationName,
		[Query] string paymentPlanCode,
		[Query] string? promoCode,
		CancellationToken cancellationToken);

	/// <summary>
	/// Change the plan of an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/billing/change-plan")]
	Task ChangeOrganizationPlanAsync(
		Provider provider,
		string organizationName,
		[Body] ChangePlan body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Sync the information about organization billing
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/billing/sync")]
	Task SyncMarketplaceBillingAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Sync the information about the billing of the organizations of the authenticated user
	/// </summary>
	[Post("/api/v3/user/billing/sync")]
	Task SyncUserMarketplaceBillingAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Delete billing subscription for organization
	/// </summary>
	[Delete("/api/v3/billing/{provider}/{organizationName}/subscription")]
	Task DeleteSubscriptionAsync(
		Provider provider,
		string organizationName,
		[Body] ChurnFeedback body,
		CancellationToken cancellationToken);

	/// <summary>
	/// List available plans in Codacy
	/// </summary>
	[Get("/api/v3/plans")]
	Task<PaymentPlansResponse> ListPaymentPlansAsync(CancellationToken cancellationToken);
}
