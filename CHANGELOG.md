# Changelog

All notable changes to the Codacy.Api project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## 4.0.0

### Fixed
- Rewrote every Security and Risk Management (SRM) model against the Codacy OpenAPI
  specification. The previous models were hand-written from guesswork and described fields the
  API never returns, so `SearchSecurityItemsAsync` threw `ApiException: An error occured
  deserializing the response` on every call, and `ListSecurityRepositoriesAsync` silently
  returned rows whose properties were all null.
- The search bodies on `ISecurityApi` are no longer nullable. A null body reached Codacy as the
  literal `null` and was rejected with `DecodingFailure at .repositories: Missing required
  field`; pass an empty instance (for example `new SearchSRMItems()`) to search unfiltered.

### Added
- `SrmPriority`, `SrmStatus` and `SrmSource` enums, so a finding's severity and lifecycle state
  are typed rather than free strings.
- `SrmIgnoredBody`, `AdvisoryInformation`, `ContainerImageFilter`, `SrmDastReportState`,
  `DastTool`, `OssfScorecardSeverity` and `OssfScorecardDocumentation`.
- SCA, DAST, container and penetration-testing fields on `SrmItem`: `Cvss*`, `Cwe`, `Cve`,
  `AffectedVersion`, `FixedVersion`, `DependencyChains`, `ImageName`, `ImageTag`, `Remediation`
  and others.

### Changed (breaking)
- `SrmItem`: `Severity` → `Priority` (now `SrmPriority`), `Status` → `SrmStatus`, `Category` →
  `SecurityCategory`, `FirstDetected`/`LastDetected` → `OpenedAt`/`DueAt`/`ClosedAt`. Added
  `ItemSource`, `ItemSourceId`, `ScanType`, `HtmlUrl`, `ProjectKey`, `Ignored`. Removed
  `Description`, `FilePath`, `LineNumber`, `IsIgnored`, `IgnoredReason`, `IgnoredBy`,
  `IgnoredAt`, `PackageName` and `PackageVersion`, none of which the API returns.
- `SearchSRMItems`: `Severities` → `Priorities` (now `List<SrmPriority>`), `Status` → `Statuses`
  (now `List<SrmStatus>`). Added `ScanTypes`, `Segments`, `DastTargetUrls`, `SearchText` and
  `ContainerImage`; removed `Query`, `IncludeIgnored`, `DateFrom` and `DateTo`. Unset filters
  are now omitted rather than serialized as null.
- `SecurityRepositoriesResponse.Data` is now `List<RepositorySummary>`; the `SecurityRepository`
  type is removed, as the endpoint returns repository identities and not issue counts.
- `SRMDashboard` now carries the real per-severity and per-scan-type counts
  (`TotalOpen`, `OnTrack`, `DueSoon`, `Overdue`, `OpenCritical`, `OpenSast`, `OpenIaC`, …) in
  place of the invented `ItemsByCategory` and `Trend`.
- `SRMDashboardRepository` → `SRMRepositoryIssueCount`, `SRMDashboardHistoryPoint` →
  `SRMHistoryDataPoint`, `SRMDashboardCategory` → `SRMCategoryIssueCount`, `DASTReport` →
  `SRMDastReport`.
- `SLAConfig` is now `CriticalSla`/`HighSla`/`MediumSla`/`LowSla`, wrapped under a `SlaConfig`
  property on `SLAConfigResponse` and `SLAConfigBody`.
- `SecurityManager`: `Username` → `Name` (optional), `AddedAt` → `CreatedAt`.
- `IgnoreSRMItemBody`: `Notes` → `Comment`, and `Reason` is optional.
- `OssfScorecard` gains `Date`, `FailingCheckCount` and `PassingCheckCount`; checks gain
  `Details`, `Documentation` and `Severity`. `OssfScorecardUrlRequest` now takes an optional
  `Url` or `Purl` rather than a required `RepositoryUrl`.

## 3.0.10

### Changed
- Adopted Central Package Management (CPM) via `Directory.Packages.props`
- Updated all outdated dependencies to latest stable versions:
  - Microsoft.Extensions.Http 9.0.10 → 10.0.5
  - Microsoft.Extensions.Logging 9.0.10 → 10.0.5
  - Microsoft.Extensions.Logging.Abstractions 9.0.10 → 10.0.5
  - Microsoft.Extensions.Configuration 9.0.10 → 10.0.5
  - Microsoft.Extensions.Configuration.Json 9.0.10 → 10.0.5
  - Microsoft.Extensions.Configuration.UserSecrets 9.0.10 → 10.0.5
  - Microsoft.Extensions.Logging.Console 9.0.10 → 10.0.5
  - Refit 8.0.0 → 10.1.6
  - Refit.HttpClientFactory 8.0.0 → 10.1.6

### Fixed
- Fixed CA1873 analyzer warnings in `LoggingHttpClientHandler` by guarding `ToString()` calls with `IsEnabled` check

## 3.0.9

### Added
- Initial release of Codacy.Api - comprehensive .NET library for Codacy API
- Modern .NET 10 implementation with latest C# features
- API token-based authentication
- Comprehensive API coverage for core endpoints:
  - **Account** - User account information and organizations
  - **Organizations** - Organization management, billing, and people
  - **Repositories** - Repository analysis, configuration, and management
  - **Analysis** - Code quality analysis results, commits, and pull requests
  - **Issues** - Code quality issues, patterns, and ignore management
  - **Coverage** - Code coverage reporting and analysis
  - **People** - Organization people management and suggestions
  - **Coding Standards** - Coding standard configuration
  - **Security** - Security and risk management (SRM) features
- Advanced HTTP client features:
  - Automatic retry logic with exponential backoff
  - Comprehensive request/response logging
  - Custom error handling with detailed exception hierarchy
  - Configurable timeouts and retry policies
- Robust error handling with custom exception types
- Full XML documentation for IntelliSense support
- NuGet package with symbols for debugging
- Comprehensive integration test suite

### Changed
- **BREAKING**: Refactored API interfaces for consistency and correctness
- **BREAKING**: `IPeopleApi.ListPeopleFromOrganizationAsync` now returns `ListResponse<OrganizationPerson>` instead of `ListResponse<Person>`
- **BREAKING**: `IPeopleApi.RemovePeopleFromOrganizationAsync` now uses `OrganizationRemovePeopleBody` and `OrganizationRemovePeopleResponse` instead of `RemovePeopleBody` and `RemovePeopleResponse`
- **BREAKING**: `OrganizationResponse.Data` now uses nested `Organization` property for organization details (access via `response.Data.Organization.Name` instead of `response.Data.Name`)
- **BREAKING**: Removed duplicate `Person`, `RemovePeopleBody`, and `RemovePeopleResponse` classes - use `OrganizationPerson` and Organization-specific models instead
- Unified model usage across `IPeopleApi` and `IOrganizationsApi` for shared endpoints

### Fixed
- Fixed deserialization issues with organization API responses by correctly handling nested data structure
- Fixed model conflicts between `IPeopleApi` and `IOrganizationsApi` that caused cascading test failures
- Aligned API models with actual Codacy API response structures
