namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for the Codacy API client
/// </summary>
public interface ICodacyClient : IDisposable
{
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
}
