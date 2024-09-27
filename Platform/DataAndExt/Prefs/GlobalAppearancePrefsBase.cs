namespace BestChat.Platform.DataAndExt.Prefs
{
	public abstract class GlobalAppearancePrefsBase : AbstractChildMgr
	{
		#region Constructors & Deconstructors
		protected GlobalAppearancePrefsBase(AbstractMgr mgrParent) :
			base(mgrParent, "Appearance", Rsrcs.strGlobalAppearancePageTitle, Rsrcs
				.strGlobalAppearancePageDesc)
		{
			confMode = new(this);
			timestamp = new(this);
			userlist = new(this);
			msgGroups = new(this);
		}

		protected GlobalAppearancePrefsBase(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.AppearanceDTO dto)
			: base(mgrParent, "Appearance", Rsrcs.strGlobalAppearancePageTitle, Rsrcs
				.strGlobalAppearancePageDesc)
		{
			confMode = new(this, dto.ConfMode);
			timestamp = new(this, dto.TimeStamp);
			userlist = new(this, dto.UserList);
			msgGroups = new(this, dto.MsgGroups);
		}
		#endregion

		#region Delegates
		#endregion

		#region Events
		#endregion

		#region Constants
		#endregion

		#region Helper Types
		public class ConfModePrefs : AbstractChildMgr
		{
			#region Constructors & Deconstructors
			public ConfModePrefs(in AbstractMgr mgrParent) : base(mgrParent, "Conference " +
																																			"Mode", Rsrcs.strGlobalAppearanceConfModeTitle, Rsrcs
				.strGlobalAppearanceConfModeDesc)
			{
				confModeEnabled = new(this, "Conference Mode Enabled?", Rsrcs
					.strGlobalAppearanceConfModeEnabledTitle, Rsrcs
					.strGlobalAppearanceConfModeEnabledDesc, false);
				userLimitBeforeTrigger = new(this, "User Limit Before Trigger",
					Rsrcs.strGlobalAppearanceConfModeLimitTitle, Rsrcs
						.strGlobalAppearanceConfModeLimitDesc, 150, iMinVal: 2);
				actionsCollapsed = new(this, "Collapse Actions When Collapsing " +
																		"Messages", Rsrcs.strGlobalAppearanceConfModeCollapseActionsTitle, Rsrcs
					.strGlobalAppearanceConfModeCollapseActionsDesc, false);
				msgsCollapsed = new(this, "Collapse Messages?", Rsrcs
					.strGlobalAppearanceConfModeCollapseMsgsTitle, Rsrcs
					.strGlobalAppearanceConfModeCollapseMsgsDesc, false);
			}

			internal ConfModePrefs(in AbstractMgr mgrParent, in DTO.PrefsDTO.GlobalDTO
				.AppearanceDTO.ConfModeDTO dto) :
				base(mgrParent, "Conference Mode", Rsrcs.strGlobalAppearanceConfModeTitle,
					Rsrcs.strGlobalAppearanceConfModeDesc)
			{
				confModeEnabled = new(this, "Conference Mode Enabled?", Rsrcs
					.strGlobalAppearanceConfModeEnabledTitle, Rsrcs
					.strGlobalAppearanceConfModeEnabledDesc, false, dto.ConfModeEnabled);
				userLimitBeforeTrigger = new(this, "User Limit Before Trigger",
					Rsrcs.strGlobalAppearanceConfModeLimitTitle, Rsrcs
						.strGlobalAppearanceConfModeLimitDesc, 150, iMinVal: 2, iCurVal: dto
						.UserLimitBeforeTrigger);
				actionsCollapsed = new(this, "Collapse Actions When Collapsing " +
																		"Messages", Rsrcs.strGlobalAppearanceConfModeCollapseActionsTitle, Rsrcs
					.strGlobalAppearanceConfModeCollapseActionsDesc, false, dto.ActionsCollapsed);
				msgsCollapsed = new(this, "Collapse Messages?", Rsrcs
					.strGlobalAppearanceConfModeCollapseMsgsTitle, Rsrcs
					.strGlobalAppearanceConfModeCollapseMsgsDesc, false, dto.MsgsCollapsed);
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
			private readonly Item<bool> confModeEnabled;

			private readonly IntItem userLimitBeforeTrigger;

			private readonly Item<bool> actionsCollapsed;

			private readonly Item<bool> msgsCollapsed;
			#endregion

			#region Properties
			public Item<bool> ConfModeEnabled
				=> confModeEnabled;

			public IntItem UserLimitBeforeTrigger
				=> userLimitBeforeTrigger;

			public Item<bool> ActionsCollapsed
				=> actionsCollapsed;

			public Item<bool> MsgsCollapsed
				=> msgsCollapsed;
			#endregion

			#region Methods
			public DTO.PrefsDTO.GlobalDTO.AppearanceDTO.ConfModeDTO ToDTO()
				=> new(confModeEnabled.CurVal, userLimitBeforeTrigger.CurVal,
					actionsCollapsed.CurVal, msgsCollapsed.CurVal);
			#endregion

			#region Event Handlers
			#endregion
		}

		public class UserListPrefs : AbstractChildMgr
		{
			#region Constructors & Deconstructors
			public UserListPrefs(AbstractMgr mgrParent) :
				base(mgrParent, "Time Stamp", Rsrcs.strGlobalAppearanceUserListLocTitle,
					Rsrcs.strGlobalAppearanceUserListLocDesc)
			{
				location = new(this, "Location", Rsrcs
						.strGlobalAppearanceUserListLocTitle, Rsrcs.strGlobalAppearanceUserListLocTitle,
					PaneLocations.left);
				howToShowModes = new(this, "Ways to show modes", Rsrcs
					.strGlobalAppearanceUserListWaysToShowModesTitle, Rsrcs
					.strGlobalAppearanceUserListWaysToShowModesDesc, WaysToShowUserModes.symbols);
				sortByMode = new(this, "Sort by mode", Rsrcs
					.strGlobalAppearanceUserListSortByModeTitle, Rsrcs
					.strGlobalAppearanceUserListSortByModeDesc, true);
			}

			internal UserListPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.AppearanceDTO
				.UserListDTO dto) :
				base(mgrParent, "Time Stamp", Rsrcs.strGlobalAppearanceUserListLocTitle,
					Rsrcs.strGlobalAppearanceUserListLocDesc)
			{
				location = new(this, "Location", Rsrcs
						.strGlobalAppearanceUserListLocTitle, Rsrcs.strGlobalAppearanceUserListLocTitle,
					PaneLocations.left, dto.Loc);
				howToShowModes = new(this, "Ways to show modes", Rsrcs
					.strGlobalAppearanceUserListWaysToShowModesTitle, Rsrcs
					.strGlobalAppearanceUserListWaysToShowModesDesc, WaysToShowUserModes.symbols, dto
					.HowToShowModes);
				sortByMode = new(this, "Sort by mode", Rsrcs
					.strGlobalAppearanceUserListSortByModeTitle, Rsrcs
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
			public DTO.PrefsDTO.GlobalDTO.AppearanceDTO.UserListDTO ToDTO()
				=> new(location.CurVal, howToShowModes.CurVal, sortByMode.CurVal);
			#endregion

			#region Event Handlers
			#endregion
		}

		public class MsgGroupsPrefs : Prefs.AbstractChildMgr
		{
			#region Constructors & Deconstructors
			public MsgGroupsPrefs(GlobalAppearancePrefsBase mgrParent) :
				base(mgrParent, "Message Groups", Rsrcs.strGlobalAppearanceMsgGroupsTitle, Rsrcs
					.strGlobalAppearanceMsgGroupsDesc)
			{
				enabled = new(this, "Enabled", Rsrcs
						.strGlobalAppearanceMsgGroupsEnableTitle, Rsrcs.strGlobalAppearanceMsgGroupsEnableDesc,
					true);
				howLongToWaitBeforeStartingNewGroup = new(this, "How Long to Wait Before " +
																												"Starting New Group", Rsrcs
					.strGlobalAppearanceMsgGroupsHowLongToWaitBeforeStartingNewGroupTitle, Rsrcs
					.strGlobalAppearanceMsgGroupsHowLongToWaitBeforeStartingNewGroupDesc, null);
				limitMsgsPerGroup = new(this, "Limit Messages Per Group", Rsrcs
					.strGlobalAppearanceMsgGroupsLimitMsgsPerGroupTitle, Rsrcs
					.strGlobalAppearanceMsgGroupsLimitMsgsPerGroupDesc, false);
				maxMsgsPerGroup = new(this, "Maximum Messages Per Group", Rsrcs
					.strGlobalAppearanceMsgGroupsMaxMsgsPerGroupTitle, Rsrcs
					.strGlobalAppearanceMsgGroupsMaxMsgsPerGroupDesc, 20, iMinVal: 2);
			}

			internal MsgGroupsPrefs(GlobalAppearancePrefsBase mgrParent, DTO.PrefsDTO.GlobalDTO.AppearanceDTO
				.MsgGroupsDTO dto) :
				base(mgrParent, "Message Groups", Rsrcs.strGlobalAppearanceMsgGroupsEnableTitle,
					Rsrcs.strGlobalAppearanceMsgGroupsEnableDesc)
			{
				enabled = new(this, "Enabled", Rsrcs
						.strGlobalAppearanceMsgGroupsEnableTitle, Rsrcs.strGlobalAppearanceMsgGroupsEnableDesc,
					true, dto.Enabled);
				howLongToWaitBeforeStartingNewGroup = new(this, "How Long to Wait Before " +
																												"Starting New Group", Rsrcs
					.strGlobalAppearanceMsgGroupsHowLongToWaitBeforeStartingNewGroupTitle, Rsrcs
					.strGlobalAppearanceMsgGroupsHowLongToWaitBeforeStartingNewGroupDesc, null, dto
					.HowLongToWaitBeforeStartingNewGroup);
				limitMsgsPerGroup = new(this, "Limit Messages Per Group", Rsrcs
					.strGlobalAppearanceMsgGroupsLimitMsgsPerGroupTitle, Rsrcs
					.strGlobalAppearanceMsgGroupsLimitMsgsPerGroupDesc, false, dto.LimitMsgsPerGroup);
				maxMsgsPerGroup = new(this, "Maximum Messages Per Group", Rsrcs
						.strGlobalAppearanceMsgGroupsMaxMsgsPerGroupTitle, Rsrcs
						.strGlobalAppearanceMsgGroupsMaxMsgsPerGroupDesc, 20, dto.MaxMsgsPerGroup,
					iMinVal: 2);
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
			private readonly Item<bool> enabled;

			private readonly Item<System.TimeSpan?> howLongToWaitBeforeStartingNewGroup;

			private readonly Item<bool> limitMsgsPerGroup;

			private readonly IntItem maxMsgsPerGroup;
			#endregion

			#region Properties
			public Item<bool> Enabled
				=> enabled;

			public Item<System.TimeSpan?> HowLongToWaitBeforeStartingNewGroup
				=> howLongToWaitBeforeStartingNewGroup;

			public Item<bool> LimitMsgsPerGroup
				=> limitMsgsPerGroup;

			public IntItem MaxMsgsPerGroup
				=> maxMsgsPerGroup;
			#endregion

			#region Methods
			public DTO.PrefsDTO.GlobalDTO.AppearanceDTO.MsgGroupsDTO ToDTO()
				=> new(
					enabled.CurVal,
					limitMsgsPerGroup.CurVal,
					maxMsgsPerGroup.CurVal,
					howLongToWaitBeforeStartingNewGroup.CurVal
				);
			#endregion

			#region Event Handlers
			#endregion
		}
		#endregion

		#region Members
		private readonly ConfModePrefs confMode;

		private readonly PrefsBase.GlobalPrefs.TimeStampPrefs timestamp;

		private readonly UserListPrefs userlist;

		private readonly MsgGroupsPrefs msgGroups;
		#endregion

		#region Properties
		public ConfModePrefs ConfMode
			=> confMode;

		public PrefsBase.GlobalPrefs.TimeStampPrefs TimeStamp
			=> timestamp;

		public UserListPrefs UserList
			=> userlist;

		public MsgGroupsPrefs MsgGroups
			=> msgGroups;
		#endregion

		#region Methods
		#endregion

		#region Event Handlers
		#endregion
	}
}