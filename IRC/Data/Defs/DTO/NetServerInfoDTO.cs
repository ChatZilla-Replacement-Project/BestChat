// Ignore Spelling: Ssl Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record NetServerInfoDTO
(
	string Domain,
	ushort[] Ports,
	ushort[] SslPorts,
	bool IsEnabled = true
);