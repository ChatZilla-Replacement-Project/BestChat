// Ignore Spelling: Prefs DTO

namespace BestChat.IRC.Data.Prefs.DTO;

public record IrcDTO(IrcDTO.GlobalDTO Global)
{
	public record GlobalDTO
	(
		GlobalDTO.AutoPerformDTO AutoPerform,
		GlobalDTO.DccDTO DCC,
		GlobalDTO.AliasDTO[]? Aliases = null,
		string[]?
		AltNicks = null,
		string[]? StalkWords = null
	)
	{
		public record AliasDTO
		(
			string Name,
			string Cmd
		);

		public record AutoPerformDTO
		(
			string[]? WhenStartingBestChat = null,
			string[]? WhenJoiningNet = null,
			string[]? WhenJoiningChan = null,
			string[]? WhenOpeningUserChat = null
		);

		public record DccDTO
		(
			bool Enabled = false,
			bool? GetLocalIpFromServer = null,
			string? DownloadsFolder = null,
			int[]? Ports = null
		);
	}
}