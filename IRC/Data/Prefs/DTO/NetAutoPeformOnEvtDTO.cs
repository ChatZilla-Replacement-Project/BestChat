namespace BestChat.IRC.Data.Prefs.DTO
{
public record NetAutoPeformOnEvtDTO
(
	System.Guid[]? DisabledInheritedSteps = null,
	GlobalAutoPerformOneStepDTO[]? AddedSteps = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/Auto-perform");
}