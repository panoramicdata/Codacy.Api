# Changelog

All notable changes to the Codacy.Api project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
