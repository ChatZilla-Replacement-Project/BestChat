namespace BestChat.Platform.DataAndExt.Prefs;

public class GlobalPluginExtHowToRunScriptsPrefs : AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalPluginExtHowToRunScriptsPrefs(AbstractMgr mgrParent) :
			base(mgrParent, "How to run them", Rsrcs
				.strGlobalPluginsHowToRunThemTitle, Rsrcs.strGlobalPluginsHowToRunThemDesc)
		{
			groupedByWhatRunsThem = new(this);
			ungrouped = new(this);
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
		private readonly GlobalPluginExtHowToRunScriptsGroupedByWhatRunsThemPrefs groupedByWhatRunsThem;

		private readonly GlobalPluginExtHowToRunScriptsUngroupedPrefs ungrouped;
	#endregion

	#region Properties
		public GlobalPluginExtHowToRunScriptsGroupedByWhatRunsThemPrefs GroupedByWhatRunsThem
			=> groupedByWhatRunsThem;

		public GlobalPluginExtHowToRunScriptsUngroupedPrefs Ungrouped
			=> ungrouped;

		public MappedListItem<string, GlobalPluginExtScriptEntry> Scripts
			=> ((GlobalPluginExtPrefs)mgrParent).Scripts;
		#endregion

		#region Methods
		#endregion

		#region Event Handlers
		#endregion
}