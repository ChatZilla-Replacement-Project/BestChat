// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record PredefinedNetworkDTO
(
	string Name,
	ServerInfoDTO[] Servers,
	ChanModeDTO[] ChanModes,
	UserModeDTO[] UserModes,
	System.Uri? Homepage = null,
	NickServOpts? NickServ = null,
	ChanServOpts? ChanServ = null,
	bool? HasAlis = null,
	bool? HasQ = null
) : NetworkDTO(Name, Servers, Homepage, NickServ, ChanServ, HasAlis, HasQ),
	IDataDefBasic<PredefinedNetworkDTO>;