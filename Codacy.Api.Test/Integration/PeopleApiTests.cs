namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for People API
/// </summary>
/// <remarks>
/// The people-listing tests are inherited: see <see cref="OrganizationPeopleTestsBase"/>.
/// </remarks>
[Trait("Category", "Integration")]
public class PeopleApiTests(ITestOutputHelper output) : OrganizationPeopleTestsBase(output)
{
	[Fact]
	public async Task PeopleSuggestionsForOrganization_ReturnsSuggestions()
	{
		// Act
		var response = await Client.People.PeopleSuggestionsForOrganizationAsync(
			TestProvider, TestOrganization, null, null, null, CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	/// <inheritdoc />
	protected override Task<ListResponse<OrganizationPerson>> ListPeopleAsync(
		int? limit = null,
		string? search = null,
		bool? onlyMembers = null)
		=> Client.People.ListPeopleFromOrganizationAsync(
			TestProvider, TestOrganization, null, limit, search, onlyMembers, CancellationToken);
}
