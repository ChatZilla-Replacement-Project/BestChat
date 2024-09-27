using BestChat.Platform.DataAndExt.Prefs;

namespace BestChat.Platform.UI.Desktop.Prefs;

public class GlobalAppearanceUserListPrefs : AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalAppearanceUserListPrefs(AbstractMgr mgrParent) :
			base(mgrParent, "Time Stamp", DataAndExt.Prefs.Rsrcs.strGlobalAppearanceUserListLocTitle,
				DataAndExt.Prefs.Rsrcs.strGlobalAppearanceUserListLocDesc)
		{
			location = new(this, "Location", DataAndExt.Prefs.Rsrcs
					.strGlobalAppearanceUserListLocTitle, DataAndExt.Prefs.Rsrcs.strGlobalAppearanceUserListLocTitle,
				PaneLocations.left);
			howToShowModes = new(this, "Ways to show modes", DataAndExt.Prefs.Rsrcs
				.strGlobalAppearanceUserListWaysToShowModesTitle, DataAndExt.Prefs.Rsrcs
				.strGlobalAppearanceUserListWaysToShowModesDesc, WaysToShowUserModes.symbols);
			sortByMode = new(this, "Sort by mode", DataAndExt.Prefs.Rsrcs
				.strGlobalAppearanceUserListSortByModeTitle, DataAndExt.Prefs.Rsrcs
				.strGlobalAppearanceUserListSortByModeDesc, true);
		}

		internal GlobalAppearanceUserListPrefs(AbstractMgr mgrParent, DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO.AppearanceDTO
			.UserListDTO dto) :
			base(mgrParent, "Time Stamp", DataAndExt.Prefs.Rsrcs.strGlobalAppearanceUserListLocTitle,
				DataAndExt.Prefs.Rsrcs.strGlobalAppearanceUserListLocDesc)
		{
			location = new(this, "Location", DataAndExt.Prefs.Rsrcs
					.strGlobalAppearanceUserListLocTitle, DataAndExt.Prefs.Rsrcs.strGlobalAppearanceUserListLocTitle,
				PaneLocations.left, dto.Loc);
			howToShowModes = new(this, "Ways to show modes", DataAndExt.Prefs.Rsrcs
				.strGlobalAppearanceUserListWaysToShowModesTitle, DataAndExt.Prefs.Rsrcs
				.strGlobalAppearanceUserListWaysToShowModesDesc, WaysToShowUserModes.symbols, dto
				.HowToShowModes);
			sortByMode = new(this, "Sort by mode", DataAndExt.Prefs.Rsrcs
				.strGlobalAppearanceUserListSortByModeTitle, DataAndExt.Prefs.Rsrcs
				.strGlobalAppearanceUserListSortByModeDesc, true, dto.SortByMode);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private readonly Item<PaneLocations> location;

		private readonly Item<WaysToShowUserModes> howToShowModes;

		private readonly Item<bool> sortByMode;
	#endregion

	#region Properties
		public Item<PaneLocations> Loc
			=> location;

		public Item<WaysToShowUserModes> HowToShowModes
			=> howToShowModes;

		public Item<bool> SortByMode
			=> sortByMode;
	#endregion

	#region Methods
		public DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO.AppearanceDTO.UserListDTO ToDTO()
			=> new(location.CurVal, howToShowModes.CurVal, sortByMode.CurVal);
	#endregion

	#region Event Handlers
	#endregion
}