namespace BestChat.IRC.ProtocolMgr.Prefs.Pages;

public partial class GlobalAltNicksPage : Platform.UI.Desktop.Prefs.AbstractVisualPrefsTabCtrl
{
	public GlobalAltNicksPage()
		=> InitializeComponent();

	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxDelConfirm = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strDelSelectedAliasesTitle, Rsrcs
		.strDelSelectedAliasesMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
		.Controls.WindowStartupLocation.CenterOwner);

	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxResetConfirm = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strResetGlobalAliasesTitle, Rsrcs
		.strResetGlobalAliasesMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
		.Controls.WindowStartupLocation.CenterOwner);

	private Data.Prefs.GlobalAltNicksPrefs? ctxt;

	public Data.Prefs.GlobalAltNicksPrefs? Ctxt
	{
		get => ctxt;

		set
		{
			if(ctxt != value)
			{
				if(value is not null && !value.IsEditMode)
					throw new System.InvalidOperationException("Before you can open a new GlobalAltNicksPage, you must turn on " +
						"the context's edit mode.");

				DataContext = ctxt = value;
			}
		}
	}

	private void OnDelCLicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property before showing a new GlobalAliasesPage");

		if(lbData.SelectedItems is null || lbData.SelectedItems.Count == 0)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		if(msgboxDelConfirm.ShowWindowDialogAsync((Avalonia.Controls.Window?)VisualRoot ?? throw new System
				.InvalidProgramException("Some how the visual root for this control isn't a window")).Result == MsBox.Avalonia
				.Enums.ButtonResult.Yes)
			foreach(Data.Prefs.GlobalAltNicksOneAltNick enickCur in lbData.SelectedItems)
				ctxt.Entries.Remove(enickCur);
	}

	private void OnResetAllAltNickClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property before showing a new GlobalAliasesPage");

		if(msgboxResetConfirm.ShowWindowDialogAsync((Avalonia.Controls.Window?)VisualRoot ?? throw new System
				.InvalidProgramException("Some how the visual root for this control isn't a window")).Result == MsBox.Avalonia
				.Enums.ButtonResult.Yes)
			ctxt.Entries.ResetValToDef();
	}

	private void OnPrependNewClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set this Ctxt property before showing a new GlobalAliasesPage");

		Editors.GlobalOneAltNickDlg dlg = new()
		{
			Mode = Editors.GlobalOneAltNickDlg.Modes.create,
			CtxtAltNick = new Data.Prefs.GlobalAltNicksOneAltNick(ctxt).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
			"how the visual root for this control isn't a window")).Result == true)
		{
			dlg.CtxtAltNick.Save();

			ctxt.Entries.Prepend(dlg.CtxtAltNick.OriginalNick);
		}
	}

	private void OnAppendClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set this Ctxt property before showing a new GlobalAliasesPage");

		Editors.GlobalOneAltNickDlg dlg = new()
		{
			Mode = Editors.GlobalOneAltNickDlg.Modes.create,
			CtxtAltNick = new Data.Prefs.GlobalAltNicksOneAltNick(ctxt).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
			"how the visual root for this control isn't a window")).Result == true)
		{
			dlg.CtxtAltNick.Save();

			ctxt.Entries.Append(dlg.CtxtAltNick.OriginalNick);
		}
	}

	private void OnAddBeforeClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set this Ctxt property before showing a new GlobalAliasesPage");

		if(lbData.SelectedItem is null || lbData.SelectedItems is null || lbData.SelectedItems.Count != 1)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		Editors.GlobalOneAltNickDlg dlg = new()
		{
			Mode = Editors.GlobalOneAltNickDlg.Modes.create,
			CtxtAltNick = new Data.Prefs.GlobalAltNicksOneAltNick(ctxt).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
			"how the visual root for this control isn't a window")).Result == true)
		{
			dlg.CtxtAltNick.Save();

			ctxt.Entries.AddBefore((Data.Prefs.GlobalAltNicksOneAltNick)lbData.SelectedItem, dlg.CtxtAltNick.OriginalNick);
		}
	}

	private void OnAddAfterClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set this Ctxt property before showing a new GlobalAliasesPage");

		if(lbData.SelectedItem is null || lbData.SelectedItems is null || lbData.SelectedItems.Count != 1)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		Editors.GlobalOneAltNickDlg dlg = new()
		{
			Mode = Editors.GlobalOneAltNickDlg.Modes.create,
			CtxtAltNick = new Data.Prefs.GlobalAltNicksOneAltNick(ctxt).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
			"how the visual root for this control isn't a window")).Result == true)
		{
			dlg.CtxtAltNick.Save();

			ctxt.Entries.AddAfter((Data.Prefs.GlobalAltNicksOneAltNick)lbData.SelectedItem, dlg.CtxtAltNick.OriginalNick);
		}
	}

	private void OnEditClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(lbData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		Editors.GlobalOneAltNickDlg dlg = new()
		{
			Mode = Editors.GlobalOneAltNickDlg.Modes.create,
			CtxtAltNick = ((Data.Prefs.GlobalAltNicksOneAltNick)lbData.SelectedItem).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
				"how the visual root for this control isn't a window")).Result == true)
			dlg.CtxtAltNick.Save();
	}

	private void OnMoveToTopClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(lbData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ctxt.Entries.MoveEntryToTop((Data.Prefs.GlobalAltNicksOneAltNick)lbData.SelectedItem);
	}

	private void OnMoveUpClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(lbData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ctxt.Entries.MoveEntryUp((Data.Prefs.GlobalAltNicksOneAltNick)lbData.SelectedItem);
	}

	private void OnMoveDownClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(lbData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ctxt.Entries.MoveEntryDown((Data.Prefs.GlobalAltNicksOneAltNick)lbData.SelectedItem);
	}

	private void OnMoveToBottomClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(lbData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ctxt.Entries.MoveEntryToBottom((Data.Prefs.GlobalAltNicksOneAltNick)lbData.SelectedItem);
	}
}