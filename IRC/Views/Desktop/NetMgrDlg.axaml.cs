using System.Linq;

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
		private async void OnViewPredefinedClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
				.RoutedEventArgs args)
			=> await new PredefinedNetViewerDlg()
			{
				ServerInfoCtxt = (Data.Defs.NetServerInfo.Editable)dgPredefined.SelectedItem,
			}.ShowDialog(this);

		private void OnAddUserNet(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs
			args)
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

		private void OnEditUserNet(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs
			args)
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

		private void OnDelUserNet(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs
			args)
	{
		if(msgboxDelUserNetAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums.ButtonResult
				.Yes)
			foreach(Data.Defs.UserNet unetToBeDel in dgUser.SelectedItems.Cast<Data.Defs.UserNet>())
				Data.Defs.UserNetMgr.mgr.Remove(unetToBeDel.Name);
	}
	#endregion
}