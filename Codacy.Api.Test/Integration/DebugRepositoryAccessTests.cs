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

		LogApiCallDetails(provider, orgName, repoName);
		CheckUrlEncoding(repoName);

		var repo = await FindRepositoryInOrgListAsync(client, provider, orgName, repoName);
		if (repo != null)
		{
			await TestDirectAccessAsync(client, provider, orgName, repo);
		}
	}

	private void LogApiCallDetails(Provider provider, string orgName, string repoName)
	{
		Output.WriteLine("=== Debug Repository API Access ===\n");
		Output.WriteLine($"Provider: {provider}");
		Output.WriteLine($"Organization: {orgName}");
		Output.WriteLine($"Repository: {repoName}");
		Output.WriteLine($"\nExpected URL: /api/v3/repositories/{provider}/{orgName}/{repoName}");
		Output.WriteLine($"Full URL: https://app.codacy.com/api/v3/repositories/{provider}/{orgName}/{repoName}");
	}

	private void CheckUrlEncoding(string repoName)
	{
		var encoded = Uri.EscapeDataString(repoName);
		if (encoded != repoName)
		{
			Output.WriteLine($"\n⚠ Repository name needs URL encoding!");
			Output.WriteLine($"Original: {repoName}");
			Output.WriteLine($"Encoded: {encoded}");
		}
	}

	private async Task<Repository?> FindRepositoryInOrgListAsync(
		CodacyClient client,
		Provider provider,
		string orgName,
		string repoName)
	{
		Output.WriteLine($"\n=== Repository from Organization List ===");
		var repos = await client.Organizations.ListOrganizationRepositoriesAsync(
			provider, orgName, null, 100, null, null, null, null, CancellationToken);

		var repo = repos.Data.FirstOrDefault(r => r.Name == repoName);
		if (repo != null)
		{
			LogFoundRepository(repo);
			return repo;
		}

		Output.WriteLine($"✗ Repository '{repoName}' NOT found in organization list");
		return null;
	}

	private void LogFoundRepository(Repository repo)
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
	}

	private async Task TestDirectAccessAsync(
		CodacyClient client,
		Provider provider,
		string orgName,
		Repository repo)
	{
		Output.WriteLine($"\n=== Attempting Direct Access ===");

		try
		{
			_ = await client.Repositories.GetRepositoryAsync(
				repo.Provider ?? provider,
				repo.Owner ?? orgName,
				repo.Name!,
				CancellationToken);
			Output.WriteLine($"✓ SUCCESS! Repository accessible");
		}
		catch (Refit.ApiException ex)
		{
			LogAccessFailure(ex, repo, orgName);
		}
	}

	private void LogAccessFailure(Refit.ApiException ex, Repository repo, string orgName)
	{
		Output.WriteLine($"✗ FAILED: {ex.StatusCode} - {ex.Message}");

		if (!string.IsNullOrEmpty(repo.RemoteIdentifier))
		{
			Output.WriteLine($"\n→ Note: Repository has RemoteIdentifier: {repo.RemoteIdentifier}");
			Output.WriteLine($"   Maybe we need to use this instead of the name?");
		}

		Output.WriteLine($"\n→ The issue:");
		Output.WriteLine($"   Repository is in organization list (AddedState: {repo.AddedState})");
		Output.WriteLine($"   But direct API access returns 404");
		Output.WriteLine($"   This suggests:");
		Output.WriteLine($"   1. API token lacks 'repository:read' permission");
		Output.WriteLine($"   2. Repository is in 'Following' not 'Added' state");
		Output.WriteLine($"   3. Codacy API requires repository to be explicitly 'Added'");

		if (repo.AddedState == AddedState.Following)
		{
			Output.WriteLine($"\n→→ SOLUTION: Repository is in 'Following' state!");
			Output.WriteLine($"   Go to: https://app.codacy.com/gh/{orgName}/{repo.Name}");
			Output.WriteLine($"   Click 'Add repository' or 'Enable analysis'");
			Output.WriteLine($"   This will change it from 'Following' → 'Added'");
		}
	}
}

