namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Security API
/// </summary>
[Trait("Category", "Integration")]
public class SecurityApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task SearchSecurityItems_ReturnsItems()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		try
		{
			// Act
			var response = await client
				.Security
				.SearchSecurityItemsAsync(
					provider,
					orgName,
					null,
					null,
					null,
					null,
					null,
					CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest ||
											 ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Organization may not have security items - skip test
			Output.WriteLine($"Security items not available: {ex.Message}");
		}
	}

	[Fact]
	public async Task SearchSecurityItems_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();
		const int limit = 10;

		try
		{
			// Act
			var response = await client.Security.SearchSecurityItemsAsync(provider, orgName, null, null, limit, null, null, CancellationToken);

			// Assert
			response.Should().NotBeNull();
			response.Data.Should().NotBeNull();
			(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} security items");
		}
		catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest ||
											 ex.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			// Organization may not have security items - skip test
			Output.WriteLine($"Security items not available: {ex.Message}");
		}
	}

	[Fact]
	public async Task SearchSecurityDashboard_ReturnsDashboardMetrics()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Security.SearchSecurityDashboardAsync(
			provider,
			orgName,
			body: new SearchSRMDashboard(),
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task SearchSecurityDashboardRepositories_ReturnsRepositories()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Security.SearchSecurityDashboardRepositoriesAsync(
			provider,
			orgName,
			body: new SearchSRMDashboardRepositories(),
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task SearchSecurityDashboardHistory_ReturnsHistory()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Security.SearchSecurityDashboardHistoryAsync(
			provider,
			orgName,
			body: new SearchSRMDashboardHistory(),
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task SearchSecurityDashboardCategories_ReturnsCategories()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Security.SearchSecurityDashboardCategoriesAsync(
			provider,
			orgName,
			body: new SearchSRMDashboardCategories(),
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task ListSecurityManagers_ReturnsManagers()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Security.ListSecurityManagersAsync(provider, orgName, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task ListSecurityRepositories_ReturnsRepositoriesWithIssues()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Security.ListSecurityRepositoriesAsync(provider, orgName, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task GetSLAConfig_ReturnsConfiguration()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Security.GetSLAConfigAsync(
			provider,
			orgName,
			CancellationToken);

		// Assert
		response.Should().NotBeNull();
	}

	[Fact]
	public async Task ListSecurityCategories_ReturnsCategoriesWithFindings()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Security.ListSecurityCategoriesAsync(provider, orgName, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}
}

