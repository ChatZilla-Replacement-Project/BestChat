namespace BestChat.IRC.Data.Prefs.DTO;

public record NetworkDTO
(
	System.Guid OwnerNet,

	NetTimeStampDTO TimeStamps,
	NetworkDTO.DccDTO DCC,
	NetAutoPerformDTO AutoPerform,
	NetworkDTO.ConnDTO Conn,
	NetAliasesDTO Aliases,
	NetworkDTO.AltNicksDTO AltNicks,
	NetworkDTO.StalkWordsDTO StalkWords,
	string[]? NotifyWhenOnline = null,
	NetworkDTO.ChanDTO[]? KnownChans = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network")
{
	public record AltNicksDTO
	(
		System.Guid[]? DisabledInheritedNicks = null,
		GlobalOneAltNickDTO[]? AddedNicks = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/AltNicks");

	public record StalkWordsDTO
	(
		System.Guid[]? DisabledInheritedStalkWords = null,
		GlobalStalkWordsOneStalkWordDTO[]? AddedStalkWords = null
	) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/StalkWords");

	public record DccDTO
	(
		bool Override,
		bool Enabled = false,
		bool? GetLocalIpFromServer = null,
		System.IO.DirectoryInfo? DownloadsFolder = null,
		int[]? Ports = null
	) : GlobalDccDTO(Enabled, GetLocalIpFromServer, DownloadsFolder, Ports, $"IRC/Network/DCC");

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
	) : GlobalConnDTO(IsIdentEnabled, IsAutoReconnectEnabled, IsRejoinAfterKickEnabled, CharEncoding,
		IsUnlimitedAttemptsOn, MaxAttempts, DefQuitMsg, $"IRC/Network/Conn");

	public record ChanDTO
	(
		string OwnerChan,

		NetTimeStampDTO TimeStamps,
		NetAliasesDTO Aliases,
		ChanDTO.AutoPerformDTO AutoPerform,
		StalkWordsDTO StalkWords
	) : Platform.DataAndExt.Prefs.PrefsBase.AbstractDTO("IRC/Network/Chan")
	{
		public record AutoPerformDTO
		(
			System.Guid[]? DisabledInheritedSteps = null,
			GlobalAutoPerformOneStepDTO[]? AddedSteps = null
		) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/Chan/AutoPerform");
	}
}