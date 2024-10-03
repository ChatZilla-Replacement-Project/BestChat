namespace BestChat.IRC.Views.Desktop;

using Platform.DataAndExt.Ext;

public partial class BncServerEditorDlg : Avalonia.Controls.Window
{
	#region Constructors & Deconstructors
		public BncServerEditorDlg()
			=> InitializeComponent();
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelCreatingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelCreatingBncServerMsg, Rsrcs.strCancelCreatingBncServerTitle, MsBox.Avalonia.Enums
			.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation
			.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelEditingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelEditingBncServerMsg, Rsrcs.strCancelEditingBncServerMsg, MsBox.Avalonia.Enums.ButtonEnum
			.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);
	#endregion

	#region Helper Types
		public enum Modes
		{
			invalid,
			create,
			edit,
		}
	#endregion

	#region Members
		private Modes mode = Modes.invalid;

		private Data.Defs.BncInfoEditable? eserverCtxt = null;
	#endregion

	#region Properties
		public Modes Mode
		{
			get => mode;

			set
			{
				if(mode == value)
					return;

				mode = value;

				if(eserverCtxt != null)
					UpdateTitle();
			}
		}

		public Data.Defs.BncInfoEditable? CtxtServer
		{
			get => eserverCtxt;

			init
			{
				if(eserverCtxt == value)
					return;

				eserverCtxt = value;

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

						Modes.create
							=> msgboxCancelCreatingAreYouSure,

						Modes.edit
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
						=> throw new System.InvalidOperationException("Set the mode before showing a port editor"),

					Modes.create
						=> Rsrcs.strCreatingBncServerTitleFmt.Fmt(eserverCtxt!.serverOriginal!.bncParent!.Name),

					Modes.edit
						=> Rsrcs.strEditingBncTitleFmt.Fmt(eserverCtxt!.serverOriginal!.bncParent!.Name),

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
							"While setting the title for a bouncer editor"),
				};
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
	#endregion
}