using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for organization and enterprise metrics API operations
/// </summary>
public interface IMetricsApi
{
	/// <summary>
	/// Start collecting metrics for an organization
	/// </summary>
	/// <remarks>
	/// Starts data collection for missing metrics. The organization must have metrics support enabled.
	/// </remarks>
	[Post("/api/v3/organizations/{provider}/{organizationName}/metrics/start")]
	Task InitiateMetricsForOrganizationAsync(
		Provider provider,
		string organizationName,
		[Body] MetricsFilter? body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the metrics that are ready for an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/metrics/ready")]
	Task<OrganizationReadyMetricsResponse> ReadyMetricsForOrganizationAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the latest value of a metric
	/// </summary>
	/// <remarks>
	/// Works for aggregating metrics (such as open issues) but not accumulating metrics (such as fixed issues).
	/// </remarks>
	[Post("/api/v3/organizations/{provider}/{organizationName}/metrics/{metricName}/latest")]
	Task<MetricValueResponse> RetrieveLatestMetricValueAsync(
		Provider provider,
		string organizationName,
		string metricName,
		[Body] MetricFilter body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the latest metric values grouped by dimension
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/metrics/{metricName}/latest-grouped")]
	Task<PeriodGroupedMetricValuesResponse> RetrieveLatestMetricGroupedValuesAsync(
		Provider provider,
		string organizationName,
		string metricName,
		[Body] GroupMetricFilter body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the metric value for a specific period
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/metrics/{metricName}/period")]
	Task<MetricValueResponse> RetrieveValueForPeriodAsync(
		Provider provider,
		string organizationName,
		string metricName,
		[Body] PeriodMetricFilterBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the metric values for a specific period grouped by dimension
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/metrics/{metricName}/period-grouped")]
	Task<PeriodGroupedMetricValuesResponse> RetrieveGroupedValuesForPeriodAsync(
		Provider provider,
		string organizationName,
		string metricName,
		[Body] PeriodGroupMetricFilterBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the metric values for a time range
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/metrics/{metricName}/timerange")]
	Task<TimerangeMetricValuesResponse> RetrieveTimerangeMetricValuesAsync(
		Provider provider,
		string organizationName,
		string metricName,
		[Body] TimerangeMetricFilterBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the metrics that are ready for each organization in an enterprise
	/// </summary>
	[Get("/api/v3/enterprises/{provider}/{enterpriseName}/metrics/ready")]
	Task<ReadyMetricsForEnterpriseResponse> ReadyMetricsForEnterpriseAsync(
		Provider provider,
		string enterpriseName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the latest metric values for all organizations in an enterprise
	/// </summary>
	[Post("/api/v3/enterprises/{provider}/{enterpriseName}/metrics/{metricName}/latest")]
	Task<MetricValueResponse> RetrieveLatestMetricValueForEnterpriseAsync(
		Provider provider,
		string enterpriseName,
		string metricName,
		[Body] EnterpriseMetricFilter? body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the latest metric values grouped by dimension for all organizations in an enterprise
	/// </summary>
	[Post("/api/v3/enterprises/{provider}/{enterpriseName}/metrics/{metricName}/latest-grouped")]
	Task<PeriodGroupedMetricValuesResponse> RetrieveLatestMetricGroupedValuesForEnterpriseAsync(
		Provider provider,
		string enterpriseName,
		string metricName,
		[Body] EnterpriseGroupMetricFilter body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the metric values for a specific period for all organizations in an enterprise
	/// </summary>
	[Post("/api/v3/enterprises/{provider}/{enterpriseName}/metrics/{metricName}/period")]
	Task<MetricValueResponse> RetrieveValueForPeriodForEnterpriseAsync(
		Provider provider,
		string enterpriseName,
		string metricName,
		[Body] EnterprisePeriodMetricFilterBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the metric values grouped by dimension for a specific period for all organizations in an enterprise
	/// </summary>
	[Post("/api/v3/enterprises/{provider}/{enterpriseName}/metrics/{metricName}/period-grouped")]
	Task<PeriodGroupedMetricValuesResponse> RetrieveGroupedValuesForPeriodForEnterpriseAsync(
		Provider provider,
		string enterpriseName,
		string metricName,
		[Body] EnterprisePeriodGroupMetricFilterBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the metric values for a time range for all organizations in an enterprise
	/// </summary>
	[Post("/api/v3/enterprises/{provider}/{enterpriseName}/metrics/{metricName}/timerange")]
	Task<TimerangeMetricValuesResponse> RetrieveTimerangeMetricValuesForEnterpriseAsync(
		Provider provider,
		string enterpriseName,
		string metricName,
		[Body] EnterpriseTimerangeMetricFilterBody body,
		CancellationToken cancellationToken);
}
