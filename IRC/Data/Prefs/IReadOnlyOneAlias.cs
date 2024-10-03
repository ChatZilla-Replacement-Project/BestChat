namespace BestChat.IRC.Data.Prefs;

public interface IReadOnlyOneAlias
{
	string Name
	{
		get;
	}

	Platform.DataAndExt.Cmd.AbstractCmdCall? WhatToRun
	{
		get;
	}

	System.Guid GUID
	{
		get;
	}

	DTO.GlobalAliasesOneAliasDTO ToDTO();
}