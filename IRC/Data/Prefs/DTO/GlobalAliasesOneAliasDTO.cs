namespace BestChat.IRC.Data.Prefs.DTO;

public record GlobalAliasesOneAliasDTO
(
	System.Guid GUID,
	string Name,
	Platform.DataAndExt.Cmd.AbstractCmdCall? WhatToRun,
	GlobalAliasesOneAliasOneParamDTO[]? PositionalParams = default,
	GlobalAliasesOneAliasOneParamDTO[]? NamedParams = default,
	string? Doc = null,
	string? KeyOverride = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/OneAlias");