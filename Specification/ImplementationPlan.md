# Codacy.Api Implementation Plan

## Overview

This document outlines the phased implementation plan for the Codacy.Api .NET library. The library will provide comprehensive coverage of the Codacy API with modern C# patterns and best practices.

## Project Goals

- Create a production-ready .NET 9 library for the Codacy API
- Follow Panoramic Data Limited best practices and patterns
- Provide complete API coverage with strongly-typed models
- Include comprehensive documentation and tests
- Support both Codacy Cloud and self-hosted instances

## Technology Stack

- **.NET 9** - Target framework
- **Refit** - HTTP client generation and API abstraction
- **System.Text.Json** - JSON serialization
- **Microsoft.Extensions.Logging** - Structured logging
- **xUnit** - Testing framework
- **Nerdbank.GitVersioning** - Semantic versioning

## Implementation Phases

### Phase 1: Foundation ✅ COMPLETE

**Status**: COMPLETE

**Objectives**:
- [x] Create project structure following HaloPSA.Api pattern
- [x] Set up build infrastructure (csproj, .editorconfig, .gitignore)
- [x] Implement core client classes (CodacyClient, CodacyClientOptions)
- [x] Create exception hierarchy
- [x] Set up test project with basic unit tests
- [x] Configure NuGet package metadata
- [x] Set up GitHub Actions workflow for publishing
- [x] Initialize git repository

**Deliverables**:
- ✅ Codacy.Api.csproj with all dependencies
- ✅ CodacyClient and CodacyClientOptions classes
- ✅ Exception classes (CodacyApiException, CodacyAuthenticationException, etc.)
- ✅ Test project with 11 passing tests
- ✅ GitHub Actions workflow for NuGet publishing
- ✅ Comprehensive README.md
- ✅ Documentation files (CHANGELOG.md, LICENSE, copilot-instructions.md)

### Phase 2: API Models and Contracts

**Status**: NOT STARTED

**Objectives**:
- [ ] Research and document Codacy API endpoints
- [ ] Create model classes for API request/response objects
- [ ] Define Refit interface contracts for each API module
- [ ] Implement JSON serialization attributes
- [ ] Add XML documentation to all models

**Key API Modules**:
1. **Organizations API**
   - Get organization details
   - List repositories
   - Organization members

2. **Repositories API**
   - Get repository information
   - Repository quality metrics
   - Repository configuration

3. **Analysis API**
   - Get analysis results
   - Analysis history
   - Quality evolution

4. **Issues API**
   - List issues
   - Issue details
   - Issue patterns

5. **Commits API**
   - Commit analysis
   - Commit quality metrics
   - Coverage information

6. **Pull Requests API**
   - PR quality gates
   - PR analysis results
   - PR suggestions

**Deliverables**:
- [ ] Model classes in `Models/` directory
- [ ] Refit interface definitions in `Interfaces/`
- [ ] Request/response DTOs
- [ ] Enumerations for API constants

### Phase 3: API Implementation

**Status**: NOT STARTED

**Objectives**:
- [ ] Implement Organizations API
- [ ] Implement Repositories API
- [ ] Implement Analysis API
- [ ] Implement Issues API
- [ ] Implement Commits API
- [ ] Implement Pull Requests API
- [ ] Add retry logic and error handling
- [ ] Implement request/response logging

**Deliverables**:
- [ ] Complete implementation of all API modules
- [ ] HTTP client configuration with retry policies
- [ ] Logging infrastructure
- [ ] Error handling and exception mapping

### Phase 4: Testing

**Status**: NOT STARTED

**Objectives**:
- [ ] Write unit tests for all API methods
- [ ] Create integration tests with real API
- [ ] Add mock tests for error scenarios
- [ ] Test pagination handling
- [ ] Test authentication flows
- [ ] Achieve >80% code coverage

**Deliverables**:
- [ ] Comprehensive unit test suite
- [ ] Integration tests (using User Secrets for credentials)
- [ ] Mock tests for edge cases
- [ ] Test documentation

### Phase 5: Documentation and Examples

**Status**: NOT STARTED

**Objectives**:
- [ ] Complete XML documentation for all public APIs
- [ ] Create usage examples in README
- [ ] Add inline code examples
- [ ] Document authentication setup
- [ ] Create troubleshooting guide
- [ ] Add API reference documentation

**Deliverables**:
- [ ] Complete XML docs on all public members
- [ ] README with comprehensive examples
- [ ] Example projects (if needed)
- [ ] API reference guide

### Phase 6: Polish and Release

**Status**: NOT STARTED

**Objectives**:
- [ ] Code review and refactoring
- [ ] Performance optimization
- [ ] Security audit
- [ ] Final testing pass
- [ ] Version 1.0.0 release
- [ ] Publish to NuGet.org

**Deliverables**:
- [ ] Release-ready codebase
- [ ] Published NuGet package
- [ ] GitHub release with notes
- [ ] Updated CHANGELOG.md

## API Endpoint Reference

### Codacy API Base URL
- **Cloud**: `https://app.codacy.com/api/v3`
- **Self-Hosted**: `https://your-instance.com/api/v3`

### Authentication
- **Method**: API Token
- **Header**: `api-token: your-token-here`

### Key Endpoints

#### Organizations
- `GET /organizations/{provider}/{organizationName}`
- `GET /organizations/{provider}/{organizationName}/repositories`

#### Repositories
- `GET /analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}`
- `GET /analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/quality-settings`

#### Analysis
- `GET /analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits`
- `GET /analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues`

#### Issues
- `GET /analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues`
- `GET /analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/{issueId}`

#### Commits
- `GET /analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}`
- `GET /analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}/coverage`

#### Pull Requests
- `GET /analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestId}`
- `GET /analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestId}/quality`

### Common Parameters
- `provider` - Git provider identifier (e.g., `gh` for GitHub, `gl` for GitLab, `bb` for Bitbucket)
- `limit` - Number of results per page (default: 100, max: 1000)
- `cursor` - Pagination cursor for next page

## Development Guidelines

### Code Style
- Follow .editorconfig settings (tabs, file-scoped namespaces)
- Use modern C# features (primary constructors, collection expressions, required properties)
- Maintain zero warnings policy
- All public APIs must have XML documentation

### Testing Strategy
- Unit tests for all public methods
- Integration tests for critical paths
- Use User Secrets for test credentials
- Mock external dependencies where appropriate

### Version Control
- Use semantic versioning (MAJOR.MINOR.PATCH)
- Maintain CHANGELOG.md for all releases
- Tag releases with `v` prefix (e.g., `v1.0.0`)

### NuGet Publishing
- Automated via GitHub Actions on version tags
- Include symbols package for debugging
- Embed source link for GitHub integration

## Current Status

**Overall Progress**: Phase 1 Complete (Foundation)

**Next Steps**:
1. Research Codacy API documentation
2. Define model classes for core entities
3. Create Refit interfaces for each API module
4. Begin implementation of Organizations API

## Notes

- The project follows the same structure and patterns as HaloPSA.Api for consistency
- All tests are currently passing (11/11)
- The solution builds with zero errors and only expected git-related warnings
- Ready to proceed with Phase 2: API Models and Contracts

## Questions and Decisions

### Pending Decisions
1. Should we support Codacy API v2 or focus on v3 only? **Decision: v3 only**
2. What level of pagination abstraction should we provide? **Decision: TBD**
3. Should we implement rate limiting handling? **Decision: TBD**

### Resolved Decisions
1. ✅ Use Refit for HTTP client generation (consistent with other PD libraries)
2. ✅ Target .NET 9 only (no multi-targeting)
3. ✅ Use System.Text.Json (no Newtonsoft.Json)
4. ✅ Follow HaloPSA.Api project structure

## References

- [Codacy API Documentation](https://docs.codacy.com/codacy-api/)
- [Codacy API Reference](https://app.codacy.com/api/api-docs)
- [HaloPSA.Api Reference Implementation](../HaloPSA.Api/)
- [Panoramic Data Coding Standards](./copilot-instructions.md)
