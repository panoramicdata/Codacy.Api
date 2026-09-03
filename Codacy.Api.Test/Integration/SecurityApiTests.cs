using System.Net;

namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Security API
/// </summary>
[Trait("Category", "Integration")]
public class SecurityApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Theory]
	[InlineData(null)]
	[InlineData(10)]
	public Task SearchSecurityItems_ReturnsItems(int? limit)
		=> RunWhenSecurityAvailableAsync(async () =>
		{
			// Act
			var response = await Client.Security.SearchSecurityItemsAsync(
				TestProvider, TestOrganization, null, null, limit, null, null, CancellationToken);

			// Assert
			response.ShouldHavePageOfAtMost(limit, r => r.Data);
		});

	[Fact]
	public async Task SearchSecurityDashboard_ReturnsDashboardMetrics()
	{
		// Act
		var response = await Client.Security.SearchSecurityDashboardAsync(
			TestProvider,
			TestOrganization,
			body: new SearchSRMDashboard(),
			cancellationToken: CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	[Fact]
	public async Task SearchSecurityDashboardRepositories_ReturnsRepositories()
	{
		// Act
		var response = await Client.Security.SearchSecurityDashboardRepositoriesAsync(
			TestProvider,
			TestOrganization,
			body: new SearchSRMDashboardRepositories(),
			cancellationToken: CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	[Fact]
	public async Task SearchSecurityDashboardHistory_ReturnsHistory()
	{
		// Act
		var response = await Client.Security.SearchSecurityDashboardHistoryAsync(
			TestProvider,
			TestOrganization,
			body: new SearchSRMDashboardHistory(),
			cancellationToken: CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	[Fact]
	public async Task SearchSecurityDashboardCategories_ReturnsCategories()
	{
		// Act
		var response = await Client.Security.SearchSecurityDashboardCategoriesAsync(
			TestProvider,
			TestOrganization,
			body: new SearchSRMDashboardCategories(),
			cancellationToken: CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	[Fact]
	public async Task ListSecurityManagers_ReturnsManagers()
	{
		// Act
		var response = await Client.Security.ListSecurityManagersAsync(TestProvider, TestOrganization, null, null, CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	[Fact]
	public async Task ListSecurityRepositories_ReturnsRepositoriesWithIssues()
	{
		// Act
		var response = await Client.Security.ListSecurityRepositoriesAsync(TestProvider, TestOrganization, null, null, null, CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	[Fact]
	public async Task GetSLAConfig_ReturnsConfiguration()
	{
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
		// Act
		var response = await Client.Security.ListSecurityCategoriesAsync(TestProvider, TestOrganization, null, null, CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data);
	}

	/// <summary>
	/// Runs a test that needs security items, which an organization need not have. The API
	/// answers 400 as well as 404 when it has nothing to report.
	/// </summary>
	private Task RunWhenSecurityAvailableAsync(Func<Task> test)
		=> RunWhenAvailableAsync(
			test,
			"Security items not available",
			HttpStatusCode.BadRequest,
			HttpStatusCode.NotFound);
}
