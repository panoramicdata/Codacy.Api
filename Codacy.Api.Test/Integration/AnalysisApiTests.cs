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
			var response = await client
				.Analysis
				.ListOrganizationRepositoriesWithAnalysisAsync(
					provider,
					orgName,
					null,
					null,
					null,
					null,
					null,
					CancellationToken);

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
			var response = await client.Analysis.GetRepositoryWithAnalysisAsync(provider, orgName, repoName, null, CancellationToken);

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

	[Fact]
	public async Task ListRepositoryTools_ReturnsTools()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		// Act
		var response = await client.Analysis.ListRepositoryToolsAsync(
			provider, orgName, repoName, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Should().NotBeEmpty();
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
			var response = await client.Analysis.ListCommitAnalysisStatsAsync(provider, orgName, repoName, null, 31, CancellationToken);

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
	public async Task ListCategoryOverviews_ReturnsCategories()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		// Act
		var response = await client.Analysis.ListCategoryOverviewsAsync(
			provider, orgName, repoName, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
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
			var response = await client.Analysis.ListRepositoryCommitsAsync(provider, orgName, repoName, null, null, null, CancellationToken);

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
			var response = await client.Analysis.ListRepositoryCommitsAsync(provider, orgName, repoName, null, null, limit, CancellationToken);

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
			var response = await client
				.Analysis
				.SearchRepositoryIssuesAsync(provider, orgName, repoName, new SearchRepositoryIssuesBody(), null, null, CancellationToken);

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
			var response = await client.Analysis.ListRepositoryPullRequestsAsync(provider, orgName, repoName, null, null, null, false, CancellationToken);

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
			var response = await client.Analysis.ListRepositoryPullRequestsAsync(provider, orgName, repoName, limit, null, null, false, CancellationToken);

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