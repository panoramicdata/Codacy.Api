using System.Text.Json;
using System.Text.Json.Serialization;
using Codacy.Api.Models;

namespace Codacy.Api.Test;

/// <summary>
/// Deserialization tests for <see cref="FileWithAnalysisInfo"/>, against a payload as the file
/// listing actually returns it.
/// </summary>
public class FileWithAnalysisInfoTests
{
	/// <summary>
	/// A real response body from
	/// <c>GET /api/v3/organizations/gh/{org}/repositories/{repo}/files</c>, for a test file graded F
	/// on duplication alone.
	/// </summary>
	private const string _fileJson = """
		{
		  "fileId": 228158475449,
		  "branchId": 26785640,
		  "path": "Athonet.Api.Test/TaiTests.cs",
		  "totalIssues": 0,
		  "grade": 0,
		  "gradeLetter": "F",
		  "numberOfMethods": 0,
		  "complexity": 3,
		  "duplication": 36,
		  "linesOfCode": 41,
		  "numberOfClones": 5
		}
		""";

	/// <summary>
	/// The same options CodacyClient configures Refit's serializer with.
	/// </summary>
	private static readonly JsonSerializerOptions _options = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		PropertyNameCaseInsensitive = true,
		Converters = { new JsonStringEnumConverter() }
	};

	[Fact]
	public void Deserialize_ReadsTheDuplicationBehindAGrade()
	{
		// Duplication and clone count are part of the grade and are often the whole of it. Without
		// them a caller can say a file is graded F but not why, and "grade F, 0 issues" reads as a
		// contradiction of the repository's own issues page.
		var file = JsonSerializer.Deserialize<FileWithAnalysisInfo>(_fileJson, _options);

		file.Should().NotBeNull();
		file!.Duplication.Should().Be(36);
		file.NumberOfClones.Should().Be(5);
	}

	[Fact]
	public void Deserialize_ReadsTheGradeAndItsOtherComponents()
	{
		var file = JsonSerializer.Deserialize<FileWithAnalysisInfo>(_fileJson, _options);

		file.Should().NotBeNull();
		file!.Path.Should().Be("Athonet.Api.Test/TaiTests.cs");
		file.GradeLetter.Should().Be("F");
		file.Grade.Should().Be(0);
		file.TotalIssues.Should().Be(0);
		file.Complexity.Should().Be(3);
		file.LinesOfCode.Should().Be(41);
	}

	[Fact]
	public void Deserialize_LeavesDuplicationUnsetWhenCodacyDidNotMeasureIt()
	{
		// Codacy omits the measurements it did not take rather than sending zero, and a missing
		// measurement must not be reported as "no duplication".
		const string json = """
			{
			  "fileId": 1,
			  "branchId": 2,
			  "path": "README.md",
			  "totalIssues": 0,
			  "grade": 0,
			  "gradeLetter": "",
			  "numberOfMethods": 0
			}
			""";

		var file = JsonSerializer.Deserialize<FileWithAnalysisInfo>(json, _options);

		file.Should().NotBeNull();
		file!.Duplication.Should().BeNull();
		file.NumberOfClones.Should().BeNull();
		file.Complexity.Should().BeNull();
	}
}
