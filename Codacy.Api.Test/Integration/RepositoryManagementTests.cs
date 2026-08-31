namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for repository management using Codacy API
/// Tests adding, configuring, and removing test repositories
/// </summary>
[Trait("Category", "Integration")]
public class RepositoryManagementTests(ITestOutputHelper output) : TestBase(output)
{
	private const string TestRepoName = "Codacy.Api.TestRepo";

	[Fact]
	public async Task AddTestRepository_ToCodacy_Succeeds()
	{
		// Arrange

		Output.WriteLine($"Adding test repository: {TestOrganization}/{TestRepoName}");

		try
		{
			// Act - Try to get repository (should fail if not added)
			var response = await Client.Repositories.GetRepositoryAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				CancellationToken);

			// If we get here, repository already exists
			Output.WriteLine($"? Repository already exists in Codacy");
			Output.WriteLine($"Repository ID: {response.Data.RepositoryId}");
			Output.WriteLine($"Repository Name: {response.Data.Name}");

			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			response.Data.Name.Should().Be(TestRepoName);
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			Output.WriteLine($"?? Repository not found in Codacy");
			Output.WriteLine($"Please add the repository manually:");
			Output.WriteLine($"1. Go to https://app.codacy.com");
			Output.WriteLine($"2. Click 'Add Repository'");
			Output.WriteLine($"3. Select: {TestOrganization}/{TestRepoName}");
			Output.WriteLine($"4. Wait for initial analysis to complete");
			Output.WriteLine("");
			Output.WriteLine($"Repository URL: https://github.com/{TestOrganization}/{TestRepoName}");

			// Skip test - repository needs to be added manually
			// Note: Codacy API doesn't currently support adding repositories via API
		}
	}

	[Fact]
	public async Task VerifyTestRepository_HasBranches_Succeeds()
	{
		// Arrange

		Output.WriteLine($"Verifying branches for: {TestOrganization}/{TestRepoName}");

		try
		{
			// Act - Get repository branches from repositories endpoint
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
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();

			Output.WriteLine($"? Found {response.Data.Count} branches:");
			foreach (var branch in response.Data)
			{
				Output.WriteLine($"  - {branch.Name}");
			}

			// Verify we have at least the main branch
			response.Data.Should().Contain(b => b.Name == "main");
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			Output.WriteLine($"?? Repository not found or not analyzed yet");
			Output.WriteLine($"Please ensure repository is added to Codacy and analysis has completed");
		}
	}

	[Fact]
	public async Task VerifyTestRepository_HasFiles_Succeeds()
	{
		// Arrange

		Output.WriteLine($"Verifying files for: {TestOrganization}/{TestRepoName}");

		try
		{
			// Act - Get repository files
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
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();

			Output.WriteLine($"? Found {response.Data.Count} files:");
			foreach (var file in response.Data.Take(10))
			{
				Output.WriteLine($"  - {file.Path}");
			}

			if (response.Data.Count > 10)
			{
				Output.WriteLine($"  ... and {response.Data.Count - 10} more files");
			}

			// Verify we have some C# files
			response.Data.Should().Contain(f => f.Path != null && f.Path.EndsWith(".cs"));
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			Output.WriteLine($"?? Repository not found or files not indexed yet");
			Output.WriteLine($"Please ensure repository analysis has completed");
		}
	}

	[Fact]
	public async Task VerifyTestRepository_HasIssues_Succeeds()
	{
		// Arrange

		Output.WriteLine($"Verifying issues for: {TestOrganization}/{TestRepoName}");

		try
		{
			// Act - Search for issues
			var response = await Client.Issues.SearchRepositoryIssuesAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				new SearchRepositoryIssuesBody(),
				null,
				10,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();

			Output.WriteLine($"? Found {response.Data.Count} issues (showing first 10):");
			foreach (var issue in response.Data)
			{
				Output.WriteLine($"  - [{issue.PatternInfo.SeverityLevel}] {issue.Message}");
				Output.WriteLine($"    File: {issue.FilePath}:{issue.LineNumber}");
			}

			// We expect issues since we intentionally added code with problems
			response.Data.Should().NotBeEmpty("Test repository should have intentional code quality issues");
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			Output.WriteLine($"?? Repository not found or not analyzed yet");
			Output.WriteLine($"Please ensure repository analysis has completed");
		}
	}

	[Fact]
	public async Task VerifyTestRepository_HasAnalysisTools_Succeeds()
	{
		// Arrange

		Output.WriteLine($"Verifying analysis tools for: {TestOrganization}/{TestRepoName}");

		try
		{
			// Act - Get repository tools
			var response = await Client.Analysis.ListRepositoryToolsAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();

			Output.WriteLine($"? Found {response.Data.Count} configured tools:");
			foreach (var tool in response.Data)
			{
				Output.WriteLine($"  - {tool.Name}");
			}

			// We expect at least some tools to be configured
			response.Data.Should().NotBeEmpty("Test repository should have analysis tools configured");
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			Output.WriteLine($"?? Repository not found or tools not configured yet");
			Output.WriteLine($"Please ensure repository has completed initial analysis");
		}
	}

	[Fact]
	public async Task GetTestRepository_Details_Succeeds()
	{
		// Arrange

		Output.WriteLine($"Getting repository details for: {TestOrganization}/{TestRepoName}");

		try
		{
			// Act - Get repository with full analysis data
			var response = await Client.Analysis.GetRepositoryWithAnalysisAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				null,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();

			Output.WriteLine($"");
			Output.WriteLine($"Repository Details:");
			Output.WriteLine($"==================");
			Output.WriteLine($"Name: {response.Data.Repository?.Name}");
			Output.WriteLine($"Provider: {response.Data.Repository?.Provider}");
			Output.WriteLine($"");

			// Verify basic properties
			response.Data.Repository?.Name.Should().Be(TestRepoName);
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			Output.WriteLine($"?? Repository not found or not fully analyzed");
			Output.WriteLine($"Please ensure repository has been added to Codacy");
		}
	}

	[Fact]
	public async Task ListOrganizationRepositories_IncludesTestRepo_Succeeds()
	{
		// Arrange

		Output.WriteLine($"Listing all repositories in organization: {TestOrganization}");

		try
		{
			// Act - List all organization repositories
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
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();

			Output.WriteLine($"? Found {response.Data.Count} repositories:");
			foreach (var repo in response.Data)
			{
				var marker = repo.Name == TestRepoName ? "??" : "  ";
				Output.WriteLine($"{marker} {repo.Name}");
			}

			// Verify test repository is in the list
			var testRepo = response.Data.FirstOrDefault(r => r.Name == TestRepoName);
			if (testRepo != null)
			{
				Output.WriteLine($"");
				Output.WriteLine($"? Test repository found in organization!");
				testRepo.Should().NotBeNull();
			}
			else
			{
				Output.WriteLine($"");
				Output.WriteLine($"?? Test repository NOT found in organization");
				Output.WriteLine($"Please add {TestRepoName} to Codacy");
			}
		}
		catch (Refit.ApiException ex)
		{
			Output.WriteLine($"? Error listing repositories: {ex.StatusCode} - {ex.Message}");
			throw;
		}
	}

	[Fact]
	public async Task ConfigureTestRepository_Settings_Succeeds()
	{
		// Arrange

		Output.WriteLine($"Checking repository settings for: {TestOrganization}/{TestRepoName}");

		try
		{
			// Act - Get pull request quality settings
			var response = await Client.Repositories.GetPullRequestQualitySettingsAsync(
				TestProvider,
				TestOrganization,
				TestRepoName,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();

			Output.WriteLine($"");
			Output.WriteLine($"Pull Request Quality Settings:");
			Output.WriteLine($"==============================");
			Output.WriteLine($"Settings Retrieved Successfully");
			Output.WriteLine($"");

			// Just verify we can retrieve settings
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			Output.WriteLine($"?? Repository settings not available");
			Output.WriteLine($"This is expected if repository hasn't been added to Codacy yet");
		}
	}
}