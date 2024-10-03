// Ignore Spelling: IRC Defs

using System;

namespace BestChat.IRC.Data.Defs
{
	public enum LogInModes : byte
	{
		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strLogInModeNoneText), "Don't attempt to log " +
			"in", nameof(Rsrcs.strLogInModeNoneToolTip), "Disables all attempts by Best Chat to log you " +
			"in.", typeof(LogInModes))]
		none,


		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strLogInModeSaslUserNamePwdText), "Use SASL " +
			"(User Name & Password)", nameof(Rsrcs.strLogInModeSaslUserNamePwdToolTip), "This option requires you to " +
			"specify your user name and password.  If a NickServ is preset, the user name and password may come from " +
			"your NickServ account.", typeof(LogInModes))]
		saslUserNamePwd,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strLogInModeSaslCertText), "SASL " +
			"(Certificate)", nameof(Rsrcs.strLogInModeSaslCertToolTip), "This option only requires a " +
			"certificate file you'll need to locate for Best Chat.", typeof(LogInModes))]
		saslCert,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strLogInModeServerPwdText), "Server Password",
			nameof(Rsrcs.strLogInModeServerPwdToolTip), "Use if the server only asks for a simple " +
			"password.", typeof(LogInModes))]
		serverPwd,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strLogInModeNickServMsgText), "NickServ (via /msg " +
			"NickServ)", nameof(Rsrcs.strLogInModeNickServMsgToolTip), "You'll need to specify your " +
			"NickServ user name and password.  However, if you have a cloak on this network, you will appear briefly " +
			"without it.  This is only for networks with a NickServ.  This option won't work if NickServ isn't present."
			+ "  This option is provided as a few networks don't define the /ns or /nickserv commands.",
			typeof(LogInModes))]
		nickServMsg,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strLogInModeNickServNsText), "NickServ (via " +
			"/ns)", nameof(Rsrcs.strLogInModeNickServNsToolTip), "You'll need to specify your NickServ" +
			" user name and password.  However, if you have a cloak on this network, you will appear briefly without it."
			+ "  This is only for networks with a NickServ.  This option won't work if NickServ isn't present.  This " +
			"option requires a network defines the /ns command.", typeof(LoaderOptimization))]
		nickServNs,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strLogInModeUseQText), "Use Q", nameof(Rsrcs
			.strLogInModeUseQToolTip), "Q exists only, that we know of, on QuakeNet.  So you will " +
			"probably never need to select this if it isn't selected.  For Quake, you only have two choices: Don't log " +
			"in and Q.  Don't pick other options as they might not work.", typeof(LogInModes))]
		q,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strLogInModeChallengeText), "Challenge (User " +
			"Name & Password)", nameof(Rsrcs.strLogInModeChallengeToolTip), "The server will for your " +
			"log in information.  If you fail to provide it in time, you won't get in.", typeof(LogInModes))]
		challenge,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strLogInModeCustomText), "Custom / Manual",
			nameof(Rsrcs.strLogInModeCustomToolTip), "If you select this option, you'll need to enter " +
			"one or more commands that will be sent to the server prior to connection.  Conceivably, some of these " +
			"commands might ask the user for information.", typeof(LogInModes))]
		custom,
	}
}