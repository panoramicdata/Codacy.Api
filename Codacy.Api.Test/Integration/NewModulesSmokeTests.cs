using Refit;

namespace Codacy.Api.Test.Integration;

/// <summary>
/// Read-only smoke tests for the modules written from the OpenAPI specification (4.0.13), run
/// against a real organization. They exist to catch models that cannot deserialize a real
/// response, the failure that went unnoticed for the original security models.
/// </summary>
/// <remarks>
/// A response Codacy refuses (401, 402, 403, 404, 409) is reported but does not fail the test: the
/// organization may not have the feature, plan or permission. A 400 or 422 means this client sent a
/// malformed request, and any other exception, including a 2xx whose body could not be read, means a
/// model does not match the real response. Both fail. Only outcomes are logged, never response
/// bodies, because some of these endpoints return secrets or capability URLs. Endpoints that mutate
/// state, return credentials, or need ids this organization may not have are left out.
/// </remarks>
[Trait("Category", "Integration")]
public class NewModulesSmokeTests(ITestOutputHelper output) : TestBase(output)
{
	private readonly List<string> _outcomes = [];
	private readonly List<string> _failures = [];

	private string TestBranch => Configuration["CodacyApi:TestBranch"] is { Length: > 0 } branch ? branch : "main";

	[Fact]
	public async Task RepositoryEndpoints_DeserializeRealResponses()
	{
		using var client = CreateQuietClient();
		var (p, o, r, ct) = (TestProvider, TestOrganization, TestRepository, CancellationToken);

		await Probe("RepositorySettings.GetQualitySettingsForRepository", () => client.RepositorySettings.GetQualitySettingsForRepositoryAsync(p, o, r, ct));
		await Probe("RepositorySettings.GetBuildServerAnalysisSetting", () => client.RepositorySettings.GetBuildServerAnalysisSettingAsync(p, o, r, ct));
		await Probe("RepositorySettings.GetRepositoryLanguages", () => client.RepositorySettings.GetRepositoryLanguagesAsync(p, o, r, ct));
		await Probe("RepositorySettings.GetRepositoryIntegrationsSettings", () => client.RepositorySettings.GetRepositoryIntegrationsSettingsAsync(p, o, r, ct));
		await Probe("RepositorySettings.GetBranchRequiredChecks", () => client.RepositorySettings.GetBranchRequiredChecksAsync(p, o, r, TestBranch, ct));
		await Probe("RepositoryFiles.ListDirectories", () => client.RepositoryFiles.ListDirectoriesAsync(p, o, r, null, null, null, null, null, 5, ct));
		await Probe("RepositoryFiles.ListIgnoredFiles", () => client.RepositoryFiles.ListIgnoredFilesAsync(p, o, r, null, null, null, 5, ct));
		await Probe("RepositoryCoverageReports.ListCoverageReports", () => client.RepositoryCoverageReports.ListCoverageReportsAsync(p, o, r, 5, ct));

		Finish(minimumOk: 3);
	}

	[Fact]
	public async Task AnalysisAndSbomEndpoints_DeserializeRealResponses()
	{
		using var client = CreateQuietClient();
		var (p, o, r, ct) = (TestProvider, TestOrganization, TestRepository, CancellationToken);

		await Probe("AnalysisActions.GetAutoconfigStatus", () => client.AnalysisActions.GetAutoconfigStatusAsync(p, o, r, ct));
		await Probe("AnalysisActions.GetLatestAutoconfigRunSummary", () => client.AnalysisActions.GetLatestAutoconfigRunSummaryAsync(p, o, r, ct));
		await Probe("AnalysisActions.HasQuickfixSuggestions", () => client.AnalysisActions.HasQuickfixSuggestionsAsync(p, o, r, TestBranch, ct));
		await Probe("Sbom.GetRepositorySbomPresignedUrl", () => client.Sbom.GetRepositorySbomPresignedUrlAsync(p, o, r, ct));

		Finish(minimumOk: 0);
	}

	[Fact]
	public async Task PolicyAndSegmentEndpoints_DeserializeRealResponses()
	{
		using var client = CreateQuietClient();
		var (p, o, ct) = (TestProvider, TestOrganization, CancellationToken);

		var policies = await Probe("GatePolicies.ListGatePolicies", () => client.GatePolicies.ListGatePoliciesAsync(p, o, null, 5, ct));
		if (policies?.Data.FirstOrDefault() is { } policy)
		{
			await Probe("GatePolicies.GetGatePolicy", () => client.GatePolicies.GetGatePolicyAsync(p, o, policy.Id, ct));
			await Probe("GatePolicies.ListRepositoriesFollowingGatePolicy", () => client.GatePolicies.ListRepositoriesFollowingGatePolicyAsync(p, o, policy.Id, null, 5, ct));
		}

		await Probe("Segments.GetSegmentsSyncStatus", () => client.Segments.GetSegmentsSyncStatusAsync(p, o, ct));
		await Probe("Segments.GetSegmentsKeys", () => client.Segments.GetSegmentsKeysAsync(p, o, null, 5, null, ct));
		await Probe("Segments.GetSegmentsKeysWithIds", () => client.Segments.GetSegmentsKeysWithIdsAsync(p, o, null, 5, null, ct));

		Finish(minimumOk: 1);
	}

	[Fact]
	public async Task IntegrationEndpoints_DeserializeRealResponses()
	{
		using var client = CreateQuietClient();
		var (p, o, ct) = (TestProvider, TestOrganization, CancellationToken);

		await Probe("Jira.GetJiraIntegration", () => client.Jira.GetJiraIntegrationAsync(p, o, ct));
		await Probe("Slack.GetSlackIntegration", () => client.Slack.GetSlackIntegrationAsync(p, o, ct));
		await Probe("Dast.GetDastTargets", () => client.Dast.GetDastTargetsAsync(p, o, null, 5, ct));
		await Probe("Images.ListOrganizationImages", () => client.Images.ListOrganizationImagesAsync(p, o, null, 5, ct));

		// An organization may legitimately have none of these features.
		Finish(minimumOk: 0);
	}

	[Fact]
	public async Task OrganizationAndPlatformEndpoints_DeserializeRealResponses()
	{
		using var client = CreateQuietClient();
		var (p, o, ct) = (TestProvider, TestOrganization, CancellationToken);

		await Probe("OrganizationSettings.GetProviderSettings", () => client.OrganizationSettings.GetProviderSettingsAsync(p, o, ct));
		await Probe("OrganizationSettings.RetrieveOrganizationOnboardingProgress", () => client.OrganizationSettings.RetrieveOrganizationOnboardingProgressAsync(p, o, ct));
		await Probe("OrganizationSettings.GetGitProviderAppPermissions", () => client.OrganizationSettings.GetGitProviderAppPermissionsAsync(p, o, ct));
		await Probe("OrganizationSettings.CheckIfUserCanLeave", () => client.OrganizationSettings.CheckIfUserCanLeaveAsync(p, o, ct));
		await Probe("OrganizationSettings.ListOrganizationJoinRequests", () => client.OrganizationSettings.ListOrganizationJoinRequestsAsync(p, o, null, 5, null, ct));
		await Probe("OrganizationSettings.CheckSubmodules", () => client.OrganizationSettings.CheckSubmodulesAsync(p, o, ct));
		await Probe("OrganizationSettings.GetAiRiskCheckList", () => client.OrganizationSettings.GetAiRiskCheckListAsync(p, o, ct));
		await Probe("Billing.ListPaymentPlans", () => client.Billing.ListPaymentPlansAsync(ct));
		await Probe("Platform.ListConfiguredLoginIntegrations", () => client.Platform.ListConfiguredLoginIntegrationsAsync(null, 5, ct));
		await Probe("Platform.ListProviderIntegrations", () => client.Platform.ListProviderIntegrationsAsync(null, 5, ct));
		await Probe("Platform.GetConfigurationStatus", () => client.Platform.GetConfigurationStatusAsync(ct));
		await Probe("Platform.Health", () => client.Platform.HealthAsync(ct));
		await Probe("Enterprises.ListEnterprises", () => client.Enterprises.ListEnterprisesAsync(p, null, 5, ct));
		await Probe("Metrics.ReadyMetricsForOrganization", () => client.Metrics.ReadyMetricsForOrganizationAsync(p, o, ct));

		Finish(minimumOk: 3);
	}

	[Fact]
	public async Task ToolAndPatternEndpoints_DeserializeRealResponses()
	{
		using var client = CreateQuietClient();
		var (p, o, r, ct) = (TestProvider, TestOrganization, TestRepository, CancellationToken);

		await Probe("Tools.ListLanguagesWithTools", () => client.Tools.ListLanguagesWithToolsAsync(ct));
		await Probe("Tools.ListDuplicationTools", () => client.Tools.ListDuplicationToolsAsync(ct));
		await Probe("Tools.ListMetricsTools", () => client.Tools.ListMetricsToolsAsync(ct));

		var tools = await Probe("Tools.ListTools", () => client.Tools.ListToolsAsync(null, 5, ct));
		if (tools?.Data.FirstOrDefault() is { } tool)
		{
			var patterns = await Probe("Tools.ListPatterns", () => client.Tools.ListPatternsAsync(tool.Uuid, null, 5, null, ct));
			if (patterns?.Data.FirstOrDefault() is { } pattern)
			{
				await Probe("Tools.GetPattern", () => client.Tools.GetPatternAsync(tool.Uuid, pattern.Id, ct));
			}

			await Probe("RepositoryToolPatterns.ListRepositoryToolPatterns", () => client.RepositoryToolPatterns.ListRepositoryToolPatternsAsync(
				p, o, r, tool.Uuid, null, null, null, null, null, null, null, null, null, null, null, 5, ct));
			await Probe("RepositoryToolPatterns.ListRepositoryToolPatternConflicts", () => client.RepositoryToolPatterns.ListRepositoryToolPatternConflictsAsync(p, o, r, tool.Uuid, ct));
		}

		Finish(minimumOk: 3);
	}

	/// <summary>
	/// A client with logging off: the shared one logs response bodies, and some of these hold secrets
	/// or capability URLs.
	/// </summary>
	private CodacyClient CreateQuietClient()
	{
		var baseOptions = GetClientOptions();
		return new CodacyClient(new CodacyClientOptions { ApiToken = baseOptions.ApiToken, BaseUrl = baseOptions.BaseUrl });
	}

	private void Finish(int minimumOk)
	{
		foreach (var line in _outcomes)
		{
			Output.WriteLine(line);
		}

		// A passing test shows no output, so the outcomes can also be appended to a file.
		if (Environment.GetEnvironmentVariable("CODACY_SMOKE_REPORT") is { Length: > 0 } reportPath)
		{
			File.AppendAllLines(reportPath, _outcomes);
		}

		var ok = _outcomes.Count(line => line.StartsWith("OK ", StringComparison.Ordinal));
		Output.WriteLine($"{ok} OK, {_outcomes.Count - ok} refused or server-side, {_failures.Count} failed");

		_failures.Should().BeEmpty("a failure here is a request Codacy rejected as malformed or a model that cannot read the real response");
		ok.Should().BeGreaterThanOrEqualTo(minimumOk, "if almost everything is refused the credentials or organization are probably wrong, and nothing was actually checked");
	}

	private async Task<T?> Probe<T>(string name, Func<Task<T>> call)
	{
		try
		{
			var result = await call();
			_outcomes.Add($"OK        {name}");
			return result;
		}
		catch (ApiException ex) when ((int)ex.StatusCode is 401 or 402 or 403 or 404 or 409)
		{
			_outcomes.Add($"REFUSED   {name}: HTTP {(int)ex.StatusCode}");
		}
		catch (ApiException ex) when ((int)ex.StatusCode >= 500)
		{
			_outcomes.Add($"SERVER    {name}: HTTP {(int)ex.StatusCode}");
		}
		catch (Exception ex)
		{
			// Message only: an ApiException's Content can hold response data we do not want in a log.
			var detail = ex is ApiException api ? $"HTTP {(int)api.StatusCode} {api.Message}" : $"{ex.GetType().Name}: {ex.Message}";
			_failures.Add($"{name}: {detail}");
			_outcomes.Add($"FAILED    {name}: {detail}");
		}

		return default;
	}
}
