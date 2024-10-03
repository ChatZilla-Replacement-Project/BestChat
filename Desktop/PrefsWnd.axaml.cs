// Ignore Spelling: Prefs

namespace BestChat.Desktop;

public partial class PrefsWnd : Avalonia.Controls.Window
{
	#region Constructors & Deconstructors
		public PrefsWnd()
		{
			InitializeComponent();

			treeMain.ItemsSource = new System.Collections.Generic.List<Platform.UI.Desktop.Prefs.VisualPrefsTreeData>()
				{
					new(Platform.UI.Desktop.Prefs.RootPrefs.Instance),
				};
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