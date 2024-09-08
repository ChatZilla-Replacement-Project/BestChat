// Ignore Spelling: Prefs

namespace BestChat.Desktop;

public partial class PrefsWnd : Avalonia.Controls.Window
{
	#region Constructors & Deconstructors
		public PrefsWnd()
		{
			InitializeComponent();

			treeMain.ItemsSource = RootPrefs.Instance.ChildMgrByName;
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