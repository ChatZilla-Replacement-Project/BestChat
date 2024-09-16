namespace BestChat.IRC.Views.Desktop;

using Platform.DataAndExt.Ext;

public partial class ServerDomainEditorDlg : Avalonia.Controls.Window, System.ComponentModel.INotifyPropertyChanged
{
	#region Constructors & Deconstructors
		public ServerDomainEditorDlg()
			=> InitializeComponent();
	#endregion

	#region Events
		public new event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Constants
		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelCreatingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelCreatingServerFmt, Rsrcs.strCancelCreatingServerTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox
			.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelEditingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelEditingServerMsgFmt, Rsrcs.strCancelEditingServerTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox
			.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxOutOfPorts = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strOutOfPortsMsg, Rsrcs.strOutOfPortsTitle, MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia
			.Enums.Icon.Error, Avalonia.Controls.WindowStartupLocation.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxDelUnencryptedPortAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strDelNetServerUnencryptedPortMsg, Rsrcs.strDelNetServerUnencryptedPortTitle, MsBox.Avalonia.Enums
			.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation
			.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxDelSslPortAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strDelNetServerSslPortMsg, Rsrcs.strDelNetServerSslPortTitle, MsBox.Avalonia.Enums.ButtonEnum.YesNo,
			MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);
	#endregion

	#region Helper Types
		public enum Modes
		{
			invalid,
			creatingNew,
			editingExisting,
		}
	#endregion

	#region Members
		private Modes mode = Modes.invalid;

		private Data.Defs.NetServerInfo.Editable? eserverCtxt;
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

					if(eserverCtxt != null)
						UpdateTitle();
				}
			}
		}

		public Data.Defs.NetServerInfo.Editable? ServerCtxt
		{
			get => eserverCtxt;

			set
			{
				if(eserverCtxt != value)
				{
					DataContext = eserverCtxt = value;

					if(mode != Modes.invalid)
						UpdateTitle();
				}
			}
		}

		private bool IsOkToClose
			=> eserverCtxt != null && (mode switch
					{
						Modes.invalid
							=> throw new System.InvalidProgramException("This dialog should never have been shown with an " +
								"invalid mode"),

						Modes.creatingNew
							=> msgboxCancelCreatingAreYouSure,

						Modes.editingExisting
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
						=> throw new System.InvalidOperationException("Set the mode before showing a server editor"),

					Modes.creatingNew
						=> Rsrcs.strCreatingServerTitleFmt.Fmt(eserverCtxt!.Parent.Name),

					Modes.editingExisting
						=> Rsrcs.strEditingServerTitleFmt.Fmt(eserverCtxt!.Domain, eserverCtxt.Parent
							.Name),

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
							"While setting the title for a server editor"),
				};
	#endregion

	#region Event Handlers
		private async void OnAddUnencryptedPortClicked(Avalonia.Controls.Button btnSender, Avalonia
			.Interactivity.RoutedEventArgs args)
		{
			ushort? usNextAvailablePort = eserverCtxt!.NextAvailablePort;

			if(usNextAvailablePort == null)
			{
				await msgboxOutOfPorts.ShowWindowDialogAsync(this);

				return;
			}

			PortEditorDlg dlg = new()
			{
				CurPort = (ushort)usNextAvailablePort,
				Mode = PortEditorDlg.Modes.@new,
				Title = Rsrcs.strAddingBncUnencryptedPortDlgTitleFmt.Fmt(eserverCtxt.Domain),
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				eserverCtxt.AddPort(dlg.CurPort);
		}

		private void OnEditUnencryptedPort(Avalonia.Controls.Button btnSender, Avalonia
			.Interactivity.RoutedEventArgs args)
		{
			ushort usExistingPort = (ushort)lbKnownUnencryptedPorts.SelectedItem;

			PortEditorDlg dlg = new()
			{
				CurPort = usExistingPort,
				Mode = PortEditorDlg.Modes.changing,
				Title = Rsrcs.strEditingServerTitleFmt.Fmt(eserverCtxt!.Domain, eserverCtxt!.eunetParent.Name),
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
			{
				eserverCtxt.RemovePort(usExistingPort);
				eserverCtxt.AddPort(dlg.CurPort);
			}
		}

		private void OnDelUnencryptedPort(Avalonia.Controls.Button btnSender, Avalonia
			.Interactivity.RoutedEventArgs args)
		{
			if(msgboxDelUnencryptedPortAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums
					.ButtonResult.Yes)
				foreach(ushort usCurPortToRemove in lbKnownUnencryptedPorts.SelectedItems)
					eserverCtxt!.RemovePort(usCurPortToRemove);
		}

		private async void OnAddSslPortClicked(Avalonia.Controls.Button btnSender, Avalonia
			.Interactivity.RoutedEventArgs args)
		{
			ushort? usNextAvailablePort = eserverCtxt!.NextAvailablePort;

			if(usNextAvailablePort == null)
			{
				await msgboxOutOfPorts.ShowWindowDialogAsync(this);

				return;
			}

			PortEditorDlg dlg = new()
			{
				CurPort = (ushort)usNextAvailablePort,
				Mode = PortEditorDlg.Modes.@new,
				Title = Rsrcs.strAddingBncSslPortDlgTitleFmt.Fmt(eserverCtxt.Domain),
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
				eserverCtxt.AddPort(dlg.CurPort);
		}

		private void OnEditSslPort(Avalonia.Controls.Button btnSender, Avalonia
			.Interactivity.RoutedEventArgs args)
		{
			ushort usExistingPort = (ushort)lbKnownSslPorts.SelectedItem;

			PortEditorDlg dlg = new()
			{
				CurPort = usExistingPort,
				Mode = PortEditorDlg.Modes.changing,
				Title = Rsrcs.strEditingServerTitleFmt.Fmt(eserverCtxt!.Domain, eserverCtxt!.eunetParent.Name),
			};

			if(dlg.ShowDialog<bool?>(this).Result == true)
			{
				eserverCtxt.RemovePort(usExistingPort);
				eserverCtxt.AddPort(dlg.CurPort);
			}
		}

		private void OnDelSslPort(Avalonia.Controls.Button btnSender, Avalonia
			.Interactivity.RoutedEventArgs args)
		{
			if(msgboxDelSslPortAreYouSure.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums
					.ButtonResult.Yes)
				foreach(ushort usCurPortToRemove in lbKnownSslPorts.SelectedItems)
					eserverCtxt!.RemovePort(usCurPortToRemove);
		}

		protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
		{
			if(!e.IsProgrammatic && !IsOkToClose)
				e.Cancel = true;

			base.OnClosing(e);
		}

		private void OnCancelClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs
			args)
		{
			if(IsOkToClose)
				Close(false);
		}

		private void OnOkClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs
			args)
			=> Close(true);

		private void OnCloseClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs
			args)
			=> Close(null);
	#endregion
}