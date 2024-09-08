// Ignore Spelling: Prefs DTO Dcc Evt

namespace BestChat.IRC.Data.Prefs.DTO;

public abstract record IrcDTO(IrcDTO.GlobalDTO Global, IrcDTO.NetworkDTO[]? Networks = null)
{
	public abstract record GlobalDTO
	(
		GlobalDTO.AutoPerformDTO AutoPerform,
		GlobalDTO.DccDTO DCC,
		GlobalDTO.ConnDTO Conn,
		GlobalDTO.OneAliasDTO[]? Aliases = null,
		GlobalDTO.OneAltNickDTO[]? AltNicks = null,
		GlobalDTO.OneStalkWordDTO[]? StalkWords = null,
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global")
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

	public record NetworkDTO
	(
		System.Guid OwnerNet,

		NetworkDTO.TimeStampDTO TimeStamps,
		NetworkDTO.DccDTO DCC,
		NetworkDTO.AutoPerformDTO AutoPerform,
		NetworkDTO.ConnDTO Conn,
		NetworkDTO.AliasesDTO Aliases,
		NetworkDTO.AltNicksDTO AltNicks,
		NetworkDTO.StalkWordsDTO StalkWords,
		string[]? NotifyWhenOnline = null,
		NetworkDTO.ChanDTO[]? KnownChans = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network")
	{
		public record TimeStampDTO
		(
			bool Override,
			bool Show = true,
			string Fmt = "G"
		) : Platform.DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO.AppearanceDTO.TimeStampDTO(Show, Fmt, $"IRC/Network/TimeStamp");

		public record AliasesDTO
		(
			System.Guid[]? DisabledInheritedAliases = null,
			GlobalDTO.OneAliasDTO[]? AddedAliases = null
		) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/Aliases");

		public record AutoPerformDTO
		(
			AutoPerformDTO.OnEvtDTO WhenJoiningNet,
			AutoPerformDTO.OnEvtDTO WhenJoiningChan,
			AutoPerformDTO.OnEvtDTO WhenOpeningUserChat
		) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/AutoPerform")
		{
			public record OnEvtDTO
			(
				System.Guid[]? DisabledInheritedSteps = null,
				GlobalDTO.AutoPerformDTO.OneStepDTO[]? AddedSteps = null
			) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/Auto-perform");
		}

		public record AltNicksDTO
		(
			System.Guid[]? DisabledInheritedNicks = null,
			GlobalDTO.OneAltNickDTO[]? AddedNicks = null
		) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/AltNicks");

		public record StalkWordsDTO
		(
			System.Guid[]? DisabledInheritedStalkWords = null,
			GlobalDTO.OneStalkWordDTO[]? AddedStalkWords = null
		) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/StalkWords");

		public record DccDTO
		(
			bool Override,
			bool Enabled = false,
			bool? GetLocalIpFromServer = null,
			System.IO.DirectoryInfo? DownloadsFolder = null,
			int[]? Ports = null
		) : GlobalDTO.DccDTO(Enabled, GetLocalIpFromServer, DownloadsFolder, Ports, $"IRC/Network/DCC");

		public record ConnDTO
		(
			bool Override,
			bool IsIdentEnabled = true,
			bool IsAutoReconnectEnabled = true,
			bool IsRejoinAfterKickEnabled = true,
			string CharEncoding = "UTF-8",
			bool IsUnlimitedAttemptsOn = true,
			int MaxAttempts = 1,
			string? DefQuitMsg = null
		) : GlobalDTO.ConnDTO(IsIdentEnabled, IsAutoReconnectEnabled, IsRejoinAfterKickEnabled, CharEncoding,
			IsUnlimitedAttemptsOn, MaxAttempts, DefQuitMsg, $"IRC/Network/Conn");

		public record ChanDTO
		(
			string OwnerChan,

			TimeStampDTO TimeStamps,
			AliasesDTO Aliases,
			ChanDTO.AutoPerformDTO AutoPerform,
			StalkWordsDTO StalkWords
		) : Platform.DataAndExt.Prefs.PrefsBase.AbstractDTO("IRC/Network/Chan")
		{
			public record AutoPerformDTO
			(
				System.Guid[]? DisabledInheritedSteps = null,
				GlobalDTO.AutoPerformDTO.OneStepDTO[]? AddedSteps = null
			) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/Chan/AutoPerform");
		}
	}
}