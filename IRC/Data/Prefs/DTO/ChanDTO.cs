namespace BestChat.IRC.Data.Prefs.DTO
{
public record ChanDTO
(
	string OwnerChan,

	NetTimeStampDTO TimeStamps,
	NetAliasesDTO Aliases,
	ChanDTO.AutoPerformDTO AutoPerform,
	NetStalkWordsDTO StalkWords
) : Platform.DataAndExt.Prefs.PrefsBase.AbstractDTO("IRC/Network/Chan")
{
	public record AutoPerformDTO
	(
		System.Guid[]? DisabledInheritedSteps = null,
		GlobalAutoPerformOneStepDTO[]? AddedSteps = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/Chan/AutoPerform");
}
}