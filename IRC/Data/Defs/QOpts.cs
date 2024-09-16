namespace BestChat.IRC.Data.Defs
{
	public enum QOpts : byte
	{
		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strHasQUnknown), "Unknown", nameof(Rsrcs
			.strHasQUnknownToolTip), "Q might or might not be present on this network", typeof(QOpts))]
		unknown,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strHasQTrue), "True / Available", nameof(Rsrcs
			.strHasQTrueToolTip), "Q is present", typeof(QOpts))]
		present,

		[Platform.DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strHasQFalse), "False / Not Available",
			nameof(Rsrcs.strHasQFalseToolTip), "No Q present on this network.  Q is really only " +
			"QuakeNet.", typeof(QOpts))]
		notPresent,
	}
}