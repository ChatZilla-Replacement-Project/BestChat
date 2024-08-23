// Ignore Spelling: Dieable

namespace BestChat.Platform.DataAndExt.Dieable;

/// <summary>
/// Provides a way to inform interested callers that the sender isn't valid any more and should be discarded.
/// </summary>
public interface IDieable
{
	/// <summary>
	/// Fired when the object is dieing
	/// </summary>
	event System.Action<IDieable>? evtDieing;
}