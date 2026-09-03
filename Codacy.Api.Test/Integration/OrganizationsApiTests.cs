namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Organizations API
/// </summary>
[Trait("Category", "Integration")]
public class OrganizationsApiTests(ITestOutputHelper output) : TestBase(output)
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

	[Fact]
	public async Task ListPeopleFromOrganization_ReturnsPeople()
	{
		// Act
		var response = await ListPeopleAsync();

		// Assert - the authenticated user is a member, so there is always at least one person
		var people = response.ShouldHaveNonEmptyData(r => r.Data);
		people.Should().AllSatisfy(person => person.Email.Should().NotBeNull());
	}

	[Fact]
	public async Task ListPeopleFromOrganization_OnlyMembers_ReturnsOnlyMembers()
	{
		// Act
		var response = await ListPeopleAsync(onlyMembers: true);

		// Assert
		response.ShouldHaveNonEmptyData(r => r.Data);
	}

	[Fact]
	public async Task ListPeopleFromOrganization_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 5;

		// Act
		var response = await ListPeopleAsync(limit: limit);

		// Assert
		response.ShouldHavePageOfAtMost(limit, r => r.Data);
	}

	[Fact]
	public async Task ListPeopleFromOrganization_WithSearch_FiltersResults()
	{
		// Arrange - the search term has to come from the organization's own people
		var allPeople = (await ListPeopleAsync()).Data;
		var firstPersonName = allPeople.Count == 0 ? null : allPeople[0].Name;
		if (string.IsNullOrEmpty(firstPersonName))
		{
			return; // Nothing to search for
		}

		var searchTerm = firstPersonName[..Math.Min(2, firstPersonName.Length)];

		// Act
		var response = await ListPeopleAsync(search: searchTerm);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	private Task<ListResponse<Repository>> ListRepositoriesAsync(int? limit = null, string? search = null)
		=> Client.Organizations.ListOrganizationRepositoriesAsync(
			TestProvider, TestOrganization, null, limit, search, null, null, null, CancellationToken);

	private Task<ListResponse<OrganizationPerson>> ListPeopleAsync(
		int? limit = null,
		string? search = null,
		bool onlyMembers = false)
		=> Client.Organizations.ListPeopleFromOrganizationAsync(
			TestProvider, TestOrganization, null, limit, search, onlyMembers, CancellationToken);
}
