namespace BestChat.IRC.Data.Prefs.DTO;

public abstract record AbstractGlobalAliasesOneAliasOneParamDTO
(
	string ParamType,
	string? Doc = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("One Param");

public record GlobalAliasesOneAliasOnePositionalParamDTO
(
	string ParamType,
	string? Doc = null
) : AbstractGlobalAliasesOneAliasOneParamDTO(ParamType, Doc);

public record GlobalAliasesOneAliasOneNamedParamDTO
(
	string Name,
	string ParamType,
	string? Doc = null
) : AbstractGlobalAliasesOneAliasOneParamDTO(ParamType, Doc);