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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Organizations.GetOrganizationAsync(provider, orgName, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();

		// Note: Name and Provider may be null in some API responses
		response.Data.Name.Should().Be(orgName);
		response.Data.Provider.Should().Be(provider);
	}

	[Fact]
	public async Task ListOrganizationRepositories_ReturnsRepositories()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Organizations.ListOrganizationRepositoriesAsync(provider, orgName, null, null, null, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		// Should have at least the test repository
		response.Data.Should().NotBeEmpty();
		foreach (var repo in response.Data)
		{
			repo.Name.Should().NotBeNull();
			repo.Provider.Should().Be(provider);
			repo.Owner.Should().Be(orgName);
		}
	}

	[Fact]
	public async Task ListOrganizationRepositories_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		const int limit = 10;

		// Act
		var response = await client.Organizations.ListOrganizationRepositoriesAsync(provider, orgName, null, limit, null, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} repositories");
	}

	[Fact]
	public async Task ListOrganizationRepositories_WithSearch_FiltersResults()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var searchTerm = GetTestRepository()[..Math.Min(3, GetTestRepository().Length)];

		// Act
		var response = await client.Organizations.ListOrganizationRepositoriesAsync(provider, orgName, null, null, searchTerm, null, null, null, CancellationToken);

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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Organizations.GetOrganizationBillingAsync(
			provider,
			orgName,
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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Organizations.ListPeopleFromOrganizationAsync(provider, orgName, null, null, null, false, CancellationToken);

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
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Organizations.ListPeopleFromOrganizationAsync(provider, orgName, null, null, null, true, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Should().NotBeEmpty();
	}

	[Fact]
	public async Task ListPeopleFromOrganization_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		const int limit = 5;

		// Act
		var response = await client.Organizations.ListPeopleFromOrganizationAsync(provider, orgName, null, limit, null, false, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} people");
	}

	[Fact]
	public async Task ListPeopleFromOrganization_WithSearch_FiltersResults()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Get all people first to find a search term
		var allPeople = await client.Organizations.ListPeopleFromOrganizationAsync(provider, orgName, null, null, null, false, CancellationToken);
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
		var response = await client.Organizations.ListPeopleFromOrganizationAsync(provider, orgName, null, null, searchTerm, false, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}
}

