namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Analysis API
/// </summary>
[Trait("Category", "Integration")]
public class AnalysisApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public Task ListOrganizationRepositoriesWithAnalysis_ReturnsRepositories()
		=> RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Analysis.ListOrganizationRepositoriesWithAnalysisAsync(
					TestProvider, TestOrganization, null, null, null, null, null, CancellationToken);

				// Assert
				response.ShouldHaveData(r => r.Data);
			},
			"Organization or repositories not found");

	[Fact]
	public Task GetRepositoryWithAnalysis_ReturnsAnalysisData()
		=> RunWhenRepositoryAvailableAsync(async () =>
		{
			// Act
			var response = await Client.Analysis.GetRepositoryWithAnalysisAsync(
				TestProvider, TestOrganization, TestRepository, null, CancellationToken);

			// Assert
			response.ShouldHaveData(r => r.Data);
		});

	[Fact]
	public async Task ListRepositoryTools_ReturnsTools()
	{
		// Act
		var response = await Client.Analysis.ListRepositoryToolsAsync(
			TestProvider, TestOrganization, TestRepository, CancellationToken);

		// Assert
		response.ShouldHaveNonEmptyData(r => r.Data);
	}

	[Fact]
	public Task ListCommitAnalysisStats_ReturnsStatistics()
		=> RunWhenRepositoryAvailableAsync(async () =>
		{
			// Act
			var response = await Client.Analysis.ListCommitAnalysisStatsAsync(
				TestProvider, TestOrganization, TestRepository, null, 31, CancellationToken);

			// Assert
			response.ShouldHaveData(r => r.Data);
		});

	[Fact]
	public async Task ListCategoryOverviews_ReturnsCategories()
	{
		// Act
		var response = await Client.Analysis.ListCategoryOverviewsAsync(
			TestProvider, TestOrganization, TestRepository, null, CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	[Theory]
	[InlineData(null)]
	[InlineData(5)]
	public Task ListRepositoryCommits_ReturnsCommits(int? limit)
		=> RunWhenRepositoryAvailableAsync(async () =>
		{
			// Act
			var response = await Client.Analysis.ListRepositoryCommitsAsync(
				TestProvider, TestOrganization, TestRepository, null, null, limit, CancellationToken);

			// Assert
			response.ShouldHavePageOfAtMost(limit, r => r.Data);
		});

	[Fact]
	public Task SearchRepositoryIssues_ReturnsIssues()
		=> RunWhenRepositoryAvailableAsync(async () =>
		{
			// Act
			var response = await Client.Analysis.SearchRepositoryIssuesAsync(
				TestProvider, TestOrganization, TestRepository, new SearchRepositoryIssuesBody(), null, null, CancellationToken);

			// Assert
			response.ShouldHaveData(r => r.Data);
		});

	[Fact]
	public Task GetIssuesOverview_ReturnsOverview()
		=> RunWhenRepositoryAvailableAsync(async () =>
		{
			// Act
			var response = await Client.Analysis.GetIssuesOverviewAsync(
				TestProvider,
				TestOrganization,
				TestRepository,
				body: new SearchRepositoryIssuesBody(),
				cancellationToken: CancellationToken);

			// Assert
			response.ShouldHaveData(r => r.Data);
		});

	[Theory]
	[InlineData(null)]
	[InlineData(10)]
	public Task ListRepositoryPullRequests_ReturnsPullRequests(int? limit)
		=> RunWhenRepositoryAvailableAsync(async () =>
		{
			// Act
			var response = await Client.Analysis.ListRepositoryPullRequestsAsync(
				TestProvider, TestOrganization, TestRepository, limit, null, null, false, CancellationToken);

			// Assert
			response.ShouldHavePageOfAtMost(limit, r => r.Data);
		});
}
