// Ignore Spelling: Prefs

namespace BestChat.Platform.DataAndExt.Prefs
{
	public enum PaneLocations : byte
	{
		[Attr.LocalizedDesc(nameof(Rsrcs.strGlobalAppearanceUserListLeftTitle), "Left", nameof(Rsrcs
			.strGlobalAppearanceUserListLeftDesc), "Places the user list on the left side of the client area.",
			typeof(PaneLocations))]
		left = 0,

		[Attr.LocalizedDesc(nameof(Rsrcs.strGlobalAppearanceUserListRightTitle), "Left", nameof(Rsrcs
			.strGlobalAppearanceUserListRightDesc), "Places the user list on the right side of the client area.",
			typeof(PaneLocations))]
		right = 1,
	}
}