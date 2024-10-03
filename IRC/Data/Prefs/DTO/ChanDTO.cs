﻿namespace BestChat.IRC.Data.Prefs.DTO;

public record ChanDTO
(
	string OwnerChan,

	NetTimeStampDTO TimeStamps,
	NetAliasesDTO Aliases,
	ChanAutoPerformDTO AutoPerform,
	NetStalkWordsDTO StalkWords
) : Platform.DataAndExt.Prefs.PrefsBase.AbstractDTO("IRC/Network/Chan");