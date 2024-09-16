namespace BestChat.IRC.Views.Desktop;

public partial class BncMgrDlg : Avalonia.Controls.Window
{
	#region Constructors & Deconstructors
		public BncMgrDlg()
		{
			InitializeComponent();

			DataContext = Data.Defs.BncMgr.mgr;
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
		private void OnAddClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
		}

		private void OnEditClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			Data.Defs.BNC? bncSelected = dg.SelectedItem as Data.Defs.BNC;

			if(bncSelected != null)
			{
				if(bncSelected.IsPredefined)
				{
					new PredefinedBncEditorDlg()
					{
						CtxtBNC = bncSelected,
					}.ShowDialog(this);
				}
			}
		}

		private void OnDelClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
		}
	#endregion
}