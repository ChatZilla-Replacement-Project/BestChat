namespace BestChat.Desktop
{
	public partial class MainWnd : Avalonia.Controls.Window
	{
		#region Constructors & Deconstructors
			public MainWnd()
			{
				instance = this;

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
			private PrefsWnd? prefsWnd;

			private static MainWnd? instance;
		#endregion

		#region Properties
			public static MainWnd? Instance
				=> instance;
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