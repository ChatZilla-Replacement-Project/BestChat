namespace BestChat.Platform.DataAndExt.Prefs
{
	public class GlobalAppearanceConfModePrefs : AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public GlobalAppearanceConfModePrefs(in AbstractMgr mgrParent) : base(mgrParent, "Conference " +
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

		internal GlobalAppearanceConfModePrefs(in AbstractMgr mgrParent, in DTO.PrefsDTO.GlobalDTO
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
}