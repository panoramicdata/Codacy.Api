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
		// Act
		var response = await ListPeopleAsync();

		// Assert
		var people = response.ShouldHaveNonEmptyData(r => r.Data);
		people.Should().AllSatisfy(person => person.Email.Should().NotBeNull());
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
		if (allPeople.Count == 0)
		{
			return; // Nothing to search for
		}

		var email = allPeople[0].Email;
		var searchTerm = email[..Math.Min(3, email.Length)];

		// Act
		var response = await ListPeopleAsync(search: searchTerm);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	[Fact]
	public async Task PeopleSuggestionsForOrganization_ReturnsSuggestions()
	{
		// Act
		var response = await Client.People.PeopleSuggestionsForOrganizationAsync(
			TestProvider, TestOrganization, null, null, null, CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	private Task<ListResponse<OrganizationPerson>> ListPeopleAsync(int? limit = null, string? search = null)
		=> Client.People.ListPeopleFromOrganizationAsync(
			TestProvider, TestOrganization, null, limit, search, null, CancellationToken);
}
