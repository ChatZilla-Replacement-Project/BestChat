namespace BestChat.IRC.Data.Prefs.DTO;

public record NetConnDTO
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