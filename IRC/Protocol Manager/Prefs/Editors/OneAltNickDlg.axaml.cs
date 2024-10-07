namespace BestChat.IRC.ProtocolMgr.Prefs.Editors;

public partial class GlobalOneAltNickDlg : Avalonia.Controls.Window
{
	public GlobalOneAltNickDlg()
		=> InitializeComponent();

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirmNew = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelCreatingNewAltNickTitle, Rsrcs
		.strCancelCreatingNewAltNickMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
			.Controls.WindowStartupLocation.CenterOwner);

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirmEdit = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelEditingAltNickTitle, Rsrcs.strCancelEditingAltNickMsg,
		MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation
		.CenterOwner);


	public enum Modes
	{
		invalid,
		create,
		edit,
	}

	private Modes mode;

	private Data.Prefs.GlobalAltNicksOneAltNickEditable? enickCtxt;

	public Modes Mode
	{
		get => mode;

		set
		{
			if(mode != value)
			{
				mode = value;

				UpdateTitle();
			}
		}
	}

	public Data.Prefs.GlobalAltNicksOneAltNickEditable? CtxtAltNick
	{
		get => enickCtxt;

		set
		{
			if(enickCtxt != value)
			{
				DataContext = enickCtxt = value;

				UpdateTitle();
			}
		}
	}

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> MsgBoxToUseWhenUserTriesToCloseWithoutSaving
		=> mode switch
		{
			Modes.create
				=> msgboxCancelConfirmNew,

			Modes.edit
				=> msgboxCancelConfirmEdit,

			Modes.invalid
				=> throw new System.InvalidOperationException("Set the Mode before showing a alternate nick editor dialog"),

			var _
				=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode, @"While selecting a " +
					@"message box as the user tried to close the window."),
		};

	private void UpdateTitle()
		=> Title = mode switch
		{
			Modes.create
				=> Rsrcs.strOneAltNickEditorCreatingTitle,

			Modes.edit
				=> Rsrcs.strOneAltNickEditorEditingTitle,

			var _
				=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode, @"While selecting a " +
					@"title"),
		};

	protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs args)
	{
		if(msgboxCancelConfirmNew.ShowWindowDialogAsync((Avalonia.Controls.Window)(VisualRoot ?? throw new System
				.InvalidProgramException("How is this in a non-window?"))).Result != MsBox.Avalonia.Enums.ButtonResult.Yes)
			args.Cancel = true;

		base.OnClosing(args);
	}

	private void OnOkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(true);

	private void OnCancelClicked(object? ojbSender, Avalonia.Interactivity.RoutedEventArgs arg)
	{
		if(MsgBoxToUseWhenUserTriesToCloseWithoutSaving.ShowWindowDialogAsync((Avalonia.Controls.Window)(VisualRoot ?? throw
				new System.InvalidProgramException("How is this in a non-window?"))).Result == MsBox.Avalonia.Enums.ButtonResult
				.Yes)
			Close(false);
	}

	private void OnCloseClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(null);
}