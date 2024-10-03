// Ignore Spelling: Defs DTO

namespace BestChat.IRC.Data.Defs.DTO;

public record UserNetDTO
(
	string Name,
	NetServerInfoDTO[] Servers,
	System.Uri? Homepage = null,
	NickServOpts? NickServ = null,
	ChanServOpts? ChanServ = null,
	AlisOpts AlisStatus = AlisOpts.unknown,
	QOpts QStatus = QOpts.unknown,
	bool AutoConnect = false,
	bool Hidden = false,
	bool UseSSL = true,
	ushort? PortToUse = null,
	LogInModes LogInMode = LogInModes.none,
	string? LogInChallengeUserName = null,
	string? LogInChallengeBncName = null,
	string? LogInChallengePwd = null,
	string? LogInUserName = null,
	string? LogInPwd = null,
	string[]? LogInCustomSteps = null,
	System.IO.FileInfo? LogInSaslCert = null
) : NetDTO(Name, Servers, Homepage, NickServ, ChanServ, AlisStatus, QStatus), IDataDefBasic<UserNetDTO>;