namespace BestChat.IRC.Data.Prefs.DTO
{
public record NetAliasesDTO
(
	System.Guid[]? DisabledInheritedAliases = null,
	GlobalAliasesOneAliasDTO[]? AddedAliases = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/Aliases");
}