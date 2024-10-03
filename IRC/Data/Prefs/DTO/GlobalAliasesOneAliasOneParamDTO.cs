namespace BestChat.IRC.Data.Prefs.DTO;

public record GlobalAliasesOneAliasOneParamDTO
(
	string Name,
	string ParamType,
	string? Doc = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("OneParam");