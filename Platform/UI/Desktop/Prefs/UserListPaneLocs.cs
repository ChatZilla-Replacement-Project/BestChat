// Ignore Spelling: Prefs

namespace BestChat.Platform.UI.Desktop.Prefs
{
	public enum UserListPaneLocations : byte
	{
		[DataAndExt.Attr.LocalizedDesc(nameof(DataAndExt.Prefs.Rsrcs.strGlobalAppearanceUserListLeftTitle), "Left", nameof(DataAndExt.Prefs.Rsrcs
			.strGlobalAppearanceUserListLeftDesc), "Places the user list on the left side of the client area.",
			typeof(UserListPaneLocations))]
		left = 0,

		[DataAndExt.Attr.LocalizedDesc(nameof(DataAndExt.Prefs.Rsrcs.strGlobalAppearanceUserListRightTitle), "Left", nameof(DataAndExt.Prefs.Rsrcs
			.strGlobalAppearanceUserListRightDesc), "Places the user list on the right side of the client area.",
			typeof(UserListPaneLocations))]
		right = 1,
	}
}