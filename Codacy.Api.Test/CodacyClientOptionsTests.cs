namespace Codacy.Api.Test;

/// <summary>
/// Unit tests for CodacyClientOptions
/// </summary>
public class CodacyClientOptionsTests
{
	[Fact]
	public void Validate_WithValidOptions_DoesNotThrow()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token-123"
		};

		// Act & Assert
		var exception = Record.Exception(() => options.Validate());
		exception.Should().BeNull();
	}

	[Fact]
	public void Validate_WithEmptyApiToken_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = ""
		};

		// Act & Assert
		var result = ((Action)(() => options.Validate()))
			.Should()
			.ThrowExactly<ArgumentException>()
			.WithMessage("*ApiToken*");
	}

	[Fact]
	public void Validate_WithWhitespaceApiToken_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "   "
		};

		// Act & Assert
		var result = ((Action)(() => options.Validate()))
			.Should()
			.ThrowExactly<ArgumentException>()
			.WithMessage("*ApiToken*");
	}

	[Fact]
	public void Validate_WithEmptyBaseUrl_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			BaseUrl = ""
		};

		// Act & Assert
		var result = ((Action)(() => options.Validate()))
			.Should()
			.ThrowExactly<ArgumentException>()
			.WithMessage("*BaseUrl*");
	}

	[Fact]
	public void Validate_WithWhitespaceBaseUrl_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			BaseUrl = "   "
		};

		// Act & Assert
		var result = ((Action)(() => options.Validate()))
			.Should()
			.ThrowExactly<ArgumentException>()
			.WithMessage("*BaseUrl*");
	}

	[Fact]
	public void Validate_WithNegativeTimeout_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			RequestTimeout = TimeSpan.FromSeconds(-1)
		};

		// Act & Assert
		var result = ((Action)(() => options.Validate()))
			.Should()
			.ThrowExactly<ArgumentException>()
			.WithMessage("*RequestTimeout*");
	}

	[Fact]
	public void Validate_WithZeroTimeout_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			RequestTimeout = TimeSpan.Zero
		};

		// Act & Assert
		var result = ((Action)(() => options.Validate()))
			.Should()
			.ThrowExactly<ArgumentException>()
			.WithMessage("*RequestTimeout*");
	}

	[Fact]
	public void Validate_WithNegativeRetryAttempts_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			MaxRetryAttempts = -1
		};

		// Act & Assert
		var result = ((Action)(() => options.Validate()))
			.Should()
			.ThrowExactly<ArgumentException>()
			.WithMessage("*MaxRetryAttempts*");
	}

	[Fact]
	public void Validate_WithZeroRetryAttempts_DoesNotThrow()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			MaxRetryAttempts = 0
		};

		// Act & Assert
		var exception = Record.Exception(() => options.Validate());
		exception.Should().BeNull();
	}

	[Fact]
	public void Validate_WithNegativeRetryDelay_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			RetryDelay = TimeSpan.FromSeconds(-1)
		};

		// Act & Assert
		var result = ((Action)(() => options.Validate()))
			.Should()
			.ThrowExactly<ArgumentException>()
			.WithMessage("*RetryDelay*");
	}

	[Fact]
	public void Validate_WithMaxRetryDelayLessThanRetryDelay_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			RetryDelay = TimeSpan.FromSeconds(10),
			MaxRetryDelay = TimeSpan.FromSeconds(5)
		};

		// Act & Assert
		var result = ((Action)(() => options.Validate()))
			.Should()
			.ThrowExactly<ArgumentException>()
			.WithMessage("*MaxRetryDelay*");
	}

	[Fact]
	public void Validate_WithMaxRetryDelayEqualToRetryDelay_DoesNotThrow()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			RetryDelay = TimeSpan.FromSeconds(5),
			MaxRetryDelay = TimeSpan.FromSeconds(5)
		};

		// Act & Assert
		var exception = Record.Exception(() => options.Validate());
		exception.Should().BeNull();
	}

	[Fact]
	public void Validate_WithBothHttpClientAndFactory_ThrowsArgumentException()
	{
		// Arrange
		var mockFactory = new Mock<IHttpClientFactory>();
		var httpClient = new HttpClient();

		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			HttpClient = httpClient,
			HttpClientFactory = mockFactory.Object
		};

		// Act & Assert
		var result = ((Action)(() => options.Validate()))
			.Should()
			.ThrowExactly<ArgumentException>()
			.WithMessage("*HttpClient*HttpClientFactory*");
	}

	[Fact]
	public void Validate_WithOnlyHttpClient_DoesNotThrow()
	{
		// Arrange
		var httpClient = new HttpClient();
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			HttpClient = httpClient
		};

		// Act & Assert
		var exception = Record.Exception(() => options.Validate());
		exception.Should().BeNull();
	}

	[Fact]
	public void Validate_WithOnlyHttpClientFactory_DoesNotThrow()
	{
		// Arrange
		var mockFactory = new Mock<IHttpClientFactory>();
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			HttpClientFactory = mockFactory.Object
		};

		// Act & Assert
		var exception = Record.Exception(() => options.Validate());
		exception.Should().BeNull();
	}

	[Fact]
	public void DefaultBaseUrl_ShouldBeAppCodacyCom()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		options.BaseUrl.Should().Be("https://app.codacy.com");
	}

	[Fact]
	public void DefaultRequestTimeout_ShouldBe30Seconds()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		Assert.Equal(TimeSpan.FromSeconds(30), options.RequestTimeout);
	}

	[Fact]
	public void DefaultMaxRetryAttempts_ShouldBe3()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		Assert.Equal(3, options.MaxRetryAttempts);
	}

	[Fact]
	public void DefaultRetryDelay_ShouldBe1Second()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		Assert.Equal(TimeSpan.FromSeconds(1), options.RetryDelay);
	}

	[Fact]
	public void DefaultMaxRetryDelay_ShouldBe30Seconds()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		Assert.Equal(TimeSpan.FromSeconds(30), options.MaxRetryDelay);
	}

	[Fact]
	public void DefaultUseExponentialBackoff_ShouldBeTrue()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		options.UseExponentialBackoff.Should().BeTrue();
	}

	[Fact]
	public void DefaultLogger_ShouldBeNull()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		options.Logger.Should().BeNull();
	}

	[Fact]
	public void DefaultEnableRequestLogging_ShouldBeFalse()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		options.EnableRequestLogging.Should().BeFalse();
	}

	[Fact]
	public void DefaultEnableResponseLogging_ShouldBeFalse()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		options.EnableResponseLogging.Should().BeFalse();
	}

	[Fact]
	public void DefaultHeaders_ShouldBeEmptyDictionary()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		options.DefaultHeaders.Should().NotBeNull();
		options.DefaultHeaders.Should().BeEmpty();
	}

	[Fact]
	public void DefaultHttpClient_ShouldBeNull()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		options.HttpClient.Should().BeNull();
	}

	[Fact]
	public void DefaultHttpClientFactory_ShouldBeNull()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		options.HttpClientFactory.Should().BeNull();
	}

	[Fact]
	public void CustomProperties_CanBeSet()
	{
		// Arrange & Act
		var customHeaders = new Dictionary<string, string> { { "X-Custom", "Value" } };
		var logger = new Mock<ILogger>().Object;
		var httpClient = new HttpClient();

		var options = new CodacyClientOptions
		{
			ApiToken = "custom-token",
			BaseUrl = "https://custom.codacy.com",
			RequestTimeout = TimeSpan.FromMinutes(2),
			MaxRetryAttempts = 5,
			RetryDelay = TimeSpan.FromSeconds(2),
			MaxRetryDelay = TimeSpan.FromSeconds(60),
			UseExponentialBackoff = false,
			Logger = logger,
			EnableRequestLogging = true,
			EnableResponseLogging = true,
			DefaultHeaders = customHeaders,
			HttpClient = httpClient
		};

		// Assert
		options.ApiToken.Should().Be("custom-token");
		options.BaseUrl.Should().Be("https://custom.codacy.com");
		Assert.Equal(TimeSpan.FromMinutes(2), options.RequestTimeout);
		Assert.Equal(5, options.MaxRetryAttempts);
		Assert.Equal(TimeSpan.FromSeconds(2), options.RetryDelay);
		Assert.Equal(TimeSpan.FromSeconds(60), options.MaxRetryDelay);
		options.UseExponentialBackoff.Should().BeFalse();
		options.Logger.Should().BeSameAs(logger);
		options.EnableRequestLogging.Should().BeTrue();
		options.EnableResponseLogging.Should().BeTrue();
		options.DefaultHeaders.Should().BeSameAs(customHeaders);
		options.HttpClient.Should().BeSameAs(httpClient);
	}
}
