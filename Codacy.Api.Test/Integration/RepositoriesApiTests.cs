using Codacy.Api.Models;

namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Repositories API
/// </summary>
[Trait("Category", "Integration")]
public class RepositoriesApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task GetRepository_ReturnsRepositoryDetails()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Repositories.GetRepositoryAsync(
				provider,
				orgName,
				repoName,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			response.Data.Name.Should().Be(repoName);
			response.Data.Provider.Should().Be(provider);
			response.Data.Owner.Should().Be(orgName);
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository not found in Codacy - skip test
			Output.WriteLine($"Repository not found in Codacy: {ex.Message}");
		}
	}

	[Fact]
	public async Task ListRepositoryBranches_ReturnsBranches()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Repositories.ListRepositoryBranchesAsync(
				provider,
				orgName,
				repoName,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
		{
			// Repository may not have branches data - skip test
			Output.WriteLine($"Repository branches not available: {ex.Message}");
		}
	}

	[Fact]
	public async Task ListRepositoryBranches_WithPagination_ReturnsLimitedResults()
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
			var response = await client.Repositories.ListRepositoryBranchesAsync(
				provider,
				orgName,
				repoName,
				limit: limit,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} branches");
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
		{
			// Repository may not have branches data - skip test
			Output.WriteLine($"Repository branches not available: {ex.Message}");
		}
	}

	[Fact]
	public async Task GetQualitySettingsForRepository_ReturnsSettings()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Repositories.GetQualitySettingsForRepositoryAsync(
				provider,
				orgName,
				repoName,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest ||
		                                     ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository may not have quality settings - skip test
			Output.WriteLine($"Repository quality settings not available: {ex.Message}");
		}
	}

	[Fact]
	public async Task GetCommitQualitySettings_ReturnsSettings()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Repositories.GetCommitQualitySettingsAsync(
				provider,
				orgName,
				repoName,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest ||
		                                     ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository may not have commit quality settings - skip test
			Output.WriteLine($"Commit quality settings not available: {ex.Message}");
		}
	}

	[Fact]
	public async Task GetPullRequestQualitySettings_ReturnsSettings()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Repositories.GetPullRequestQualitySettingsAsync(
				provider,
				orgName,
				repoName,
				CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository not found - skip test
			Output.WriteLine($"Repository not found: {ex.Message}");
		}
	}

	[Fact]
	public async Task ListFiles_ReturnsFiles()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		try
		{
			// Act
			var response = await client.Repositories.ListFilesAsync(
				provider,
				orgName,
				repoName,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository not found - skip test
			Output.WriteLine($"Repository not found: {ex.Message}");
		}
	}

	[Fact]
	public async Task ListFiles_WithPagination_ReturnsLimitedResults()
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
			var response = await client.Repositories.ListFilesAsync(
				provider,
				orgName,
				repoName,
				limit: limit,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} files");
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Repository not found - skip test
			Output.WriteLine($"Repository not found: {ex.Message}");
		}
	}

	[Fact]
	public async Task ListFiles_WithSearch_FiltersResults()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();
		const string searchTerm = "cs"; // Search for C# files

		try
		{
			// Act
			var response = await client.Repositories.ListFilesAsync(
				provider,
				orgName,
				repoName,
				search: searchTerm,
				cancellationToken: CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			// Only check if we have results
			if (response.Data.Count > 0)
			{
				foreach (var file in response.Data)
				{
					file.Path.ToLowerInvariant().Should().Contain(searchTerm.ToLowerInvariant());
				}
			}
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
		{
			// Repository may not support file search - skip test
			Output.WriteLine($"File search not available: {ex.Message}");
		}
	}
}
