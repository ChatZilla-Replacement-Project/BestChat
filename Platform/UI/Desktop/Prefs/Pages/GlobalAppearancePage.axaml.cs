namespace BestChat.Platform.UI.Desktop.Prefs.Pages;

public partial class GlobalAppearancePage : AbstractVisualPrefsTabCtrl
{
	public GlobalAppearancePage()
	{
		InitializeComponent();
	}

	public override System.Collections.Generic.IEnumerable<System.Type> HandlesChildMgrsOfType
		=>
			[
				typeof(DataAndExt.Prefs.GlobalAppearanceConfModePrefs),
				typeof(DataAndExt.Prefs.GlobalAppearanceTimeStampPrefs),
				typeof(DataAndExt.Prefs.GlobalAppearanceMsgGroupsPrefs),
			];
}