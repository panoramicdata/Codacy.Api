namespace Codacy.Api.Test.Integration;

/// <summary>
/// Example integration tests demonstrating TestDataManager usage
/// </summary>
[Trait("Category", "Integration")]
[Trait("Category", "Example")]
public class TestDataManagerExampleTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task TestDataManager_VerifyRepositoryExists_ReturnsStatus()
	{
		// Arrange
		using var testDataManager = GetTestDataManager();

		// Act
		var exists = await testDataManager.VerifyRepositoryExistsAsync(CancellationToken);

		// Assert
		Output.WriteLine($"Repository exists: {exists}");
		
		// If repository doesn't exist, this test provides guidance
		if (!exists)
		{
			Output.WriteLine("Repository not found in Codacy.");
			Output.WriteLine("Please ensure the repository is added to Codacy:");
			Output.WriteLine($"1. Go to https://app.codacy.com");
			Output.WriteLine($"2. Add repository: {GetTestOrganization()}/{GetTestRepository()}");
			Output.WriteLine($"3. Wait for initial analysis to complete");
		}
	}

	[Fact]
	public async Task TestDataManager_GetEnvironmentStatus_ReturnsCompleteStatus()
	{
		// Arrange
		using var testDataManager = GetTestDataManager();

		// Act
		var status = await testDataManager.GetEnvironmentStatusAsync(CancellationToken);

		// Assert
		status.Should().NotBeNull();
		Output.WriteLine(status.ToString());

		// Detailed status reporting
		Output.WriteLine($"Repository Exists: {status.RepositoryExists}");
		Output.WriteLine($"Has Analysis Data: {status.HasAnalysisData}");
		Output.WriteLine($"Has Branches: {status.HasBranches}");
		Output.WriteLine($"Branch Count: {status.BranchCount}");
		Output.WriteLine($"File Count: {status.FileCount}");
		Output.WriteLine($"Environment Ready: {status.IsReady}");

		if (!status.IsReady)
		{
			Output.WriteLine("\nTest environment is not ready. Please check:");
			if (!status.RepositoryExists)
			{
				Output.WriteLine("- Repository needs to be added to Codacy");
			}
			if (!status.HasAnalysisData)
			{
				Output.WriteLine("- Repository needs to be analyzed");
			}
			if (!status.HasBranches)
			{
				Output.WriteLine("- Repository needs to have branches configured");
			}
		}
	}

	[Fact]
	public async Task TestDataManager_GetTestRepository_WithRetry_ReturnsRepository()
	{
		// Arrange
		using var testDataManager = GetTestDataManager();

		// Act - This demonstrates retry logic for transient failures
		var repository = await testDataManager.GetTestRepositoryAsync(CancellationToken);

		// Assert
		if (repository != null)
		{
			repository.Name.Should().Be(GetTestRepository());
			repository.Owner.Should().Be(GetTestOrganization());
			Output.WriteLine($"Retrieved repository: {repository.Name}");
		}
		else
		{
			Output.WriteLine("Repository not found in Codacy");
		}
	}

	[Fact]
	public async Task TestDataManager_GetBranches_ReturnsBranchList()
	{
		// Arrange
		using var testDataManager = GetTestDataManager();

		// Act
		var branches = await testDataManager.GetTestRepositoryBranchesAsync(
			cancellationToken: CancellationToken);

		// Assert
		branches.Should().NotBeNull();
		Output.WriteLine($"Found {branches.Count} branches");

		foreach (var branch in branches)
		{
			Output.WriteLine($"  - {branch.Name} (Default: {branch.IsDefault}, Enabled: {branch.IsEnabled})");
		}
	}

	[Fact]
	public async Task TestDataManager_GetFiles_ReturnsFileList()
	{
		// Arrange
		using var testDataManager = GetTestDataManager();

		// Act
		var files = await testDataManager.GetTestRepositoryFilesAsync(
			limit: 10,
			cancellationToken: CancellationToken);

		// Assert
		files.Should().NotBeNull();
		Output.WriteLine($"Found {files.Count} files (limited to 10)");

		foreach (var file in files)
		{
			Output.WriteLine($"  - {file.Path} (Grade: {file.GradeLetter}, Issues: {file.TotalIssues})");
		}
	}

	[Fact]
	public async Task TestDataManager_CleanupActions_ExecuteOnDispose()
	{
		// Arrange
		var cleanupExecuted = false;
		using var testDataManager = GetTestDataManager();

		// Act
		testDataManager.RegisterCleanupAction(() =>
		{
			cleanupExecuted = true;
			Output.WriteLine("Cleanup action executed");
		});

		// Assert - cleanup will execute when using block ends
		// This is just to demonstrate the cleanup mechanism
		testDataManager.Dispose();
		cleanupExecuted.Should().BeTrue();
	}

	[Fact(Skip = "Long running test - only run manually to verify wait functionality")]
	public async Task TestDataManager_WaitForAnalysis_WaitsForCompletion()
	{
		// Arrange
		using var testDataManager = GetTestDataManager();

		// Act
		var analyzed = await testDataManager.WaitForRepositoryAnalysisAsync(
			maxWaitTime: TimeSpan.FromMinutes(2),
			pollingInterval: TimeSpan.FromSeconds(10),
			cancellationToken: CancellationToken);

		// Assert
		Output.WriteLine($"Repository analyzed: {analyzed}");
	}
}
