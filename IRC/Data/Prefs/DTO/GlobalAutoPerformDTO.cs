namespace BestChat.IRC.Data.Prefs.DTO;

public record GlobalAutoPerformDTO
(
	GlobalAutoPerformOneStepDTO[]? WhenStartingBestChat = null,
	GlobalAutoPerformOneStepDTO[]? WhenJoiningNet = null,
	GlobalAutoPerformOneStepDTO[]? WhenJoiningChan = null,
	GlobalAutoPerformOneStepDTO[]? WhenOpeningUserChat = null,
	string? KeyOverride = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/AutoPerform");