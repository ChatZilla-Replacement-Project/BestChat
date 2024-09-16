// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record UserBncDTO
(
	string Name,
	System.Uri? HomePage,
	string[] AllowedNets,
	string[] ProhibitedNets,
	BncDTO.ServerDTO[] Servers,
	string? HomeNet = null,
	string? HomeChan = null,
	string? OwnBot = null,
	UserBncDTO.InstanceDTO[] Instances = null,
	ushort[]? Ports = null,
	ushort[]? SslPorts = null,
	uint? MaxNetworksPerBouncerInstance = null
) : BncDTO(Name, HomePage, AllowedNets, ProhibitedNets, Servers, HomeNet, HomeChan, OwnBot, Ports,
		SslPorts, MaxNetworksPerBouncerInstance), IDataDefBasic<UserBncDTO>
{
	public record InstanceDTO
	(
		string Name,
		string AssignedServer
	) : IDataDefBasic<InstanceDTO>;
}