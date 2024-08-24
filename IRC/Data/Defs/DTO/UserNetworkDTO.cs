// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record UserNetworkDTO
(
	string Name,
	ServerInfoDTO[] Servers,
	System.Uri? Homepage = null,
	NickServOpts? NickServ = null,
	ChanServOpts? ChanServ = null,
	bool? HasAlis = null,
	bool? HasQ = null,
	bool AutoConnect = false,
	bool Hidden = false,
	bool UseSSL = true
) : NetworkDTO(Name, Servers, Homepage, NickServ, ChanServ, HasAlis, HasAlis),
	IDataDefBasic<UserNetworkDTO>;