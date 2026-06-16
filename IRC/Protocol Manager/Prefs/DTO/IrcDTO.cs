// Ignore Spelling: Prefs DTO

namespace BestChat.IRC.ProtocolMgr.Prefs.DTO;

public record IrcDTO
(
	GlobalDTO Global,
	Data.Prefs.DTO.NetDTO[]? Networks = null
) : Data.Prefs.DTO.IrcDTO<GlobalDTO>(Global, Networks);