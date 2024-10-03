using BestChat.IRC.Data.Defs;

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
		private void OnAddClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
		}

		private void OnEditClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(dg.SelectedItem is not BncEditable bncSelected)
				return;

			if(bncSelected.IsPredefined)
			{
				new PredefinedBncEditorDlg()
				{
					CtxtBNC = bncSelected,
				}.ShowDialog(this);
			}
		}

		private void OnDelClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
		}
	#endregion
}