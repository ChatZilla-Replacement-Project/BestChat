using System.Linq;
using Avalonia.VisualTree;

namespace BestChat.IRC.ProtocolMgr.Prefs.Pages;

public partial class GlobalStalkWordsPage : Platform.UI.Desktop.Prefs.VisualPrefsTabCtrl
{
	public GlobalStalkWordsPage()
		=> InitializeComponent();

	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxDelPortConfirm = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strDelStalkWordTitle, Rsrcs.strDelStalkWordMsg, MsBox
		.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia.Controls.WindowStartupLocation
		.CenterOwner);

	private Data.Prefs.GlobalStalkWordsPrefs? ctxt;

	public Data.Prefs.GlobalStalkWordsPrefs? Ctxt
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
			throw new System.InvalidOperationException("Set Ctxt before showing the global stalk words page");

		base.OnInitialized();
	}

	private void OnVisitGlobalAppearancePrefsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		// TODO: Show the correct page
		throw new System.NotImplementedException();
	}

	private void OnAddClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt == null)
			throw new System.InvalidOperationException("Set Ctxt before showing the global stalk words page");

		Avalonia.Controls.Window wnd = this.GetVisualRoot() as Avalonia.Controls.Window ?? throw new System
			.InvalidProgramException("How do we have a control that isn't in a window?");

		Editors.StalkWordEditorDlg dlg = new()
		{
			CtxtStalkWord = new Data.Prefs.GlobalStalkWordsOneStalkWord("", ctxt).MakeEditable(),
			Mode = Editors.StalkWordEditorDlg.Modes.create,
		};

		if(dlg.ShowDialog<bool?>(wnd).Result == true)
		{
			dlg.CtxtStalkWord.Save();

			ctxt.Entries.Add(dlg.CtxtStalkWord.swOriginal);
		}
	}

	private void OnEditClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt == null)
			throw new System.InvalidOperationException("Set Ctxt before showing the global stalk words page");

		Avalonia.Controls.Window wnd = this.GetVisualRoot() as Avalonia.Controls.Window ?? throw new System
			.InvalidProgramException("How do we have a control that isn't in a window?");

		Data.Prefs.GlobalStalkWordsOneStalkWord swEditThis = lbData.SelectedItem as Data.Prefs.GlobalStalkWordsOneStalkWord
			?? throw new System.InvalidProgramException("How did we get a clicked message without a selection?");

		Editors.StalkWordEditorDlg dlg = new()
		{
			CtxtStalkWord = swEditThis.MakeEditable(),
			Mode = Editors.StalkWordEditorDlg.Modes.edit,
		};

		if(dlg.ShowDialog<bool?>(wnd).Result == true)
			dlg.CtxtStalkWord.Save();
	}

	private void OnDelCLicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt == null)
			throw new System.InvalidOperationException("Set Ctxt before showing the global stalk words page");

		Avalonia.Controls.Window wnd = this.GetVisualRoot() as Avalonia.Controls.Window ?? throw new System
			.InvalidProgramException("How do we have a control that isn't in a window?");

		if(lbData.SelectedItems is not null && lbData.SelectedItems.Count > 0 && msgboxDelPortConfirm
				.ShowWindowDialogAsync(wnd).Result == MsBox.Avalonia.Enums.ButtonResult.Yes)
			foreach(Data.Prefs.GlobalStalkWordsOneStalkWord swCur in lbData.SelectedItems.Cast<Data.Prefs
					.GlobalStalkWordsOneStalkWord>())
				ctxt.Entries.Remove(swCur);
	}

	private void OnResetListClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Entries.ResetValToDef();
}