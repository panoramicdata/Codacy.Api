namespace Codacy.Api.Test.Integration;

/// <summary>
/// The people-listing tests shared by the two client surfaces that expose
/// <c>/api/v3/organizations/{provider}/{organizationName}/people</c>.
/// </summary>
/// <remarks>
/// <see cref="PeopleApiTests"/> reaches the endpoint through <c>Client.People</c> and
/// <see cref="OrganizationsApiTests"/> through <c>Client.Organizations</c>. They are the same
/// endpoint and have to behave identically, so both surfaces run these tests rather than each
/// carrying its own copy of them.
/// </remarks>
public abstract class OrganizationPeopleTestsBase(ITestOutputHelper output) : TestBase(output)
{
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
		var response = await ListPeopleAsync(limit);

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
		var response = await ListPeopleAsync(searchTerm);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	/// <summary>
	/// Lists the organization's people through the client surface under test.
	/// </summary>
	protected abstract Task<ListResponse<OrganizationPerson>> ListPeopleAsync(
		int? limit,
		string? search,
		bool? onlyMembers);

	protected Task<ListResponse<OrganizationPerson>> ListPeopleAsync() =>
		ListPeopleAsync(null, null, null);

	protected Task<ListResponse<OrganizationPerson>> ListPeopleAsync(int? limit) =>
		ListPeopleAsync(limit, null, null);

	protected Task<ListResponse<OrganizationPerson>> ListPeopleAsync(string? search) =>
		ListPeopleAsync(null, search, null);

	protected Task<ListResponse<OrganizationPerson>> ListPeopleAsync(bool? onlyMembers) =>
		ListPeopleAsync(null, null, onlyMembers);
}
