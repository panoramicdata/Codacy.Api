namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Coverage API
/// </summary>
[Trait("Category", "Integration")]
public class CoverageApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task GetRepositoryPullRequestCoverage_ReturnsCoverageData()
	{
		await VerifyPullRequestCoverageAsync(async pullRequestNumber =>
		{
			var response = await Client.Coverage.GetRepositoryPullRequestCoverageAsync(
				TestProvider,
				TestOrganization,
				TestRepository,
				pullRequestNumber,
				CancellationToken);

			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}, "Pull request coverage");
	}

	[Fact]
	public async Task GetRepositoryPullRequestFilesCoverage_ReturnsFileCoverage()
	{
		await VerifyPullRequestCoverageAsync(async pullRequestNumber =>
		{
			var response = await Client.Coverage.GetRepositoryPullRequestFilesCoverageAsync(
				TestProvider,
				TestOrganization,
				TestRepository,
				pullRequestNumber,
				CancellationToken);

			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}, "Pull request file coverage");
	}

	[Fact]
	public async Task GetPullRequestCoverageReports_ReturnsReportStatus()
	{
		await VerifyPullRequestCoverageAsync(async pullRequestNumber =>
		{
			var response = await Client.Coverage.GetPullRequestCoverageReportsAsync(
				TestProvider,
				TestOrganization,
				TestRepository,
				pullRequestNumber,
				CancellationToken);

			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}, "Pull request coverage reports");
	}

	private async Task VerifyPullRequestCoverageAsync(
		Func<int, Task> verifyCoverage,
		string coverageDescription)
	{
		var pullRequests = await Client.Analysis.ListRepositoryPullRequestsAsync(
			TestProvider, TestOrganization, TestRepository, 1, null, null, false, CancellationToken);

		if (pullRequests.Data.Count == 0)
		{
			Output.WriteLine("No pull requests available for coverage testing");
			return;
		}

		var pullRequestNumber = pullRequests.Data[0].PullRequest.Number;
		await RunWhenAvailableAsync(
			() => verifyCoverage(pullRequestNumber),
			$"{coverageDescription} not available",
			System.Net.HttpStatusCode.BadRequest,
			System.Net.HttpStatusCode.NotFound);
	}
}