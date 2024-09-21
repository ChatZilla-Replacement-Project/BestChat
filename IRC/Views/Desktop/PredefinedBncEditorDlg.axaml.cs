using BestChat.Platform.DataAndExt.Ext;

namespace BestChat.IRC.Views.Desktop;

public partial class PredefinedBncEditorDlg : Avalonia.Controls.Window
{
	#region Constructors & Deconstructors
		public PredefinedBncEditorDlg()
			=> InitializeComponent();
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelEditingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelEditingPredefinedBncMsg, Rsrcs.strCancelEditingPredefinedBncMsg, MsBox.Avalonia.Enums
			.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation
			.CenterOwner);
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private Data.Defs.BncEditable? bncCtxt = null;
	#endregion

	#region Properties
		public Data.Defs.BncEditable? CtxtBNC
		{
			get => bncCtxt;

			set => DataContext = bncCtxt = value;
		}

		private bool IsOkToClose
			=> msgboxCancelEditingAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums
				.ButtonResult.Yes;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
		protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
		{
			if(!e.IsProgrammatic && !IsOkToClose)
				e.Cancel = true;

			base.OnClosing(e);
		}

		private void OnCancelClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(IsOkToClose)
				Close(false);
		}

		private void OnOkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
			=> Close(true);

		private void OnCloseClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
			=> Close(null);

		private void OnTechSupportChanLinkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			// TODO: Open and select the channel.
		}
	#endregion
}