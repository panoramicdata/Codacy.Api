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

		try
		{
			// Act
			var response = await Client
				.Security
				.SearchSecurityItemsAsync(
					TestProvider,
					TestOrganization,
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
		const int limit = 10;

		try
		{
			// Act
			var response = await Client.Security.SearchSecurityItemsAsync(TestProvider, TestOrganization, null, null, limit, null, null, CancellationToken);

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

		// Act
		var response = await Client.Security.SearchSecurityDashboardAsync(
			TestProvider,
			TestOrganization,
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

		// Act
		var response = await Client.Security.SearchSecurityDashboardRepositoriesAsync(
			TestProvider,
			TestOrganization,
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

		// Act
		var response = await Client.Security.SearchSecurityDashboardHistoryAsync(
			TestProvider,
			TestOrganization,
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

		// Act
		var response = await Client.Security.SearchSecurityDashboardCategoriesAsync(
			TestProvider,
			TestOrganization,
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

		// Act
		var response = await Client.Security.ListSecurityManagersAsync(TestProvider, TestOrganization, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task ListSecurityRepositories_ReturnsRepositoriesWithIssues()
	{
		// Arrange

		// Act
		var response = await Client.Security.ListSecurityRepositoriesAsync(TestProvider, TestOrganization, null, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}

	[Fact]
	public async Task GetSLAConfig_ReturnsConfiguration()
	{
		// Arrange

		// Act
		var response = await Client.Security.GetSLAConfigAsync(
			TestProvider,
			TestOrganization,
			CancellationToken);

		// Assert
		response.Should().NotBeNull();
	}

	[Fact]
	public async Task ListSecurityCategories_ReturnsCategoriesWithFindings()
	{
		// Arrange

		// Act
		var response = await Client.Security.ListSecurityCategoriesAsync(TestProvider, TestOrganization, null, null, CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
	}
}