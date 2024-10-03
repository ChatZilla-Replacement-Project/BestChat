namespace BestChat.Platform.DataAndExt.Prefs;

public class GlobalAppearanceMsgGroupsPrefs : Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalAppearanceMsgGroupsPrefs(GlobalAppearancePrefsBase mgrParent) :
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

		internal GlobalAppearanceMsgGroupsPrefs(GlobalAppearancePrefsBase mgrParent, DTO.PrefsDTO.GlobalDTO.AppearanceDTO
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