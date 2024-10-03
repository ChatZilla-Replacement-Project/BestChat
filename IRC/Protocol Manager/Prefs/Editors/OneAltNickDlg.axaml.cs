namespace BestChat.IRC.ProtocolMgr.Prefs.Editors;

public partial class OneAltNickDlg : Avalonia.Controls.Window
{
	public OneAltNickDlg()
		=> InitializeComponent();

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirm = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelCreatingNewAltNickTitle, Rsrcs
		.strCancelCreatingNewAltNickMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
			.Controls.WindowStartupLocation.CenterOwner);

	public enum Modes
	{
		create,
		edit,
	}

	private Modes? mode;

	private Data.Prefs.GlobalAltNicksOneAltNickEditable? enickCtxt;

	public Modes? Mode
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

	private void UpdateTitle()
		=> Title = mode switch
		{
			Modes.create
				=> Rsrcs.strOneAltNickEditorCreatingTitle,

			Modes.edit
				=> Rsrcs.strOneAltNickEditorEditingTitle,

			var _
				=> null,
		};

	protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs args)
	{
		if(msgboxCancelConfirm.ShowWindowDialogAsync((Avalonia.Controls.Window)(VisualRoot ?? throw new System
				.InvalidProgramException("How is this in a non-window?"))).Result != MsBox.Avalonia.Enums.ButtonResult.Yes)
			args.Cancel = true;

		base.OnClosing(args);
	}

	private void OnOkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(true);

	private void OnCancelClicked(object? ojbSender, Avalonia.Interactivity.RoutedEventArgs arg)
	{
		if(msgboxCancelConfirm.ShowWindowDialogAsync((Avalonia.Controls.Window)(VisualRoot ?? throw new System
				.InvalidProgramException("How is this in a non-window?"))).Result == MsBox.Avalonia.Enums.ButtonResult.Yes)
			Close(true);
	}

	private void OnCloseClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(null);
}