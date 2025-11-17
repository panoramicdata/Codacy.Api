namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for the Codacy API client
/// </summary>
public interface ICodacyClient : IDisposable
{
	/// <summary>
	/// Gets the Version API module
	/// </summary>
	IVersionApi Version { get; }

	/// <summary>
	/// Gets the Account API module
	/// </summary>
	IAccountApi Account { get; }

	/// <summary>
	/// Gets the Organizations API module
	/// </summary>
	IOrganizationsApi Organizations { get; }

	/// <summary>
	/// Gets the Repositories API module
	/// </summary>
	IRepositoriesApi Repositories { get; }

	/// <summary>
	/// Gets the Analysis API module
	/// </summary>
	IAnalysisApi Analysis { get; }

	/// <summary>
	/// Gets the Issues API module
	/// </summary>
	IIssuesApi Issues { get; }

	/// <summary>
	/// Gets the Commits API module
	/// </summary>
	ICommitsApi Commits { get; }

	/// <summary>
	/// Gets the Pull Requests API module
	/// </summary>
	IPullRequestsApi PullRequests { get; }

	/// <summary>
	/// Gets the People API module
	/// </summary>
	IPeopleApi People { get; }

	/// <summary>
	/// Gets the Coverage API module
	/// </summary>
	ICoverageApi Coverage { get; }

	/// <summary>
	/// Gets the Coding Standards API module
	/// </summary>
	ICodingStandardsApi CodingStandards { get; }

	/// <summary>
	/// Gets the Security API module
	/// </summary>
	ISecurityApi Security { get; }
}
