// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record BncDTO
(
	string Name,
	System.Uri? HomePage,
	string[] AllowedNets,
	string[] ProhibitedNets,
	BncDTO.ServerDTO[] Servers,
	string? HomeNet = null,
	string? HomeChan = null,
	string? OwnBot = null,
	ushort[]? Ports = null,
	ushort[]? SslPorts = null,
	uint? MaxNetworksPerBouncerInstance = null
) : IDataDefBasic<BncDTO>
{
	public record ServerDTO
	(
		string Name,
		string Domain
	) : IDataDefBasic<ServerDTO>;
}