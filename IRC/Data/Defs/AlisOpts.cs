namespace BestChat.IRC.Data.Defs
{
	public enum AlisOpts : byte
	{
		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strHasAlisUnknown), "Unknown", nameof(Rsrcs
			.strHasAlisUnknownToolTip), "ALIS may or may not be present", typeof(AlisOpts))]
		unknown,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strHasAlisTrue), "True / Available",
			nameof(Rsrcs.strHasAlisTrueToolTip), "ALIS is available and ready to use.",
			typeof(AlisOpts))]
		present,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strHasAlisFalse), "False / Not Available",
			nameof(Rsrcs.strHasAlisFalseToolTip), "No ALIS is present on this network",
			typeof(AlisOpts))]
		notPresent,
	}
}