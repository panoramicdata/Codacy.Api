namespace Codacy.Api.Test.Integration;

/// <summary>
/// Diagnostic tests to understand repository state
/// </summary>
[Trait("Category", "Integration")]
[Trait("Category", "Diagnostic")]
public class Phase2DiagnosticTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task DiagnoseRepositoryState_ShowsDetailedInformation()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		Output.WriteLine("=== Repository State Diagnostic ===\n");

		// Test 1: Check organization list
		Output.WriteLine("1. Checking organization repository list...");
		try
		{
			var orgRepos = await client.Organizations.ListOrganizationRepositoriesAsync(provider, orgName, null, 100, null, null, null, null, CancellationToken);

			var testRepo = orgRepos.Data.FirstOrDefault(r => r.Name == repoName);

			if (testRepo != null)
			{
				Output.WriteLine($"   ? Found in organization list");
				Output.WriteLine($"   - Name: {testRepo.Name}");
				Output.WriteLine($"   - Full Path: {testRepo.FullPath}");
				Output.WriteLine($"   - Repository ID: {testRepo.RepositoryId}");
				Output.WriteLine($"   - Added State: {testRepo.AddedState}");
				Output.WriteLine($"   - Last Updated: {testRepo.LastUpdated}");
				Output.WriteLine($"   - Visibility: {testRepo.Visibility}");
				Output.WriteLine($"   - Remote Identifier: {testRepo.RemoteIdentifier}");

				if (testRepo.DefaultBranch != null)
				{
					Output.WriteLine($"   - Default Branch: {testRepo.DefaultBranch.Name}");
					Output.WriteLine($"   - Branch Enabled: {testRepo.DefaultBranch.IsEnabled}");
				}
				else
				{
					Output.WriteLine($"   - Default Branch: NULL (not set)");
				}

				if (testRepo.Languages != null && testRepo.Languages.Count > 0)
				{
					Output.WriteLine($"   - Languages: {string.Join(", ", testRepo.Languages)}");
				}

				Output.WriteLine($"\n   ** Added State: {testRepo.AddedState} **");

				if (testRepo.AddedState == AddedState.Following)
				{
					Output.WriteLine("   ? Repository is in 'Following' state, not 'Added' state!");
					Output.WriteLine("   ? This means it's tracked but not fully integrated");
					Output.WriteLine("   ? Go to Codacy UI and change from 'Following' to 'Added'");
				}
			}
			else
			{
				Output.WriteLine($"   ? NOT found in organization list");
			}
		}
		catch (Exception ex)
		{
			Output.WriteLine($"   ? Error: {ex.Message}");
		}

		// Test 2: Check direct repository access
		Output.WriteLine($"\n2. Checking direct repository access...");
		try
		{
			var repo = await client.Repositories.GetRepositoryAsync(
				provider,
				orgName,
				repoName,
				CancellationToken);

			Output.WriteLine($"   ? Direct access successful");
			Output.WriteLine($"   - Repository ID: {repo.Data.RepositoryId}");
			Output.WriteLine($"   - Name: {repo.Data.Name}");
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			Output.WriteLine($"   ? Returns 404 Not Found");
			Output.WriteLine($"   ? Repository visible in org list but not accessible directly");
			Output.WriteLine($"   ? This usually means it's in 'Following' state");
		}
		catch (Exception ex)
		{
			Output.WriteLine($"   ? Error: {ex.Message}");
		}

		// Test 3: Try to follow the repository (if not already)
		Output.WriteLine($"\n3. Checking follow/add options...");
		try
		{
			var followResult = await client.Repositories.FollowRepositoryAsync(
				provider,
				orgName,
				repoName,
				CancellationToken);

			Output.WriteLine($"   ? Follow successful");
			Output.WriteLine($"   - Added State: {followResult.Data}");
		}
		catch (Refit.ApiException ex)
		{
			Output.WriteLine($"   Response: {ex.StatusCode} - {ex.Message}");
		}
		catch (Exception ex)
		{
			Output.WriteLine($"   Error: {ex.Message}");
		}

		Output.WriteLine($"\n=== Diagnosis Complete ===");
		Output.WriteLine($"\nRECOMMENDATION:");
		Output.WriteLine($"If repository is in 'Following' state:");
		Output.WriteLine($"1. Go to: https://app.codacy.com/gh/{orgName}/{repoName}");
		Output.WriteLine($"2. Look for an 'Add repository' or 'Enable analysis' button");
		Output.WriteLine($"3. Click it to change from 'Following' to 'Added'");
		Output.WriteLine($"4. Wait 2-3 minutes for changes to propagate");
		Output.WriteLine($"5. Re-run: dotnet test --filter \"FullyQualifiedName~VerifyPhase2Prerequisites\"");
	}
}

