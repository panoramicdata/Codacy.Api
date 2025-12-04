namespace Codacy.Api.Test.Integration;

/// <summary>
/// Quick diagnostic to see actual repository state
/// </summary>
[Trait("Category", "Integration")]
[Trait("Category", "Diagnostic")]
public class QuickDiagnosticTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task ShowRepositoryDetails()
	{
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// List all repositories to find the test repo
		var repos = await client.Organizations.ListOrganizationRepositoriesAsync(
			provider,
			orgName,
			null,
			100,
			null,
			null,
			null,
			null,
			CancellationToken);

		Output.WriteLine("=== All Repositories ===\n");

		foreach (var repo in repos.Data)
		{
			Output.WriteLine($"Repository: {repo.Name}");
			Output.WriteLine($"  - Added State: {repo.AddedState}");
			Output.WriteLine($"  - Repository ID: {repo.RepositoryId}");

			if (repo.Name == "Codacy.Api.TestRepo" || repo.Name?.Contains("TestRepo") == true)
			{
				Output.WriteLine($"  ***** THIS IS THE TEST REPO *****");
				Output.WriteLine($"  - Full Path: {repo.FullPath}");
				Output.WriteLine($"  - Default Branch: {repo.DefaultBranch?.Name}");
				Output.WriteLine($"  - Last Updated: {repo.LastUpdated}");

				// Try to access it directly
				Output.WriteLine("\n  Trying direct access...");
				try
				{
					_ = await client.Repositories.GetRepositoryAsync(
						provider,
						orgName,
						repo.Name!,
						CancellationToken);
					Output.WriteLine($"  ? Direct access works!");
				}
				catch (Exception ex)
				{
					Output.WriteLine($"  ? Direct access failed: {ex.Message}");
				}
			}
			Output.WriteLine(string.Empty);
		}
	}
}

