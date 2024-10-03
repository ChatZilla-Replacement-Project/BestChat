// Ignore Spelling: Prefs DTO

using BestChat.IRC.Data.Prefs.DTO;

namespace BestChat.IRC.ProtocolMgr.Prefs.DTO;

public record IrcDTO
(
	GlobalDTO Global,
	NetDTO[]? Networks = null
) : Data.Prefs.DTO.IrcDTO<GlobalDTO>(Global, Networks);