using System.Linq;
using Avalonia.VisualTree;

namespace BestChat.IRC.ProtocolMgr.Prefs.Pages;

public partial class NetNotifyWhenOnlinePage : Platform.UI.Desktop.Prefs.AbstractVisualPrefsTabCtrl
{
	public NetNotifyWhenOnlinePage()
		=> InitializeComponent();


	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxDelConfirm = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(
			Rsrcs.strDelSelectedAliasesTitle, Rsrcs
				.strDelSelectedAliasesMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
				.Controls.WindowStartupLocation.CenterOwner);

	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxResetConfirm = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(
			Rsrcs.strResetGlobalAliasesTitle, Rsrcs
				.strResetGlobalAliasesMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
				.Controls.WindowStartupLocation.CenterOwner);


	private Data.Prefs.NetNotifyWhenOnlinePrefs? ctxt;


	public Data.Prefs.NetNotifyWhenOnlinePrefs? Ctxt
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
		if(ctxt is null)
			throw new System.InvalidOperationException("Set Ctxt before showing the NetNotifyWhenOnlinePage");

		base.OnInitialized();
	}


	private void OnAddCLicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		Editors.OneNotifyDlg dlg = new()
		{
			CtxtNotify = new Data.Prefs.NotifyWhenOnlineOneNotify("", ctxt).MakeEditable(),
			Mode = Editors.OneNotifyDlg.Modes.add,
		};

		if(dlg.ShowDialog<bool?>(this.GetVisualRoot() as Avalonia.Controls.Window ?? throw new System
				.InvalidProgramException(@"How aren't we inside a child?")).Result == true)
		{
			dlg.CtxtNotify.Save();

			ctxt.Entries.Add(dlg.CtxtNotify.notifyOriginal);
		}
	}

	private void OnEditClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		Editors.OneNotifyDlg dlg = new()
		{
			CtxtNotify = (lbData.SelectedItem as Data.Prefs.NotifyWhenOnlineOneNotify ?? throw new System
				.InvalidProgramException("How did a null entry get in the list box?")).MakeEditable(),
			Mode = Editors.OneNotifyDlg.Modes.add,
		};

		if(dlg.ShowDialog<bool?>(this.GetVisualRoot() as Avalonia.Controls.Window ?? throw new System
				.InvalidProgramException(@"How aren't we inside a child?")).Result == true)
		{
			dlg.CtxtNotify.Save();

			ctxt.Entries.Add(dlg.CtxtNotify.notifyOriginal);
		}
	}

	private void OnDelClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is not null && lbData.SelectedItems is not null && lbData.SelectedItems.Count > 0 && msgboxDelConfirm
				.ShowWindowDialogAsync(this.GetVisualRoot() as Avalonia.Controls.Window ?? throw new System
				.InvalidProgramException(@"How did we end up in something other than a control?")).Result == MsBox.Avalonia
				.Enums.ButtonResult.Yes)
			foreach(Data.Prefs.NotifyWhenOnlineOneNotify notifyDelThis in lbData.SelectedItems.Cast<Data.Prefs
					.NotifyWhenOnlineOneNotify>())
				ctxt.Entries.Remove(notifyDelThis);
	}

	private void OnResetListClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(msgboxResetConfirm.ShowWindowDialogAsync(this.GetVisualRoot() as Avalonia.Controls.Window).Result == MsBox
			.Avalonia.Enums.ButtonResult.Yes)
			ctxt?.Entries.ResetValToDef();
	}
}