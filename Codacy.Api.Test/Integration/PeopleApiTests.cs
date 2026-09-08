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

	#pragma warning disable S2360
	/// <inheritdoc />
	protected override Task<ListResponse<OrganizationPerson>> ListPeopleAsync(
		int? limit,
		string? search,
		bool? onlyMembers)
		=> Client.People.ListPeopleFromOrganizationAsync(
			TestProvider, TestOrganization, null, limit, search, onlyMembers, CancellationToken);
	#pragma warning restore S2360
}
