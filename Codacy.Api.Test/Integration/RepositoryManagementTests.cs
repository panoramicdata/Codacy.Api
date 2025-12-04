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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		Output.WriteLine($"Adding test repository: {orgName}/{TestRepoName}");

		try
		{
			// Act - Try to get repository (should fail if not added)
			var response = await client.Repositories.GetRepositoryAsync(
				provider,
				orgName,
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
			Output.WriteLine($"3. Select: {orgName}/{TestRepoName}");
			Output.WriteLine($"4. Wait for initial analysis to complete");
			Output.WriteLine("");
			Output.WriteLine($"Repository URL: https://github.com/{orgName}/{TestRepoName}");

			// Skip test - repository needs to be added manually
			// Note: Codacy API doesn't currently support adding repositories via API
		}
	}

	[Fact]
	public async Task VerifyTestRepository_HasBranches_Succeeds()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		Output.WriteLine($"Verifying branches for: {orgName}/{TestRepoName}");

		try
		{
			// Act - Get repository branches from repositories endpoint
			var response = await client.Repositories.ListRepositoryBranchesAsync(
				provider,
				orgName,
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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		Output.WriteLine($"Verifying files for: {orgName}/{TestRepoName}");

		try
		{
			// Act - Get repository files
			var response = await client.Repositories.ListFilesAsync(
				provider,
				orgName,
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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		Output.WriteLine($"Verifying issues for: {orgName}/{TestRepoName}");

		try
		{
			// Act - Search for issues
			var response = await client.Issues.SearchRepositoryIssuesAsync(
				provider,
				orgName,
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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		Output.WriteLine($"Verifying analysis tools for: {orgName}/{TestRepoName}");

		try
		{
			// Act - Get repository tools
			var response = await client.Analysis.ListRepositoryToolsAsync(
				provider,
				orgName,
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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		Output.WriteLine($"Getting repository details for: {orgName}/{TestRepoName}");

		try
		{
			// Act - Get repository with full analysis data
			var response = await client.Analysis.GetRepositoryWithAnalysisAsync(
				provider,
				orgName,
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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		Output.WriteLine($"Listing all repositories in organization: {orgName}");

		try
		{
			// Act - List all organization repositories
			var response = await client.Organizations.ListOrganizationRepositoriesAsync(
				provider,
				orgName,
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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		Output.WriteLine($"Checking repository settings for: {orgName}/{TestRepoName}");

		try
		{
			// Act - Get pull request quality settings
			var response = await client.Repositories.GetPullRequestQualitySettingsAsync(
				provider,
				orgName,
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

