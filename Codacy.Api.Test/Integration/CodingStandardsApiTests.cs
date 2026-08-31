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

		// Act
		var response = await Client.CodingStandards.ListCodingStandardsAsync(
			TestProvider,
			TestOrganization,
			CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task GetCodingStandard_ReturnsStandardDetails()
	{
		// Arrange

		var standardId = await GetFirstStandardIdAsync(Client, TestProvider, TestOrganization);
		if (standardId == null)
		{
			return;
		}

		// Act
		var response = await Client.CodingStandards.GetCodingStandardAsync(
			TestProvider,
			TestOrganization,
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

		var standardId = await GetFirstStandardIdAsync(Client, TestProvider, TestOrganization);
		if (standardId == null)
		{
			return;
		}

		// Act
		var response = await Client.CodingStandards.ListCodingStandardToolsAsync(
			TestProvider,
			TestOrganization,
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

		var (standardId, toolUuid) = await GetStandardAndToolAsync(Client, TestProvider, TestOrganization);
		if (standardId == null || toolUuid == null)
		{
			return;
		}

		// Act
		var response = await ListPatternsAsync(Client, TestProvider, TestOrganization, standardId.Value, toolUuid, null);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task ListCodingStandardPatterns_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		const int limit = 10;

		var (standardId, toolUuid) = await GetStandardAndToolAsync(Client, TestProvider, TestOrganization);
		if (standardId == null || toolUuid == null)
		{
			return;
		}

		// Act
		var response = await ListPatternsAsync(Client, TestProvider, TestOrganization, standardId.Value, toolUuid, limit);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} patterns");
	}

	[Fact]
	public async Task ListCodingStandardRepositories_ReturnsRepositories()
	{
		// Arrange

		var standardId = await GetFirstStandardIdAsync(Client, TestProvider, TestOrganization);
		if (standardId == null)
		{
			return;
		}

		// Act
		var response = await Client.CodingStandards.ListCodingStandardRepositoriesAsync(
			TestProvider,
			TestOrganization,
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
		string organization)
	{
		var standards = await client.CodingStandards.ListCodingStandardsAsync(
			provider,
			organization,
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
		string organization)
	{
		var standardId = await GetFirstStandardIdAsync(client, provider, organization);
		if (standardId == null)
		{
			return (null, null);
		}

		var tools = await client.CodingStandards.ListCodingStandardToolsAsync(
			provider,
			organization,
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
		string organization,
		long standardId,
		string toolUuid,
		int? limit)
	{
		return await client.CodingStandards.ListCodingStandardPatternsAsync(
			provider,
			organization,
			standardId,
			toolUuid,
			null, null, null, null, null, null, null, null, null, null,
			limit,
			CancellationToken);
	}

	#endregion
}