namespace BestChat.IRC.Data.Prefs.DTO;

public record GlobalStalkWordsOneStalkWordDTO
(
	System.Guid GUID,
	string Ctnts,
	string? KeyOverride = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/OneStalkWord");