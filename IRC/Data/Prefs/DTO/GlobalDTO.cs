namespace BestChat.IRC.Data.Prefs.DTO;

public abstract record GlobalDTO
(
	GlobalAutoPerformDTO AutoPerform,
	GlobalDccDTO DCC,
	GlobalConnDTO Conn,
	GlobalAliasesOneAliasDTO[]? Aliases = null,
	GlobalOneAltNickDTO[]? AltNicks = null,
	GlobalStalkWordsOneStalkWordDTO[]? StalkWords = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global");