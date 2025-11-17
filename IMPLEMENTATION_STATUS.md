# Codacy API Implementation Status

This document tracks the implementation status of all Codacy API v3 endpoints based on the official Swagger specification at https://api.codacy.com/api/api-docs/swagger.yaml

## Implementation Approach

All APIs are implemented using **Refit** for clean, type-safe HTTP client generation. Each API category is split into:
1. Interface with Refit attributes (`Codacy.Api/Interfaces/I{Name}Api.cs`)
2. Model classes (`Codacy.Api/Models/{Name}.cs`)
3. Refit automatically generates the implementation

## API Categories

### ? Version API (`IVersionApi`)
- [x] `GET /version` - Get Codacy installation version

### ? Account API (`IAccountApi`)
- [x] `GET /user` - Get authenticated user
- [x] `DELETE /user` - Delete authenticated user
- [x] `PATCH /user` - Update authenticated user
- [x] `GET /user/organizations` - List user organizations
- [x] `GET /user/organizations/{provider}` - List organizations by provider
- [x] `GET /user/organizations/{provider}/{remoteOrganizationName}` - Get specific organization
- [x] `GET /user/emails` - List user emails
- [x] `POST /user/emails/remove` - Remove email from account
- [x] `GET /user/emails/settings` - Get email notification settings
- [x] `PATCH /user/emails/settings` - Update email notification settings
- [x] `POST /user/emails/set-default` - Set default email
- [x] `GET /user/integrations` - List user integrations
- [x] `DELETE /user/integrations/{provider}` - Delete integration
- [x] `GET /user/tokens` - Get API tokens  
- [x] `POST /user/tokens` - Create API token
- [x] `DELETE /user/tokens/{tokenId}` - Delete API token

### ? Analysis API (`IAnalysisApi`)
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories` - List organization repositories with analysis
- [x] `POST /search/analysis/organizations/{provider}/{remoteOrganizationName}/repositories` - Search repositories
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}` - Get repository analysis
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/tools` - List repository tools
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/tools/conflicts` - List tool conflicts
- [x] `PATCH /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/tools/{toolUuid}` - Configure tool
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/analysis-progress` - Get analysis progress
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests` - List pull requests
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}` - Get pull request
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/commits` - Get PR commits
- [x] `POST /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/bypass` - Bypass PR analysis
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/issues` - Get PR issues
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/clones` - Get PR clones
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/logs` - Get PR logs
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/commit-statistics` - List commit stats
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/category-overviews` - List category overviews
- [x] `POST /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/issues/search` - Search issues
- [x] `POST /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/issues/overview` - Get issues overview
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/issues/{issueId}` - Get issue
- [x] `PATCH /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/issues/{issueId}` - Update issue state
- [x] `POST /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/issues/bulk-ignore` - Bulk ignore issues
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/commits` - List commits
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/commits/{commitUuid}` - Get commit
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/commits/{commitUuid}/deltaStatistics` - Get commit delta stats
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/commits/{srcCommitUuid}/deltaIssues` - Get commit delta issues
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/commits/{commitUuid}/clones` - Get commit clones
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/commits/{commitUuid}/logs` - Get commit logs
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/commits/{commitUuid}/files` - Get commit files
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/files` - Get PR files
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/pull-requests` - List organization PRs

### ? Organizations API (`IOrganizationsApi`)
Status: Partially Implemented
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}` - Get organization
- [ ] `DELETE /organizations/{provider}/{remoteOrganizationName}` - Delete organization
- [ ] `GET /organizations/{provider}/installation/{installationId}` - Get organization by installation ID
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/billing` - Get organization billing
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/billing` - Update organization billing
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/billing/card` - Get billing card
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/billing/card` - Add billing card
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/billing/estimation` - Get billing estimation
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/billing/change-plan` - Change organization plan
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/billing/sync` - Sync marketplace billing
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/integrations/providerSettings/apply` - Apply provider settings
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/integrations/providerSettings` - Get provider settings
- [ ] `PATCH /organizations/{provider}/{remoteOrganizationName}/integrations/providerSettings` - Update provider settings
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories` - List repositories
- [ ] `GET /onboarding/organizations/{provider}/{remoteOrganizationName}/progress` - Get onboarding progress
- [ ] `PATCH /organizations/{provider}/{remoteOrganizationName}/analysisConfigurationMinimumPermission` - Update org settings
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/gitProviderAppPermissions` - Get Git provider app permissions

### ? Repositories API (`IRepositoriesApi`)
Status: Partially Implemented
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/follow` - Follow repository
- [ ] `DELETE /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/follow` - Unfollow repository
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/quality/repository` - Get quality settings
- [ ] `PUT /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/quality/repository` - Update quality settings
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/ssh-user-key` - Regenerate user SSH key
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/ssh-repository-key` - Regenerate repository SSH key
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/stored-ssh-key` - Get public SSH key
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/sync` - Sync repository with provider
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/analysis` - Get build server analysis setting
- [ ] `PATCH /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/analysis` - Update build server analysis setting
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/languages` - Get repository languages
- [ ] `PATCH /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/languages` - Configure language settings
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/file-extensions` - Get file extensions (deprecated)
- [ ] `PATCH /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/file-extensions` - Update file extensions (deprecated)
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/quality/commits` - Get commit quality settings
- [ ] `PUT /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/quality/commits` - Update commit quality settings
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/quality/commits/reset` - Reset commit quality settings
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/quality/pull-requests/reset` - Reset PR quality settings
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/quality/repository/reset` - Reset repository quality settings
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/quality/pull-requests` - Get PR quality settings
- [ ] `PUT /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/settings/quality/pull-requests` - Update PR quality settings
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/reanalyzeCommit` - Reanalyze commit
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}` - Get repository
- [ ] `DELETE /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}` - Delete repository
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/branches` - List branches
- [ ] `PATCH /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/branches/{branchName}` - Update branch configuration
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/integrations/providerSettings` - Get repository integration settings
- [ ] `PATCH /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/integrations/providerSettings` - Update repository integration settings
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/integrations/postCommitHook` - Create post-commit hook
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/integrations/refreshProvider` - Refresh provider integration

### ? People API (`IPeopleApi`)
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/people` - List people
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/people` - Add people
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/people/remove` - Remove people
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/people/suggestions` - List people suggestions for organization
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/people/suggestions` - List people suggestions for repository
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/peopleCsv` - Export people as CSV

### ? Coverage API (`ICoverageApi`)
- [x] `GET /coverage/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}` - Get PR coverage
- [x] `GET /coverage/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/files` - Get PR files coverage
- [x] `GET /coverage/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/reanalyze` - Reanalyze coverage
- [x] `GET /analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/coverage/status` - Get coverage reports status

### ? Coding Standards API (`ICodingStandardsApi`)
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/coding-standards` - List coding standards
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/coding-standards` - Create coding standard
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/presets-standards` - Create from presets
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}` - Get coding standard
- [x] `DELETE /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}` - Delete coding standard
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/duplicate` - Duplicate coding standard
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/setDefault` - Set default coding standard
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/tools` - List tools
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/tools/{toolUuid}/patterns` - List patterns
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/tools/{toolUuid}/patterns/update` - Update patterns
- [x] `PATCH /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/tools/{toolUuid}` - Update tool configuration
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/repositories` - List repositories using standard
- [x] `PATCH /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/repositories` - Apply standard to repositories
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/promote` - Promote draft standard

### ? Security API (`ISecurityApi`)
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/security/items/search` - Search security items
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/security/items/{srmItemId}` - Get security item
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/security/items/{srmItemId}/ignore` - Ignore security item
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/security/items/{srmItemId}/unignore` - Unignore security item
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/security/dashboard` - Get security dashboard
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/security/dashboard/repositories/search` - Search dashboard repositories
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/security/dashboard/history/search` - Search dashboard history
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/security/dashboard/categories/search` - Search dashboard categories
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/security/managers` - List security managers
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/security/managers` - Add security manager
- [x] `DELETE /organizations/{provider}/{remoteOrganizationName}/security/managers/{userId}` - Remove security manager
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/security/repositories` - List security repositories
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/security/categories` - List security categories
- [x] `POST /organizations/{provider}/{remoteOrganizationName}/security/tools/dast/{toolName}/reports` - Upload DAST report
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/security/dast/reports` - List DAST reports
- [x] `GET /organizations/{provider}/{remoteOrganizationName}/security/sla` - Get SLA config
- [x] `PUT /organizations/{provider}/{remoteOrganizationName}/security/sla` - Update SLA config
- [x] `POST /security/dependencies/ossf/scorecard` - Get OSSF Scorecard

### ? Issues API (`IIssuesApi`)
Status: Needs Update
- [ ] Update to use Refit attributes

### ? Pull Requests API (`IPullRequestsApi`)
Status: Needs Update
- [ ] Update to use Refit attributes

### ? Commits API (`ICommitsApi`)
Status: Needs Update
- [ ] Update to use Refit attributes

### ?? Additional APIs To Implement

#### Tools API
- [ ] `GET /tools` - List available tools
- [ ] `GET /tools/{toolUuid}` - Get tool details
- [ ] `GET /tools/{toolUuid}/patterns` - List tool patterns

#### Languages API  
- [ ] `GET /languages` - List supported languages

#### Metrics API
- [ ] Various metrics endpoints

#### File API
- [ ] File-specific operations

#### Integrations API
- [ ] Integration management endpoints

#### Health API
- [ ] `GET /health` - Health check

#### Admin API
- [ ] Administrative endpoints (for Codacy admin users)

#### JIRA API
- [ ] JIRA integration endpoints

#### Segments API
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/segments` - List segments
- [ ] `POST /organizations/{provider}/{remoteOrganizationName}/segments` - Create segment
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/segments/{segmentId}` - Get segment
- [ ] `PUT /organizations/{provider}/{remoteOrganizationName}/segments/{segmentId}` - Update segment
- [ ] `DELETE /organizations/{provider}/{remoteOrganizationName}/segments/{segmentId}` - Delete segment

#### Enterprise API
- [ ] Enterprise-specific endpoints

#### SBOM (Software Bill of Materials) API
- [ ] `GET /organizations/{provider}/{remoteOrganizationName}/sbom` - Get SBOM
- [ ] SBOM-related endpoints

#### Audit API
- [ ] Audit log endpoints

## Model Files

### ? Common.cs
Contains shared models: Provider, Pagination, User, Organization, Enums, etc.

### ? Analysis.cs
Contains analysis-related models: Commits, Coverage, Quality Analysis, etc.

### ? Organization.cs
Status: Needs expansion for billing, settings, etc.

### ? Repository.cs
Status: Needs expansion for settings, branches, etc.

### ? Issue.cs
Status: Needs expansion

### ? PullRequest.cs
Status: Needs expansion

### ? Version.cs
Status: Complete

### ?? Additional Model Files Needed
- [ ] CodingStandards.cs
- [ ] Security.cs
- [ ] Coverage.cs
- [ ] People.cs
- [ ] Billing.cs
- [ ] Integrations.cs
- [ ] Tools.cs
- [ ] Languages.cs
- [ ] Segments.cs
- [ ] SBOM.cs
- [ ] Audit.cs

## Next Steps

1. **Complete Missing Model Classes**: Create all model classes for the new APIs
2. **Update Existing API Interfaces**: Convert remaining APIs to use Refit attributes
3. **Update CodacyClient**: Add all new API properties
4. **Testing**: Create comprehensive integration tests
5. **Documentation**: Add XML documentation and usage examples
6. **Error Handling**: Implement proper exception handling for all HTTP status codes

## Notes

- All APIs use Refit for automatic client generation
- Authentication is handled via `api-token` header in HttpClient
- Base URL defaults to `https://app.codacy.com/api/v3`
- All async methods support CancellationToken
- Pagination is supported via cursor-based pagination where applicable
