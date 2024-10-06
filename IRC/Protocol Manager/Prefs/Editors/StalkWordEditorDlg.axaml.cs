using BestChat.Platform.DataAndExt.Ext;

namespace BestChat.IRC.ProtocolMgr.Prefs.Editors;

public partial class StalkWordEditorDlg : Avalonia.Controls.Window
{
	public StalkWordEditorDlg()
		=> InitializeComponent();

	private static MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirmCreating = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelCreatingNewStalkWordTitle, Rsrcs
				.strCancelCreatingNewStalkWordMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon
				.Question, Avalonia.Controls.WindowStartupLocation.CenterOwner);

	private static MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirmEditing = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelEditingStalkWordTitle, Rsrcs
			.strCancelEditingStalkWordMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
			.Controls.WindowStartupLocation.CenterOwner);


	public enum Modes
	{
		invalid,
		create,
		edit,
	}


	private Modes mode;

	private Data.Prefs.GlobalStalkWordsOneStalkWordEditable? swCtxt;


	public Modes Mode
	{
		get => mode;

		set
		{
			if(mode != value)
			{
				mode = value;

				if(swCtxt == null)
					UpdateTitle();
			}
		}
	}

	public Data.Prefs.GlobalStalkWordsOneStalkWordEditable? CtxtStalkWord
	{
		get => swCtxt;

		set
		{
			if(swCtxt != value)
			{
				swCtxt = value;

				if(mode != Modes.invalid)
					UpdateTitle();
			}
		}
	}

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> MsgBoxToUse
		=> mode switch
		{
			Modes.create
				=> msgboxCancelConfirmCreating,

			Modes.edit
				=> msgboxCancelConfirmEditing,

			Modes.invalid
				=> throw new System.InvalidOperationException("You must set Mode before opening a stalk word editor"),

			var _
				=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode, @"While selecting a " +
					@"message box as the user wanted to close the dialog without saving changes"),
		};

	private void UpdateTitle()
		=> Title = mode switch
		{
			Modes.create
				=> Rsrcs.strCreatingNewStalkWordDlgTitle,

			Modes.edit
				=> Rsrcs.strEditingStalkWordDlgTitleFmt.Fmt(editCtnts.Text ?? ""),

			Modes.invalid
				=> throw new System.InvalidOperationException("You must set Mode before opening a stalk word editor"),

			var _
				=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode, @"While updating the " +
					@"title"),
		};

	protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs args)
	{
		if(swCtxt is not null && swCtxt.WereChangesMade && MsgBoxToUse.ShowWindowDialogAsync(
					(Avalonia.Controls
						.Window)(VisualRoot ?? throw new System.InvalidProgramException("How is this in a non-window?")))
				.Result !=
			MsBox.Avalonia.Enums.ButtonResult.Yes)
			args.Cancel = true;

		base.OnClosing(args);
	}

	private void OnOkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(true);

	private void OnCancelClicked(object? ojbSender, Avalonia.Interactivity.RoutedEventArgs arg)
	{
		if(MsgBoxToUse.ShowWindowDialogAsync((Avalonia.Controls.Window)(VisualRoot ?? throw new System
				.InvalidProgramException("How is this in a non-window?"))).Result == MsBox.Avalonia.Enums.ButtonResult.Yes)
			Close(true);
	}

	private void OnCloseClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(null);
}