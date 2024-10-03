// Ignore Spelling: Prefs DTO

using BestChat.IRC.Data.Prefs.DTO;

namespace BestChat.IRC.ProtocolMgr.Prefs.DTO;

public record GlobalDTO
(
	GlobalAutoPerformDTO GlobalAutoPerform,
	GlobalDccDTO DCC,
	GlobalConnDTO Conn,
	GlobalFmtDTO Fmt,
	GlobalAliasesOneAliasDTO[]? Aliases = null,
	GlobalOneAltNickDTO[]? AltNicks = null,
	GlobalStalkWordsOneStalkWordDTO[]? StalkWords = null
) : Data.Prefs.DTO.GlobalDTO(GlobalAutoPerform, DCC, Conn, Aliases, AltNicks, StalkWords);