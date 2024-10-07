namespace BestChat.IRC.ProtocolMgr.Prefs.Pages;

public partial class NetAutoPerformOnEvtPage : Platform.UI.Desktop.Prefs.VisualPrefsTabCtrl
{
	public NetAutoPerformOnEvtPage()
		=> InitializeComponent();

	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxDelConfirm = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strDelSelectedAliasesTitle, Rsrcs
		.strDelSelectedAliasesMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
		.Controls.WindowStartupLocation.CenterOwner);

	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxResetConfirm = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strResetGlobalAliasesTitle, Rsrcs
		.strResetGlobalAliasesMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
		.Controls.WindowStartupLocation.CenterOwner);


	private Data.Prefs.NetAutoPerformOnEvtPrefs? ctxt;

	public Data.Prefs.NetAutoPerformOnEvtPrefs? Ctxt
	{
		get => ctxt;

		set
		{
			if(ctxt != value)
			{
				if(value is not null && !value.IsEditMode)
					throw new System.InvalidOperationException(@"Before you can open a new NetAutoPerformOnEvtPage, you must turn"
						+ @" on the context's edit mode.");

				DataContext = ctxt = value;
			}
		}
	}


	protected override void OnInitialized()
	{
		if(ctxt is null)
			throw new System.InvalidOperationException(@"Set Ctxt before creating a new NetAutoPerformOnEvtPage.");

		base.OnInitialized();
	}


	private void OnResetInheritedClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.AllInheritanceOverrides.ResetValToDef();

	private void OnDelCLicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property before showing a new NetAutoPerformOnEvtPage");

		if(lbData.SelectedItems is null || lbData.SelectedItems.Count == 0)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		if(msgboxDelConfirm.ShowWindowDialogAsync((Avalonia.Controls.Window?)VisualRoot ?? throw new System
				.InvalidProgramException("Some how the visual root for this control isn't a window")).Result == MsBox.Avalonia
				.Enums.ButtonResult.Yes)
			foreach(Data.Prefs.GlobalAutoPerformOneStepEditable enickCur in lbData.SelectedItems)
				ctxt.Steps.Remove(enickCur);
	}

	private void OnResetAllStepsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property before showing a new NetAutoPerformOnEvtPage");

		if(msgboxResetConfirm.ShowWindowDialogAsync((Avalonia.Controls.Window?)VisualRoot ?? throw new System
				.InvalidProgramException("Some how the visual root for this control isn't a window")).Result == MsBox.Avalonia
				.Enums.ButtonResult.Yes)
			ctxt.Steps.ResetValToDef();
	}

	private void OnPrependNewClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set this Ctxt property before showing a new NetAutoPerformOnEvtPage");

		Editors.AutoPerformOneStepDlg dlg = new()
		{
			Mode = Editors.AutoPerformOneStepDlg.Modes.create,
			CtxtStep = new Data.Prefs.GlobalAutoPerformOneStep(ctxt).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
			"how the visual root for this control isn't a window")).Result == true)
		{
			dlg.CtxtStep.Save();

			ctxt.Steps.Prepend(dlg.CtxtStep.OriginalStep);
		}
	}

	private void OnAppendClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set this Ctxt property before showing a new NetAutoPerformOnEvtPage");

		Editors.AutoPerformOneStepDlg dlg = new()
		{
			Mode = Editors.AutoPerformOneStepDlg.Modes.create,
			CtxtStep = new Data.Prefs.GlobalAutoPerformOneStep(ctxt).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
			"how the visual root for this control isn't a window")).Result == true)
		{
			dlg.CtxtStep.Save();

			ctxt.Steps.Append(dlg.CtxtStep.OriginalStep);
		}
	}

	private void OnAddBeforeClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set this Ctxt property before showing a new NetAutoPerformOnEvtPage");

		if(lbData.SelectedItem is null || lbData.SelectedItems is null || lbData.SelectedItems.Count != 1)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		Editors.AutoPerformOneStepDlg dlg = new()
		{
			Mode = Editors.AutoPerformOneStepDlg.Modes.create,
			CtxtStep = new Data.Prefs.GlobalAutoPerformOneStep(ctxt).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
			"how the visual root for this control isn't a window")).Result == true)
		{
			dlg.CtxtStep.Save();

			ctxt.Steps.AddBefore((Data.Prefs.GlobalAutoPerformOneStepEditable)lbData.SelectedItem, dlg.CtxtStep.OriginalStep);
		}
	}

	private void OnAddAfterClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set this Ctxt property before showing a new NetAutoPerformOnEvtPage");

		if(lbData.SelectedItem is null || lbData.SelectedItems is null || lbData.SelectedItems.Count != 1)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		Editors.AutoPerformOneStepDlg dlg = new()
		{
			Mode = Editors.AutoPerformOneStepDlg.Modes.create,
			CtxtStep = new Data.Prefs.GlobalAutoPerformOneStep(ctxt).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
			"how the visual root for this control isn't a window")).Result == true)
		{
			dlg.CtxtStep.Save();

			ctxt.Steps.AddAfter((Data.Prefs.GlobalAutoPerformOneStepEditable)lbData.SelectedItem, dlg.CtxtStep.OriginalStep);
		}
	}

	private void OnEditClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(lbData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		Editors.AutoPerformOneStepDlg dlg = new()
		{
			Mode = Editors.AutoPerformOneStepDlg.Modes.create,
			CtxtStep = ((Data.Prefs.GlobalAutoPerformOneStep)lbData.SelectedItem).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
				"how the visual root for this control isn't a window")).Result == true)
			dlg.CtxtStep.Save();
	}

	private void OnMoveToTopClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(lbData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ctxt.Steps.MoveEntryToTop((Data.Prefs.GlobalAutoPerformOneStepEditable)lbData.SelectedItem);
	}

	private void OnMoveUpClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(lbData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ctxt.Steps.MoveEntryUp((Data.Prefs.GlobalAutoPerformOneStepEditable)lbData.SelectedItem);
	}

	private void OnMoveDownClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(lbData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ctxt.Steps.MoveEntryDown((Data.Prefs.GlobalAutoPerformOneStepEditable)lbData.SelectedItem);
	}

	private void OnMoveToBottomClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(lbData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ctxt.Steps.MoveEntryToBottom((Data.Prefs.GlobalAutoPerformOneStepEditable)lbData.SelectedItem);
	}
}