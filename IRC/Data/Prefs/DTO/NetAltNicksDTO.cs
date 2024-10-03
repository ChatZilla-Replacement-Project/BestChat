namespace BestChat.IRC.Data.Prefs.DTO
{
public record NetAltNicksDTO
(
	System.Guid[]? DisabledInheritedNicks = null,
	GlobalOneAltNickDTO[]? AddedNicks = null
) : Platform.DataAndExt.Prefs.AbstractMgr.AbstractDTO("IRC/Network/AltNicks");
}