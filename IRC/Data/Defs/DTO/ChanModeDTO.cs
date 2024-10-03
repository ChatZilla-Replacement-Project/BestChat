// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record ChanModeDTO
(
	char Mode,
	LocalizedTextDTO[] LocalizedDesc,
	string DefaultDesc,
	ModeParamDTO[] Parameters,
	string FmtAsSentToNetwork = "",
	bool NotAlwaysAvailable = false,
	bool IsOperRequiredToChange = false,
	ChanModeDTO.StdModeTypes? StdModeType = null
)
{
	public enum StdModeTypes
	{
		TopicLock,
		Moderated,
		ColorStrip,
		NoOutsideMsg,
		InviteOnly,
		Keyword,
		Private,
		Secret
	}
}