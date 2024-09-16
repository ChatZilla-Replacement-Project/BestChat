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

		private Data.Defs.NetServerInfo.Editable? eserverWhatsBeingEdited;
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

					if(eserverWhatsBeingEdited != null)
						UpdateTitle();
				}
			}
		}

		public Data.Defs.NetServerInfo.Editable? WhatsBeingEdited
		{
			get => eserverWhatsBeingEdited;

			set
			{
				if(eserverWhatsBeingEdited != value)
				{
					DataContext = eserverWhatsBeingEdited = value;

					if(mode != Modes.invalid)
						UpdateTitle();
				}
			}
		}

		private bool IsOkToClose
			=> eserverWhatsBeingEdited != null && (mode switch
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
						=> Rsrcs.strCreatingServerTitleFmt.Fmt(eserverWhatsBeingEdited!.Parent.Name),

					Modes.editingExisting
						=> Rsrcs.strEditingServerTitleFmt.Fmt(eserverWhatsBeingEdited!.Domain, eserverWhatsBeingEdited.Parent
							.Name),

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
							"While setting the title for a server editor"),
				};
	#endregion

	#region Event Handlers
		protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
		{
			if(!e.IsProgrammatic && !IsOkToClose)
				e.Cancel = true;

			base.OnClosing(e);
		}

		private void OnCancelClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if(IsOkToClose)
				Close(false);
		}

		private void OnOkClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
			=> Close(true);

		private void OnCloseClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
			=> Close(null);
	#endregion
}