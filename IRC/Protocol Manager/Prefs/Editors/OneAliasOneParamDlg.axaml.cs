namespace BestChat.IRC.ProtocolMgr.Prefs.Editors;

public partial class OneAliasOneParamDlg : Avalonia.Controls.Window
{
	public OneAliasOneParamDlg()
		=> InitializeComponent();

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirmCreating = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelCreatingNewAliasParamTitle, Rsrcs
		.strCancelCreatingNewAliasParamMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question,
		Avalonia.Controls.WindowStartupLocation.CenterOwner);

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirmEditing = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelEditingAliasParamTitle, Rsrcs
		.strCancelEditingAliasParamMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question,
		Avalonia.Controls.WindowStartupLocation.CenterOwner);

	public enum Modes : byte
	{
		invalid,
		create,
		edit,
	}

	private Modes mode;

	private Data.Prefs.GlobalAliasesOneAliasOneParamEditable? eaparamCtxt;

	public Modes Mode
	{
		get => mode;

		set
		{
			if(mode != value)
			{
				mode = value;

				if(eaparamCtxt is not null)
					UpdateTitle();
			}
		}
	}

	public Data.Prefs.GlobalAliasesOneAliasOneParamEditable? CtxtParam
	{
		get => eaparamCtxt;

		set
		{
			if(eaparamCtxt != value)
			{
				DataContext = eaparamCtxt = value;

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

			var _
				=> throw new System.InvalidOperationException("Invalid mode for the auto perform one step editor.")
		};

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
		if(eaparamCtxt is not null && eaparamCtxt.WereChangesMade && MsgBoxToUse.ShowWindowDialogAsync((Avalonia.Controls
				.Window)(VisualRoot ?? throw new System.InvalidProgramException(@"How is this in a non-window?"))).Result !=
				MsBox.Avalonia.Enums.ButtonResult.Yes)
			args.Cancel = true;

		base.OnClosing(args);
	}

	private void OnOkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(true);

	private void OnCancelClicked(object? ojbSender, Avalonia.Interactivity.RoutedEventArgs arg)
	{
		if(eaparamCtxt is not null && eaparamCtxt.WereChangesMade && MsgBoxToUse.ShowWindowDialogAsync((Avalonia.Controls
				.Window)(VisualRoot ?? throw new System.InvalidProgramException(@"How is this in a non-window?"))).Result ==
				MsBox.Avalonia.Enums.ButtonResult.Yes)
			Close(true);
	}

	private void OnCloseClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(null);
}