// Ignore Spelling: Defs Serv

namespace BestChat.IRC.Data.Defs
{
	public enum NickServOpts
	{
		[Platform.DataAndExt.Attr.LocalizedDesc("strNickServOptUnknown", "Available, but type unknown",
			"strNickServOptUnknownTooltip", "Select this if the network has a NickServ, but you don't know much " +
			"about it", typeof(NickServOpts))]
		unknown,

		[Platform.DataAndExt.Attr.LocalizedDesc("strNickServOptAtTheme", "AtTheme", "strNickServOptAtThemeToolTip",
			"NickServ is implemented by AtTheme.  Only select this if you're sure that's what they use.", typeof(NickServOpts))]
		atTheme,
	}
}