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

		var standardId = await GetFirstStandardIdAsync(client, provider, orgName);
		if (standardId == null)
		{
			return;
		}

		// Act
		var response = await client.CodingStandards.GetCodingStandardAsync(
			provider,
			orgName,
			standardId.Value,
			CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Id.Should().Be(standardId.Value);
	}

	[Fact]
	public async Task ListCodingStandardTools_ReturnsTools()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		var standardId = await GetFirstStandardIdAsync(client, provider, orgName);
		if (standardId == null)
		{
			return;
		}

		// Act
		var response = await client.CodingStandards.ListCodingStandardToolsAsync(
			provider,
			orgName,
			standardId.Value,
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

		var (standardId, toolUuid) = await GetStandardAndToolAsync(client, provider, orgName);
		if (standardId == null || toolUuid == null)
		{
			return;
		}

		// Act
		var response = await ListPatternsAsync(client, provider, orgName, standardId.Value, toolUuid, null);

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

		var (standardId, toolUuid) = await GetStandardAndToolAsync(client, provider, orgName);
		if (standardId == null || toolUuid == null)
		{
			return;
		}

		// Act
		var response = await ListPatternsAsync(client, provider, orgName, standardId.Value, toolUuid, limit);

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

		var standardId = await GetFirstStandardIdAsync(client, provider, orgName);
		if (standardId == null)
		{
			return;
		}

		// Act
		var response = await client.CodingStandards.ListCodingStandardRepositoriesAsync(
			provider,
			orgName,
			standardId.Value,
			null,
			null,
			CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	#region Helper Methods

	private async Task<long?> GetFirstStandardIdAsync(
		CodacyClient client,
		Provider provider,
		string orgName)
	{
		var standards = await client.CodingStandards.ListCodingStandardsAsync(
			provider,
			orgName,
			CancellationToken);

		if (standards.Data.Count == 0)
		{
			Output.WriteLine("No coding standards available - skipping test");
			return null;
		}

		return standards.Data[0].Id;
	}

	private async Task<(long? StandardId, string? ToolUuid)> GetStandardAndToolAsync(
		CodacyClient client,
		Provider provider,
		string orgName)
	{
		var standardId = await GetFirstStandardIdAsync(client, provider, orgName);
		if (standardId == null)
		{
			return (null, null);
		}

		var tools = await client.CodingStandards.ListCodingStandardToolsAsync(
			provider,
			orgName,
			standardId.Value,
			CancellationToken);

		if (tools.Data.Count == 0)
		{
			Output.WriteLine("No tools available - skipping test");
			return (null, null);
		}

		var toolUuid = tools.Data[0].Uuid;
		if (string.IsNullOrEmpty(toolUuid))
		{
			Output.WriteLine("Tool UUID is null or empty - skipping test");
			return (null, null);
		}

		return (standardId, toolUuid);
	}

	private async Task<ConfiguredPatternsListResponse> ListPatternsAsync(
		CodacyClient client,
		Provider provider,
		string orgName,
		long standardId,
		string toolUuid,
		int? limit)
	{
		return await client.CodingStandards.ListCodingStandardPatternsAsync(
			provider,
			orgName,
			standardId,
			toolUuid,
			null, null, null, null, null, null, null, null, null, null,
			limit,
			CancellationToken);
	}

	#endregion
}

