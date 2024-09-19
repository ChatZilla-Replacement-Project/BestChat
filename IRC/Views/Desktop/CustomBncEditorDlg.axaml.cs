namespace BestChat.IRC.Views.Desktop;

using DocumentFormat.OpenXml.Bibliography;
using Platform.DataAndExt.Ext;

public partial class CustomBncEditorDlg : Avalonia.Controls.Window
{
	#region Constructors & Deconstructors
		public CustomBncEditorDlg()
			=> InitializeComponent();
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelCreatingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelCreatingBncMsg, Rsrcs.strCancelCreatingBncTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox
			.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelEditingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelEditingBncMsg, Rsrcs.strCancelEditingBncTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox
			.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxDelAllowedNetAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strDelBncAllowedNetQueryMsg, Rsrcs.strDelBncAllowedNetQueryTitle, MsBox.Avalonia.Enums.ButtonEnum
			.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxDelProhibitedNetAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strDelBncProhibitedNetQueryMsg, Rsrcs.strDelBncProhibitedNetQueryTitle, MsBox.Avalonia.Enums
			.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation
			.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxDelServerAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strDelBncServerAreYouSureMsg, Rsrcs.strDelBncServerAreYouSureTitle, MsBox.Avalonia.Enums.ButtonEnum
			.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxOutOfPorts = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strOutOfPortsMsg, Rsrcs.strOutOfPortsTitle, MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia
			.Enums.Icon.Error, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxDelUnencryptedPortAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strDelBncUnencryptedPortMsg, Rsrcs.strDelBncUnencryptedPortTitle, MsBox.Avalonia.Enums.ButtonEnum
			.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxDelSslPortAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strDelBncSslPortMsg, Rsrcs.strDelBncSslPortTitle, MsBox.Avalonia.Enums.ButtonEnum
			.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);
	#endregion

	#region Helper Types
		public enum Modes
		{
			invalid,
			@new,
			changing,
		}
	#endregion

	#region Members
		private Modes mode = Modes.invalid;

		private Data.Defs.BNC.Editable? ebncCtxt = null;
	#endregion

	#region Properties
		public Modes Mode
		{
			get => mode;

			set
			{
				if(mode != value)
				{
					mode = value;

					if(ebncCtxt != null)
						UpdateTitle();
				}
			}
		}

		public Data.Defs.BNC.Editable? CtxtBNC
		{
			get => ebncCtxt;

			set
			{
				DataContext = ebncCtxt = value;

				if(mode != Modes.invalid)
					UpdateTitle();
			}
		}

		private bool IsOkToClose
			=> (mode switch
					{
						Modes.invalid
							=> throw new System.InvalidProgramException("This dialog should never have been shown with "
								+ "an invalid mode"),

						Modes.@new
							=> msgboxCancelCreatingAreYouSure,

						Modes.changing
							=> msgboxCancelEditingAreYouSure,

						_
							=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
								"While handling the cancel button being clicked"),
					}).ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums.ButtonResult.Yes;
	#endregion

	#region Methods
		private void UpdateTitle()
			=> Title = mode switch
				{
					Modes.invalid
						=> throw new System.InvalidOperationException("Set the mode before showing a bouncer " +
							"editor"),

					Modes.@new
						=> Rsrcs.strCreatingBncTitle,

					Modes.changing
						=> Rsrcs.strEditingBncTitleFmt.Fmt(ebncCtxt!.Name),

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
							"While setting the title for a bouncer editor"),
				};
	#endregion

	#region Event Handlers
		private void OnUserWantsToVisitBncHomePage(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			using(System.Diagnostics.Process process = new System.Diagnostics.Process()
			{
				StartInfo = new System.Diagnostics.ProcessStartInfo()
				{
					UseShellExecute = true,
					FileName = ebncCtxt!.HomePage!.AbsolutePath,
				}
			})
			{
				process.Start();
			}
		}

		private void OnAddAllowedNetClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			BncNetworkSelectionDlg dlg = new()
			{
				CtxtBNC = ebncCtxt,
				Mode = BncNetworkSelectionDlg.Modes.@new,
				NetStatus = BncNetworkSelectionDlg.NetStatuses.allowed,
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				ebncCtxt!.AddAllowedNetwork(dlg.CurVal);
		}

		private void OnEditAllowedNetClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			BncNetworkSelectionDlg dlg = new()
			{
				CtxtBNC = ebncCtxt,
				Mode = BncNetworkSelectionDlg.Modes.changing,
				NetStatus = BncNetworkSelectionDlg.NetStatuses.allowed,
				StartingVal = (string)lbAllowedNets.SelectedItem,
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
			{
				ebncCtxt!.RemoveAllowedNetwork(dlg.StartingVal);
				ebncCtxt!.AddAllowedNetwork(dlg.CurVal);
			}
		}

		private void OnDelAllowedNetClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			if(msgboxDelAllowedNetAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums
					.ButtonResult.Yes)
				foreach(string strCurAllowedNetToRemove in lbAllowedNets.SelectedItems.Cast<string>())
					ebncCtxt!.RemoveAllowedNetwork(strCurAllowedNetToRemove);
		}

		private void OnAddProhibitedNetClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			BncNetworkSelectionDlg dlg = new()
			{
				CtxtBNC = ebncCtxt,
				Mode = BncNetworkSelectionDlg.Modes.@new,
				NetStatus = BncNetworkSelectionDlg.NetStatuses.prohibited,
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
			{
				ebncCtxt!.AddAllowedNetwork(dlg.CurVal);
			}
		}

		private void OnEditProhibitedNetClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			BncNetworkSelectionDlg dlg = new()
			{
				CtxtBNC = ebncCtxt,
				Mode = BncNetworkSelectionDlg.Modes.changing,
				NetStatus = BncNetworkSelectionDlg.NetStatuses.allowed,
				StartingVal = (string)lbProhibitedNets.SelectedItem,
			};

			if(dlg.ShowDialog<bool?>(this).Result == true && dlg.CurVal != dlg.StartingVal)
			{
				ebncCtxt!.RemoveAllowedNetwork(dlg.StartingVal);
				ebncCtxt!.AddAllowedNetwork(dlg.CurVal);
			}
		}

		private void OnDelProbibitedNetClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			if(msgboxDelProhibitedNetAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums
					.ButtonResult.Yes)
				foreach(string strCurProhibitedNetToRemove in lbProhibitedNets.SelectedItems.Cast<string>())
					ebncCtxt!.RemoveProhibitedNetwork(strCurProhibitedNetToRemove);
		}

		private void OnAddServerClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			if(ebncCtxt == null)
				return;

			BncServerEditorDlg dlg = new()
			{
				CtxtServer = new Data.Defs.BNC.ServerInfo(ebncCtxt).MakeEditable(ebncCtxt),
				Mode = BncServerEditorDlg.Modes.create,
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
			{
				dlg.CtxtServer.Save();

				ebncCtxt.AddServer(dlg.CtxtServer.serverOriginal);
			}
		}

		private void OnEditServerClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			if(ebncCtxt == null)
				return;

			Data.Defs.BNC.ServerInfo serverToEdit = (Data.Defs.BNC.ServerInfo)dgAllServers.SelectedItem;

			BncServerEditorDlg dlg = new()
			{
				CtxtServer = serverToEdit.MakeEditable(ebncCtxt),
				Mode = BncServerEditorDlg.Modes.edit,
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				dlg.CtxtServer.Save();
		}

		private void OnDelServerClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			if(msgboxDelServerAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums.ButtonResult
					.Yes)
				foreach(string strCurServerToRemove in dgAllServers.SelectedItems.Cast<string>())
					ebncCtxt!.RemoveServer(strCurServerToRemove);
		}

		private async void OnAddUnencryptedPortClicked(Avalonia.Controls.Button btnSender, Avalonia
			.Interactivity.RoutedEventArgs e)
		{
			ushort? usNextAvailablePort = ebncCtxt!.NextAvailablePort;

			if(usNextAvailablePort == null)
			{
				await msgboxOutOfPorts.ShowWindowDialogAsync(this);

				return;
			}

			PortEditorDlg dlg = new()
			{
				CurPort = (ushort)usNextAvailablePort,
				Mode = PortEditorDlg.Modes.@new,
				Title = Rsrcs.strAddingBncUnencryptedPortDlgTitleFmt.Fmt(ebncCtxt.Name),
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				ebncCtxt.AddPort(dlg.CurPort);
		}

		private void OnEditUnencryptedPortClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			ushort usExistingPort = (ushort)lbAllUnencryptedPorts.SelectedItem;

			PortEditorDlg dlg = new()
			{
				CurPort = usExistingPort,
				Mode = PortEditorDlg.Modes.changing,
				Title = Rsrcs.strEditingBncUnencryptedPortDlgTitleFmt.Fmt(ebncCtxt!.Name),
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
			{
				ebncCtxt.RemovePort(usExistingPort);
				ebncCtxt.AddPort(dlg.CurPort);
			}
		}

		private void OnDelUnencryptedPortClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			if(msgboxDelUnencryptedPortAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums
					.ButtonResult.Yes)
				foreach(ushort usCurPortToRemove in lbAllUnencryptedPorts.SelectedItems)
					ebncCtxt!.RemovePort(usCurPortToRemove);
		}

		private async void OnAddSslPortClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			ushort? usNextAvailablePort = ebncCtxt!.NextAvailablePort;

			if(usNextAvailablePort == null)
			{
				await msgboxOutOfPorts.ShowWindowDialogAsync(this);

				return;
			}

			PortEditorDlg dlg = new()
			{
				CurPort = (ushort)usNextAvailablePort,
				Mode = PortEditorDlg.Modes.@new,
				Title = Rsrcs.strAddingBncSslPortDlgTitleFmt.Fmt(ebncCtxt.Name),
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				ebncCtxt.AddSslPort(dlg.CurPort);
		}

		private void OnEditSslPortClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			ushort usExistingPort = (ushort)lbAllSslPorts.SelectedItem;

			PortEditorDlg dlg = new()
			{
				CurPort = usExistingPort,
				Mode = PortEditorDlg.Modes.changing,
				Title = Rsrcs.strEditingBncSslPortDlgTitleFmt.Fmt(ebncCtxt!.Name),
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
			{
				ebncCtxt.RemoveSslPort(usExistingPort);
				ebncCtxt.AddSslPort(dlg.CurPort);
			}
		}

		private void OnDelSslPortClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			if(msgboxDelSslPortAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums
					.ButtonResult.Yes)
				foreach(ushort usCurPortToRemove in lbAllSslPorts.SelectedItems)
					ebncCtxt!.RemoveSslPort(usCurPortToRemove);
		}

		protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
		{
			if(!e.IsProgrammatic && !IsOkToClose)
				e.Cancel = true;

			base.OnClosing(e);
		}

		private void OnCancelClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs
			e)
		{
			if(IsOkToClose)
				Close(false);
		}

		private void OnOkClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
			=> Close(true);
	#endregion
}