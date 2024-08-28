// Ignore Spelling: Prefs DTO Dcc

namespace BestChat.IRC.Data.Prefs.DTO;

public record IrcDTO(IrcDTO.GlobalDTO Global)
{
	public record GlobalDTO
	(
		GlobalDTO.AutoPerformDTO AutoPerform,
		GlobalDTO.DccDTO DCC,
		GlobalDTO.ConnDTO conn,
		GlobalDTO.AliasDTO[]? Aliases = null,
		string[]? AltNicks = null,
		string[]? StalkWords = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global")
	{
		public record AliasDTO
		(
			string Name,
			string Cmd
		) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global/Alias");

		public record AutoPerformDTO
		(
			string[]? WhenStartingBestChat = null,
			string[]? WhenJoiningNet = null,
			string[]? WhenJoiningChan = null,
			string[]? WhenOpeningUserChat = null
		) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global/AutoPerform");

		public record DccDTO
		(
			bool Enabled = false,
			bool? GetLocalIpFromServer = null,
			string? DownloadsFolder = null,
			int[]? Ports = null
		) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global/DCC");

		public record ConnDTO
		(
			bool IsIdentEnabled = true,
			bool IsAutoReconnectEnabled = true,
			bool IsRejoinAfterKickEnabled = true,
			string CharEncoding = "UTF-8",
			bool IsUnlimitedAttemptsOn = true,
			int MaxAttempts = 1,
			string? DefQuitMsg = null
		) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Global/Connection");
	}
}