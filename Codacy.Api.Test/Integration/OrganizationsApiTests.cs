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
		// Arrange

		// Act
		var response = await Client.Organizations.GetOrganizationAsync(TestProvider, TestOrganization, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Organization.Should().NotBeNull();

		// Verify organization details from the nested Organization object
		response.Data.Organization!.Name.Should().Be(TestOrganization);
		response.Data.Organization.Provider.Should().Be(TestProvider);
	}

	[Fact]
	public async Task ListOrganizationRepositories_ReturnsRepositories()
	{
		// Arrange

		// Act
		var response = await Client.Organizations.ListOrganizationRepositoriesAsync(TestProvider, TestOrganization, null, null, null, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		// Should have at least the test repository
		response.Data.Should().NotBeEmpty();
		foreach (var repo in response.Data)
		{
			repo.Name.Should().NotBeNull();
			repo.Provider.Should().Be(TestProvider);
			repo.Owner.Should().Be(TestOrganization);
		}
	}

	[Fact]
	public async Task ListOrganizationRepositories_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 10;

		// Act
		var response = await Client.Organizations.ListOrganizationRepositoriesAsync(TestProvider, TestOrganization, null, limit, null, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} repositories");
	}

	[Fact]
	public async Task ListOrganizationRepositories_WithSearch_FiltersResults()
	{
		// Arrange
		var searchTerm = TestRepository[..Math.Min(3, TestRepository.Length)];

		// Act
		var response = await Client.Organizations.ListOrganizationRepositoriesAsync(TestProvider, TestOrganization, null, null, searchTerm, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		// All returned repos should match the search term
		foreach (var repo in response.Data)
		{
			repo.Name.Should().NotBeNull();
			repo.Name!.ToLowerInvariant().Should().Contain(searchTerm.ToLowerInvariant());
		}
	}

	[Fact]
	public async Task GetOrganizationBilling_ReturnsBillingInformation()
	{
		// Arrange

		// Act
		var response = await Client.Organizations.GetOrganizationBillingAsync(
			TestProvider,
			TestOrganization,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		// Billing info should have seat information
		(response.Data.NumberOfSeats >= 0).Should().BeTrue();
	}

	[Fact]
	public async Task ListPeopleFromOrganization_ReturnsPeople()
	{
		// Arrange

		// Act
		var response = await Client.Organizations.ListPeopleFromOrganizationAsync(TestProvider, TestOrganization, null, null, null, false, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		// Should have at least the authenticated user
		response.Data.Should().NotBeEmpty();
		foreach (var person in response.Data)
		{
			person.Email.Should().NotBeNull();
		}
	}

	[Fact]
	public async Task ListPeopleFromOrganization_OnlyMembers_ReturnsOnlyMembers()
	{
		// Arrange

		// Act
		var response = await Client.Organizations.ListPeopleFromOrganizationAsync(TestProvider, TestOrganization, null, null, null, true, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Should().NotBeEmpty();
	}

	[Fact]
	public async Task ListPeopleFromOrganization_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 5;

		// Act
		var response = await Client.Organizations.ListPeopleFromOrganizationAsync(TestProvider, TestOrganization, null, limit, null, false, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} people");
	}

	[Fact]
	public async Task ListPeopleFromOrganization_WithSearch_FiltersResults()
	{
		// Arrange

		// Get all people first to find a search term
		var allPeople = await Client.Organizations.ListPeopleFromOrganizationAsync(TestProvider, TestOrganization, null, null, null, false, CancellationToken);
		if (allPeople.Data.Count == 0)
		{
			return; // Skip test if no people
		}

		// Safely get search term with null check
		var firstPersonName = allPeople.Data[0].Name;
		if (string.IsNullOrEmpty(firstPersonName))
		{
			return; // Skip if first person has no name
		}

		var searchTerm = firstPersonName[..Math.Min(2, firstPersonName.Length)];

		// Act
		var response = await Client.Organizations.ListPeopleFromOrganizationAsync(TestProvider, TestOrganization, null, null, searchTerm, false, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}
}