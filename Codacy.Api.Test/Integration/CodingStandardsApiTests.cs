namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Coding Standards API
/// </summary>
[Trait("Category", "Integration")]
public class CodingStandardsApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task ListCodingStandards_ReturnsStandards()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.CodingStandards.ListCodingStandardsAsync(
			provider,
			orgName,
			CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task GetCodingStandard_ReturnsStandardDetails()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// First, list standards to get an ID
		var standards = await client.CodingStandards.ListCodingStandardsAsync(
			provider,
			orgName,
			CancellationToken);

		if (standards.Data.Count == 0)
		{
			Output.WriteLine("No coding standards available - skipping test");
			return;
		}

		var standardId = standards.Data[0].Id;

		// Act
		var response = await client.CodingStandards.GetCodingStandardAsync(
			provider,
			orgName,
			standardId,
			CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Id.Should().Be(standardId);
	}

	[Fact]
	public async Task ListCodingStandardTools_ReturnsTools()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// First, list standards to get an ID
		var standards = await client.CodingStandards.ListCodingStandardsAsync(
			provider,
			orgName,
			CancellationToken);

		if (standards.Data.Count == 0)
		{
			Output.WriteLine("No coding standards available - skipping test");
			return;
		}

		var standardId = standards.Data[0].Id;

		// Act
		var response = await client.CodingStandards.ListCodingStandardToolsAsync(
			provider,
			orgName,
			standardId,
			CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task ListCodingStandardPatterns_ReturnsPatterns()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// First, list standards and tools
		var standards = await client.CodingStandards.ListCodingStandardsAsync(
			provider,
			orgName,
			CancellationToken);

		if (standards.Data.Count == 0)
		{
			Output.WriteLine("No coding standards available - skipping test");
			return;
		}

		var standardId = standards.Data[0].Id;
		var tools = await client.CodingStandards.ListCodingStandardToolsAsync(
			provider,
			orgName,
			standardId,
			CancellationToken);

		if (tools.Data.Count == 0)
		{
			Output.WriteLine("No tools available - skipping test");
			return;
		}

		var toolUuid = tools.Data[0].Uuid;
		if (string.IsNullOrEmpty(toolUuid))
		{
			Output.WriteLine("Tool UUID is null or empty - skipping test");
			return;
		}

		// Act
		var response = await client.CodingStandards.ListCodingStandardPatternsAsync(
			provider,
			orgName,
			standardId,
			toolUuid,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task ListCodingStandardPatterns_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		const int limit = 10;

		// First, list standards and tools
		var standards = await client.CodingStandards.ListCodingStandardsAsync(
			provider,
			orgName,
			CancellationToken);

		if (standards.Data.Count == 0)
		{
			Output.WriteLine("No coding standards available - skipping test");
			return;
		}

		var standardId = standards.Data[0].Id;
		var tools = await client.CodingStandards.ListCodingStandardToolsAsync(
			provider,
			orgName,
			standardId,
			CancellationToken);

		if (tools.Data.Count == 0)
		{
			Output.WriteLine("No tools available - skipping test");
			return;
		}

		var toolUuid = tools.Data[0].Uuid;
		if (string.IsNullOrEmpty(toolUuid))
		{
			Output.WriteLine("Tool UUID is null or empty - skipping test");
			return;
		}

		// Act
		var response = await client.CodingStandards.ListCodingStandardPatternsAsync(
			provider,
			orgName,
			standardId,
			toolUuid,
			limit: limit,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} patterns");
	}

	[Fact]
	public async Task ListCodingStandardRepositories_ReturnsRepositories()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// First, list standards to get an ID
		var standards = await client.CodingStandards.ListCodingStandardsAsync(
			provider,
			orgName,
			CancellationToken);

		if (standards.Data.Count == 0)
		{
			Output.WriteLine("No coding standards available - skipping test");
			return;
		}

		var standardId = standards.Data[0].Id;

		// Act
		var response = await client.CodingStandards.ListCodingStandardRepositoriesAsync(
			provider,
			orgName,
			standardId,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}
}
