namespace BestChat.IRC.Data.Prefs.DTO;

public record NetAutoPerformDTO
(
	NetAutoPeformOnEvtDTO WhenJoiningNet,
	NetAutoPeformOnEvtDTO WhenJoiningChan,
	NetAutoPeformOnEvtDTO WhenOpeningUserChat
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/AutoPerform");