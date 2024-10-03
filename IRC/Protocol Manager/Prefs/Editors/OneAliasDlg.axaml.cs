using BestChat.Platform.UI.Desktop.Ext;

namespace BestChat.IRC.ProtocolMgr.Prefs.Editors;

public partial class OneAliasEditorDlg : Avalonia.Controls.Window
{
	public OneAliasEditorDlg()
		=>InitializeComponent();

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirm = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelCreatingNewAliasTitle, Rsrcs
		.strCancelCreatingNewAliasMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
		.Controls.WindowStartupLocation.CenterOwner);

	public enum Modes
	{
		create,
		edit,
	}

	private Modes? mode;

	private Data.Prefs.GlobalAliasesOneAliasEditable? aliasCtxt;

	public Modes? Mode
	{
		get
			=> mode;

		set
		{
			if(mode != value)
			{
				mode = value;

				UpdateTitle();
			}
		}
	}

	public Data.Prefs.GlobalAliasesOneAliasEditable? CtxtAlias
	{
		get => aliasCtxt;

		set
		{
			if(aliasCtxt != value)
			{
				DataContext = aliasCtxt = value;

				UpdateTitle();
			}
		}
	}

	private void UpdateTitle()
		=> Title = mode switch
		{
			Modes.create
				=> Rsrcs.strOneAliasEditorAddingTitle,

			Modes.edit
				=> Rsrcs.strOneAliasEditorEditingTitle,

			_
				=> null,
		};

	protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs args)
	{
		if(msgboxCancelConfirm.ShowWindowDialogAsync(this.GetWndParent()).Result != MsBox.Avalonia.Enums.ButtonResult
				.Yes)
			args.Cancel = true;

		base.OnClosing(args);
	}

	private void OnOkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(true);

	private void OnCancelClicked(object? ojbSender, Avalonia.Interactivity.RoutedEventArgs arg)
	{
		if(msgboxCancelConfirm.ShowWindowDialogAsync(this.GetWndParent()).Result == MsBox.Avalonia.Enums.ButtonResult
			.Yes)
			Close(true);
	}

	private void OnCloseClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(null);
}