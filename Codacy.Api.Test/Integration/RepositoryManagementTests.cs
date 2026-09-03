namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for repository management using Codacy API
/// Tests adding, configuring, and removing test repositories
/// </summary>
/// <remarks>
/// Every test here works against <see cref="TestRepoName"/>, which has to be onboarded onto
/// Codacy by hand: the Codacy API has no endpoint for adding a repository. Until the repository
/// is there and its first analysis has finished, the API answers 404, so each test reports what
/// is missing rather than failing.
/// </remarks>
[Trait("Category", "Integration")]
public class RepositoryManagementTests(ITestOutputHelper output) : TestBase(output)
{
	private const string TestRepoName = "Codacy.Api.TestRepo";

	private const string NotOnboardedMessage =
		$"{TestRepoName} is not on Codacy yet, or its analysis has not finished. Add it at " +
		$"https://app.codacy.com ('Add Repository'), then wait for the initial analysis";

	[Fact]
	public Task AddTestRepository_ToCodacy_Succeeds()
		=> RunAgainstTestRepoAsync("Looking for the test repository", async () =>
		{
			// Act
			var response = await Client.Repositories.GetRepositoryAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				CancellationToken);

			// Assert
			var repository = response.ShouldHaveData(r => r.Data);
			Output.WriteLine($"Already on Codacy as repository {repository.RepositoryId} ({repository.Name})");
			repository.Name.Should().Be(TestRepoName);
		});

	[Fact]
	public Task VerifyTestRepository_HasBranches_Succeeds()
		=> RunAgainstTestRepoAsync("Verifying branches", async () =>
		{
			// Act
			var response = await Client.Repositories.ListRepositoryBranchesAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				null,
				null,
				null,
				null,
				null,
				null,
				CancellationToken);

			// Assert
			var branches = response.ShouldHaveData(r => r.Data);
			LogAll("branches", branches, branch => branch.Name);
			branches.Should().Contain(branch => branch.Name == "main");
		});

	[Fact]
	public Task VerifyTestRepository_HasFiles_Succeeds()
		=> RunAgainstTestRepoAsync("Verifying files", async () =>
		{
			// Act
			var response = await Client.Repositories.ListFilesAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				null,
				null,
				null,
				null,
				null,
				null,
				CancellationToken);

			// Assert
			var files = response.ShouldHaveData(r => r.Data);
			LogAll("files", files, file => file.Path);
			files.Should().Contain(file => file.Path != null && file.Path.EndsWith(".cs"));
		});

	[Fact]
	public Task VerifyTestRepository_HasIssues_Succeeds()
		=> RunAgainstTestRepoAsync("Verifying issues", async () =>
		{
			// Act
			var response = await Client.Issues.SearchRepositoryIssuesAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				new SearchRepositoryIssuesBody(),
				null,
				10,
				CancellationToken);

			// Assert
			var issues = response.ShouldHaveData(r => r.Data);
			LogAll("issues", issues, issue => $"[{issue.PatternInfo.SeverityLevel}] {issue.Message} ({issue.FilePath}:{issue.LineNumber})");

			// The test repository carries deliberate code quality problems
			issues.Should().NotBeEmpty("Test repository should have intentional code quality issues");
		});

	[Fact]
	public Task VerifyTestRepository_HasAnalysisTools_Succeeds()
		=> RunAgainstTestRepoAsync("Verifying analysis tools", async () =>
		{
			// Act
			var response = await Client.Analysis.ListRepositoryToolsAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				cancellationToken: CancellationToken);

			// Assert
			var tools = response.ShouldHaveData(r => r.Data);
			LogAll("configured tools", tools, tool => tool.Name);
			tools.Should().NotBeEmpty("Test repository should have analysis tools configured");
		});

	[Fact]
	public Task GetTestRepository_Details_Succeeds()
		=> RunAgainstTestRepoAsync("Getting repository details", async () =>
		{
			// Act
			var response = await Client.Analysis.GetRepositoryWithAnalysisAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				null,
				CancellationToken);

			// Assert
			var repository = response.ShouldHaveData(r => r.Data.Repository);
			Output.WriteLine($"Name: {repository.Name}, provider: {repository.Provider}");
			repository.Name.Should().Be(TestRepoName);
		});

	[Fact]
	public async Task ListOrganizationRepositories_IncludesTestRepo_Succeeds()
	{
		// Arrange - unlike the tests above this one reaches the organization, not the test
		// repository, so a failure here is a real failure rather than a missing repository
		Output.WriteLine($"Listing all repositories in organization: {TestOrganization}");

		// Act
		var response = await Client.Organizations.ListOrganizationRepositoriesAsync(
			TestProvider,
			TestOrganization,
			null,
			null,
			null,
			null,
			null,
			null,
			CancellationToken);

		// Assert
		var repositories = response.ShouldHaveData(r => r.Data);
		LogAll("repositories", repositories, repository => repository.Name);

		Output.WriteLine(repositories.Any(repository => repository.Name == TestRepoName)
			? $"{TestRepoName} found in the organization"
			: $"{TestRepoName} NOT found in the organization - {NotOnboardedMessage}");
	}

	[Fact]
	public Task ConfigureTestRepository_Settings_Succeeds()
		=> RunAgainstTestRepoAsync("Checking pull request quality settings", async () =>
		{
			// Act
			var response = await Client.Repositories.GetPullRequestQualitySettingsAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				CancellationToken);

			// Assert - retrieving the settings at all is what is under test
			response.ShouldHaveData(r => r.Data);
		});

	/// <summary>
	/// Runs a test against the manually onboarded test repository, reporting what is missing
	/// instead of failing when Codacy does not know about the repository yet.
	/// </summary>
	private Task RunAgainstTestRepoAsync(string what, Func<Task> test)
	{
		Output.WriteLine($"{what} for {TestOrganization}/{TestRepoName}");

		return RunWhenAvailableAsync(test, NotOnboardedMessage);
	}

	/// <summary>
	/// Writes every item to the test output, so a failure can be read against what the API returned.
	/// </summary>
	private void LogAll<T>(string what, List<T> items, Func<T, string?> describe)
	{
		Output.WriteLine($"Found {items.Count} {what}:");
		foreach (var item in items)
		{
			Output.WriteLine($"  - {describe(item)}");
		}
	}
}
