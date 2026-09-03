namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Issues API
/// </summary>
[Trait("Category", "Integration")]
public class IssuesApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Theory]
	[InlineData(null)]
	[InlineData(10)]
	public Task SearchRepositoryIssues_ReturnsIssues(int? limit)
		=> RunWhenRepositoryAvailableAsync(async () =>
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
			response.ShouldHavePageOfAtMost(limit, r => r.Data);
		});

	[Theory]
	[InlineData(null)]
	[InlineData(5)]
	public Task SearchRepositoryIgnoredIssues_ReturnsIgnoredIssues(int? limit)
		=> RunWhenRepositoryAvailableAsync(async () =>
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
			response.ShouldHavePageOfAtMost(limit, r => r.Data);
		});

	[Fact]
	public Task GetIssuesOverview_ReturnsOverview()
		=> RunWhenRepositoryAvailableAsync(async () =>
		{
			// Act
			var response = await Client.Issues.GetIssuesOverviewAsync(
				TestProvider,
				TestOrganization,
				TestRepository,
				filter: new SearchRepositoryIssuesBody(),
				cancellationToken: CancellationToken);

			// Assert
			var overview = response.ShouldHaveData(r => r.Data);
			overview.Counts.Should().NotBeNull();
		});
}
