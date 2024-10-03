namespace BestChat.Platform.UI.Desktop.Prefs;

public class GlobalAppearanceUserListPrefs : DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalAppearanceUserListPrefs(DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent, "Time Stamp", UiDesktopRsrcs.strGlobalAppearanceUserListLocTitle,
				UiDesktopRsrcs.strGlobalAppearanceUserListLocDesc)
		{
			loc = new(this, "Location", UiDesktopRsrcs
					.strGlobalAppearanceUserListLocTitle, UiDesktopRsrcs.strGlobalAppearanceUserListLocTitle,
				UserListPaneLocs.left);
			howToShowModes = new(this, "Ways to show modes", UiDesktopRsrcs
				.strGlobalAppearanceUserListWaysToShowModesTitle, UiDesktopRsrcs
				.strGlobalAppearanceUserListWaysToShowModesDesc, WaysToShowUserModes.symbols);
			sortByMode = new(this, "Sort by mode", UiDesktopRsrcs.strGlobalAppearanceUserListSortByModeTitle,
				UiDesktopRsrcs.strGlobalAppearanceUserListSortByModeDesc, SortOrders.nameOnly);
		}

		internal GlobalAppearanceUserListPrefs(DataAndExt.Prefs.AbstractMgr mgrParent, DTO.RootDTO.GlobalDTO.AppearanceDTO.UserListDTO dto) :
			base(mgrParent, "Time Stamp", UiDesktopRsrcs.strGlobalAppearanceUserListLocTitle,
				UiDesktopRsrcs.strGlobalAppearanceUserListLocDesc)
		{
			loc = new(this, "Location", UiDesktopRsrcs
					.strGlobalAppearanceUserListLocTitle, UiDesktopRsrcs.strGlobalAppearanceUserListLocTitle,
				UserListPaneLocs.left, dto.Loc);
			howToShowModes = new(this, "Ways to show modes", UiDesktopRsrcs
				.strGlobalAppearanceUserListWaysToShowModesTitle, UiDesktopRsrcs
				.strGlobalAppearanceUserListWaysToShowModesDesc, WaysToShowUserModes.symbols, dto
				.HowToShowModes);
			sortByMode = new(this, "Sort by mode", UiDesktopRsrcs.strGlobalAppearanceUserListSortByModeTitle,
				UiDesktopRsrcs.strGlobalAppearanceUserListSortByModeDesc, SortOrders.nameOnly, dto.SortByMode);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public enum UserListPaneLocs : byte
		{
			[DataAndExt.Attr.LocalizedDesc(
				nameof(UiDesktopRsrcs.strGlobalAppearanceUserListLeftTitle), "Left", nameof(
					UiDesktopRsrcs.strGlobalAppearanceUserListLeftDesc),
				"Places the user list on the left side of the client area.",
				typeof(UserListPaneLocs))]
			left = 0,

			[DataAndExt.Attr.LocalizedDesc(
				nameof(UiDesktopRsrcs.strGlobalAppearanceUserListRightTitle), "Left", nameof(
					UiDesktopRsrcs), "Places the user list on the right side of the client area.", typeof(UserListPaneLocs))]
			right = 1,
		}

		public enum WaysToShowUserModes : byte
		{
			[DataAndExt.Attr.LocalizedDesc(
				nameof(UiDesktopRsrcs.strGlobalAppearanceUserListWaysToShowModesSymbolsTitle),
				"Show Symbols", nameof(UiDesktopRsrcs.strGlobalAppearanceUserListWaysToShowModesSymbolsDesc), "If you select " +
				"this, Best Chat will show op, half op, and voice with traditional symbols.", typeof(WaysToShowUserModes))]
			symbols,

			[DataAndExt.Attr.LocalizedDesc(
				nameof(UiDesktopRsrcs.strGlobalAppearanceUserListWaysToShowModesColoredDiscsTitle),
				"Colored Discs", nameof(UiDesktopRsrcs.strGlobalAppearanceUserListWaysToShowModesColoredDiscsDesc), "If you " +
				"select this, colored discs will be used to indicate op/half op status and voice.", typeof(WaysToShowUserModes))]
			coloredDiscs,

			[DataAndExt.Attr.LocalizedDesc(
				nameof(UiDesktopRsrcs.strGlobalAppearanceUserListWaysToShowModesHiddenTitle),
				"Hidden (no modes visible)", nameof(UiDesktopRsrcs.strGlobalAppearanceUserListWaysToShowModesHiddenDesc),
				"Select if you don't need to know who is an op, half-op, or has voice.", typeof(WaysToShowUserModes))]
			hidden,
		}

		public enum SortOrders : byte
		{
			[DataAndExt.Attr.LocalizedDesc(nameof(UiDesktopRsrcs.strGlobalAppearanceUserListSortOrderUnsorted), "Unsorted",
				nameof(UiDesktopRsrcs.strGlobalAppearanceUserListSortOrderUnsortedToolTip), "Prevents Best Chat from sorting "
				+ "the user list", typeof(SortOrders))]
			unsorted,

			[DataAndExt.Attr.LocalizedDesc(nameof(UiDesktopRsrcs.strGlobalAppearanceUserListSortOrderName), "User‘s nick or "
				+ "display name only", nameof(UiDesktopRsrcs.strGlobalAppearanceUserListSortOrderNameToolTip), "Sorts the user"
				+ " list, but only by the user nick or display name", typeof(SortOrders))]
			nameOnly,

			[DataAndExt.Attr.LocalizedDesc(nameof(UiDesktopRsrcs.strGlobalAppearanceUserListSortOrderModeThenName), "User‘s "
				+ "Mode, then the user's nick or display name", nameof(UiDesktopRsrcs
					.strGlobalAppearanceUserListSortOrderModeThenNameToolTip), "Sorts the user list by the mode and then the " +
				"user's nick or display name", typeof(SortOrders))]
			modeThenName,

			[DataAndExt.Attr.LocalizedDesc(nameof(UiDesktopRsrcs.strGlobalAppearanceUserListSortOrderLocalActivityThenName),
				"User‘s mode, then how recently the user was active, then by their nick or display name", nameof(UiDesktopRsrcs
					.strGlobalAppearanceUserListSortOrderLocalActivityThenNameToolTip), "Sorts the user list by how recently " +
				"they‘ve been active in that channel or room and then by their nick or display name", typeof(SortOrders))]
			localActivityThenName,

			[DataAndExt.Attr.LocalizedDesc(nameof(UiDesktopRsrcs
				.strGlobalAppearanceUserListSortOrderModeThenLocalActivityThenName), "User‘s mode, then how recently the user "
				+ "was active, then by their nick or display name", nameof(UiDesktopRsrcs
					.strGlobalAppearanceUserListSortOrderModeThenLocalActivityThenNameToolTip), "Sorts the user list by the " +
				"user's mode first, then how recently in this channel or room they were active, then by their nick or display "
				+ "name", typeof(SortOrders))]
			modeThenLocalActivityThenName,
		}
	#endregion

	#region Members
		private readonly DataAndExt.Prefs.Item<UserListPaneLocs> loc;

		private readonly DataAndExt.Prefs.Item<WaysToShowUserModes> howToShowModes;

		private readonly DataAndExt.Prefs.Item<SortOrders> sortByMode;
	#endregion

	#region Properties
		public DataAndExt.Prefs.Item<UserListPaneLocs> Loc
			=> loc;

		public DataAndExt.Prefs.Item<WaysToShowUserModes> HowToShowModes
			=> howToShowModes;

		public DataAndExt.Prefs.Item<SortOrders> SortByMode
			=> sortByMode;
	#endregion

	#region Methods
		internal DTO.RootDTO.GlobalDTO.AppearanceDTO.UserListDTO ToDTO()
			=> new(loc.CurVal, howToShowModes.CurVal, sortByMode.CurVal);
	#endregion

	#region Event Handlers
	#endregion
}