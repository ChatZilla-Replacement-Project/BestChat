// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record PredefinedNetDTO
(
	string strName,
	NetServerInfoDTO[] aservers,
	ChanModeDTO[] aChanModes,
	UserModeDTO[] aUserModes,
	System.Uri? uriHomepage = null,
	NickServOpts? nickServ = null,
	ChanServOpts? chanServ = null,
	AlisOpts alisStatus = AlisOpts.unknown,
	QOpts qStatus = QOpts.unknown
) : NetDTO(strName, aservers, uriHomepage, nickServ, chanServ, alisStatus, qStatus),
	IDataDefBasic<PredefinedNetDTO>;