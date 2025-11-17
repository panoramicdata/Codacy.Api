# Changelog

All notable changes to the Codacy.Api project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Initial release of Codacy.Api - comprehensive .NET library for Codacy API
- Modern .NET 9 implementation with latest C# features
- API token-based authentication
- Comprehensive API coverage for core endpoints:
  - **Organizations** - Organization management and information
  - **Repositories** - Repository analysis and configuration
  - **Analysis** - Code quality analysis results
  - **Issues** - Code quality issues and patterns
  - **Commits** - Commit analysis and coverage
  - **Pull Requests** - PR quality gates and analysis
- Advanced HTTP client features:
  - Automatic retry logic with exponential backoff
  - Comprehensive request/response logging
  - Custom error handling with detailed exception hierarchy
  - Configurable timeouts and retry policies
- Robust error handling with custom exception types
- Full XML documentation for IntelliSense support
- NuGet package with symbols for debugging
- Comprehensive test suite
