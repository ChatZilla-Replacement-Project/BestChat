// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public abstract record NetworkDTO
(
	string Name,
	ServerInfoDTO[] Servers,
	System.Uri? Homepage = null,
	NickServOpts? NickServ = null,
	ChanServOpts? ChanServ = null,
	bool? HasAlis = null,
	bool? HasQ = null,
	bool AutoConnect = false
) : IDataDefBasic<NetworkDTO>;