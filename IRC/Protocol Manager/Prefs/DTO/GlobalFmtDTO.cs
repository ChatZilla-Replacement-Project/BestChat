namespace BestChat.IRC.ProtocolMgr.Prefs.DTO
{
public record GlobalFmtDTO
(
	bool Bold,
	bool Italics,
	bool Underline,
	bool StrikeThrough,
	bool TeleType,
	bool MircColors,
	GlobalPrefs.FmtPrefs.MarkerTypes MarkerType
);
}