namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Account API
/// </summary>
[Trait("Category", "Integration")]
public class AccountApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task GetUser_ReturnsAuthenticatedUser()
	{
		// Arrange
		using var client = GetClient();

		// Act
		var response = await client.Account.GetUserAsync(CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		(response.Data.Id > 0).Should().BeTrue();
		response.Data.MainEmail.Should().NotBeNull();
		response.Data.MainEmail.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetUser_UserHasValidProperties()
	{
		// Arrange
		using var client = GetClient();

		// Act
		var response = await client.Account.GetUserAsync(CancellationToken);

		// Assert
		var user = response.Data;
		user.Should().NotBeNull();
		(user.Id > 0).Should().BeTrue("User ID should be positive");
		user.MainEmail.Should().NotBeNull();
		user.MainEmail.Should().Contain("@");
		user.OtherEmails.Should().NotBeNull();
		(user.Created > DateTimeOffset.MinValue).Should().BeTrue("Created date should be set");
	}

	[Fact]
	public async Task ListUserOrganizations_ReturnsOrganizations()
	{
		// Arrange
		using var client = GetClient();

		// Act
		var response = await client.Account.ListUserOrganizationsAsync(cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		// User should have at least one organization
		response.Data.Should().NotBeEmpty();
	}

	[Fact]
	public async Task ListUserOrganizations_WithPagination_ReturnsLimitedResults()
	{
		// Arrange
		using var client = GetClient();
		const int limit = 5;

		// Act
		var response = await client.Account.ListUserOrganizationsAsync(
			limit: limit,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		(response.Data.Count <= limit).Should().BeTrue($"Should return at most {limit} organizations");
	}

	[Fact]
	public async Task ListOrganizationsForProvider_ReturnsProviderOrganizations()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());

		// Act
		var response = await client.Account.ListOrganizationsAsync(
			provider,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		foreach (var org in response.Data)
		{
			org.Provider.Should().Be(provider);
			org.Name.Should().NotBeNull();
			org.RemoteIdentifier.Should().NotBeNull();
		}
	}

	[Fact]
	public async Task GetUserOrganization_ReturnsSpecificOrganization()
	{
		// Arrange
		using var client = GetClient();
		var provider = Enum.Parse<Provider>(GetTestProvider());
		var orgName = GetTestOrganization();

		// Act
		var response = await client.Account.GetUserOrganizationAsync(
			provider,
			orgName,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Provider.Should().Be(provider);
		response.Data.Name.Should().Be(orgName);
	}

	[Fact]
	public async Task ListUserEmails_ReturnsEmailAddresses()
	{
		// Arrange
		using var client = GetClient();

		// Act
		var response = await client.Account.ListUserEmailsAsync(CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.MainEmail.Should().NotBeNull();
		response.Data.MainEmail.Email.Should().NotBeNull();
		response.Data.MainEmail.Email.Should().Contain("@");
		response.Data.OtherEmails.Should().NotBeNull();
	}

	[Fact]
	public async Task GetEmailSettings_ReturnsNotificationSettings()
	{
		// Arrange
		using var client = GetClient();

		// Act
		var response = await client.Account.GetEmailSettingsAsync(CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		// Settings should have valid boolean values (not throwing)
		_ = response.Data.PerCommit;
		_ = response.Data.PerPullRequest;
		_ = response.Data.OnlyMyActivity;
	}

	[Fact]
	public async Task ListUserIntegrations_ReturnsIntegrations()
	{
		// Arrange
		using var client = GetClient();

		// Act
		var response = await client.Account.ListUserIntegrationsAsync(cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		// User should have at least one integration (the one used for auth)
		response.Data.Should().NotBeEmpty();
		foreach (var integration in response.Data)
		{
			integration.Host.Should().NotBeNull();
			(integration.LastAuthenticated > DateTimeOffset.MinValue).Should().BeTrue();
		}
	}

	[Fact]
	public async Task GetUserApiTokens_ReturnsTokenList()
	{
		// Arrange
		using var client = GetClient();

		// Act
		var response = await client.Account.GetUserApiTokensAsync(cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		// List may be empty if no tokens created, but should not be null
	}

	[Fact]
	public async Task UpdateUser_WithValidData_UpdatesSuccessfully()
	{
		// Arrange
		using var client = GetClient();
		var originalUser = await client.Account.GetUserAsync(CancellationToken);

		// Note: The Codacy API's UpdateUser endpoint has strict username validation:
		// - Only alphanumeric characters, underscores, and hyphens allowed
		// - Length between 3-15 characters
		// The GET /user endpoint returns a display name (e.g., "David Bond"), but the
		// PATCH endpoint expects a username format.
		//
		// To avoid permanently modifying the user's data, we only test with
		// ShouldDoClientQualification which is a toggle-able boolean setting.
		var updateBody = new Interfaces.UserBody
		{
			ShouldDoClientQualification = originalUser.Data.ShouldDoClientQualification ?? false
		};

		// Act
		var response = await client.Account.UpdateUserAsync(
			updateBody,
			cancellationToken: CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		response.Data.Id.Should().Be(originalUser.Data.Id);
	}
}
