namespace BestChat.IRC.Data.Prefs.DTO
{
public record NetTimeStampDTO
(
	bool Override,
	bool Show = true,
	string Fmt = "G"
) : Platform.DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO.AppearanceDTO.TimeStampDTO(Show, Fmt, $"IRC/Network/TimeStamp");
}