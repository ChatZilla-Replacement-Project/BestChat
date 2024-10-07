using BestChat.Platform.DataAndExt.Ext;

namespace BestChat.IRC.ProtocolMgr.Prefs.Editors;

public partial class OneNotifyDlg : Avalonia.Controls.Window
{
	public OneNotifyDlg()
		=> InitializeComponent();


	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirmNew = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelAddingNewNotifyTitle, Rsrcs
		.strCancelAddingNewNotifyMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
		.Controls.WindowStartupLocation.CenterOwner);

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirmEdit = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelChangingNotifyTitle, Rsrcs
		.strCancelCreatingNewAliasMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
		.Controls.WindowStartupLocation.CenterOwner);


	public enum Modes
	{
		invalid,
		add,
		edit,
	}


	private Modes mode;

	private Data.Prefs.NotifyWhenOnlineOneNotifyEditable? enotifyCtxt;


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

	public Data.Prefs.NotifyWhenOnlineOneNotifyEditable? CtxtNotify
	{
		get => enotifyCtxt;

		set
		{
			if(enotifyCtxt != value)
			{
				DataContext = enotifyCtxt = value;

				UpdateTitle();
			}
		}
	}

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> MsgBoxToUseWhenUserTriesToCloseWithoutSaving
		=> mode switch
		{
			Modes.add
				=> msgboxCancelConfirmNew,

			Modes.edit
				=> msgboxCancelConfirmEdit,

			Modes.invalid
				=> throw new System.InvalidOperationException(@"Set the Mode before showing a alternate nick editor dialog"),

			var _
				=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(
					mode, @"While selecting a " +
					@"message box as the user tried to close the window."),
		};


	private void UpdateTitle()
		=> Title = mode switch
		{
			Modes.add
				=> Rsrcs.strAddingNewNotifyDlgTitle,

			Modes.edit
				=> Rsrcs.strChangingNotifyDlgTitleFmt.Fmt(editWhatToFollow.Text ?? ""),

			var _
				=> throw new Platform.DataAndExt.Exceptions.UnknownOrInvalidEnumException<Modes>(mode, @"While selecting a " +
					@"title"),
		};


	private void OnOkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(true);

	private void OnCancelClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(MsgBoxToUseWhenUserTriesToCloseWithoutSaving.ShowWindowDialogAsync((Avalonia.Controls.Window)(VisualRoot ?? throw
				new System.InvalidProgramException("How is this in a non-window?"))).Result == MsBox.Avalonia.Enums.ButtonResult
				.Yes)
			Close(false);
	}

	private void OnCloseClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(null);

	private void OnWhatToFollowTextChanged(object? objSender, Avalonia.Controls.TextChangedEventArgs args)
		=> UpdateTitle();
}