namespace Codacy.Api.Test.Integration;

/// <summary>
/// Setup tests for Phase 2 - adds test repository to Codacy
/// </summary>
[Trait("Category", "Integration")]
[Trait("Category", "Setup")]
public class Phase2SetupTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task AddTestRepositoryToCodacy_AddsRepository_Successfully()
	{
		// Arrange
		using var client = GetClient();
		using var testDataManager = GetTestDataManager();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		// First check if repository already exists
		if (await CheckExistingRepositoryAsync(testDataManager, orgName, repoName))
		{
			return;
		}

		Output.WriteLine($"Adding repository {orgName}/{repoName} to Codacy...");

		try
		{
			await AddAndVerifyRepositoryAsync(client, testDataManager, provider, orgName, repoName);
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
		{
			await HandleConflictResponseAsync(testDataManager, ex);
		}
	}

	private async Task<bool> CheckExistingRepositoryAsync(TestDataManager testDataManager, string orgName, string repoName)
	{
		var exists = await testDataManager.VerifyRepositoryExistsAsync(CancellationToken);

		if (exists)
		{
			Output.WriteLine($"✓ Repository {orgName}/{repoName} already exists in Codacy");
			var status = await testDataManager.GetEnvironmentStatusAsync(CancellationToken);
			Output.WriteLine($"Status: {status}");
			return true;
		}

		return false;
	}

	private async Task AddAndVerifyRepositoryAsync(
		CodacyClient client,
		TestDataManager testDataManager,
		Provider provider,
		string orgName,
		string repoName)
	{
		var addBody = new AddRepositoryBody
		{
			RepositoryFullPath = $"{orgName}/{repoName}",
			Provider = provider
		};

		var repository = await testDataManager.ExecuteWithRetryAsync(
			() => client.Repositories.AddRepositoryAsync(addBody, CancellationToken),
			CancellationToken);

		// Assert
		repository.Should().NotBeNull();
		repository.Name.Should().Be(repoName);
		repository.Owner.Should().Be(orgName);
		repository.Provider.Should().Be(provider);

		LogRepositoryAdded(repository);
		await WaitForAnalysisAsync(testDataManager);
	}

	private void LogRepositoryAdded(Repository repository)
	{
		Output.WriteLine($"✓ Repository added successfully: {repository.FullPath}");
		Output.WriteLine($"  - Repository ID: {repository.RepositoryId}");
		Output.WriteLine($"  - Provider: {repository.Provider}");
		Output.WriteLine($"  - Added State: {repository.AddedState}");
	}

	private async Task WaitForAnalysisAsync(TestDataManager testDataManager)
	{
		Output.WriteLine("\nWaiting for initial analysis to complete (max 5 minutes)...");

		var analyzed = await testDataManager.WaitForRepositoryAnalysisAsync(
			maxWaitTime: TimeSpan.FromMinutes(5),
			pollingInterval: TimeSpan.FromSeconds(15),
			cancellationToken: CancellationToken);

		if (analyzed)
		{
			Output.WriteLine("✓ Repository analyzed successfully");
			var status = await testDataManager.GetEnvironmentStatusAsync(CancellationToken);
			Output.WriteLine($"\nFinal Status: {status}");
		}
		else
		{
			Output.WriteLine("⚠ Repository added but analysis not completed within timeout");
			Output.WriteLine("  Analysis may still be in progress. Check Codacy UI.");
		}
	}

	private async Task HandleConflictResponseAsync(TestDataManager testDataManager, Refit.ApiException ex)
	{
		Output.WriteLine($"Repository may already exist (conflict): {ex.Message}");

		var exists = await testDataManager.VerifyRepositoryExistsAsync(CancellationToken);
		if (exists)
		{
			Output.WriteLine("✓ Repository confirmed to exist after conflict response");
		}
		else
		{
			Output.WriteLine("⚠ API inconsistency: Conflict returned but repository not found via GetRepository.");
			Output.WriteLine("  This may indicate the repository exists in a different state or the API is eventually consistent.");
			Output.WriteLine("  Please verify the repository status manually in the Codacy UI.");
		}
	}

	[Fact]
	public async Task VerifyPhase2Prerequisites_ChecksAllRequirements()
	{
		// Arrange
		using var testDataManager = GetTestDataManager();

		Output.WriteLine("=== Phase 2 Prerequisites Check ===\n");

		// Act & Assert
		var status = await testDataManager.GetEnvironmentStatusAsync(CancellationToken);

		LogPrerequisiteHeader(status);
		LogPrerequisiteChecks(status);
		LogPrerequisiteSummary(status);

		// Final assertion
		status.IsReady.Should().BeTrue(
			"Environment must be ready before running Phase 2 tests. " +
			"See test output for specific issues.");
	}

	private void LogPrerequisiteHeader(TestEnvironmentStatus status)
	{
		Output.WriteLine($"Repository: {status.Organization}/{status.Repository}");
		Output.WriteLine($"Provider: {status.Provider}");
		Output.WriteLine(string.Empty);
	}

	private void LogPrerequisiteChecks(TestEnvironmentStatus status)
	{
		LogCheck("1. Repository Exists", status.RepositoryExists,
			"Run: dotnet test --filter \"FullyQualifiedName~AddTestRepositoryToCodacy\"");

		LogCheck("2. Repository Analyzed", status.HasAnalysisData,
			status.RepositoryExists ? "Wait for analysis to complete (5-10 minutes)" : null);

		LogCheck($"3. Branches Configured", status.HasBranches,
			status.RepositoryExists ? "Check GitHub repository has branches" : null,
			$"({status.BranchCount} branches)");

		LogCheck($"4. Files Indexed", status.FileCount > 0,
			status.HasAnalysisData ? "Wait for file indexing to complete" : null,
			$"({status.FileCount} files)");
	}

	private void LogCheck(string checkName, bool passed, string? failureHint = null, string? suffix = null)
	{
		var statusText = passed ? "✓ PASS" : "✗ FAIL";
		var suffixText = suffix != null ? $" {suffix}" : "";
		Output.WriteLine($"{checkName}: {statusText}{suffixText}");

		if (!passed && failureHint != null)
		{
			Output.WriteLine($"   → {failureHint}");
		}
	}

	private void LogPrerequisiteSummary(TestEnvironmentStatus status)
	{
		Output.WriteLine(string.Empty);
		Output.WriteLine($"Overall Status: {(status.IsReady ? "✓ READY FOR PHASE 2" : "✗ NOT READY")}");

		if (!status.IsReady)
		{
			Output.WriteLine("\n⚠ Prerequisites not met. Please complete setup before running Phase 2 tests.");
			Output.WriteLine("See PHASE_2_SETUP_GUIDE.md for detailed instructions.");
		}
		else
		{
			Output.WriteLine("\n✓ All prerequisites met! Ready to run Phase 2 tests.");
			Output.WriteLine("\nNext steps:");
			Output.WriteLine("  1. Run Repository API tests: dotnet test --filter \"FullyQualifiedName~RepositoriesApiTests\"");
			Output.WriteLine("  2. Run Analysis API tests: dotnet test --filter \"FullyQualifiedName~AnalysisApiTests\"");
		}
	}

	[Fact]
	public async Task ListAvailableRepositories_ShowsRepositoriesInOrganization()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		Output.WriteLine($"=== Repositories in {orgName} Organization ===\n");

		try
		{
			// Act
			var response = await client.Organizations.ListOrganizationRepositoriesAsync(provider, orgName, null, 50, null, null, null, null, CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();

			Output.WriteLine($"Total repositories: {response.Data.Count}");
			Output.WriteLine(string.Empty);

			foreach (var repo in response.Data)
			{
				var isTestRepo = repo.Name == GetTestRepository();
				var marker = isTestRepo ? "? TEST REPO" : "";

				Output.WriteLine($"{(isTestRepo ? "?" : " ")} {repo.Name} {marker}");
				Output.WriteLine($"   Full Path: {repo.FullPath}");
				Output.WriteLine($"   Added State: {repo.AddedState}");
				Output.WriteLine($"   Last Updated: {repo.LastUpdated}");

				if (repo.DefaultBranch != null)
				{
					Output.WriteLine($"   Default Branch: {repo.DefaultBranch.Name}");
				}

				Output.WriteLine(string.Empty);
			}

			// Check if test repository is in the list
			var testRepoExists = response.Data.Any(r => r.Name == GetTestRepository());

			if (testRepoExists)
			{
				Output.WriteLine($"? Test repository '{GetTestRepository()}' found in organization");
			}
			else
			{
				Output.WriteLine($"? Test repository '{GetTestRepository()}' NOT found in organization");
				Output.WriteLine("   Run: dotnet test --filter \"FullyQualifiedName~AddTestRepositoryToCodacy\"");
			}
		}
		catch (Refit.ApiException ex)
		{
			Output.WriteLine($"Error listing repositories: {ex.Message}");
			throw;
		}
	}
}
