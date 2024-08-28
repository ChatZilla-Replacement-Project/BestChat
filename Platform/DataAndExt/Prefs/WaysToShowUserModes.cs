// Ignore Spelling: Prefs

namespace BestChat.Platform.DataAndExt.Prefs
{
	public enum WaysToShowUserModes : byte
	{
		[Attr.LocalizedDesc(nameof(Rsrcs.strGlobalAppearanceUserListWaysToShowModesSymbolsTitle),
			"Show Symbols", nameof(Rsrcs.strGlobalAppearanceUserListWaysToShowModesSymbolsDesc), "If you select this, " +
			"Best Chat will show op, half op, and voice with traditional symbols.", typeof(WaysToShowUserModes))]
		symbols,

		[Attr.LocalizedDesc(nameof(Rsrcs.strGlobalAppearanceUserLIstWaysToShowModesColoredDiscsTitle),
			"Colored Discs", nameof(Rsrcs.strGlobalAppearanceUserLIstWaysToShowModesColoredDiscsDesc), "If you select " +
			"this, colored discs will be used to indicate op/half op status and voice.", typeof(WaysToShowUserModes))]
		coloredDiscs,

		[Attr.LocalizedDesc(nameof(Rsrcs.strGlobalAppearanceUserListWaysToShowModesHiddenTitle), "Hidden (no modes visible)",
			nameof(Rsrcs.strGlobalAppearanceUserListWaysToShowModesHiddenDesc), "Select if you don't need to know who is an op, half-op, or has" +
			" voice.", typeof(WaysToShowUserModes))]
		hidden,
	}
}