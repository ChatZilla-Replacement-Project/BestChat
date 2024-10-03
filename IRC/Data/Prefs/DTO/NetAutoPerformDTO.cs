namespace BestChat.IRC.Data.Prefs.DTO
{
public record NetAutoPerformDTO
(
	NetAutoPerformDTO.OnEvtDTO WhenJoiningNet,
	NetAutoPerformDTO.OnEvtDTO WhenJoiningChan,
	NetAutoPerformDTO.OnEvtDTO WhenOpeningUserChat
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/AutoPerform")
{
	public record OnEvtDTO
	(
		System.Guid[]? DisabledInheritedSteps = null,
		GlobalAutoPerformOneStepDTO[]? AddedSteps = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/Auto-perform");
}
}