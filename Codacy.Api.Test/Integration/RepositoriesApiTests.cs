using Codacy.Api.Models;

namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Repositories API
/// </summary>
/// <remarks>
/// These were long skipped as an "API token limitation", on the strength of a 404 from
/// <c>/api/v3/repositories/{provider}/{org}/{repo}</c>. The 404 meant the route did not exist:
/// repositories are addressed under their organization. The routes work, so the tests run.
/// </remarks>
[Trait("Category", "Integration")]
public class RepositoriesApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task GetRepository_ReturnsRepositoryDetails()
	{
		// Act
		var response = await Client.Repositories.GetRepositoryAsync(TestProvider, TestOrganization, TestRepository, CancellationToken);

		// Assert
		var repository = response.ShouldHaveData(r => r.Data);
		repository.Name.Should().Be(TestRepository);
		repository.Provider.Should().Be(TestProvider);
	}

	[Fact]
	public async Task ListRepositoryBranches_ReturnsBranches()
	{
		// Act
		var response = await ListBranchesAsync();

		// Assert
		var branches = response.ShouldHaveNonEmptyData(r => r.Data);
		branches.Should().AllSatisfy(branch => branch.Name.Should().NotBeNullOrWhiteSpace());

		// Every repository Codacy analyses has exactly one default branch.
		branches.Count(branch => branch.IsDefault).Should().Be(1);
	}

	[Fact]
	public async Task ListRepositoryBranches_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 1;

		// Act
		var response = await ListBranchesAsync(limit: limit);

		// Assert
		response.ShouldHavePageOfAtMost(limit, r => r.Data);
	}

	[Fact]
	public async Task ListRepositoryBranches_EnabledOnly_ReturnsOnlyEnabledBranches()
	{
		// A bool reaching the query string as "True" rather than "true" was silently ignored by the
		// API, so this filter used to return every branch.

		// Act
		var response = await ListBranchesAsync(enabled: true);

		// Assert
		var branches = response.ShouldHaveData(r => r.Data);
		branches.Should().AllSatisfy(branch => branch.IsEnabled.Should().BeTrue());
	}

	[Fact]
	public async Task GetCommitQualitySettings_ReturnsSettings()
	{
		// Act
		var response = await Client.Repositories.GetCommitQualitySettingsAsync(
			TestProvider, TestOrganization, TestRepository, CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	[Fact]
	public async Task GetPullRequestQualitySettings_ReturnsSettings()
	{
		// Act
		var response = await Client.Repositories.GetPullRequestQualitySettingsAsync(
			TestProvider, TestOrganization, TestRepository, CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	[Fact]
	public async Task ListFiles_ReturnsFiles()
	{
		// Act
		var response = await ListFilesAsync();

		// Assert
		var files = response.ShouldHaveNonEmptyData(r => r.Data);
		files.Should().AllSatisfy(file => file.Path.Should().NotBeNullOrWhiteSpace());
	}

	[Fact]
	public async Task ListFiles_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 3;

		// Act
		var response = await ListFilesAsync(limit: limit);

		// Assert
		response.ShouldHavePageOfAtMost(limit, r => r.Data);
	}

	[Fact]
	public async Task ListFiles_WithSearch_FiltersResults()
	{
		// Arrange
		const string searchTerm = "cs";

		// Act
		var response = await ListFilesAsync(search: searchTerm);

		// Assert
		var files = response.ShouldHaveData(r => r.Data);
		files.Should().AllSatisfy(file => file.Path.Should().Contain(searchTerm));
	}

	private Task<BranchListResponse> ListBranchesAsync(bool? enabled = null, int? limit = null)
		=> Client.Repositories.ListRepositoryBranchesAsync(
			TestProvider, TestOrganization, TestRepository, enabled, null, limit, null, null, null, CancellationToken);

	private Task<FileListResponse> ListFilesAsync(string? search = null, int? limit = null)
		=> Client.Repositories.ListFilesAsync(
			TestProvider, TestOrganization, TestRepository, null, search, null, null, null, limit, CancellationToken);
}
