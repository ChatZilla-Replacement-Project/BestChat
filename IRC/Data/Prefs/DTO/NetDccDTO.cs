namespace BestChat.IRC.Data.Prefs.DTO;

public record NetDccDTO
(
	bool Override,
	bool Enabled = false,
	bool? GetLocalIpFromServer = null,
	System.IO.DirectoryInfo? DownloadsFolder = null,
	int[]? Ports = null
) : GlobalDccDTO(Enabled, GetLocalIpFromServer, DownloadsFolder, Ports, $"IRC/Network/DCC");