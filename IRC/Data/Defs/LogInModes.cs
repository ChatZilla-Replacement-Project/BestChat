// Ignore Spelling: IRC Defs

using System;

namespace BestChat.IRC.Data.Defs
{
	public enum LogInModes
	{
		[Platform.DataAndExt.Attr.LocalizedDesc("strManual", "Log in manually", "", "",
			typeof(LogInModes))]
		manual,

		[Platform.DataAndExt.Attr.LocalizedDesc("strSASL", "Use SASL", "", "",
			typeof(LogInModes))]
		sasl,

		[Platform.DataAndExt.Attr.LocalizedDesc("strNickServ", "Attempt to use NickServ (Requires a network that provides " +
			"NickServ)", "", "", typeof(LogInModes))]
		nickServ,
	}
}