# GitHub Copilot Instructions

## Code Style Guidelines

### Zero Warnings Policy
- **Always aim for zero warnings and messages on every build**
- Address all compiler warnings before considering code complete
- Use `#pragma warning disable` only in exceptional cases with clear justification
- Enable "Treat warnings as errors" where appropriate

### Modern C# Practices (.NET 9)

#### Primary Constructors
- **Use primary constructors where possible** for cleaner, more concise code:
```csharp
// Preferred
public class CodacyClient(CodacyClientOptions options) : ICodacyClient
{
    public string ApiToken => options.ApiToken;
}

// Instead of
public class CodacyClient : ICodacyClient
{
    private readonly CodacyClientOptions _options;
    
    public CodacyClient(CodacyClientOptions options)
    {
        _options = options;
    }
    
    public string ApiToken => _options.ApiToken;
}
```

#### Collection Initialization
- **Use collection expressions `[]` instead of `new List<T>()`**:
```csharp
// Preferred
var items = [item1, item2, item3];
var emptyList = <string>[];

// Instead of
var items = new List<string> { item1, item2, item3 };
var emptyList = new List<string>();
```

#### Required Properties
- Use `required` keyword for mandatory properties:
```csharp
public class CodacyClientOptions
{
    public required string ApiToken { get; init; }
}
```

#### File-Scoped Namespaces
- Always use file-scoped namespaces:
```csharp
// Preferred
namespace Codacy.Api;

public class CodacyClient
{
}

// Instead of
namespace Codacy.Api
{
    public class CodacyClient
    {
    }
}
```

#### String Interpolation
- Use string interpolation over concatenation:
```csharp
// Preferred
var message = $"Error in {methodName}: {errorDetails}";

// Instead of
var message = "Error in " + methodName + ": " + errorDetails;
```

#### Pattern Matching
- Use modern pattern matching features:
```csharp
// Preferred
var result = value switch
{
    null => "null value",
    string s when s.Length > 0 => $"String: {s}",
    _ => "other"
};

// Use is patterns for type checks
if (obj is CodacyClient client && client.IsValid)
{
    // Process client
}
```

#### Null Handling
- Use null-conditional operators and null-coalescing:
```csharp
// Preferred
var result = client?.GetData()?.FirstOrDefault() ?? defaultValue;

// Use ArgumentNullException.ThrowIfNull
ArgumentNullException.ThrowIfNull(parameter);
```

#### Expression-Bodied Members
- Use expression-bodied members for simple operations:
```csharp
// Preferred
public string FullName => $"{FirstName} {LastName}";
public void LogError(string message) => _logger.LogError(message);
```

#### Records
- Use records for immutable data structures:
```csharp
public record CodacyApiResponse(string Data, int StatusCode, DateTime Timestamp);
```

### Code Organization

#### Using Statements
- Place using statements outside namespace (file-scoped)
- Group and sort using statements
- Remove unused using statements

#### Access Modifiers
- Always be explicit about access modifiers
- Use `internal` for implementation details not meant for public API

#### Naming Conventions
- Use PascalCase for public members
- Use camelCase with underscore prefix for private fields: `_fieldName`
- Use meaningful, descriptive names

### Testing Standards
- Use xUnit for testing framework
- Follow AAA pattern (Arrange, Act, Assert)
- Use descriptive test method names that explain the scenario
- **Use AwesomeAssertions for all assertions**

#### AwesomeAssertions Patterns
**Basic Assertions:**
```csharp
// Null checks
response.Should().NotBeNull();
response.Data.Should().BeNull();

// Equality
value.Should().Be(expected);
value.Should().NotBe(unexpected);

// Booleans
condition.Should().BeTrue();
condition.Should().BeFalse();

// Strings
text.Should().Contain("substring");
text.Should().NotBeEmpty();
text.Should().Be("expected");

// Collections
list.Should().NotBeEmpty();
list.Should().BeEmpty();
(list.Count <= limit).Should().BeTrue("reason message");
```

**Exception Assertions:**
```csharp
// Correct pattern - cast lambda to Action and use discard for unused instance
((Action)(() => _ = new MyClass(invalidArg)))
    .Should()
    .ThrowExactly<ArgumentException>()
    .WithMessage("*parameter*");

// For method calls
((Action)(() => _ = methodThatThrows()))
    .Should()
    .ThrowExactly<ArgumentException>()
    .WithMessage("*ApiToken*");

// For void methods (no discard needed)
((Action)(() => options.Validate()))
    .Should()
    .ThrowExactly<ArgumentException>()
    .WithMessage("*RequestTimeout*");

// No exception expected
var exception = Record.Exception(() => method());
exception.Should().BeNull();
```

**Iterating Collections:**
```csharp
// Use foreach instead of Assert.ALL
foreach (var item in collection)
{
    item.Property.Should().NotBeNull();
    item.Value.Should().Be(expected);
}
```

### EditorConfig Compliance
- Follow the .editorconfig settings in the workspace
- Use tabs for indentation as configured
- Maintain consistent formatting across all files

### Performance Considerations
- Use `ConfigureAwait(false)` for library code
- Prefer `StringBuilder` for multiple string concatenations
- Use `span` and `memory` types where appropriate for performance-critical code

### Documentation
- Use XML documentation comments for public APIs
- Include `<summary>`, `<param>`, and `<returns>` tags
- Document any non-obvious behavior or assumptions

### Error Handling
- Use specific exception types (FormatException, ArgumentException, etc.)
- Provide meaningful error messages
- Use structured logging where applicable

## Project-Specific Guidelines

### Codacy API Library Structure

#### Project Organization
- **Main Library**: `Codacy.Api` (targets .NET 9)
  - Core client classes and interfaces
  - API models and DTOs
  - Authentication and configuration
  
- **Test Project**: `Codacy.Api.Test` (targets .NET 9)
  - Unit tests
  - Integration tests using Microsoft Testing Platform
  - Uses User Secrets for sensitive configuration

#### Authentication & Configuration
```csharp
// CodacyClientOptions pattern - always use required properties
public class CodacyClientOptions
{
    public required string ApiToken { get; init; }
    public string BaseUrl { get; init; } = "https://app.codacy.com";
}
```

#### Interface Design
- All public client functionality should be exposed through interfaces
- Use `ICodacyClient` for main client operations
- Consider creating specialized interfaces for different API areas (e.g., `IOrganizationsApi`, `IRepositoriesApi`)

#### API Endpoint Patterns
Based on the Codacy API specification:

1. **Organizations**: `/api/v3/organizations/{provider}/{organizationName}`
2. **Repositories**: `/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}`
3. **Issues**: `/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues`
4. **Coverage**: `/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}/coverage`
5. **Common Parameters**:
   - `provider` - Git provider (e.g., gh for GitHub, bb for Bitbucket, gl for GitLab)
   - `limit` - Number of results to return
   - `cursor` - Pagination cursor

#### Error Handling Patterns
```csharp
// Validation in client options
internal void Validate()
{
    if (string.IsNullOrWhiteSpace(ApiToken))
        throw new ArgumentException("ApiToken cannot be null or empty.", nameof(ApiToken));
    
    if (string.IsNullOrWhiteSpace(BaseUrl))
        throw new ArgumentException("BaseUrl cannot be null or empty.", nameof(BaseUrl));
}
```

#### Logging Integration
- Use structured logging with Microsoft.Extensions.Logging
- Log important operations and errors
- Use appropriate log levels (Information for normal operations, Warning for issues)

### Build and Packaging
- Uses Nerdbank.GitVersioning for semantic versioning
- Source Link enabled for debugging support
- Package generation enabled with comprehensive metadata
- Zero warnings policy enforced at build time
- XML documentation generation required for all public APIs

### Development Tools
- Microsoft Testing Platform for test execution
- User Secrets for local development configuration
- Git versioning with semantic release tags

## Ongoing Development Guidelines

### Development Workflow
1. **Run tests** after each significant change
2. **Maintain zero warnings** policy throughout development
3. **Update documentation** as new features are added
4. **Follow semantic versioning** for releases

### API Client Structure
```csharp
// Target API structure
var client = new CodacyClient(options);
await client.Organizations.GetAsync("org-name", cancellationToken);
await client.Repositories.GetAsync("provider", "org", "repo", cancellationToken);
```

### Integration Testing Approach
- Use real API tokens in test environment (via User Secrets)
- Test each API group thoroughly
- Include both positive and negative test cases
- Clean up test data where possible
