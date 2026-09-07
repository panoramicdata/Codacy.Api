namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Organizations API
/// </summary>
/// <remarks>
/// The people-listing tests are inherited: see <see cref="OrganizationPeopleTestsBase"/>.
/// </remarks>
[Trait("Category", "Integration")]
public class OrganizationsApiTests(ITestOutputHelper output) : OrganizationPeopleTestsBase(output)
{
	[Fact]
	public async Task GetOrganization_ReturnsOrganizationDetails()
	{
		// Act
		var response = await Client.Organizations.GetOrganizationAsync(TestProvider, TestOrganization, CancellationToken);

		// Assert - the details live on a nested Organization object
		var organization = response.ShouldHaveData(r => r.Data.Organization);
		organization.Name.Should().Be(TestOrganization);
		organization.Provider.Should().Be(TestProvider);
	}

	[Fact]
	public async Task ListOrganizationRepositories_ReturnsRepositories()
	{
		// Act
		var response = await ListRepositoriesAsync();

		// Assert - the organization owns at least the test repository
		var repositories = response.ShouldHaveNonEmptyData(r => r.Data);
		repositories.Should().AllSatisfy(repository =>
		{
			repository.Name.Should().NotBeNull();
			repository.Provider.Should().Be(TestProvider);
			repository.Owner.Should().Be(TestOrganization);
		});
	}

	[Fact]
	public async Task ListOrganizationRepositories_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 10;

		// Act
		var response = await ListRepositoriesAsync(limit: limit);

		// Assert
		response.ShouldHavePageOfAtMost(limit, r => r.Data);
	}

	[Fact]
	public async Task ListOrganizationRepositories_WithSearch_FiltersResults()
	{
		// Arrange
		var searchTerm = TestRepository[..Math.Min(3, TestRepository.Length)];

		// Act
		var response = await ListRepositoriesAsync(search: searchTerm);

		// Assert - every returned repository matches the search term
		var repositories = response.ShouldHaveData(r => r.Data);
		repositories.Should().AllSatisfy(repository =>
			repository.Name.Should().ContainEquivalentOf(searchTerm));
	}

	[Fact]
	public async Task GetOrganizationBilling_ReturnsBillingInformation()
	{
		// Act
		var response = await Client.Organizations.GetOrganizationBillingAsync(
			TestProvider,
			TestOrganization,
			cancellationToken: CancellationToken);

		// Assert
		var billing = response.ShouldHaveData(r => r.Data);
		billing.NumberOfSeats.Should().BeGreaterThanOrEqualTo(0);
	}

	/// <inheritdoc />
	/// <remarks>
	/// This surface declares <c>onlyMembers</c> as a plain <c>bool</c> rather than the
	/// <c>bool?</c> the People surface takes, so "unspecified" becomes the API's own default.
	/// </remarks>
	protected override Task<ListResponse<OrganizationPerson>> ListPeopleAsync(
		int? limit = null,
		string? search = null,
		bool? onlyMembers = null)
		=> Client.Organizations.ListPeopleFromOrganizationAsync(
			TestProvider, TestOrganization, null, limit, search, onlyMembers ?? false, CancellationToken);

	private Task<ListResponse<Repository>> ListRepositoriesAsync(int? limit = null, string? search = null)
		=> Client.Organizations.ListOrganizationRepositoriesAsync(
			TestProvider, TestOrganization, null, limit, search, null, null, null, CancellationToken);
}
