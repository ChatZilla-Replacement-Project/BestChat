namespace BestChat.IRC.Data.Prefs.DTO
{
public record GlobalConnDTO
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
}