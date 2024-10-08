using System.Linq;
using Avalonia.VisualTree;

namespace BestChat.IRC.ProtocolMgr.Prefs.Pages;

public partial class GlobalDccPage : Platform.UI.Desktop.Prefs.AbstractVisualPrefsTabCtrl
{
	public GlobalDccPage()
		=> InitializeComponent();

	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxDelPortConfirm = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strDelPortConfirmTitle, Rsrcs.strDelPortConfirmMsg,
		MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation
		.CenterOwner);

	private Data.Prefs.GlobalDccPrefs? ctxt;

	public Data.Prefs.GlobalDccPrefs? Ctxt
	{
		get => ctxt;

		set
		{
			if(ctxt != value)
				DataContext = ctxt = value;
		}
	}

	protected override void OnInitialized()
	{
		if(ctxt == null)
			throw new System.InvalidOperationException("Set Ctxt before showing the global DCC page");

		base.OnInitialized();
	}

	private void OnResetEnabledClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Enabled.ResetValToDef();

	private void OnResetGetIpFromServerClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.GetIpFromServer.ResetValToDef();

	private void OnResetDownloadsFolderClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.DownloadsFolder.ResetValToDef();

	private void OnResetPorts(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Ports.ResetValToDef();

	private void OnAddPortClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalDccPage");

		Avalonia.Controls.Window wnd = this.GetVisualRoot() as Avalonia.Controls.Window ?? throw new System
			.InvalidProgramException("How do we have a control that isn't in a window?");

		Editors.PortEditorDlg dlg = new()
		{
			Mode = Editors.PortEditorDlg.Modes.@new,
			PortsAlreadyListed = ctxt.Ports.ToHashSet(),
			Title = Rsrcs.strAddingNewPortDlgTitle,
		};

		if(dlg.ShowDialog<bool?>(wnd).Result == true && dlg.IsDataValid)
		{
			int iChosenPort = dlg.SelPort ?? throw new System.InvalidProgramException(@"How did the dialog return a null " +
				@"port?");

			ctxt.Ports.Add(iChosenPort);
		}
	}

	private void OnEditPortClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalDccPage");

		Avalonia.Controls.Window wnd = this.GetVisualRoot() as Avalonia.Controls.Window ?? throw new System
			.InvalidProgramException("How do we have a control that isn't in a window?");

		int iPortToChange = (int?)lbAllowedPorts.SelectedItem ?? throw new System.InvalidProgramException(@"How did a null"
			+ "port get into the list?");

		Editors.PortEditorDlg dlg = new()
		{
			Mode = Editors.PortEditorDlg.Modes.edit,
			PortsAlreadyListed = ctxt.Ports.ToHashSet() ?? [],
			SelPort = iPortToChange,
			Title = Rsrcs.strChangingPortDlgTitle,
		};

		if(dlg.ShowDialog<bool?>(wnd).Result == true && dlg.IsDataValid)
		{
			int iChosenPort = dlg.SelPort ?? throw new System.InvalidProgramException(@"How did the dialog return a null " +
				@"port?");

			ctxt.Ports.Remove(iPortToChange);
			ctxt.Ports.Add(iChosenPort);
		}
	}

	private void OnDelPortsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalDccPage");

		Avalonia.Controls.Window wnd = this.GetVisualRoot() as Avalonia.Controls.Window ?? throw new System
			.InvalidProgramException("How do we have a control that isn't in a window?");

		if(lbAllowedPorts.SelectedItems is not null && lbAllowedPorts.SelectedItems.Count > 0 && msgboxDelPortConfirm
				.ShowWindowDialogAsync(wnd).Result == MsBox.Avalonia.Enums.ButtonResult.Yes)
			foreach(int iCurPortToDel in lbAllowedPorts.SelectedItems.Cast<int>())
				ctxt.Ports.Remove(iCurPortToDel);
	}

	private void OnMovePortUpClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalDccPage");

		int iPortToMove = (int?)lbAllowedPorts.SelectedItem ?? throw new System.InvalidProgramException(@"How did a null"
			+ "port get into the list?");

		ctxt.Ports.MoveEntryUp(iPortToMove);
	}

	private void OnMovePortDownClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalDccPage");

		int iPortToMove = (int?)lbAllowedPorts.SelectedItem ?? throw new System.InvalidProgramException(@"How did a null"
			+ "port get into the list?");

		ctxt.Ports.MoveEntryDown(iPortToMove);
	}
}