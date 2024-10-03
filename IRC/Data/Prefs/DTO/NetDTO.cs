namespace BestChat.IRC.Data.Prefs.DTO;

public record NetDTO
(
	System.Guid OwnerNet,

	NetTimeStampDTO TimeStamps,
	NetDccDTO DCC,
	NetAutoPerformDTO AutoPerform,
	NetConnDTO Conn,
	NetAliasesDTO Aliases,
	NetAltNicksDTO AltNicks,
	NetStalkWordsDTO StalkWords,
	string[]? NotifyWhenOnline = null,
	ChanDTO[]? KnownChans = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network");