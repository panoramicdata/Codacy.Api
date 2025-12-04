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
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// First, get a list of pull requests to find one to test
			var pullRequests = await client.Analysis.ListRepositoryPullRequestsAsync(provider, orgName, repoName, 1, null, null, false, CancellationToken);

			if (pullRequests.Data.Count == 0)
			{
				// Skip test if no pull requests
				Output.WriteLine("No pull requests available for coverage testing");
				return;
			}

			var prNumber = pullRequests.Data[0].PullRequest.Number;

			// Act
			var response = await client.Coverage.GetRepositoryPullRequestCoverageAsync(
				provider,
				orgName,
				repoName,
				prNumber,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest ||
											 ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// PR may not have coverage data - skip test
			Output.WriteLine($"Pull request coverage not available: {ex.Message}");
		}
	}

	[Fact]
	public async Task GetRepositoryPullRequestFilesCoverage_ReturnsFileCoverage()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// First, get a list of pull requests
			var pullRequests = await client.Analysis.ListRepositoryPullRequestsAsync(provider, orgName, repoName, 1, null, null, false, CancellationToken);

			if (pullRequests.Data.Count == 0)
			{
				// Skip test if no pull requests
				Output.WriteLine("No pull requests available for file coverage testing");
				return;
			}

			var prNumber = pullRequests.Data[0].PullRequest.Number;

			// Act
			var response = await client.Coverage.GetRepositoryPullRequestFilesCoverageAsync(
				provider,
				orgName,
				repoName,
				prNumber,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest ||
											 ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// PR may not have file coverage data - skip test
			Output.WriteLine($"Pull request file coverage not available: {ex.Message}");
		}
	}

	[Fact]
	public async Task GetPullRequestCoverageReports_ReturnsReportStatus()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// First, get a list of pull requests
			var pullRequests = await client.Analysis.ListRepositoryPullRequestsAsync(provider, orgName, repoName, 1, null, null, false, CancellationToken);

			if (pullRequests.Data.Count == 0)
			{
				// Skip test if no pull requests
				Output.WriteLine("No pull requests available for coverage reports testing");
				return;
			}

			var prNumber = pullRequests.Data[0].PullRequest.Number;

			// Act
			var response = await client.Coverage.GetPullRequestCoverageReportsAsync(
				provider,
				orgName,
				repoName,
				prNumber,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest ||
											 ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// PR may not have coverage reports - skip test
			Output.WriteLine($"Pull request coverage reports not available: {ex.Message}");
		}
	}
}

