// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record PredefinedNetworkDTO
(
	string Name,
	NetServerInfoDTO[] Servers,
	ChanModeDTO[] ChanModes,
	UserModeDTO[] UserModes,
	System.Uri? Homepage = null,
	NickServOpts? NickServ = null,
	ChanServOpts? ChanServ = null,
	AlisOpts AlisStatus = AlisOpts.unknown,
	QOpts QStatus = QOpts.unknown
) : NetDTO(Name, Servers, Homepage, NickServ, ChanServ, AlisStatus, QStatus),
	IDataDefBasic<PredefinedNetworkDTO>;