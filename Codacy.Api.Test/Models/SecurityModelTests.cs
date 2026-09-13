using System.Text.Json;

namespace Codacy.Api.Test.Models;

/// <summary>
/// Deserialization tests for the Security and Risk Management models, using response bodies
/// captured verbatim from the Codacy API. These caught the original models, which were written
/// speculatively rather than from the OpenAPI specification and threw on every real response.
/// </summary>
public class SecurityModelTests
{
	private static JsonSerializerOptions Options => CodacyClient.JsonSerializerOptions;

	private static T Deserialize<T>(string json) =>
		JsonSerializer.Deserialize<T>(json, Options)
		?? throw new InvalidOperationException("Deserialized to null.");

	[Fact]
	public void SrmItemsResponse_WithSastItem_ReadsPriorityCategoryAndScanType()
	{
		const string json = """
			{
			  "pagination": { "cursor": "2", "limit": 3, "total": 9 },
			  "data": [
			    {
			      "id": "9bbae4d1-da30-43c0-a243-6e8add96ae8c",
			      "itemSource": "Codacy",
			      "itemSourceId": "131466833554",
			      "title": "OS command injection is a critical vulnerability.",
			      "openedAt": "2026-04-03T14:16:17.133Z",
			      "dueAt": "2026-05-03T14:16:17.139358Z",
			      "priority": "Critical",
			      "status": "Overdue",
			      "repository": "PanoramicData.NugetManagement",
			      "htmlUrl": "https://app.codacy.com/p/848725/issues/index?resultDataId=131466833554",
			      "securityCategory": "CommandInjection",
			      "scanType": "SAST"
			    }
			  ]
			}
			""";

		var response = Deserialize<SrmItemsResponse>(json);

		var item = response.Data.Should().ContainSingle().Subject;
		item.Id.Should().Be(Guid.Parse("9bbae4d1-da30-43c0-a243-6e8add96ae8c"));
		item.ItemSource.Should().Be(SrmSource.Codacy);
		item.ItemSourceId.Should().Be("131466833554");
		item.Priority.Should().Be(SrmPriority.Critical);
		item.Status.Should().Be(SrmStatus.Overdue);
		item.Repository.Should().Be("PanoramicData.NugetManagement");
		item.SecurityCategory.Should().Be("CommandInjection");
		item.ScanType.Should().Be("SAST");
		item.HtmlUrl.Should().Be("https://app.codacy.com/p/848725/issues/index?resultDataId=131466833554");
		item.OpenedAt.Should().Be(DateTimeOffset.Parse("2026-04-03T14:16:17.133Z", CultureInfo.InvariantCulture));
		item.DueAt.Should().Be(DateTimeOffset.Parse("2026-05-03T14:16:17.139358Z", CultureInfo.InvariantCulture));
		item.ClosedAt.Should().BeNull();
		response.Pagination.Cursor.Should().Be("2");
		response.Pagination.Total.Should().Be(9);
	}

	[Fact]
	public void SrmItem_WhenClosed_ReadsClosedAt()
	{
		const string json = """
			{
			  "id": "cc21a0b0-af18-498b-927b-45a074e1d0c0",
			  "itemSource": "Codacy",
			  "itemSourceId": "131447856000",
			  "title": "Generic API Key detected",
			  "openedAt": "2026-02-25T17:00:18.934Z",
			  "dueAt": "2026-03-27T17:00:18.951537Z",
			  "priority": "Critical",
			  "status": "ClosedLate",
			  "repository": "Meraki.Api",
			  "closedAt": "2026-08-30T00:27:54.615Z",
			  "securityCategory": "InsecureStorage",
			  "scanType": "Secrets"
			}
			""";

		var item = Deserialize<SrmItem>(json);

		item.Status.Should().Be(SrmStatus.ClosedLate);
		item.ClosedAt.Should().Be(DateTimeOffset.Parse("2026-08-30T00:27:54.615Z", CultureInfo.InvariantCulture));
		item.ScanType.Should().Be("Secrets");
	}

	[Fact]
	public void SrmItem_WithoutOptionalFields_OmitsThemRatherThanThrowing()
	{
		// The oldest items carry neither securityCategory nor scanType; a required property
		// on either turns a whole page of findings into a deserialization failure.
		const string json = """
			{
			  "id": "d27bd3b4-32b9-4f0e-b02a-db6879b3c417",
			  "itemSource": "Codacy",
			  "itemSourceId": "130749883922",
			  "title": "Add a 'default' clause to this 'switch' statement.",
			  "openedAt": "2023-04-24T09:35:32.321Z",
			  "dueAt": "2023-05-24T09:35:32.342825Z",
			  "priority": "Critical",
			  "status": "ClosedLate",
			  "repository": "LogicMonitor.Datamart",
			  "closedAt": "2026-03-15T13:05:34.569Z"
			}
			""";

		var item = Deserialize<SrmItem>(json);

		item.SecurityCategory.Should().BeNull();
		item.ScanType.Should().BeNull();
		item.HtmlUrl.Should().BeNull();
		item.Ignored.Should().BeNull();
	}

	[Fact]
	public void SrmItem_WhenIgnored_ReadsIgnoredBody()
	{
		const string json = """
			{
			  "id": "d27bd3b4-32b9-4f0e-b02a-db6879b3c417",
			  "itemSource": "Jira",
			  "itemSourceId": "764334",
			  "title": "Weak random number generator",
			  "openedAt": "2017-07-21T17:32:28Z",
			  "dueAt": "2017-08-21T17:32:28Z",
			  "priority": "Low",
			  "status": "Ignored",
			  "ignored": {
			    "at": "2017-08-21T17:32:28Z",
			    "authorId": 42,
			    "authorName": "John Doe",
			    "reason": "False positive"
			  }
			}
			""";

		var item = Deserialize<SrmItem>(json);

		item.ItemSource.Should().Be(SrmSource.Jira);
		item.Status.Should().Be(SrmStatus.Ignored);
		item.Repository.Should().BeNull();
		item.Ignored.Should().NotBeNull();
		item.Ignored!.AuthorId.Should().Be(42);
		item.Ignored.AuthorName.Should().Be("John Doe");
		item.Ignored.Reason.Should().Be("False positive");
		item.Ignored.At.Should().Be(DateTimeOffset.Parse("2017-08-21T17:32:28Z", CultureInfo.InvariantCulture));
	}

	[Fact]
	public void SrmItem_WithScaFields_ReadsAdvisoryAndDependencyChains()
	{
		const string json = """
			{
			  "id": "d27bd3b4-32b9-4f0e-b02a-db6879b3c417",
			  "itemSource": "Trivy",
			  "itemSourceId": "764335",
			  "title": "Vulnerable dependency",
			  "openedAt": "2024-03-14T06:54:42.869673Z",
			  "dueAt": "2024-04-14T06:54:42.869673Z",
			  "priority": "High",
			  "status": "OnTrack",
			  "scanType": "SCA",
			  "cvssScore": 0.42,
			  "cvssVector": "CVSS:3.0/AV:A/AC:H/PR:L/UI:R/S:C/C:L/I:L/A:L/E:U/MUI:N",
			  "cwe": "CWE-122",
			  "cve": "CVE-1970-1234",
			  "affectedVersion": "1.0.0",
			  "fixedVersion": ["1.0.1"],
			  "dependencyChains": [["root-app", "lodash", "vulnerable-pkg"]],
			  "advisoryInformation": {
			    "advisoryId": "CVE-2024-24786",
			    "vulnerableFunctions": ["Unmarshal", "UnmarshalOptions.Unmarshal"],
			    "publishedAt": "2024-03-14T06:54:42.869673Z"
			  }
			}
			""";

		var item = Deserialize<SrmItem>(json);

		item.CvssScore.Should().BeApproximately(0.42f, 0.0001f);
		item.Cwe.Should().Be("CWE-122");
		item.Cve.Should().Be("CVE-1970-1234");
		item.AffectedVersion.Should().Be("1.0.0");
		item.FixedVersion.Should().Equal("1.0.1");
		item.DependencyChains.Should().ContainSingle()
			.Which.Should().Equal("root-app", "lodash", "vulnerable-pkg");
		item.AdvisoryInformation.Should().NotBeNull();
		item.AdvisoryInformation!.AdvisoryId.Should().Be("CVE-2024-24786");
		item.AdvisoryInformation.VulnerableFunctions.Should().Equal("Unmarshal", "UnmarshalOptions.Unmarshal");
	}

	[Fact]
	public void SearchSRMItems_SerializesFilterNamesTheApiAccepts()
	{
		var body = new SearchSRMItems
		{
			Repositories = ["PanoramicData.NugetManagement"],
			Priorities = [SrmPriority.Critical, SrmPriority.High],
			Statuses = [SrmStatus.OnTrack, SrmStatus.DueSoon, SrmStatus.Overdue],
			Categories = ["CommandInjection"],
			ScanTypes = ["SAST"],
			Segments = [1],
			SearchText = "injection"
		};

		var json = JsonSerializer.Serialize(body, Options);

		json.Should().Contain("\"repositories\":[\"PanoramicData.NugetManagement\"]");
		json.Should().Contain("\"priorities\":[\"Critical\",\"High\"]");
		json.Should().Contain("\"statuses\":[\"OnTrack\",\"DueSoon\",\"Overdue\"]");
		json.Should().Contain("\"categories\":[\"CommandInjection\"]");
		json.Should().Contain("\"scanTypes\":[\"SAST\"]");
		json.Should().Contain("\"segments\":[1]");
		json.Should().Contain("\"searchText\":\"injection\"");
	}

	[Fact]
	public void SearchSRMItems_WhenEmpty_OmitsEveryFilter()
	{
		// Codacy rejects explicit nulls on these filters, so an unfiltered search must send "{}".
		var json = JsonSerializer.Serialize(new SearchSRMItems(), Options);

		json.Should().Be("{}");
	}

	[Fact]
	public void SRMDashboardResponse_ReadsSeverityAndScanTypeCounts()
	{
		const string json = """
			{
			  "data": {
			    "totalOpen": 754, "totalNewThisWeek": 83, "totalClosed": 6722,
			    "onTrack": 97, "dueSoon": 82, "overdue": 575,
			    "closedOnTime": 4707, "closedLate": 2015,
			    "openCritical": 427, "openHigh": 251, "openMedium": 63, "openLow": 13,
			    "openSAST": 446, "openSCA": 0, "openContainerSCA": 0, "openSecrets": 13,
			    "openIaC": 92, "openCICD": 1, "openLicense": 0, "openPenTesting": 0,
			    "openDAST": 0, "openCSPM": 0, "openScanNotAttributed": 202
			  }
			}
			""";

		var dashboard = Deserialize<SRMDashboardResponse>(json).Data;

		dashboard.TotalOpen.Should().Be(754);
		dashboard.Overdue.Should().Be(575);
		dashboard.OpenCritical.Should().Be(427);
		dashboard.OpenHigh.Should().Be(251);
		dashboard.OpenMedium.Should().Be(63);
		dashboard.OpenLow.Should().Be(13);
		dashboard.OpenSast.Should().Be(446);
		dashboard.OpenSecrets.Should().Be(13);
		dashboard.OpenIaC.Should().Be(92);
		dashboard.OpenCicd.Should().Be(1);
		dashboard.OpenScanNotAttributed.Should().Be(202);
	}

	[Fact]
	public void SRMDashboardRepositoriesResponse_ReadsPerRepositorySeverityCounts()
	{
		const string json = """
			{ "data": [ { "id": 848725, "name": "PanoramicData.NugetManagement", "critical": 8, "high": 1, "medium": 0, "low": 0 } ] }
			""";

		var repository = Deserialize<SRMDashboardRepositoriesResponse>(json).Data.Should().ContainSingle().Subject;

		repository.Id.Should().Be(848725);
		repository.Name.Should().Be("PanoramicData.NugetManagement");
		repository.Critical.Should().Be(8);
		repository.High.Should().Be(1);
		repository.Medium.Should().Be(0);
		repository.Low.Should().Be(0);
	}

	[Fact]
	public void SRMDashboardCategoriesResponse_ReadsCategoryTotals()
	{
		const string json = """
			{ "data": [ { "name": "InsecureModulesLibraries", "total": 160 }, { "name": "_other_", "total": 63 } ] }
			""";

		var categories = Deserialize<SRMDashboardCategoriesResponse>(json).Data;

		categories.Should().HaveCount(2);
		categories[0].Name.Should().Be("InsecureModulesLibraries");
		categories[0].Total.Should().Be(160);
		categories[1].Name.Should().Be("_other_");
	}

	[Fact]
	public void SRMDashboardHistoryResponse_ReadsWeeklyDataPoint()
	{
		const string json = """
			{
			  "data": [
			    {
			      "since": "2026-06-15T00:00:00Z", "until": "2026-06-22T00:00:00Z",
			      "newCritical": 0, "newHigh": 0, "newMedium": 0, "newLow": 0,
			      "fixedCritical": 0, "fixedHigh": 0, "fixedMedium": 0, "fixedLow": 0,
			      "openCritical": 6, "openHigh": 2, "openMedium": 0, "openLow": 0,
			      "ignoredCritical": 0, "ignoredHigh": 0, "ignoredMedium": 0, "ignoredLow": 0,
			      "unignoredCritical": 0, "unignoredHigh": 0, "unignoredMedium": 0, "unignoredLow": 0
			    }
			  ]
			}
			""";

		var point = Deserialize<SRMDashboardHistoryResponse>(json).Data.Should().ContainSingle().Subject;

		point.Since.Should().Be(DateTimeOffset.Parse("2026-06-15T00:00:00Z", CultureInfo.InvariantCulture));
		point.Until.Should().Be(DateTimeOffset.Parse("2026-06-22T00:00:00Z", CultureInfo.InvariantCulture));
		point.OpenCritical.Should().Be(6);
		point.OpenHigh.Should().Be(2);
	}

	[Fact]
	public void SecurityRepositoriesResponse_ReadsRepositorySummaries()
	{
		const string json = """
			{
			  "pagination": { "cursor": "2", "limit": 3 },
			  "data": [ { "provider": "gh", "owner": "panoramicdata", "name": "Ans.Portal", "repositoryId": 490616 } ]
			}
			""";

		var repository = Deserialize<SecurityRepositoriesResponse>(json).Data.Should().ContainSingle().Subject;

		repository.Provider.Should().Be(Provider.Github);
		repository.Owner.Should().Be("panoramicdata");
		repository.Name.Should().Be("Ans.Portal");
		repository.RepositoryId.Should().Be(490616);
	}

	[Fact]
	public void SecurityCategoriesResponse_ReadsCategoryNames()
	{
		const string json = """
			{ "pagination": { "cursor": "2", "limit": 5, "total": 19 }, "data": ["Auth", "CommandInjection", "Cryptography"] }
			""";

		var response = Deserialize<SecurityCategoriesResponse>(json);

		response.Data.Should().Equal("Auth", "CommandInjection", "Cryptography");
		response.Pagination.Total.Should().Be(19);
	}

	[Fact]
	public void SecurityManagersResponse_WhenEmpty_ReadsEmptyPaginationWithoutThrowing()
	{
		const string json = """{ "pagination": {}, "data": [] }""";

		var response = Deserialize<SecurityManagersResponse>(json);

		response.Data.Should().BeEmpty();
		response.Pagination.Cursor.Should().BeNull();
	}

	[Fact]
	public void SecurityManager_ReadsCreatedAtAndOptionalName()
	{
		const string json = """
			{ "userId": 867842577, "name": "John Doe", "email": "example@codacy.com", "createdAt": "2017-07-21T17:32:28Z" }
			""";

		var manager = Deserialize<SecurityManager>(json);

		manager.UserId.Should().Be(867842577);
		manager.Name.Should().Be("John Doe");
		manager.Email.Should().Be("example@codacy.com");
		manager.CreatedAt.Should().Be(DateTimeOffset.Parse("2017-07-21T17:32:28Z", CultureInfo.InvariantCulture));
	}

	[Fact]
	public void SLAConfigResponse_ReadsPerSeverityDayThresholds()
	{
		const string json = """
			{ "slaConfig": { "criticalSla": 30, "highSla": 60, "mediumSla": 90, "lowSla": 120 } }
			""";

		var config = Deserialize<SLAConfigResponse>(json).SlaConfig;

		config.CriticalSla.Should().Be(30);
		config.HighSla.Should().Be(60);
		config.MediumSla.Should().Be(90);
		config.LowSla.Should().Be(120);
	}

	[Fact]
	public void SLAConfigBody_WrapsTheConfigurationUnderSlaConfig()
	{
		var body = new SLAConfigBody
		{
			SlaConfig = new SLAConfig { CriticalSla = 30, HighSla = 60, MediumSla = 90, LowSla = 120 }
		};

		var json = JsonSerializer.Serialize(body, Options);

		json.Should().Be("""{"slaConfig":{"criticalSla":30,"highSla":60,"mediumSla":90,"lowSla":120}}""");
	}

	[Fact]
	public void IgnoreSRMItemBody_SerializesReasonAndComment()
	{
		var body = new IgnoreSRMItemBody { Reason = "FalsePositive", Comment = "Test fixture, not a real key." };

		var json = JsonSerializer.Serialize(body, Options);

		json.Should().Be("""{"reason":"FalsePositive","comment":"Test fixture, not a real key."}""");
	}

	[Fact]
	public void SRMDASTReportUploadResponse_ReadsReportId()
	{
		const string json = """{ "id": "847feb32-9ff2-11ea-bb37-0242ac130002" }""";

		var response = Deserialize<SRMDASTReportUploadResponse>(json);

		response.Id.Should().Be("847feb32-9ff2-11ea-bb37-0242ac130002");
	}

	[Fact]
	public void SRMDastReportResponse_ReadsReportStateAndTool()
	{
		const string json = """
			{
			  "data": [
			    {
			      "id": "9a4f2183-c2a8-4f38-abe9-1041c5dc9b05",
			      "organizationId": 12455,
			      "createdAt": "2024-03-14T06:54:42.869673Z",
			      "updatedAt": "2024-03-14T06:54:42.869673Z",
			      "generatedAt": "2024-03-14T06:54:42.869673Z",
			      "state": "Failure",
			      "tool": "ZAP",
			      "failureReason": "Unrecognized report format."
			    }
			  ]
			}
			""";

		var report = Deserialize<SRMDastReportResponse>(json).Data.Should().ContainSingle().Subject;

		report.Id.Should().Be(Guid.Parse("9a4f2183-c2a8-4f38-abe9-1041c5dc9b05"));
		report.OrganizationId.Should().Be(12455);
		report.State.Should().Be(SrmDastReportState.Failure);
		report.Tool.Should().Be(DastTool.Zap);
		report.FailureReason.Should().Be("Unrecognized report format.");
	}

	[Fact]
	public void OssfScorecardResponse_ReadsChecksAndCounts()
	{
		const string json = """
			{
			  "data": {
			    "score": 7.4,
			    "date": "2024-03-14",
			    "failingCheckCount": 3,
			    "passingCheckCount": 15,
			    "checks": [
			      {
			        "name": "Code-Review",
			        "score": 8.0,
			        "reason": "found 20 unreviewed changesets out of 30",
			        "details": ["Warn: no reviews found"],
			        "documentation": { "url": "https://github.com/ossf/scorecard/blob/main/docs/checks.md#code-review", "short": "Determines if the project requires human code review." },
			        "severity": "High"
			      }
			    ]
			  }
			}
			""";

		var scorecard = Deserialize<OssfScorecardResponse>(json).Data;

		scorecard.Score.Should().BeApproximately(7.4f, 0.0001f);
		scorecard.Date.Should().Be("2024-03-14");
		scorecard.FailingCheckCount.Should().Be(3);
		scorecard.PassingCheckCount.Should().Be(15);

		var check = scorecard.Checks.Should().ContainSingle().Subject;
		check.Name.Should().Be("Code-Review");
		check.Severity.Should().Be(OssfScorecardSeverity.High);
		check.Details.Should().Equal("Warn: no reviews found");
		check.Documentation.ShortDescription.Should().Be("Determines if the project requires human code review.");
	}
}
