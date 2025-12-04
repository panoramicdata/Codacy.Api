using Codacy.Api.Models;
using System.Web;

namespace Codacy.Api.Test.Integration;

/// <summary>
/// Debug test to understand the 404 issue
/// </summary>
[Trait("Category", "Integration")]
[Trait("Category", "Debug")]
public class DebugRepositoryAccessTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task DebugRepositoryAccess_ShowsExactApiCall()
	{
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		Output.WriteLine("=== Debug Repository API Access ===\n");
		Output.WriteLine($"Provider: {provider}");
		Output.WriteLine($"Organization: {orgName}");
		Output.WriteLine($"Repository: {repoName}");
		Output.WriteLine($"\nExpected URL: /api/v3/repositories/{provider}/{orgName}/{repoName}");
		Output.WriteLine($"Full URL: https://app.codacy.com/api/v3/repositories/{provider}/{orgName}/{repoName}");
		
		// Check if repo name needs encoding
		var encoded = Uri.EscapeDataString(repoName);
		if (encoded != repoName)
		{
			Output.WriteLine($"\n? Repository name needs URL encoding!");
			Output.WriteLine($"Original: {repoName}");
			Output.WriteLine($"Encoded: {encoded}");
		}

		// Get repo from organization list to see its exact properties
		Output.WriteLine($"\n=== Repository from Organization List ===");
		var repos = await client.Organizations.ListOrganizationRepositoriesAsync(
			provider,
			orgName,
			limit: 100,
			cancellationToken: CancellationToken);

		var repo = repos.Data.FirstOrDefault(r => r.Name == repoName);
		if (repo != null)
		{
			Output.WriteLine($"Found in organization list:");
			Output.WriteLine($"  Name: {repo.Name}");
			Output.WriteLine($"  Owner: {repo.Owner}");
			Output.WriteLine($"  Provider: {repo.Provider}");
			Output.WriteLine($"  FullPath: '{repo.FullPath}'");
			Output.WriteLine($"  RemoteIdentifier: {repo.RemoteIdentifier}");
			Output.WriteLine($"  RepositoryId: {repo.RepositoryId}");
			Output.WriteLine($"  AddedState: {repo.AddedState}");
			Output.WriteLine($"  Visibility: {repo.Visibility}");

			// Try accessing with exact values from org list
			Output.WriteLine($"\n=== Attempting Direct Access ===");
			
			try
			{
				_ = await client.Repositories.GetRepositoryAsync(
					repo.Provider ?? provider,
					repo.Owner ?? orgName,
					repo.Name!,
					CancellationToken);
				Output.WriteLine($"? SUCCESS! Repository accessible");
			}
			catch (Refit.ApiException ex)
			{
				Output.WriteLine($"? FAILED: {ex.StatusCode} - {ex.Message}");
				
				if (!string.IsNullOrEmpty(repo.RemoteIdentifier))
				{
					Output.WriteLine($"\n? Note: Repository has RemoteIdentifier: {repo.RemoteIdentifier}");
					Output.WriteLine($"   Maybe we need to use this instead of the name?");
				}

				Output.WriteLine($"\n? The issue:");
				Output.WriteLine($"   Repository is in organization list (AddedState: {repo.AddedState})");
				Output.WriteLine($"   But direct API access returns 404");
				Output.WriteLine($"   This suggests:");
				Output.WriteLine($"   1. API token lacks 'repository:read' permission");
				Output.WriteLine($"   2. Repository is in 'Following' not 'Added' state");
				Output.WriteLine($"   3. Codacy API requires repository to be explicitly 'Added'");
				
				if (repo.AddedState == AddedState.Following)
				{
					Output.WriteLine($"\n?? SOLUTION: Repository is in 'Following' state!");
					Output.WriteLine($"   Go to: https://app.codacy.com/gh/{orgName}/{repoName}");
					Output.WriteLine($"   Click 'Add repository' or 'Enable analysis'");
					Output.WriteLine($"   This will change it from 'Following' ? 'Added'");
				}
			}
		}
		else
		{
			Output.WriteLine($"? Repository '{repoName}' NOT found in organization list");
		}
	}
}
