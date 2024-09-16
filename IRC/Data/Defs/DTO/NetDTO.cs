// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public abstract record NetDTO
(
	string Name,
	NetServerInfoDTO[] Servers,
	System.Uri? Homepage = null,
	NickServOpts? NickServ = null,
	ChanServOpts? ChanServ = null,
	AlisOpts AlisStatus = AlisOpts.unknown,
	QOpts QStatus = QOpts.unknown
) : IDataDefBasic<NetDTO>;