namespace Codacy.Api.Test;

/// <summary>
/// Assertions for the shape every Codacy API response shares: an envelope carrying a
/// <c>Data</c> payload.
/// </summary>
/// <remarks>
/// The envelope types have no common base type, so the payload is reached through a selector
/// rather than an interface. Folding the envelope and payload checks into one call keeps the
/// integration tests down to the part that differs between them: the request.
/// </remarks>
internal static class ApiResponseAssertions
{
	/// <summary>
	/// Asserts that the envelope and its payload both arrived, and returns the payload.
	/// </summary>
	public static TData ShouldHaveData<TResponse, TData>(
		this TResponse? response,
		Func<TResponse, TData?> payload)
		where TResponse : class
		where TData : class
	{
		response.Should().NotBeNull();

		var data = payload(response);
		data.Should().NotBeNull();

		return data;
	}

	/// <summary>
	/// Asserts that the payload arrived and holds at least one item, and returns it.
	/// </summary>
	public static List<TItem> ShouldHaveNonEmptyData<TResponse, TItem>(
		this TResponse? response,
		Func<TResponse, List<TItem>?> payload)
		where TResponse : class
	{
		var data = response.ShouldHaveData(payload);
		data.Should().NotBeEmpty();

		return data;
	}

	/// <summary>
	/// Asserts that the payload arrived and, where a page size was requested, that it was
	/// honoured. Returns the payload.
	/// </summary>
	/// <param name="limit">The requested page size, or <c>null</c> if none was requested.</param>
	public static List<TItem> ShouldHavePageOfAtMost<TResponse, TItem>(
		this TResponse? response,
		int? limit,
		Func<TResponse, List<TItem>?> payload)
		where TResponse : class
	{
		var data = response.ShouldHaveData(payload);
		if (limit is not null)
		{
			data.Count.Should().BeLessThanOrEqualTo(limit.Value, $"at most {limit} items were requested");
		}

		return data;
	}
}
