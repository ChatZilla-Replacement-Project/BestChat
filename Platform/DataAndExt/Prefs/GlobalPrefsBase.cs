namespace BestChat.Platform.DataAndExt.Prefs;

public abstract class GlobalPrefsBase<GlobalPrefsType, AppearancePrefsType> : AbstractChildMgr
	where GlobalPrefsType : GlobalPrefsBase<GlobalPrefsType, AppearancePrefsType>
	where AppearancePrefsType : GlobalAppearancePrefsBase
{
	#region Constructors & Deconstructors
		protected GlobalPrefsBase(AbstractMgr mgrParent) :
			base(mgrParent, "Global", Rsrcs.strGlobalName, Rsrcs.strGlobalNameToolTipText)
		{
			plugin = new(this);
		}

		protected GlobalPrefsBase(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO dto) :
			base(mgrParent, "Global", Rsrcs.strGlobalName, Rsrcs.strGlobalNameToolTipText)
		{
			plugin = new(this, dto.Plugins);
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
		private readonly GlobalPluginPrefs plugin;
	#endregion

	#region Properties
		public abstract AppearancePrefsType Appearance
		{
			get;
		}

		public GlobalPluginPrefs Plugins
			=> plugin;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}