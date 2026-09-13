namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Security API.
/// </summary>
/// <remarks>
/// These assert on the content of the responses, not merely that a response arrived. An earlier
/// version checked only that <c>Data</c> was non-null on properties already declared
/// <c>required</c>, and tolerated a 400, which is why models that could not deserialize a single
/// real response shipped unnoticed. A 400 from these endpoints now means a malformed request, so
/// it is allowed to fail the test rather than being reported as "security items not available".
/// </remarks>
[Trait("Category", "Integration")]
public class SecurityApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task SearchSecurityItems_ReturnsItemsWithPriorityAndStatus()
	{
		// Act
		var response = await Client.Security.SearchSecurityItemsAsync(
			TestProvider, TestOrganization, new SearchSRMItems(), null, null, null, null, CancellationToken);

		// Assert
		var items = response.ShouldHaveNonEmptyData(r => r.Data);
		foreach (var item in items)
		{
			item.Id.Should().NotBe(Guid.Empty);
			item.Title.Should().NotBeNullOrWhiteSpace();
			item.ItemSourceId.Should().NotBeNullOrWhiteSpace();
			item.Priority.Should().BeOneOf(SrmPriority.Low, SrmPriority.Medium, SrmPriority.High, SrmPriority.Critical);
			item.DueAt.Should().BeAfter(item.OpenedAt);
		}
	}

	[Fact]
	public async Task SearchSecurityItems_FilteredByOpenStatuses_ReturnsOnlyOpenItems()
	{
		// Arrange
		var body = new SearchSRMItems
		{
			Statuses = [SrmStatus.OnTrack, SrmStatus.DueSoon, SrmStatus.Overdue]
		};

		// Act
		var response = await Client.Security.SearchSecurityItemsAsync(
			TestProvider, TestOrganization, body, null, 100, null, null, CancellationToken);

		// Assert
		var items = response.ShouldHaveNonEmptyData(r => r.Data);
		items.Should().OnlyContain(
			item => item.Status == SrmStatus.OnTrack
				|| item.Status == SrmStatus.DueSoon
				|| item.Status == SrmStatus.Overdue);
		items.Should().OnlyContain(item => item.ClosedAt == null);
	}

	[Fact]
	public async Task SearchSecurityItems_FilteredByPriority_ReturnsOnlyThatPriority()
	{
		// Arrange
		var body = new SearchSRMItems { Priorities = [SrmPriority.Critical] };

		// Act
		var response = await Client.Security.SearchSecurityItemsAsync(
			TestProvider, TestOrganization, body, null, 20, null, null, CancellationToken);

		// Assert
		response.ShouldHaveNonEmptyData(r => r.Data)
			.Should().OnlyContain(item => item.Priority == SrmPriority.Critical);
	}

	[Theory]
	[InlineData(null)]
	[InlineData(10)]
	public async Task SearchSecurityItems_WithLimit_ReturnsPageOfAtMostThatSize(int? limit)
	{
		// Act
		var response = await Client.Security.SearchSecurityItemsAsync(
			TestProvider, TestOrganization, new SearchSRMItems(), null, limit, null, null, CancellationToken);

		// Assert
		response.ShouldHavePageOfAtMost(limit, r => r.Data);
		response.Pagination.Limit.Should().Be(limit ?? response.Pagination.Limit);
		response.Pagination.Cursor.Should().NotBeNullOrWhiteSpace("more findings than one page exist");
	}

	[Fact]
	public async Task SearchSecurityDashboard_ReturnsCountsThatAgreeWithTheTotal()
	{
		// Act
		var response = await Client.Security.SearchSecurityDashboardAsync(
			TestProvider,
			TestOrganization,
			body: new SearchSRMDashboard(),
			cancellationToken: CancellationToken);

		// Assert
		var dashboard = response.ShouldHaveData(r => r.Data);
		dashboard.TotalOpen.Should().BePositive();
		(dashboard.OpenCritical + dashboard.OpenHigh + dashboard.OpenMedium + dashboard.OpenLow)
			.Should().Be(dashboard.TotalOpen, "every open item carries exactly one severity");
		(dashboard.OnTrack + dashboard.DueSoon + dashboard.Overdue)
			.Should().Be(dashboard.TotalOpen, "every open item carries exactly one SLA status");
	}

	[Fact]
	public async Task SearchSecurityDashboardRepositories_ReturnsNamedRepositoriesWithSeverityCounts()
	{
		// Act
		var response = await Client.Security.SearchSecurityDashboardRepositoriesAsync(
			TestProvider,
			TestOrganization,
			body: new SearchSRMDashboardRepositories(),
			cancellationToken: CancellationToken);

		// Assert
		var repositories = response.ShouldHaveNonEmptyData(r => r.Data);
		foreach (var repository in repositories)
		{
			repository.Id.Should().BePositive();
			repository.Name.Should().NotBeNullOrWhiteSpace();
			(repository.Critical + repository.High + repository.Medium + repository.Low)
				.Should().BePositive("a repository only appears here when it has findings");
		}
	}

	[Fact]
	public async Task SearchSecurityDashboardRepositories_FilteredByName_ReturnsOnlyThatRepository()
	{
		// Act
		var response = await Client.Security.SearchSecurityDashboardRepositoriesAsync(
			TestProvider,
			TestOrganization,
			body: new SearchSRMDashboardRepositories { Repositories = [TestRepository] },
			cancellationToken: CancellationToken);

		// Assert
		response.ShouldHaveData(r => r.Data)
			.Should().OnlyContain(repository => repository.Name == TestRepository);
	}

	[Fact]
	public async Task SearchSecurityDashboardHistory_ReturnsOrderedNonOverlappingIntervals()
	{
		// Act
		var response = await Client.Security.SearchSecurityDashboardHistoryAsync(
			TestProvider,
			TestOrganization,
			body: new SearchSRMDashboardHistory(),
			cancellationToken: CancellationToken);

		// Assert
		var points = response.ShouldHaveNonEmptyData(r => r.Data);
		points.Should().OnlyContain(point => point.Until > point.Since);
		points.Should().BeInAscendingOrder(point => point.Since);
	}

	[Fact]
	public async Task SearchSecurityDashboardCategories_ReturnsNamedCategoriesWithTotals()
	{
		// Act
		var response = await Client.Security.SearchSecurityDashboardCategoriesAsync(
			TestProvider,
			TestOrganization,
			body: new SearchSRMDashboardCategories(),
			cancellationToken: CancellationToken);

		// Assert
		var categories = response.ShouldHaveNonEmptyData(r => r.Data);
		categories.Should().OnlyContain(category => !string.IsNullOrWhiteSpace(category.Name));
		categories.Should().OnlyContain(category => category.Total > 0);
	}

	[Fact]
	public async Task ListSecurityManagers_ReturnsManagers()
	{
		// Act
		var response = await Client.Security.ListSecurityManagersAsync(
			TestProvider, TestOrganization, null, null, CancellationToken);

		// Assert
		// An organization need not have dedicated security managers, so only the shape is asserted.
		var managers = response.ShouldHaveData(r => r.Data);
		managers.Should().OnlyContain(manager => manager.UserId > 0);
		managers.Should().OnlyContain(manager => !string.IsNullOrWhiteSpace(manager.Email));
	}

	[Fact]
	public async Task ListSecurityRepositories_ReturnsRepositoriesInTheTestOrganization()
	{
		// Act
		var response = await Client.Security.ListSecurityRepositoriesAsync(
			TestProvider, TestOrganization, null, null, null, CancellationToken);

		// Assert
		var repositories = response.ShouldHaveNonEmptyData(r => r.Data);
		repositories.Should().OnlyContain(repository => repository.Owner == TestOrganization);
		repositories.Should().OnlyContain(repository => repository.Provider == TestProvider);
		repositories.Should().OnlyContain(repository => !string.IsNullOrWhiteSpace(repository.Name));
	}

	[Fact]
	public async Task GetSLAConfig_ReturnsPositiveThresholdsOrderedBySeverity()
	{
		// Act
		var response = await Client.Security.GetSLAConfigAsync(
			TestProvider,
			TestOrganization,
			CancellationToken);

		// Assert
		var config = response.ShouldHaveData(r => r.SlaConfig);
		config.CriticalSla.Should().BePositive();
		config.CriticalSla.Should().BeLessThanOrEqualTo(config.HighSla);
		config.HighSla.Should().BeLessThanOrEqualTo(config.MediumSla);
		config.MediumSla.Should().BeLessThanOrEqualTo(config.LowSla);
	}

	[Fact]
	public async Task ListSecurityCategories_ReturnsCategoryNames()
	{
		// Act
		var response = await Client.Security.ListSecurityCategoriesAsync(
			TestProvider, TestOrganization, null, null, CancellationToken);

		// Assert
		response.ShouldHaveNonEmptyData(r => r.Data)
			.Should().OnlyContain(category => !string.IsNullOrWhiteSpace(category));
	}
}
