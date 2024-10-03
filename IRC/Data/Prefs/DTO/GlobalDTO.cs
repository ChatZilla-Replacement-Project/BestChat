namespace BestChat.IRC.Data.Prefs.DTO
{
public abstract record GlobalDTO<GlobalDtoType>
(
	GlobalDTO<GlobalDtoType>.AutoPerformDTO AutoPerform,
	GlobalDTO<GlobalDtoType>.DccDTO DCC,
	GlobalDTO<GlobalDtoType>.ConnDTO Conn,
	GlobalDTO<GlobalDtoType>.OneAliasDTO[]? Aliases = null,
	GlobalDTO<GlobalDtoType>.OneAltNickDTO[]? AltNicks = null,
	GlobalDTO<GlobalDtoType>.OneStalkWordDTO[]? StalkWords = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global")
	where GlobalDtoType : GlobalDTO<GlobalDtoType>
{
	public record AutoPerformDTO
	(
		AutoPerformDTO.OneStepDTO[]? WhenStartingBestChat = null,
		AutoPerformDTO.OneStepDTO[]? WhenJoiningNet = null,
		AutoPerformDTO.OneStepDTO[]? WhenJoiningChan = null,
		AutoPerformDTO.OneStepDTO[]? WhenOpeningUserChat = null,
		string? KeyOverride = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/AutoPerform")
	{
		public record OneStepDTO
		(
			System.Guid GUID,
			string WhatToDo
		) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global/AutoPerform/OnEvt/OneStep");
	}

	public record DccDTO
	(
		bool Enabled = false,
		bool? GetLocalIpFromServer = null,
		System.IO.DirectoryInfo? DownloadsFolder = null,
		int[]? Ports = null,
		string? KeyOverride = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/DCC");

	public record ConnDTO
	(
		bool IsIdentEnabled = true,
		bool IsAutoReconnectEnabled = true,
		bool IsRejoinAfterKickEnabled = true,
		string CharEncoding = "UTF-8",
		bool IsUnlimitedAttemptsOn = true,
		int MaxAttempts = 1,
		string? DefQuitMsg = null,
		string? KeyOverride = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/Connection");

	public record OneAliasDTO
	(
		System.Guid GUID,
		string Name,
		string Cmd,
		string? KeyOverride = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/OneAlias");

	public record OneAltNickDTO
	(
		System.Guid GUID,
		string NickToUse,
		string? KeyOverride = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/OneAltNick");

	public record OneStalkWordDTO
	(
		System.Guid GUID,
		string Ctnts,
		string? KeyOverride = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/OneStalkWord");
}
}