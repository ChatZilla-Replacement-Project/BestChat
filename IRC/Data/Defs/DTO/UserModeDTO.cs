// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record UserModeDTO
(
	char Mode,
	LocalizedTextDTO[] LocalizedDesc,
	string DefaultDesc,
	bool NotAlwaysAvailable = false,
	bool IsReadOnly = false
);