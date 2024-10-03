namespace BestChat.Platform.DataAndExt.Prefs;

public abstract class GlobalAppearancePrefsBase : AbstractChildMgr
{
	#region Constructors & Deconstructors
		protected GlobalAppearancePrefsBase(AbstractMgr mgrParent) :
			base(mgrParent, "Appearance", Rsrcs.strGlobalAppearancePageTitle, Rsrcs
				.strGlobalAppearancePageDesc)
		{
			confMode = new(this);
			timestamp = new(this);
			msgGroups = new(this);
		}

		protected GlobalAppearancePrefsBase(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.AppearanceDTO dto)
			: base(mgrParent, "Appearance", Rsrcs.strGlobalAppearancePageTitle, Rsrcs
				.strGlobalAppearancePageDesc)
		{
			confMode = new(this, dto.ConfMode);
			timestamp = new(this, dto.TimeStamp);
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
	#endregion

	#region Members
		private readonly GlobalAppearanceConfModePrefs confMode;

		private readonly GlobalAppearanceTimeStampPrefs timestamp;

		private readonly GlobalAppearanceMsgGroupsPrefs msgGroups;
	#endregion

	#region Properties
		public GlobalAppearanceConfModePrefs ConfMode
			=> confMode;

		public GlobalAppearanceTimeStampPrefs TimeStamp
			=> timestamp;

		public GlobalAppearanceMsgGroupsPrefs MsgGroups
			=> msgGroups;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}