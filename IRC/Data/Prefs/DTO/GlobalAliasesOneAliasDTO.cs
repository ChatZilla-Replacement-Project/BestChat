namespace BestChat.IRC.Data.Prefs.DTO
{
public record OneAliasDTO
(
	System.Guid GUID,
	string Name,
	string Cmd,
	string? KeyOverride = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO(KeyOverride ?? "IRC/Global/OneAlias");
}