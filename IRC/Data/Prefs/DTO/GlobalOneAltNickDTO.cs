namespace BestChat.IRC.Data.Prefs.DTO
{
public record GlobalOneAltNickDTO
(
	System.Guid GUID,
	string NickToUse,
	string? KeyOverride = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/OneAltNick");
}