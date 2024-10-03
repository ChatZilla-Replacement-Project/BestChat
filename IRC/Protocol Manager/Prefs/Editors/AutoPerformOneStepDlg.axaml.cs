namespace BestChat.IRC.ProtocolMgr.Prefs.Editors;

public partial class AutoPerformOneStepDlg :Avalonia.Controls.Window
{
	public AutoPerformOneStepDlg()
	{
		InitializeComponent();
	}

	private static MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirmCreating = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelCreatingNewAutoPerformStepTitle, Rsrcs
			.strCancelCreatingNewAutoPerformStepMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon
			.Question,Avalonia.Controls.WindowStartupLocation.CenterOwner);

	private static MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirmEditing = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelEditingAutoPerformStepTitle, Rsrcs
			.strCancelEditingAutoPerformStepMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon
			.Question,Avalonia.Controls.WindowStartupLocation.CenterOwner);

	public enum Modes
	{
		invalid,
		create,
		edit,
	}

	private Modes mode;

	private Data.Prefs.GlobalAutoPerformOneStepEditable? stepCtxt;

	public Modes Mode
	{
		get => mode;

		set
		{
			if(mode != value)
			{
				mode = value;

				if(stepCtxt == null)
					UpdateTitle();
			}
		}
	}

	public Data.Prefs.GlobalAutoPerformOneStepEditable? CtxtStep
	{
		get => stepCtxt;

		set
		{
			if(stepCtxt != value)
			{
				stepCtxt = value;

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
				=> Rsrcs.strCreatingNewAutoPerformStepTitle,

			Modes.edit
				=> Rsrcs.strEditingAutoPerformStepTitle,

			var _
				=> null,
		};

	protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs args)
	{
		if(stepCtxt is not null && stepCtxt.WereChangesMade &&  MsgBoxToUse.ShowWindowDialogAsync((Avalonia.Controls
				.Window)(VisualRoot ?? throw new System.InvalidProgramException("How is this in a non-window?"))).Result !=
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