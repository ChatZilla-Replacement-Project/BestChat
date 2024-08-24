namespace BestChat
{
	public partial class MainWnd : Avalonia.Controls.Window
	{
		#region Constructors & Deconstructors
			public MainWnd()
			{
				InitializeComponent();
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
			private PrefsWnd? prefsWnd = null;
		#endregion

		#region Properties
		#endregion

		#region Methods
		#endregion

		#region Event Handlers
			private void OnFilePrefsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
			{
				if(prefsWnd == null)
				{
					prefsWnd = new PrefsWnd();

					prefsWnd.Show();
				}
				else
					prefsWnd.Activate();
			}

			private void OnFileExitClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e) => System.Environment.Exit(0);
		#endregion
	}
}