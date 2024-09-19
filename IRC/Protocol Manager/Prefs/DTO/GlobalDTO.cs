// Ignore Spelling: Prefs DTO

namespace BestChat.IRC.ProtocolMgr.Prefs.DTO;

public record GlobalDTO
(
	Data.Prefs.DTO.IrcDTO<GlobalDTO>.GlobalDTO.AutoPerformDTO AutoPerform,
	Data.Prefs.DTO.IrcDTO<GlobalDTO>.GlobalDTO.DccDTO DCC,
	Data.Prefs.DTO.IrcDTO<GlobalDTO>.GlobalDTO.ConnDTO Conn,
	Data.Prefs.DTO.IrcDTO<GlobalDTO>.GlobalDTO.OneAliasDTO[]? Aliases = null,
	Data.Prefs.DTO.IrcDTO<GlobalDTO>.GlobalDTO.OneAltNickDTO[]? AltNicks = null,
	Data.Prefs.DTO.IrcDTO<GlobalDTO>.GlobalDTO.OneStalkWordDTO[]? StalkWords = null
) : Data.Prefs.DTO.IrcDTO<GlobalDTO>.GlobalDTO(AutoPerform, DCC, Conn, Aliases, AltNicks, StalkWords);