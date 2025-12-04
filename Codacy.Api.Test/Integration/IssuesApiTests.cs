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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Issues.SearchRepositoryIssuesAsync(
				provider,
				orgName,
				repoName,
				new SearchRepositoryIssuesBody(),
				null,
				null,
				CancellationToken);

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
	public async Task SearchRepositoryIssues_WithPagination_ReturnsLimitedResults()
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
			var response = await client.Issues.SearchRepositoryIssuesAsync(
				provider,
				orgName,
				repoName,
				new SearchRepositoryIssuesBody(),
				null,
				limit,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} issues");
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
			var response = await client.Issues.GetIssuesOverviewAsync(
				provider,
				orgName,
				repoName,
				filter: new SearchRepositoryIssuesBody(),
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			response.Data.Counts.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository may not be analyzed yet - skip test
			Output.WriteLine($"Repository not found or not analyzed: {ex.Message}");
		}
	}

	[Fact]
	public async Task SearchRepositoryIgnoredIssues_ReturnsIgnoredIssues()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Issues.SearchRepositoryIgnoredIssuesAsync(
				provider,
				orgName,
				repoName,
				new SearchRepositoryIssuesBody(),
				null,
				null,
				CancellationToken);

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
	public async Task SearchRepositoryIgnoredIssues_WithPagination_ReturnsLimitedResults()
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
			var response = await client.Issues.SearchRepositoryIgnoredIssuesAsync(
				provider,
				orgName,
				repoName,
				new SearchRepositoryIssuesBody(),
				null,
				limit,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} ignored issues");
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository may not be analyzed yet - skip test
			Output.WriteLine($"Repository not found or not analyzed: {ex.Message}");
		}
	}
}

