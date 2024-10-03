namespace BestChat.IRC.Data.Prefs.DTO
{
public record ChanAutoPerformDTO
(
	System.Guid[]? DisabledInheritedSteps = null,
	GlobalAutoPerformOneStepDTO[]? AddedSteps = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/Chan/AutoPerform");
}