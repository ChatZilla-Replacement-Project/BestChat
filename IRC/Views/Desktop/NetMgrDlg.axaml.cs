using System.Linq;
using BestChat.IRC.Data.Defs;

namespace BestChat.IRC.Views.Desktop;

public partial class NetMgrDlg : Avalonia.Controls.Window
{
	#region Constructors & Deconstructors
		public NetMgrDlg()
			=> InitializeComponent();
	#endregion

	#region Constants
		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxDelUserNetAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strDelUserNetMsg, Rsrcs.strDelUserNetTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia
			.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);
	#endregion

	#region Methods
		protected override void OnInitialized()
		{
			base.OnInitialized();

			groupPredefined.DataContext = Data.Defs.PredefinedNetMgr.mgr.AllItems.Values;
			groupUser.DataContext = Data.Defs.UserNetMgr.mgr.AllItems.Values;
		}
	#endregion

	#region Event Handlers
		private async void OnViewPredefinedClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
			=> await new PredefinedNetViewerDlg()
			{
				NetCtxt = (PredefinedNet?)dgPredefined.SelectedItem ?? throw new System.InvalidProgramException("Somehow we " +
					"let the user click view predefined even though there is no selection."),
			}.ShowDialog(this);

		private void OnAddUserNet(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		{
			Data.Defs.UserNet unetNew = new();

			UserNetEditorDlg dlg = new()
			{
				UserNetCtxt = unetNew.MakeEditableVersion(),
				Mode = UserNetEditorDlg.Modes.create,
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
			{
				dlg.UserNetCtxt.Save();

				Data.Defs.UserNetMgr.mgr.Add(unetNew);
			}
		}

		private void OnEditUserNet(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		{
			Data.Defs.UserNet unetWhatsBeingEdited = (Data.Defs.UserNet)dgUser.SelectedItem;

			UserNetEditorDlg dlg = new()
			{
				UserNetCtxt = unetWhatsBeingEdited.MakeEditableVersion(),
				Mode = UserNetEditorDlg.Modes.create,
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				dlg.UserNetCtxt.Save();
		}

		private void OnDelUserNet(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(msgboxDelUserNetAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums.ButtonResult
				.Yes)
			foreach(Data.Defs.UserNet unetToBeDel in dgUser.SelectedItems.Cast<Data.Defs.UserNet>())
				Data.Defs.UserNetMgr.mgr.Remove(unetToBeDel.Name);
	}
	#endregion
}