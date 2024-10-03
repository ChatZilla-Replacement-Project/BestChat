// Ignore Spelling: Prefs DTO Dcc Evt

namespace BestChat.IRC.Data.Prefs.DTO;

public abstract record IrcDTO<GlobalDtoType>
(
	GlobalDtoType Global,
	NetDTO[]? Networks = null
) where GlobalDtoType : GlobalDTO;