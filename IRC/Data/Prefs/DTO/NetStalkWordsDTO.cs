namespace BestChat.IRC.Data.Prefs.DTO;

public record NetStalkWordsDTO
(
	System.Guid[]? DisabledInheritedStalkWords = null,
	GlobalStalkWordsOneStalkWordDTO[]? AddedStalkWords = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/StalkWords");