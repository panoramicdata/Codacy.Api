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
		// Arrange

		// Act
		var response = await Client.Repositories.GetRepositoryAsync(TestProvider, TestOrganization, TestRepository, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Name.Should().Be(TestRepository);
		response.Data.Provider.Should().Be(TestProvider);
	}

	[Fact]
	public async Task ListRepositoryBranches_ReturnsBranches()
	{
		// Arrange

		// Act
		var response = await Client.Repositories.ListRepositoryBranchesAsync(
			TestProvider, TestOrganization, TestRepository, null, null, null, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Should().NotBeEmpty();
		response.Data!.Should().AllSatisfy(branch => branch.Name.Should().NotBeNullOrWhiteSpace());

		// Every repository Codacy analyses has exactly one default branch.
		response.Data.Count(branch => branch.IsDefault).Should().Be(1);
	}

	[Fact]
	public async Task ListRepositoryBranches_WithPagination_ReturnsLimitedResults()
	{
		// Arrange

		// Act
		var response = await Client.Repositories.ListRepositoryBranchesAsync(
			TestProvider, TestOrganization, TestRepository, null, null, 1, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data!.Count.Should().BeLessThanOrEqualTo(1);
	}

	[Fact]
	public async Task ListRepositoryBranches_EnabledOnly_ReturnsOnlyEnabledBranches()
	{
		// A bool reaching the query string as "True" rather than "true" was silently ignored by the
		// API, so this filter used to return every branch.

		// Arrange

		// Act
		var response = await Client.Repositories.ListRepositoryBranchesAsync(
			TestProvider, TestOrganization, TestRepository, true, null, null, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data!.Should().AllSatisfy(branch => branch.IsEnabled.Should().BeTrue());
	}

	[Fact]
	public async Task GetCommitQualitySettings_ReturnsSettings()
	{
		// Arrange

		// Act
		var response = await Client.Repositories.GetCommitQualitySettingsAsync(
			TestProvider, TestOrganization, TestRepository, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task GetPullRequestQualitySettings_ReturnsSettings()
	{
		// Arrange

		// Act
		var response = await Client.Repositories.GetPullRequestQualitySettingsAsync(
			TestProvider, TestOrganization, TestRepository, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task ListFiles_ReturnsFiles()
	{
		// Arrange

		// Act
		var response = await Client.Repositories.ListFilesAsync(
			TestProvider, TestOrganization, TestRepository, null, null, null, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Should().NotBeEmpty();
		response.Data!.Should().AllSatisfy(file => file.Path.Should().NotBeNullOrWhiteSpace());
	}

	[Fact]
	public async Task ListFiles_WithPagination_ReturnsLimitedResults()
	{
		// Arrange

		// Act
		var response = await Client.Repositories.ListFilesAsync(
			TestProvider, TestOrganization, TestRepository, null, null, null, null, null, 3, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data!.Count.Should().BeLessThanOrEqualTo(3);
	}

	[Fact]
	public async Task ListFiles_WithSearch_FiltersResults()
	{
		// Arrange

		// Act
		var response = await Client.Repositories.ListFilesAsync(
			TestProvider, TestOrganization, TestRepository, null, "cs", null, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data!.Should().AllSatisfy(file => file.Path.Should().Contain("cs"));
	}
}