using BestChat.IRC.Data.Defs;

namespace BestChat.IRC.Views.Desktop;

using Platform.DataAndExt.Ext;

public partial class BncInstanceEditorDlg : Avalonia.Controls.Window
{
	#region Constructors & Deconstructors
		public BncInstanceEditorDlg()
			=> InitializeComponent();
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelCreatingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelCreatingBncInstanceMsg, Rsrcs.strCancelCreatingBncInstanceTitle, MsBox.Avalonia.Enums
			.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation
			.CenterOwner);

		private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult>
			msgboxCancelEditingAreYouSure = MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs
			.strCancelEditingBncInstanceMsg, Rsrcs.strCancelEditingBncInstanceTitle, MsBox.Avalonia.Enums
			.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation
			.CenterOwner);
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

		private BncInstanceEditable? einstanceCtxt = null;
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

					if(einstanceCtxt != null)
						UpdateTitle();
				}
			}
		}

		public BncInstanceEditable? CtxtBNC
		{
			get => einstanceCtxt;

			set
			{
				DataContext = einstanceCtxt = value;

				if(mode != Modes.invalid)
					UpdateTitle();
			}
		}

		private bool IsOkToClose
			=> einstanceCtxt!.WereChangesMade && (mode switch
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
						=> throw new System.InvalidOperationException("Set the mode before showing a bouncer " +
							"instance editor"),

					Modes.create
						=> Rsrcs.strCreatingBncTitle,

					Modes.edit
						=> Rsrcs.strEditingBncTitleFmt.Fmt(einstanceCtxt!.Name),

					_
						=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode,
							"While setting the title for a bouncer instance editor"),
				};
	#endregion

	#region Event Handlers
		protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
		{
			if(!e.IsProgrammatic && !IsOkToClose)
				e.Cancel = true;

			base.OnClosing(e);
		}

		private void OnCancelClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs
			e)
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