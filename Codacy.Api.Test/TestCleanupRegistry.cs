#pragma warning disable CA1848 // Test diagnostics do not require source-generated logging
#pragma warning disable CA1873 // Cleanup collections are intentionally small

namespace Codacy.Api.Test;

internal sealed class TestCleanupRegistry(ILogger? logger)
{
	private readonly List<Action> _actions = [];

	public void Register(Action action)
	{
		ArgumentNullException.ThrowIfNull(action);
		_actions.Add(action);
		logger?.LogDebug("Registered cleanup action. Total cleanup actions: {Count}", _actions.Count);
	}

	public void Execute()
	{
		logger?.LogInformation("Executing {Count} cleanup actions", _actions.Count);

		foreach (var action in _actions)
		{
			Execute(action);
		}

		_actions.Clear();
		logger?.LogInformation("Cleanup completed");
	}

	private void Execute(Action action)
	{
		try
		{
			action();
		}
		catch (Exception ex)
		{
			logger?.LogError(ex, "Error executing cleanup action: {Message}", ex.Message);
		}
	}
}