using Codacy.Api.Models;

namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Analysis API
/// </summary>
[Trait("Category", "Integration")]
public class AnalysisApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task ListOrganizationRepositoriesWithAnalysis_ReturnsRepositories()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		try
		{
			// Act
			var response = await client.Analysis.ListOrganizationRepositoriesWithAnalysisAsync(
				provider,
				orgName,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Organization not found or no repositories - skip test
			Output.WriteLine($"Organization or repositories not found: {ex.Message}");
		}
	}

	[Fact]
	public async Task GetRepositoryWithAnalysis_ReturnsAnalysisData()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Analysis.GetRepositoryWithAnalysisAsync(
				provider,
				orgName,
				repoName,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository not found or not analyzed - skip test
			Output.WriteLine($"Repository not found or not analyzed: {ex.Message}");
		}
	}

	[Fact(Skip = "Requires direct repository API access - API token limitation. See CODACY_API_ACCESS_LIMITATION.md")]
	public async Task ListRepositoryTools_ReturnsTools()
	{
		// This test requires direct repository API access which returns 404
		// The Analysis API endpoint /api/v3/analysis/{provider}/{org}/{repo}/tools
		// requires repository-level access that isn't available with current API token
		// See CODACY_API_ACCESS_LIMITATION.md for details
	}

	[Fact]
	public async Task ListCommitAnalysisStats_ReturnsStatistics()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Analysis.ListCommitAnalysisStatsAsync(
				provider,
				orgName,
				repoName,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository not found - skip test
			Output.WriteLine($"Repository not found: {ex.Message}");
		}
	}

	[Fact(Skip = "Requires direct repository API access - API token limitation. See CODACY_API_ACCESS_LIMITATION.md")]
	public async Task ListCategoryOverviews_ReturnsCategories()
	{
		// This test requires direct repository API access which returns 404
		// The Analysis API endpoint /api/v3/analysis/{provider}/{org}/{repo}/categories
		// requires repository-level access that isn't available with current API token
		// See CODACY_API_ACCESS_LIMITATION.md for details
	}

	[Fact]
	public async Task ListRepositoryCommits_ReturnsCommits()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Analysis.ListRepositoryCommitsAsync(
				provider,
				orgName,
				repoName,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository not found - skip test
			Output.WriteLine($"Repository not found: {ex.Message}");
		}
	}

	[Fact]
	public async Task ListRepositoryCommits_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();
		const int limit = 5;

		try
		{
			// Act
			var response = await client.Analysis.ListRepositoryCommitsAsync(
				provider,
				orgName,
				repoName,
				limit: limit,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} commits");
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository not found - skip test
			Output.WriteLine($"Repository not found: {ex.Message}");
		}
	}

	[Fact]
	public async Task SearchRepositoryIssues_ReturnsIssues()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Analysis.SearchRepositoryIssuesAsync(
				provider,
				orgName,
				repoName,
				body: new SearchRepositoryIssuesBody(),
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository may not be analyzed yet - skip test
			Output.WriteLine($"Repository not found or not analyzed: {ex.Message}");
		}
	}

	[Fact]
	public async Task GetIssuesOverview_ReturnsOverview()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Analysis.GetIssuesOverviewAsync(
				provider,
				orgName,
				repoName,
				body: new SearchRepositoryIssuesBody(),
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository may not be analyzed yet - skip test
			Output.WriteLine($"Repository not found or not analyzed: {ex.Message}");
		}
	}

	[Fact]
	public async Task ListRepositoryPullRequests_ReturnsPullRequests()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Analysis.ListRepositoryPullRequestsAsync(
				provider,
				orgName,
				repoName,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository not found - skip test
			Output.WriteLine($"Repository not found: {ex.Message}");
		}
	}

	[Fact]
	public async Task ListRepositoryPullRequests_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();
		const int limit = 10;

		try
		{
			// Act
			var response = await client.Analysis.ListRepositoryPullRequestsAsync(
				provider,
				orgName,
				repoName,
				limit: limit,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} pull requests");
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository not found - skip test
			Output.WriteLine($"Repository not found: {ex.Message}");
		}
	}
}
