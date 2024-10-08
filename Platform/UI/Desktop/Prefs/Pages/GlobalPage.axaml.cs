namespace BestChat.Platform.UI.Desktop.Prefs.Pages;

public partial class GlobalPage : AbstractVisualPrefsTabCtrl
{
	public GlobalPage()
		=> InitializeComponent();

	public override System.Collections.Generic.IEnumerable<System.Type> HandlesChildMgrsOfType
		=>
			[
				typeof(GlobalCompositionPrefs),
			];
}