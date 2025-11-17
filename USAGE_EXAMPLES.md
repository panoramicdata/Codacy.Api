# Codacy.Api Usage Examples

This file provides comprehensive examples for using the Codacy.Api library with Refit.

## Table of Contents
- [Getting Started](#getting-started)
- [Version API](#version-api)
- [Account API](#account-api)
- [Analysis API](#analysis-api)
- [People API](#people-api)
- [Coverage API](#coverage-api)
- [Coding Standards API](#coding-standards-api)
- [Security API](#security-api)
- [Best Practices](#best-practices)

## Getting Started

### Installation
```bash
dotnet add package Codacy.Api
```

### Basic Setup
```csharp
using Codacy.Api;
using Codacy.Api.Models;

var client = new CodacyClient(new CodacyClientOptions
{
    ApiToken = "your-api-token-here"
});
```

## Version API

### Get Codacy Version
```csharp
var version = await client.Version.GetVersionAsync();
Console.WriteLine($"Codacy Version: {version.Data}");
```

## Account API

### Get Current User
```csharp
var user = await client.Account.GetUserAsync();
Console.WriteLine($"User: {user.Data.Name}");
Console.WriteLine($"Email: {user.Data.MainEmail}");
Console.WriteLine($"Is Admin: {user.Data.IsAdmin}");
```

### Update User Profile
```csharp
await client.Account.UpdateUserAsync(new UserBody 
{ 
    Name = "John Doe" 
});
```

### Manage API Tokens
```csharp
// List tokens
var tokens = await client.Account.GetUserApiTokensAsync();
foreach (var token in tokens.Data)
{
    Console.WriteLine($"Token ID: {token.Id}, Expires: {token.ExpiresAt}");
}

// Create new token (expires in 30 days)
var newToken = await client.Account.CreateUserApiTokenAsync(
    new ApiTokenCreateRequest 
    { 
        ExpiresAt = DateTimeOffset.UtcNow.AddDays(30) 
    }
);
Console.WriteLine($"New Token: {newToken.Token}");

// Delete token
await client.Account.DeleteUserApiTokenAsync(newToken.Id);
```

### Manage Organizations
```csharp
// List all organizations
var orgs = await client.Account.ListUserOrganizationsAsync(limit: 50);
foreach (var org in orgs.Data)
{
    Console.WriteLine($"{org.Name} ({org.Provider})");
}

// List organizations for specific provider
var ghOrgs = await client.Account.ListOrganizationsAsync(Provider.gh);

// Get specific organization
var org = await client.Account.GetUserOrganizationAsync(
    Provider.gh, 
    "my-organization"
);
```

### Email Management
```csharp
// List emails
var emails = await client.Account.ListUserEmailsAsync();
Console.WriteLine($"Main: {emails.Data.MainEmail.Email}");
foreach (var email in emails.Data.OtherEmails)
{
    Console.WriteLine($"Other: {email.Email} (Private: {email.IsPrivate})");
}

// Set default email
await client.Account.SetDefaultEmailAsync("new-default@example.com");

// Remove email
await client.Account.RemoveUserEmailAsync("old-email@example.com");

// Get/Update notification settings
var settings = await client.Account.GetEmailSettingsAsync();
await client.Account.UpdateEmailSettingsAsync(new EmailNotificationSettingsOptional
{
    PerCommit = false,
    PerPullRequest = true,
    OnlyMyActivity = true
});
```

## Analysis API

### Repository Analysis

```csharp
// List repositories with analysis
var repos = await client.Analysis.ListOrganizationRepositoriesWithAnalysisAsync(
    Provider.gh,
    "my-organization",
    search: "backend",
    limit: 50
);

foreach (var repo in repos.Data)
{
    Console.WriteLine($"{repo.Repository.Name}:");
    Console.WriteLine($"  Grade: {repo.GradeLetter} ({repo.Grade}/100)");
    Console.WriteLine($"  Issues: {repo.IssuesCount}");
    Console.WriteLine($"  Coverage: {repo.Coverage?.CoveragePercentageWithDecimals}%");
    Console.WriteLine($"  LOC: {repo.Loc}");
}

// Get specific repository
var repoAnalysis = await client.Analysis.GetRepositoryWithAnalysisAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    branch: "main"
);

// Get analysis tools
var tools = await client.Analysis.ListRepositoryToolsAsync(
    Provider.gh,
    "my-org",
    "my-repo"
);

foreach (var tool in tools.Data)
{
    Console.WriteLine($"{tool.Name}: {(tool.Settings.IsEnabled ? "Enabled" : "Disabled")}");
}
```

### Pull Request Analysis

```csharp
// List pull requests
var pullRequests = await client.Analysis.ListRepositoryPullRequestsAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    limit: 20,
    includeNotAnalyzed: false
);

foreach (var pr in pullRequests.Data)
{
    Console.WriteLine($"PR #{pr.PullRequest.Number}: {pr.PullRequest.Title}");
    Console.WriteLine($"  New Issues: {pr.Analysis?.NewIssues ?? 0}");
    Console.WriteLine($"  Grade: {pr.Analysis?.Grade}");
}

// Get specific PR
var prDetails = await client.Analysis.GetRepositoryPullRequestAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    pullRequestNumber: 123
);

// Get PR issues
var prIssues = await client.Analysis.ListPullRequestIssuesAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    pullRequestNumber: 123,
    status: "new"
);

foreach (var issue in prIssues.Data)
{
    Console.WriteLine($"{issue.FilePath}:{issue.LineNumber} - {issue.Message}");
}

// Get PR files
var files = await client.Analysis.ListPullRequestFilesAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    pullRequestNumber: 123
);

// Bypass PR analysis (require admin permissions)
await client.Analysis.BypassPullRequestAnalysisAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    pullRequestNumber: 123
);
```

### Commit Analysis

```csharp
// List commits
var commits = await client.Analysis.ListRepositoryCommitsAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    branch: "main",
    limit: 50
);

foreach (var commit in commits.Data)
{
    Console.WriteLine($"{commit.Commit.Sha.Substring(0, 7)}: {commit.Commit.Message}");
    Console.WriteLine($"  Quality: {commit.Quality?.NewIssues} new, {commit.Quality?.FixedIssues} fixed");
    Console.WriteLine($"  Coverage: {commit.Coverage?.DeltaCoveragePercentage}% delta");
}

// Get specific commit
var commitDetails = await client.Analysis.GetCommitAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    "abc123def456789"
);

// Get commit delta statistics
var stats = await client.Analysis.GetCommitDeltaStatisticsAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    "abc123def456789"
);

Console.WriteLine($"Delta: +{stats.NewIssues} issues, -{stats.FixedIssues} issues");
Console.WriteLine($"Coverage: {stats.DeltaCoverageWithDecimals}%");

// Get commit files
var commitFiles = await client.Analysis.ListCommitFilesAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    "abc123def456789",
    filter: "withCoverageChanges"
);

// Get commit logs
var logs = await client.Analysis.ListCommitLogsAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    "abc123def456789"
);
```

### Search and Filter Issues

```csharp
// Search repository issues
var searchResults = await client.Analysis.SearchRepositoryIssuesAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    body: new SearchRepositoryIssuesBody
    {
        Categories = new List<string> { "Security", "ErrorProne" },
        Levels = new List<SeverityLevel> { SeverityLevel.High, SeverityLevel.Error },
        Languages = new List<string> { "C#" },
        FilePaths = new List<string> { "src/controllers/" }
    },
    limit: 100
);

// Get issues overview
var overview = await client.Analysis.GetIssuesOverviewAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    body: new SearchRepositoryIssuesBody
    {
        Categories = new List<string> { "Security" }
    }
);

Console.WriteLine($"Total security issues: {overview.Data.TotalIssues}");
Console.WriteLine($"Critical: {overview.Data.CriticalIssues}");

// Get specific issue
var issue = await client.Analysis.GetIssueAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    issueId: 12345
);

// Ignore/Unignore issue
await client.Analysis.UpdateIssueStateAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    "issue-id",
    new IssueStateBody { IsIgnored = true, Reason = "False positive" }
);

// Bulk ignore issues
await client.Analysis.BulkIgnoreIssuesAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    new BulkIgnoreIssuesBody 
    { 
        IssueIds = new List<long> { 1, 2, 3, 4, 5 },
        Reason = "Accepted risk"
    }
);
```

### Statistics and Metrics

```csharp
// Get commit statistics over time
var commitStats = await client.Analysis.ListCommitAnalysisStatsAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    branch: "main",
    days: 30
);

foreach (var stat in commitStats.Data)
{
    Console.WriteLine($"{stat.CommitTimestamp:yyyy-MM-dd}: " +
        $"{stat.NumberIssues} issues, {stat.NumberLoc} LOC");
}

// Get category overviews
var categories = await client.Analysis.ListCategoryOverviewsAsync(
    Provider.gh,
    "my-org",
    "my-repo"
);

foreach (var category in categories.Data)
{
    Console.WriteLine($"{category.Category}: {category.IssueCount} issues in {category.FileCount} files");
}
```

## People API

### Manage Team Members

```csharp
// List people
var people = await client.People.ListPeopleFromOrganizationAsync(
    Provider.gh,
    "my-org",
    search: "john",
    onlyMembers: true
);

foreach (var person in people.Data)
{
    Console.WriteLine($"{person.Username} ({person.Email}) - {person.Role}");
    Console.WriteLine($"  Joined: {person.Joined}");
    Console.WriteLine($"  Pending: {person.IsPending}");
}

// Add people
await client.People.AddPeopleToOrganizationAsync(
    Provider.gh,
    "my-org",
    new List<string> 
    { 
        "newuser@example.com",
        "another@example.com"
    }
);

// Remove people
var result = await client.People.RemovePeopleFromOrganizationAsync(
    Provider.gh,
    "my-org",
    new RemovePeopleBody 
    { 
        UserIds = new List<long> { 123, 456 } 
    }
);

Console.WriteLine($"Removed {result.Removed} people");

// Get people suggestions (potential team members based on commits)
var suggestions = await client.People.PeopleSuggestionsForOrganizationAsync(
    Provider.gh,
    "my-org",
    limit: 20
);

foreach (var suggestion in suggestions.Data)
{
    Console.WriteLine($"{suggestion.Name} <{suggestion.Email}>: {suggestion.CommitCount} commits");
}

// Repository-specific suggestions
var repoSuggestions = await client.People.PeopleSuggestionsForRepositoryAsync(
    Provider.gh,
    "my-org",
    "my-repo"
);

// Export as CSV
var csv = await client.People.ListPeopleFromOrganizationCsvAsync(
    Provider.gh,
    "my-org"
);
File.WriteAllText("team-members.csv", csv);
```

## Coverage API

### Pull Request Coverage

```csharp
// Get PR coverage summary
var prCoverage = await client.Coverage.GetRepositoryPullRequestCoverageAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    pullRequestNumber: 123
);

Console.WriteLine($"Total Coverage: {prCoverage.Data.TotalCoverage}%");
Console.WriteLine($"Delta Coverage: {prCoverage.Data.DeltaCoverage}%");
Console.WriteLine($"Meets Standards: {prCoverage.Data.IsUpToStandards}");

// Get file-level coverage
var fileCoverage = await client.Coverage.GetRepositoryPullRequestFilesCoverageAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    pullRequestNumber: 123
);

foreach (var file in fileCoverage.Data)
{
    Console.WriteLine($"{file.FilePath}:");
    Console.WriteLine($"  Coverage: {file.Coverage}%");
    Console.WriteLine($"  Delta: {file.DeltaCoverage}%");
    Console.WriteLine($"  Covered: {file.CoveredLines}/{file.CoverableLines}");
}

// Trigger reanalysis
await client.Coverage.ReanalyzeCoverageAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    pullRequestNumber: 123
);

// Get coverage reports
var reports = await client.Coverage.GetPullRequestCoverageReportsAsync(
    Provider.gh,
    "my-org",
    "my-repo",
    pullRequestNumber: 123
);

foreach (var report in reports.Data)
{
    Console.WriteLine($"Report for {report.CommitSha}:");
    Console.WriteLine($"  Coverage: {report.CoveragePercentage}%");
    Console.WriteLine($"  Files: {report.FilesCovered}");
    Console.WriteLine($"  Uploaded: {report.UploadTimestamp}");
}
```

## Coding Standards API

### Manage Coding Standards

```csharp
// List all coding standards
var standards = await client.CodingStandards.ListCodingStandardsAsync(
    Provider.gh,
    "my-org"
);

foreach (var std in standards.Data)
{
    Console.WriteLine($"{std.Name} (ID: {std.Id})");
    Console.WriteLine($"  Default: {std.IsDefault}, Draft: {std.IsDraft}");
    Console.WriteLine($"  Repositories: {std.RepositoryCount}");
    Console.WriteLine($"  Languages: {string.Join(", ", std.Languages)}");
}

// Create a new coding standard
var newStandard = await client.CodingStandards.CreateCodingStandardAsync(
    Provider.gh,
    "my-org",
    new CreateCodingStandardBody
    {
        Name = "Team Standard v2",
        Description = "Updated coding standard for 2024",
        IsDraft = false
    }
);

// Create from preset
var presetStandard = await client.CodingStandards.CreateCodingStandardPresetAsync(
    Provider.gh,
    "my-org",
    new CreateCodingStandardPresetBody
    {
        Name = "Strict Security Standard",
        Description = "Focused on security best practices",
        PresetName = "strict",
        Languages = new List<string> { "C#", "JavaScript", "TypeScript" }
    }
);

// Duplicate existing standard
var duplicated = await client.CodingStandards.DuplicateCodingStandardAsync(
    Provider.gh,
    "my-org",
    codingStandardId: 123
);

// Set as default
await client.CodingStandards.SetDefaultCodingStandardAsync(
    Provider.gh,
    "my-org",
    codingStandardId: newStandard.Data.Id,
    new SetDefaultCodingStandardBody 
    { 
        ApplyToAllRepositories = true 
    }
);

// Delete standard
await client.CodingStandards.DeleteCodingStandardAsync(
    Provider.gh,
    "my-org",
    codingStandardId: 999
);
```

### Manage Tools and Patterns

```csharp
// List tools in standard
var tools = await client.CodingStandards.ListCodingStandardToolsAsync(
    Provider.gh,
    "my-org",
    codingStandardId: 123
);

foreach (var tool in tools.Data)
{
    Console.WriteLine($"{tool.Name} ({tool.Language}):");
    Console.WriteLine($"  Enabled: {tool.IsEnabled}");
    Console.WriteLine($"  Patterns: {tool.EnabledPatterns}/{tool.TotalPatterns}");
}

// List patterns for a specific tool
var patterns = await client.CodingStandards.ListCodingStandardPatternsAsync(
    Provider.gh,
    "my-org",
    codingStandardId: 123,
    toolUuid: "tool-uuid-here",
    categories: "Security,ErrorProne",
    severityLevels: "High,Error",
    enabled: true,
    limit: 100
);

foreach (var pattern in patterns.Data)
{
    Console.WriteLine($"{pattern.PatternId}: {pattern.Title}");
    Console.WriteLine($"  Category: {pattern.Category}, Level: {pattern.Level}");
    Console.WriteLine($"  Enabled: {pattern.IsEnabled}, Recommended: {pattern.IsRecommended}");
}

// Update patterns (enable specific patterns)
await client.CodingStandards.UpdateCodingStandardPatternsAsync(
    Provider.gh,
    "my-org",
    codingStandardId: 123,
    toolUuid: "tool-uuid-here",
    new UpdatePatternsBody
    {
        Action = "enable",
        PatternIds = new List<string> { "pattern1", "pattern2", "pattern3" }
    }
);

// Enable all security patterns
await client.CodingStandards.UpdateCodingStandardPatternsAsync(
    Provider.gh,
    "my-org",
    codingStandardId: 123,
    toolUuid: "tool-uuid-here",
    new UpdatePatternsBody
    {
        Action = "enable",
        UpdateAllMatchingFilters = true
    },
    categories: "Security"
);

// Update tool configuration
await client.CodingStandards.UpdateCodingStandardToolConfigurationAsync(
    Provider.gh,
    "my-org",
    codingStandardId: 123,
    toolUuid: "tool-uuid-here",
    new ToolConfiguration
    {
        IsEnabled = true,
        UseConfigurationFile = false
    }
);
```

### Apply Standards to Repositories

```csharp
// List repositories using a standard
var repos = await client.CodingStandards.ListCodingStandardRepositoriesAsync(
    Provider.gh,
    "my-org",
    codingStandardId: 123
);

foreach (var repo in repos.Data)
{
    Console.WriteLine($"{repo.RepositoryName} (Applied: {repo.AppliedAt})");
}

// Apply standard to specific repositories
var applyResult = await client.CodingStandards.ApplyCodingStandardToRepositoriesAsync(
    Provider.gh,
    "my-org",
    codingStandardId: 123,
    new ApplyCodingStandardToRepositoriesBody
    {
        RepositoryIds = new List<long> { 1, 2, 3, 4, 5 },
        ForceApply = true
    }
);

Console.WriteLine($"Total: {applyResult.Data.TotalRepositories}");
Console.WriteLine($"Success: {applyResult.Data.SuccessCount}");
Console.WriteLine($"Failed: {applyResult.Data.FailedCount}");
Console.WriteLine($"Skipped: {applyResult.Data.SkippedCount}");

foreach (var result in applyResult.Data.Results)
{
    Console.WriteLine($"{result.RepositoryName}: {result.Status}");
    if (result.Status == "failed")
    {
        Console.WriteLine($"  Error: {result.ErrorMessage}");
    }
}

// Promote draft to production
var promoteResult = await client.CodingStandards.PromoteDraftCodingStandardAsync(
    Provider.gh,
    "my-org",
    codingStandardId: 456
);
```

## Security API

### Search and Manage Security Items

```csharp
// Search for critical security items
var items = await client.Security.SearchSecurityItemsAsync(
    Provider.gh,
    "my-org",
    body: new SearchSRMItems
    {
        Severities = new List<string> { "Critical", "High" },
        Categories = new List<string> { "SQL Injection", "XSS" },
        Status = new List<string> { "Open" },
        IncludeIgnored = false,
        DateFrom = DateTimeOffset.UtcNow.AddMonths(-3)
    },
    limit: 100,
    sort: "severity",
    direction: "desc"
);

foreach (var item in items.Data)
{
    Console.WriteLine($"{item.Title} ({item.Severity})");
    Console.WriteLine($"  Category: {item.Category}");
    Console.WriteLine($"  Repository: {item.Repository}");
    Console.WriteLine($"  Location: {item.FilePath}:{item.LineNumber}");
    Console.WriteLine($"  CVSS: {item.CvssScore}");
    if (item.CveId != null)
    {
        Console.WriteLine($"  CVE: {item.CveId}");
    }
}

// Get specific item
var item = await client.Security.GetSecurityItemAsync(
    Provider.gh,
    "my-org",
    srmItemId: Guid.Parse("...")
);

// Ignore item
await client.Security.IgnoreSecurityItemAsync(
    Provider.gh,
    "my-org",
    srmItemId: Guid.Parse("..."),
    new IgnoreSRMItemBody
    {
        Reason = "False positive - using safe wrapper",
        Notes = "Validated by security team on 2024-01-15"
    }
);

// Unignore item
await client.Security.UnignoreSecurityItemAsync(
    Provider.gh,
    "my-org",
    srmItemId: Guid.Parse("...")
);
```

### Security Dashboard

```csharp
// Get dashboard overview
var dashboard = await client.Security.SearchSecurityDashboardAsync(
    Provider.gh,
    "my-org",
    body: new SearchSRMDashboard
    {
        Repositories = new List<string> { "backend-api", "frontend-app" },
        DateFrom = DateTimeOffset.UtcNow.AddMonths(-1)
    }
);

Console.WriteLine($"Total Items: {dashboard.Data.TotalItems}");
Console.WriteLine($"Critical: {dashboard.Data.CriticalItems}");
Console.WriteLine($"High: {dashboard.Data.HighItems}");
Console.WriteLine($"Medium: {dashboard.Data.MediumItems}");
Console.WriteLine($"Low: {dashboard.Data.LowItems}");

Console.WriteLine("\nBy Category:");
foreach (var (category, count) in dashboard.Data.ItemsByCategory)
{
    Console.WriteLine($"  {category}: {count}");
}

// Get repository breakdown
var reposDashboard = await client.Security.SearchSecurityDashboardRepositoriesAsync(
    Provider.gh,
    "my-org",
    body: new SearchSRMDashboardRepositories
    {
        Repositories = new List<string> { "backend-api", "frontend-app" }
    }
);

foreach (var repo in reposDashboard.Data)
{
    Console.WriteLine($"{repo.Repository}:");
    Console.WriteLine($"  Total: {repo.TotalItems}");
    Console.WriteLine($"  Critical: {repo.CriticalItems}");
    Console.WriteLine($"  High: {repo.HighItems}");
}

// Get historical trend
var history = await client.Security.SearchSecurityDashboardHistoryAsync(
    Provider.gh,
    "my-org",
    body: new SearchSRMDashboardHistory
    {
        DateFrom = DateTimeOffset.UtcNow.AddMonths(-6),
        DateTo = DateTimeOffset.UtcNow
    }
);

foreach (var point in history.Data)
{
    Console.WriteLine($"{point.Date:yyyy-MM-dd}: {point.TotalItems} items " +
        $"({point.CriticalItems} critical, {point.HighItems} high)");
}

// Get category breakdown
var categories = await client.Security.SearchSecurityDashboardCategoriesAsync(
    Provider.gh,
    "my-org"
);

foreach (var category in categories.Data)
{
    Console.WriteLine($"{category.Category}: {category.Count}");
}
```

### Security Managers

```csharp
// List security managers
var managers = await client.Security.ListSecurityManagersAsync(
    Provider.gh,
    "my-org"
);

foreach (var manager in managers.Data)
{
    Console.WriteLine($"{manager.Username} <{manager.Email}>");
    Console.WriteLine($"  Added: {manager.AddedAt}");
}

// Add security manager
await client.Security.PostSecurityManagerAsync(
    Provider.gh,
    "my-org",
    new SecurityManagerBody { UserId = 123 }
);

// Remove security manager
await client.Security.DeleteSecurityManagerAsync(
    Provider.gh,
    "my-org",
    userId: 123
);
```

### SLA Configuration

```csharp
// Get current SLA config
var slaConfig = await client.Security.GetSLAConfigAsync(
    Provider.gh,
    "my-org"
);

Console.WriteLine($"SLA Enabled: {slaConfig.Data.IsEnabled}");
Console.WriteLine($"Critical: {slaConfig.Data.CriticalThresholdDays} days");
Console.WriteLine($"High: {slaConfig.Data.HighThresholdDays} days");
Console.WriteLine($"Medium: {slaConfig.Data.MediumThresholdDays} days");
Console.WriteLine($"Low: {slaConfig.Data.LowThresholdDays} days");

// Update SLA config
await client.Security.UpdateSLAConfigAsync(
    Provider.gh,
    "my-org",
    new SLAConfigBody
    {
        IsEnabled = true,
        CriticalThresholdDays = 3,
        HighThresholdDays = 7,
        MediumThresholdDays = 30,
        LowThresholdDays = 90
    }
);
```

### OSSF Scorecard

```csharp
// Get OSSF Scorecard for a repository
var scorecard = await client.Security.GetOssfScorecardAsync(
    new OssfScorecardUrlRequest
    {
        RepositoryUrl = "https://github.com/owner/repository"
    }
);

Console.WriteLine($"Repository: {scorecard.Data.RepositoryUrl}");
Console.WriteLine($"Overall Score: {scorecard.Data.Score}/10");
Console.WriteLine($"Scorecard Version: {scorecard.Data.ScorecardVersion}");

Console.WriteLine("\nChecks:");
foreach (var check in scorecard.Data.Checks)
{
    Console.WriteLine($"  {check.Name}: {check.Score}/10");
    if (check.Reason != null)
    {
        Console.WriteLine($"    Reason: {check.Reason}");
    }
}
```

## Best Practices

### Using CancellationToken

```csharp
var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

try
{
    var repos = await client.Analysis.ListOrganizationRepositoriesWithAnalysisAsync(
        Provider.gh,
        "my-org",
        cancellationToken: cts.Token
    );
}
catch (OperationCanceledException)
{
    Console.WriteLine("Request timed out");
}
```

### Error Handling

```csharp
try
{
    var user = await client.Account.GetUserAsync();
}
catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
{
    Console.WriteLine("Invalid API token");
}
catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
{
    Console.WriteLine("Resource not found");
}
catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
{
    var retryAfter = ex.Headers.RetryAfter?.Delta;
    Console.WriteLine($"Rate limited. Retry after: {retryAfter}");
}
catch (Refit.ApiException ex)
{
    Console.WriteLine($"API Error {ex.StatusCode}: {ex.Content}");
}
```

### Pagination Pattern

```csharp
async Task<List<T>> GetAllPaginatedAsync<T>(
    Func<string?, int, CancellationToken, Task<PaginatedResponse<T>>> fetchPage,
    int pageSize = 100,
    CancellationToken cancellationToken = default)
{
    var allItems = new List<T>();
    string? cursor = null;

    do
    {
        var response = await fetchPage(cursor, pageSize, cancellationToken);
        allItems.AddRange(response.Data);
        cursor = response.Pagination?.Cursor;
    }
    while (cursor != null);

    return allItems;
}

// Usage
var allRepos = await GetAllPaginatedAsync(
    (cursor, limit, ct) => client.Analysis.ListOrganizationRepositoriesWithAnalysisAsync(
        Provider.gh,
        "my-org",
        cursor: cursor,
        limit: limit,
        cancellationToken: ct
    )
);
```

### Proper Disposal

```csharp
// Using statement (recommended)
await using (var client = new CodacyClient(options))
{
    var user = await client.Account.GetUserAsync();
    // ... do work
}

// Or explicit disposal
var client = new CodacyClient(options);
try
{
    var user = await client.Account.GetUserAsync();
    // ... do work
}
finally
{
    client.Dispose();
}
```

### Dependency Injection in ASP.NET Core

```csharp
// Program.cs
builder.Services.AddSingleton<ICodacyClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new CodacyClient(new CodacyClientOptions
    {
        ApiToken = configuration["Codacy:ApiToken"] 
            ?? throw new InvalidOperationException("Codacy API token not configured")
    });
});

// Controller or Service
public class MyController : ControllerBase
{
    private readonly ICodacyClient _codacy;

    public MyController(ICodacyClient codacy)
    {
        _codacy = codacy;
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUser()
    {
        var user = await _codacy.Account.GetUserAsync();
        return Ok(user.Data);
    }
}
```

## Additional Resources

- [Codacy API Documentation](https://docs.codacy.com/codacy-api/)
- [Refit Documentation](https://github.com/reactiveui/refit)
- [Implementation Status](IMPLEMENTATION_STATUS.md)
