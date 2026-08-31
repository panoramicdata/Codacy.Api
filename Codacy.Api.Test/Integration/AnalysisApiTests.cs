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

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client
					.Analysis
					.ListOrganizationRepositoriesWithAnalysisAsync(
						TestProvider,
						TestOrganization,
						null,
						null,
						null,
						null,
						null,
						CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
			},
			"Organization or repositories not found");
	}

	[Fact]
	public async Task GetRepositoryWithAnalysis_ReturnsAnalysisData()
	{
		// Arrange

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Analysis.GetRepositoryWithAnalysisAsync(TestProvider, TestOrganization, TestRepository, null, CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
			},
			"Repository not found or not analyzed");
	}

	[Fact]
	public async Task ListRepositoryTools_ReturnsTools()
	{
		// Arrange

		// Act
		var response = await Client.Analysis.ListRepositoryToolsAsync(
			TestProvider, TestOrganization, TestRepository, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Should().NotBeEmpty();
	}

	[Fact]
	public async Task ListCommitAnalysisStats_ReturnsStatistics()
	{
		// Arrange

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Analysis.ListCommitAnalysisStatsAsync(TestProvider, TestOrganization, TestRepository, null, 31, CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
			},
			"Repository not found");
	}

	[Fact]
	public async Task ListCategoryOverviews_ReturnsCategories()
	{
		// Arrange

		// Act
		var response = await Client.Analysis.ListCategoryOverviewsAsync(
			TestProvider, TestOrganization, TestRepository, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task ListRepositoryCommits_ReturnsCommits()
	{
		// Arrange

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Analysis.ListRepositoryCommitsAsync(TestProvider, TestOrganization, TestRepository, null, null, null, CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
			},
			"Repository not found");
	}

	[Fact]
	public async Task ListRepositoryCommits_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 5;

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Analysis.ListRepositoryCommitsAsync(TestProvider, TestOrganization, TestRepository, null, null, limit, CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
				(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} commits");
			},
			"Repository not found");
	}

	[Fact]
	public async Task SearchRepositoryIssues_ReturnsIssues()
	{
		// Arrange

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client
					.Analysis
					.SearchRepositoryIssuesAsync(TestProvider, TestOrganization, TestRepository, new SearchRepositoryIssuesBody(), null, null, CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
			},
			"Repository not found or not analyzed");
	}

	[Fact]
	public async Task GetIssuesOverview_ReturnsOverview()
	{
		// Arrange

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Analysis.GetIssuesOverviewAsync(
					TestProvider,
					TestOrganization,
					TestRepository,
					body: new SearchRepositoryIssuesBody(),
					cancellationToken: CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
			},
			"Repository not found or not analyzed");
	}

	[Fact]
	public async Task ListRepositoryPullRequests_ReturnsPullRequests()
	{
		// Arrange

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Analysis.ListRepositoryPullRequestsAsync(TestProvider, TestOrganization, TestRepository, null, null, null, false, CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
			},
			"Repository not found");
	}

	[Fact]
	public async Task ListRepositoryPullRequests_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 10;

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Analysis.ListRepositoryPullRequestsAsync(TestProvider, TestOrganization, TestRepository, limit, null, null, false, CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
				(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} pull requests");
			},
			"Repository not found");
	}
}