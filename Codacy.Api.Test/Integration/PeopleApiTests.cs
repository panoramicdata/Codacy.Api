namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for People API
/// </summary>
[Trait("Category", "Integration")]
public class PeopleApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task ListPeopleFromOrganization_ReturnsPeople()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.People.ListPeopleFromOrganizationAsync(provider, orgName, null, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Should().NotBeEmpty();
		foreach (var person in response.Data)
		{
			person.Email.Should().NotBeNull();
		}
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
		var response = await client.People.ListPeopleFromOrganizationAsync(provider, orgName, null, limit, null, null, CancellationToken);

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
		var allPeople = await client.People.ListPeopleFromOrganizationAsync(provider, orgName, null, null, null, null, CancellationToken);

		if (allPeople.Data.Count == 0)
		{
			return; // Skip test if no people
		}

		var searchTerm = allPeople.Data[0].Email[..Math.Min(3, allPeople.Data[0].Email.Length)];

		// Act
		var response = await client.People.ListPeopleFromOrganizationAsync(provider, orgName, null, null, searchTerm, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task PeopleSuggestionsForOrganization_ReturnsSuggestions()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.People.PeopleSuggestionsForOrganizationAsync(provider, orgName, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}
}

