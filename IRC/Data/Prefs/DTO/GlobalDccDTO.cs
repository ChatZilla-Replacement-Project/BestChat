namespace BestChat.IRC.Data.Prefs.DTO;

public record GlobalDccDTO
(
	bool Enabled = false,
	bool? GetLocalIpFromServer = null,
	System.IO.DirectoryInfo? DownloadsFolder = null,
	int[]? Ports = null,
	string? KeyOverride = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/DCC");