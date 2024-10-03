namespace BestChat.Platform.UI.Desktop.Prefs.Pages;

public partial class GlobalAppearancePage : VisualPrefsTabCtrl
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