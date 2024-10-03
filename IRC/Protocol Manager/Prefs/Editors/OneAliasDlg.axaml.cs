using System.Linq;

namespace BestChat.IRC.ProtocolMgr.Prefs.Editors;

public partial class OneAliasDlg : Avalonia.Controls.Window
{
	public OneAliasDlg()
		=>InitializeComponent();

	private MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxCancelConfirm = MsBox.Avalonia
		.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strCancelCreatingNewAliasTitle, Rsrcs
		.strCancelCreatingNewAliasMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
		.Controls.WindowStartupLocation.CenterOwner);

	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxDelPositionalParamConfirm
		= MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strDelSelectedAliasesTitle, Rsrcs
			.strDelSelectedAliasesMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
			.Controls.WindowStartupLocation.CenterOwner);

	public enum Modes
	{
		create,
		edit,
	}

	private Modes? mode;

	private Data.Prefs.GlobalAliasesOneAliasEditable? ealiasCtxt;

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
		get => ealiasCtxt;

		set
		{
			if(ealiasCtxt != value)
			{
				DataContext = ealiasCtxt = value;

				UpdateTitle();
			}
		}
	}

	public bool CanMoveSelectedPositionedParamUp
	{
		get
		{
			if(ealiasCtxt is null)
				throw new System.InvalidOperationException("Somehow we opened a window without a valid context");

			if(dgPositionalParams.SelectedItem is not Data.Prefs.GlobalAliasesOneAliasOneParam aparamCurSelPositioned)
				return false;

			System.Collections.Generic.LinkedListNode<Data.Prefs.GlobalAliasesOneAliasOneParam> llnparamCurSelPositioned =
				ealiasCtxt.PositionalParameters.Find(aparamCurSelPositioned)
				?? throw new System.InvalidOperationException("Can't find the positional parameter in the list");

			return llnparamCurSelPositioned.Previous is not null && (!aparamCurSelPositioned.IsRequired ||
				llnparamCurSelPositioned.Previous.Value.IsRequired);
		}
	}

	public bool CanMoveSelectedPositionedParamDown
	{
		get
		{
			if(ealiasCtxt is null)
				throw new System.InvalidOperationException("Somehow we opened a window without a valid context");

			if(dgPositionalParams.SelectedItem is not Data.Prefs.GlobalAliasesOneAliasOneParam aparamCurSelPositioned)
				return false;

			System.Collections.Generic.LinkedListNode<Data.Prefs.GlobalAliasesOneAliasOneParam> llnparamCurSelPositioned =
				ealiasCtxt.PositionalParameters.Find(aparamCurSelPositioned)
				?? throw new System.InvalidOperationException("Can't find the positional parameter in the list");

			return llnparamCurSelPositioned.Next is not null && (aparamCurSelPositioned.IsRequired ||
				!llnparamCurSelPositioned.Next.Value.IsRequired);
		}
	}

	private void UpdateTitle()
		=> Title = mode switch
		{
			Modes.create
				=> Rsrcs.strOneAliasEditorCreatingTitle,

			Modes.edit
				=> Rsrcs.strOneAliasEditorEditingTitle,

			_
				=> null,
		};

	protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs args)
	{
		if(ealiasCtxt is not null && ealiasCtxt.WereChangesMade && msgboxCancelConfirm.ShowWindowDialogAsync((Avalonia
				.Controls.Window)(VisualRoot ?? throw new System.InvalidProgramException("How is this in a non-window?")))
				.Result != MsBox.Avalonia.Enums.ButtonResult.Yes)
			args.Cancel = true;

		base.OnClosing(args);
	}

	private void OnRemoveFromPositionalListClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ealiasCtxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property before showing a new GlobalAliasesPage");

		if(dgPositionalParams.SelectedItems.Count == 0)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		if(msgboxDelPositionalParamConfirm.ShowWindowDialogAsync((Avalonia.Controls.Window?)VisualRoot ?? throw new System
				.InvalidProgramException("Some how the visual root for this control isn't a window")).Result == MsBox.Avalonia
				.Enums.ButtonResult.Yes)
			foreach(Data.Prefs.GlobalAliasesOneAliasOneParam aparamCur in dgPositionalParams.SelectedItems)
				ealiasCtxt.RemovePositionedParameter(aparamCur);
	}

	private void OnMovePositionalParamToTopClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ealiasCtxt is null)
			throw new System.InvalidOperationException("Set the ealiasCtxt property first");

		if(dgPositionalParams.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ealiasCtxt.MovePositionedParameterToTop((Data.Prefs.GlobalAliasesOneAliasOneParam)dgPositionalParams.SelectedItem);
	}

	private void OnMovePositionalParamUpClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ealiasCtxt is null)
			throw new System.InvalidOperationException("Set the ealiasCtxt property first");

		if(dgPositionalParams.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ealiasCtxt.MovePositionedParameterUp((Data.Prefs.GlobalAliasesOneAliasOneParam)dgPositionalParams.SelectedItem);
	}

	private void OnMovePositionalParamDownClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ealiasCtxt is null)
			throw new System.InvalidOperationException("Set the ealiasCtxt property first");

		if(dgPositionalParams.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ealiasCtxt.MovePositionedParameterDown((Data.Prefs.GlobalAliasesOneAliasOneParam)dgPositionalParams.SelectedItem);
	}

	private void OnMovePositionalParamToBottomClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ealiasCtxt is null)
			throw new System.InvalidOperationException("Set the ealiasCtxt property first");

		if(dgPositionalParams.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		ealiasCtxt.MovePositonedParameterToBottom((Data.Prefs.GlobalAliasesOneAliasOneParam)dgPositionalParams
			.SelectedItem);
	}

	private void OnAddNamedParamClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ealiasCtxt is null)
			throw new System.InvalidProgramException(@"Some how this window was opened without a context.");

		OneAliasOneParamDlg dlg = new()
		{
			CtxtParam = new Data.Prefs.GlobalAliasesOneAliasOneParam(ealiasCtxt).MakeEditable(),
			Mode = OneAliasOneParamDlg.Modes.create,
		};

		if(dlg.ShowDialog<bool?>(this).Result == true)
			ealiasCtxt.AddNamedParameter(dlg.CtxtParam.aparamOriginal);
	}

	private void OnEditNamedParamClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ealiasCtxt is null)
			throw new System.InvalidProgramException(@"Some how this window was opened without a context.");

		Data.Prefs.GlobalAliasesOneAliasOneParam aparamToEdit = dgAllParams.SelectedItem as Data.Prefs
			.GlobalAliasesOneAliasOneParam ?? throw new System.InvalidProgramException(@"Somehow the datagrid has a row that "
				+ @"isn't an alias parameter.");

		OneAliasOneParamDlg dlg = new()
		{
			CtxtParam = aparamToEdit.MakeEditable(),
			Mode = OneAliasOneParamDlg.Modes.create,
		};

		if(dlg.ShowDialog<bool?>(this).Result == true)
			dlg.CtxtParam.Save();
	}

	private void OnDelNamedParamClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ealiasCtxt is null)
			throw new System.InvalidProgramException(@"Some how this window was opened without a context.");

		if(dgAllParams.SelectedItem is null || dgAllParams.SelectedItems.Count == 0)
			return;

		foreach(Data.Prefs.GlobalAliasesOneAliasOneParam aparamCurToBeDel in dgAllParams.SelectedItems
				.Cast<Data.Prefs.GlobalAliasesOneAliasOneParam>())
			ealiasCtxt.RemoveNamedParameter(aparamCurToBeDel);
	}

	private void OnOkClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(true);

	private void OnCancelClicked(object? ojbSender, Avalonia.Interactivity.RoutedEventArgs arg)
	{
		if(msgboxCancelConfirm.ShowWindowDialogAsync((Avalonia.Controls.Window)(VisualRoot ?? throw new System
			.InvalidProgramException("How is this in a non-window?"))).Result == MsBox.Avalonia.Enums.ButtonResult.Yes)
			Close(true);
	}

	private void OnCloseClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> Close(null);

	private void OnExportClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ealiasCtxt == null)
			throw new System.InvalidOperationException("How did we get to exporting without a context.");

		System.Collections.Generic.IEnumerable<Avalonia.Platform.Storage.IStorageFile> files = StorageProvider
			.OpenFilePickerAsync(new()
				{
					AllowMultiple = false,
					FileTypeFilter =
					[
						new($@"{Rsrcs.strFileTypeFilterNameAllAliasFiles} (*{Data.Prefs.GlobalAliasesOneAlias
							.strOneAliasFileExt})")
						{
							Patterns =
							[
								$@"*{Data.Prefs.GlobalAliasesOneAlias.strOneAliasFileExt}",
							],
						},
						new($@"{Rsrcs.strFileTypeFilterNameAllJsonFiles} (*{Platform.DataAndExt.ObjBase
							.strAllJsonFileTypeExt})")
						{
							Patterns =
							[
								$@"*{Platform.DataAndExt.ObjBase.strAllJsonFileTypeExt}",
							],
						},
						new($@"{Rsrcs.strFileTypeFilterNameAllFiles}	(*{Platform.DataAndExt.ObjBase.strAllFileTypesExt})")
						{
							Patterns =
							[
								$@"*{Platform.DataAndExt.ObjBase.strAllFileTypesExt}",
							],
						},
					],
					SuggestedFileName = $@"{ealiasCtxt.Name}.{Data.Prefs.GlobalAliasesOneAlias.strOneAliasFileExt}",
					SuggestedStartLocation = StorageProvider.TryGetFolderFromPathAsync(
							new(
								System.IO.Directory
									.GetCurrentDirectory()))
						.Result,
					Title = Rsrcs.strExportAliasDlgTitle,
				})
			.Result;

		foreach(Avalonia.Platform.Storage.IStorageFile fileCur in files)
		{
			using System.IO.StreamWriter sw = new(fileCur.Path.AbsolutePath);

			sw.Write(ealiasCtxt.ExportAsString());
		}
	}

	private void OnCopyToClipboardClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ealiasCtxt == null)
			throw new System.InvalidOperationException("How did we get to exporting without a context.");

		Clipboard?.SetTextAsync(ealiasCtxt.ExportAsString());
	}
}