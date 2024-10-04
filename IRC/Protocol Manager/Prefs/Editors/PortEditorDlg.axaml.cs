namespace BestChat.IRC.ProtocolMgr.Prefs.Editors;

public partial class PortEditorDlg : Avalonia.Controls.Window
{
	public PortEditorDlg()
		=> InitializeComponent();

	public static readonly Avalonia.DirectProperty<PortEditorDlg, int?> SelPortProperty = Avalonia.AvaloniaProperty
		.RegisterDirect<PortEditorDlg, int?>(
			nameof(SelPort),
			sender
				=> sender.SelPort,
			(sender, iNewSelPort)
				=> sender.SelPort = iNewSelPort,
			null
		);

	public static readonly Avalonia.DirectProperty<PortEditorDlg, System.Collections.Generic.IReadOnlySet<int>>
		PortsAlreadyListedProperty = Avalonia.AvaloniaProperty.RegisterDirect<PortEditorDlg, System.Collections.Generic
		.IReadOnlySet<int>>(
			nameof(PortsAlreadyListed),
			sender
				=> sender.PortsAlreadyListed,
			(sender, setNewVal)
				=> sender.PortsAlreadyListed = setNewVal,
			new System.Collections.Generic.SortedSet<int>()
		);

	public static readonly Avalonia.StyledProperty<bool> WereChangesMadeProperty = Avalonia.AvaloniaProperty
		.Register<PortEditorDlg, bool>(nameof(WereChangesMade));

	public static readonly Avalonia.StyledProperty<bool> HasErrorsProperty = Avalonia.AvaloniaProperty
		.Register<PortEditorDlg, bool>(nameof(HasErrors));

	public static readonly Avalonia.StyledProperty<bool> IsDataValidProperty = Avalonia.AvaloniaProperty
		.Register<PortEditorDlg, bool>(nameof(IsDataValid));


	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxNewCancelConfirm = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strPortEditorCancelNewPortTitle, Rsrcs
		.strPortEditorCancelNewPortMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question,Avalonia
		.Controls.WindowStartupLocation.CenterOwner);

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxEditCancelConfirm = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strPortEditorCancelPortEditTitle, Rsrcs
		.strPortEditorCancelPortEditMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question,Avalonia
		.Controls.WindowStartupLocation.CenterOwner);



	public enum Modes : byte
	{
		invalid,
		@new,
		edit,
	}

	private Modes mode = Modes.invalid;

	private int? iInitialVal;

	public Modes Mode
	{
		get => mode;

		set => mode = value;
	}

	public int? SelPort
	{
		get => GetValue(SelPortProperty);

		set => SetValue(SelPortProperty, value);
	}

	public System.Collections.Generic.IReadOnlySet<int> PortsAlreadyListed
	{
		get => GetValue(PortsAlreadyListedProperty);

		set => SetValue(PortsAlreadyListedProperty, value);
	}

	public bool WereChangesMade
	{
		get => GetValue(WereChangesMadeProperty);

		private set => SetValue(WereChangesMadeProperty, value);
	}

	public bool HasErrors
	{
		get => GetValue(HasErrorsProperty);

		set => SetValue(HasErrorsProperty, value);
	}

	public bool IsDataValid
	{
		get => GetValue(IsDataValidProperty);

		private set => SetValue(IsDataValidProperty, value);
	}

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> WhichMsgBoxToUseWhenCancelling
		=> mode switch
		{
			Modes.@new
				=> msgboxNewCancelConfirm,

			Modes.edit
				=> msgboxEditCancelConfirm,

			Modes.invalid
				=> throw new System.InvalidOperationException("Set the Mode before opening a port editor dialog."),

			var _
				=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode, @"While deciding which " +
					@"message box to display after the user tried to close the dialog with changes, but not by clicking OK."),
		};

	private void UpdateHasErrors()
		=> HasErrors = mode switch
		{
			Modes.edit
				=> SelPort != iInitialVal && SelPort is int iSelPort && PortsAlreadyListed.Contains(iSelPort),

			Modes.@new
				=> SelPort is int iSelPort && PortsAlreadyListed.Contains(iSelPort),

			Modes.invalid
				=> throw new System.InvalidOperationException("Set the mode before opening the port editor."),

			var _
				=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(
					mode, @"While checking for " +
					@"errors."),
		};

	protected override void OnInitialized()
	{
		switch(mode)
		{
			case Modes.invalid:
				throw new System.InvalidOperationException(@"Set the mode before creating the window");

			case Modes.edit when SelPort is null:
				throw new System.InvalidOperationException(@"A null value for SelPort is only allowed if mode is @new.  " +
					@"Otherwise, set it before creating the window");

			case Modes.@new:
			default:
				base.OnInitialized();

				iInitialVal = SelPort;

				WereChangesMade = false;

				break;
		}
	}

	protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs args)
	{
		if(HasErrors && WhichMsgBoxToUseWhenCancelling.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums.ButtonResult.No)
			args.Cancel = true;

		base.OnClosing(args);
	}

	private void OnOkClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(true);

	private void OnCancelClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(WhichMsgBoxToUseWhenCancelling.ShowWindowDialogAsync(this).Result == MsBox.Avalonia.Enums.ButtonResult
				.Yes)
			Close(false);
	}

	private void OnCloseClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(null);

	private void OnPortChanged(object? sender, Avalonia.Controls.NumericUpDownValueChangedEventArgs args)
		=> UpdateHasErrors();
}