namespace BestChat.Platform.DataAndExt.Prefs
{
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
	public class GroupedByWhatRunsThemPrefs : AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public GroupedByWhatRunsThemPrefs(AbstractMgr mgrParent) : base(mgrParent, "Grouped by what runs them", Rsrcs
			.strGlobalPluginsHowToRunThemGroupedByWhatRunsThemTitle, Rsrcs
			.strGlobalPluginsHowToRunThemGroupedByWhatRunsThemDesc)
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
		#endregion

		#region Methods
		#endregion

		#region Event Handlers
		#endregion
	}

	public class UngroupedPrefs : AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public UngroupedPrefs(AbstractMgr mgrParent)
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
		#endregion

		#region Methods
		#endregion

		#region Event Handlers
		#endregion
	}
	#endregion

	#region Members
	private readonly GroupedByWhatRunsThemPrefs groupedByWhatRunsThem;

	private readonly UngroupedPrefs ungrouped;
	#endregion

	#region Properties
	public GroupedByWhatRunsThemPrefs GroupedByWhatRunsThem
		=> groupedByWhatRunsThem;

	public UngroupedPrefs Ungrouped
		=> ungrouped;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}
}