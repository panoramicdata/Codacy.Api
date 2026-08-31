namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Issues API
/// </summary>
[Trait("Category", "Integration")]
public class IssuesApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task SearchRepositoryIssues_ReturnsIssues()
	{
		// Arrange

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Issues.SearchRepositoryIssuesAsync(
					TestProvider,
					TestOrganization,
					TestRepository,
					new SearchRepositoryIssuesBody(),
					null,
					null,
					CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
			},
			"Repository not found or not analyzed");
	}

	[Fact]
	public async Task SearchRepositoryIssues_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 10;

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Issues.SearchRepositoryIssuesAsync(
					TestProvider,
					TestOrganization,
					TestRepository,
					new SearchRepositoryIssuesBody(),
					null,
					limit,
					CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
				(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} issues");
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
				var response = await Client.Issues.GetIssuesOverviewAsync(
					TestProvider,
					TestOrganization,
					TestRepository,
					filter: new SearchRepositoryIssuesBody(),
					cancellationToken: CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
				response.Data.Counts.Should().NotBeNull();
			},
			"Repository not found or not analyzed");
	}

	[Fact]
	public async Task SearchRepositoryIgnoredIssues_ReturnsIgnoredIssues()
	{
		// Arrange

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Issues.SearchRepositoryIgnoredIssuesAsync(
					TestProvider,
					TestOrganization,
					TestRepository,
					new SearchRepositoryIssuesBody(),
					null,
					null,
					CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
			},
			"Repository not found or not analyzed");
	}

	[Fact]
	public async Task SearchRepositoryIgnoredIssues_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 5;

		await RunWhenAvailableAsync(
			async () =>
			{
				// Act
				var response = await Client.Issues.SearchRepositoryIgnoredIssuesAsync(
					TestProvider,
					TestOrganization,
					TestRepository,
					new SearchRepositoryIssuesBody(),
					null,
					limit,
					CancellationToken);

				// Assert
				response.Should().NotBeNull();
				response.Data.Should().NotBeNull();
				(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} ignored issues");
			},
			"Repository not found or not analyzed");
	}
}