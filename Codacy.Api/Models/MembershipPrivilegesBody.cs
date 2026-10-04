namespace Codacy.Api.Models;

/// <summary>
/// Minimum permission level regarding configuring patterns, configuring which files to analyze and other analysis settings
/// </summary>
public class MembershipPrivilegesBody
{
	/// <summary>Minimum permission</summary>
	public MembershipPrivileges? Permission { get; set; }
}
