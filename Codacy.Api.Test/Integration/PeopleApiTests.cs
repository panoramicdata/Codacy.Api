using Codacy.Api.Models;

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
		var response = await client.People.ListPeopleFromOrganizationAsync(
			provider,
			orgName,
			cancellationToken: CancellationToken);

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
		var response = await client.People.ListPeopleFromOrganizationAsync(
			provider,
			orgName,
			limit: limit,
			cancellationToken: CancellationToken);

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
		var allPeople = await client.People.ListPeopleFromOrganizationAsync(
			provider,
			orgName,
			cancellationToken: CancellationToken);
		
		if (allPeople.Data.Count == 0)
		{
			return; // Skip test if no people
		}

		var searchTerm = allPeople.Data[0].Email[..Math.Min(3, allPeople.Data[0].Email.Length)];

		// Act
		var response = await client.People.ListPeopleFromOrganizationAsync(
			provider,
			orgName,
			search: searchTerm,
			cancellationToken: CancellationToken);

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
		var response = await client.People.PeopleSuggestionsForOrganizationAsync(
			provider,
			orgName,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task PeopleSuggestionsForRepository_ReturnsSuggestions()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();

		// Act
		var response = await client.People.PeopleSuggestionsForRepositoryAsync(
			provider,
			orgName,
			repoName,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task PeopleSuggestionsForRepository_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		var repoName = GetTestRepository();
		const int limit = 10;

		// Act
		var response = await client.People.PeopleSuggestionsForRepositoryAsync(
			provider,
			orgName,
			repoName,
			limit: limit,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} suggestions");
	}
}
