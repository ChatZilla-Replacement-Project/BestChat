namespace BestChat.Platform.DataAndExt.Prefs;

public class GlobalPluginExtHowToRunScriptsUngroupedPrefs : AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalPluginExtHowToRunScriptsUngroupedPrefs(AbstractMgr mgrParent)
			: base(mgrParent, "Ungrouped", Rsrcs.strGlobalPluginsHowToRunThemTitle, Rsrcs
				.strGlobalPluginsHowToRunThemDesc)
		{
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
	#endregion

	#region Properties
		public MappedListItem<string, GlobalPluginExtScriptEntry> Scripts
			=> ((GlobalPluginExtHowToRunScriptsPrefs)mgrParent).Scripts;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}