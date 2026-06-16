// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record ModeParamDTO
(
	string Name,
	ModeParamDTO.Types Type,
	LocalizedTextDTO[] LocalizedDisplayNames,
	string DefaultDisplayName,
	LocalizedTextDTO[] LocalizedDesc,
	string DefaultDesc,
	LocalizedTextDTO[]? LocalizedPostFixLabel = null,
	string? DefaultPostFixLabel = null,
	int? MinForNumber = null,
	int? MaxForNumber = null
) : IDataDefBasic
{
	public enum Types
	{
		@string,
		number,
		chanName
	}
}