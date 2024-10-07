using System.Linq;
using Avalonia.Input;
using Avalonia.VisualTree;

namespace BestChat.IRC.ProtocolMgr.Prefs.Pages;

using Platform.DataAndExt.Ext;

public partial class NetAliasesPage : Platform.UI.Desktop.Prefs.VisualPrefsTabCtrl
{
	public NetAliasesPage()
		=> InitializeComponent();

	private static readonly Avalonia.Point ptMinDragDistance = new(3, 3);
	private const string strJsonMimeType = "application/json";

	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxDelConfirm = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strDelSelectedAliasesTitle, Rsrcs
		.strDelSelectedAliasesMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
		.Controls.WindowStartupLocation.CenterOwner);

	private static readonly MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msgboxResetConfirm = MsBox
		.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strResetGlobalAliasesTitle, Rsrcs
		.strResetGlobalAliasesMsg, MsBox.Avalonia.Enums.ButtonEnum.YesNo, MsBox.Avalonia.Enums.Icon.Question, Avalonia
		.Controls.WindowStartupLocation.CenterOwner);

	private Data.Prefs.NetAliasesPrefs? ctxt;

	private Avalonia.Point? ptDragStartedAt = null;

	public Data.Prefs.NetAliasesPrefs? Ctxt
	{
		get => ctxt;

		set
		{
			if(ctxt != value)
			{
				if(value is not null && !value.IsEditMode)
					throw new System.InvalidOperationException("Before you can open a new GlobalAliasesPage, you must turn on the" +
						" context's edit mode.");

				DataContext = ctxt = value;
			}
		}
	}

	protected override void OnInitialized()
	{
		base.OnInitialized();

		dgData.AddHandler(DragDrop.DragOverEvent, OnDragOver);
		dgData.AddHandler(DragDrop.DropEvent, OnFilesDropped);
	}

	#pragma warning disable CA1859
		private System.Collections.Generic.IReadOnlyCollection<string>? Import(in string strImportFrom)
		{
			if(ctxt is null)
				throw new System.InvalidProgramException("How did we open this page without a context in it?");

			System.Collections.Generic.IEnumerable<Data.Prefs.GlobalAliasesOneAlias>? eNewAliases =
				Data.Prefs.GlobalAliasesOneAlias.IsStringCtntsOneAlias(strImportFrom)
					? [Data.Prefs.GlobalAliasesOneAlias.ImportOneAliasFromString(strImportFrom)]
					: Data.Prefs.GlobalAliasesOneAlias.IsStringCtntsMultipleAliases(strImportFrom)
						? Data.Prefs.GlobalAliasesOneAlias.ImportManyAliasesFromString(strImportFrom)
						: null;

			if(eNewAliases is null)
				return [Rsrcs.strNoAliasesFoundDuringImport,];

			System.Collections.Generic.LinkedList<string> llstrErrors = [];
			foreach(Data.Prefs.GlobalAliasesOneAlias aliasCurToBeImported in eNewAliases)
				if(ctxt.Entries.ContainsKey(aliasCurToBeImported.Name))
					llstrErrors.AddLast($"{aliasCurToBeImported.Name} already exists.");
				else
					ctxt.Entries.Add(aliasCurToBeImported);

			return llstrErrors.Count == 0 ? null : llstrErrors;
		}
	#pragma warning restore CA1859

	private System.Threading.Tasks.Task? Import(System.Collections.Generic.IEnumerable<System.IO.FileInfo> efilesToImportFrom)
	{
		if(ctxt is null)
			throw new System.InvalidProgramException("How did we manage to open this page without a context?");

		Avalonia.Controls.Window wnd = (Avalonia.Controls.Window)(this.GetVisualRoot() ??
			throw new System.InvalidProgramException("How did this page open without a window?"));

		int iExistingAliasCnt = ctxt.Entries.Count;

		System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>>
			mapErrorsGroupedByFile = [];
		foreach(System.IO.FileInfo fileCur in efilesToImportFrom)
		{
			using System.IO.StreamReader sr = new(fileCur.FullName);

			System.Collections.Generic.IEnumerable<string>? eErrorsForCurFIle = Import(sr.ReadToEnd());

			if(eErrorsForCurFIle is not null)
				mapErrorsGroupedByFile[fileCur.FullName] = eErrorsForCurFIle;
		}

		if(mapErrorsGroupedByFile.Count > 0)
		{
			AliasImportFailureDlg dlg = new()
			{
				AliasesSuccessfullyImported = ctxt.Entries.Count - iExistingAliasCnt,
				Errors = mapErrorsGroupedByFile,
			};

			return dlg.ShowDialog(wnd);
		}

		return MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strAliasesImportedSuccessfullyTitle, Rsrcs
			.strAliasesImportedSuccessfullyMsgFmt.Fmt(ctxt.Entries.Count - iExistingAliasCnt), MsBox.Avalonia.Enums.ButtonEnum
			.Ok, MsBox.Avalonia.Enums.Icon.Success).ShowWindowDialogAsync(wnd);
	}

	private void OnResetInheritedClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
		=> ctxt?.AllInheritanceOverridesByName.ResetValToDef();

	protected void OnPointerPressedOnRow(object? objSender, PointerPressedEventArgs args)
	{
		base.OnPointerPressed(args);

		if(args.ClickCount == 1 && dgData.SelectedItems.Count > 0)
			ptDragStartedAt = args.GetPosition(this);
	}

	protected void OnPointerMovedWithRow(object? objSender, PointerEventArgs args)
	{
		base.OnPointerMoved(args);

		if(ptDragStartedAt is not null && args.GetPosition(this) is var ptMouseAt && ptMouseAt.X >
			ptMinDragDistance.X && ptMouseAt.Y > ptMinDragDistance.Y)
		{
			DataObject dobj = new();
			if(dgData.SelectedItems.Count > 1)
				dobj.Set(strJsonMimeType, Data.Prefs.GlobalAliasesOneAlias.ExportManyAliasesAsString(dgData
					.SelectedItems.Cast<Data.Prefs.GlobalAliasesOneAlias>()));
			else if(dgData.SelectedItem is Data.Prefs.GlobalAliasesOneAlias aliasToExport)
				dobj.Set(strJsonMimeType, aliasToExport.ExportAsString());

			DragDrop.DoDragDrop(args, dobj, DragDropEffects.Copy);
		}
	}

	private void OnResetAllAliasesClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property before showing a new GlobalAliasesPage");

		if(msgboxResetConfirm.ShowWindowDialogAsync((Avalonia.Controls.Window?)VisualRoot ?? throw new System
			.InvalidProgramException("Some how the visual root for this control isn't a window")).Result == MsBox.Avalonia
			.Enums.ButtonResult.Yes)
			ctxt.Entries.ResetValToDef();
	}

	private void OnAddClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		Editors.OneAliasDlg dlg = new()
		{
			Mode = Editors.OneAliasDlg.Modes.create,
			CtxtAlias = new Data.Prefs.GlobalAliasesOneAlias().MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
			"how the visual root for this control isn't a window")).Result == true)
		{
			dlg.CtxtAlias.Save();

			ctxt.Entries.Add(dlg.CtxtAlias.OriginalAlias);
		}
	}

	private void OnEditClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property first");

		if(dgData.SelectedItem is null)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		Editors.OneAliasDlg dlg = new()
		{
			Mode = Editors.OneAliasDlg.Modes.create,
			CtxtAlias = ((Data.Prefs.GlobalAliasesOneAlias)dgData.SelectedItem).MakeEditable(),
		};

		if(dlg.ShowDialog<bool?>((Avalonia.Controls.Window?)VisualRoot ?? throw new System.InvalidProgramException("Some " +
				"how the visual root for this control isn't a window")).Result == true)
			dlg.CtxtAlias.Save();
	}

	private void OnDelClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set the Ctxt property before showing a new GlobalAliasesPage");

		if(dgData.SelectedItems.Count == 0)
			throw new System.InvalidProgramException("Somehow we are editing without a selection");

		if(msgboxDelConfirm.ShowWindowDialogAsync((Avalonia.Controls.Window?)VisualRoot ?? throw new System
				.InvalidProgramException("Some how the visual root for this control isn't a window")).Result == MsBox.Avalonia
				.Enums.ButtonResult.Yes)
			foreach(Data.Prefs.GlobalAliasesOneAlias ealiasCur in dgData.SelectedItems)
				ctxt.Entries.Remove(ealiasCur.Name);
	}

	private void OnExportToFileClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidProgramException("How did we manage to open this page without a context?");

		Avalonia.Controls.Window wnd = (Avalonia.Controls.Window)(this.GetVisualRoot() ??
			throw new System.InvalidProgramException("How did this page open without a window?"));

		string strRecommendedFileExt = dgData.SelectedItems.Count > 1
			? Data.Prefs.GlobalAliasesOneAlias.strManyAliasesFileExt
			: Data.Prefs.GlobalAliasesOneAlias.strOneAliasFileExt;

		string strNameOfRecommendedFileExt = dgData.SelectedItems.Count > 1
			? Rsrcs.strFileTypeFilterNameAllAliasArrayFiles
			: Rsrcs.strFileTypeFilterNameAllAliasFiles;

		Avalonia.Platform.Storage.IStorageFile? file = wnd.StorageProvider.SaveFilePickerAsync(new()
			{
				DefaultExtension = dgData.SelectedItems.Count > 1
					? Data.Prefs.GlobalAliasesOneAlias.strManyAliasesFileExt
					: Data.Prefs.GlobalAliasesOneAlias.strOneAliasFileExt,
				FileTypeChoices =
				[
					new($@"{strNameOfRecommendedFileExt} (*{strRecommendedFileExt})")
					{
						Patterns =
						[
							$@"*{strRecommendedFileExt}",
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
				ShowOverwritePrompt = true,
				SuggestedFileName = $@".{Data.Prefs.GlobalAliasesOneAlias.strOneAliasFileExt}",
				SuggestedStartLocation = wnd.StorageProvider.TryGetFolderFromPathAsync(
						new(System.IO.Directory.GetCurrentDirectory())
					).Result,
				Title = Rsrcs.strExportAliasDlgTitle,
			})
		.Result;

		if(file is not null)
		{
			using System.IO.StreamWriter sw = new(file.Path.AbsolutePath);

			if(dgData.SelectedItems.Count > 1)
				sw.Write(Data.Prefs.GlobalAliasesOneAlias.ExportManyAliasesAsString(dgData.SelectedItems.Cast<Data.Prefs
					.GlobalAliasesOneAlias>()));
			else
				sw.Write(((Data.Prefs.GlobalAliasesOneAlias)dgData.SelectedItem).ExportAsString());
		}
	}

	private void OnExportToClipboardClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidProgramException("How did we manage to open this page without a context?");

		Avalonia.Controls.Window wnd = (Avalonia.Controls.Window)(this.GetVisualRoot() ??
			throw new System.InvalidProgramException("How did this page open without a window?"));

		if(wnd.Clipboard == null)
			throw new System.InvalidProgramException(Rsrcs.strNoClipboardAvailable);

		wnd.Clipboard.SetTextAsync(dgData.SelectedItems.Count > 1
				? Data.Prefs.GlobalAliasesOneAlias.ExportManyAliasesAsString(dgData.SelectedItems.Cast<Data.Prefs
					.GlobalAliasesOneAlias>())
				: ((Data.Prefs.GlobalAliasesOneAlias)dgData.SelectedItem).ExportAsString()
		);
	}

	private void OnImportFromFileClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidProgramException("How did we manage to open this page without a context?");

		Avalonia.Controls.Window wnd = (Avalonia.Controls.Window)(this.GetVisualRoot() ??
			throw new System.InvalidProgramException("How did this page open without a window?"));

		System.Collections.Generic.IEnumerable<Avalonia.Platform.Storage.IStorageFile> files = wnd.StorageProvider
			.OpenFilePickerAsync(new()
				{
					AllowMultiple = true,
					FileTypeFilter =
						[
							new($@"{Rsrcs.strFileTypeFilterNameAllAliasArrayFiles} (*{Data.Prefs.GlobalAliasesOneAlias
								.strManyAliasesFileExt})")
							{
								Patterns =
								[
									$@"*{Data.Prefs.GlobalAliasesOneAlias.strManyAliasesFileExt}",
								],
							},
							new($@"{Rsrcs.strFileTypeFilterNameAllAliasFiles} (*{Data.Prefs.GlobalAliasesOneAlias
								.strOneAliasFileExt})")
							{
								Patterns =
								[
									$@"*{Data.Prefs.GlobalAliasesOneAlias.strOneAliasFileExt}",
								],
							},
							new(
								$@"{Rsrcs.strFileTypeFilterNameAllJsonFiles} (*{Platform.DataAndExt.ObjBase
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
					SuggestedStartLocation = wnd.StorageProvider.TryGetWellKnownFolderAsync(Avalonia.Platform.Storage
						.WellKnownFolder.Documents).Result ?? wnd.StorageProvider.TryGetFolderFromPathAsync(new(System.IO.Directory
						.GetCurrentDirectory())).Result,
					Title = Rsrcs.strImportAliasDlgTitle,
				}).Result;

		Import(files.Select(fileCur => new System.IO.FileInfo(fileCur.Path.AbsolutePath)))?.Wait();
	}

	private void OnImportFromClipboardClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
	{
		if(ctxt is null)
			throw new System.InvalidProgramException("How did we manage to open this page without a context?");

		Avalonia.Controls.Window wnd = (Avalonia.Controls.Window)(this.GetVisualRoot() ??
			throw new System.InvalidProgramException("How did this page open without a window?"));

		if(wnd.Clipboard is null)
			MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strAliasImportFromClipboardFailedAsItWasEmptyTitle,
				Rsrcs.strAliasImportFromClipboardFailedAsItWasEmptyMsg, MsBox.Avalonia.Enums.ButtonEnum.Ok,
				MsBox.Avalonia.Enums.Icon.Error, Avalonia.Controls.WindowStartupLocation.CenterOwner);
		else
		{
			string? strClipboardCtnts = wnd.Clipboard.GetTextAsync().Result;

			if(strClipboardCtnts is null)
				MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(Rsrcs.strAliasImportFromClipboardFailedAsItWasEmptyTitle,
					Rsrcs.strAliasImportFromClipboardFailedAsItWasEmptyMsg, MsBox.Avalonia.Enums.ButtonEnum.Ok,
					MsBox.Avalonia.Enums.Icon.Error, Avalonia.Controls.WindowStartupLocation.CenterOwner);
			else
			{
				int iExistingAliasCnt = ctxt.Entries.Count;

				System.Collections.Generic.IEnumerable<string>? estrErrors = Import(strClipboardCtnts);

				if(estrErrors is null)
					 MsBox.Avalonia.MessageBoxManager.GetMessageBoxStandard(
							Rsrcs.strAliasesImportedSuccessfullyTitle, Rsrcs
								.strAliasesImportedSuccessfullyMsgFmt.Fmt(ctxt.Entries.Count - iExistingAliasCnt), MsBox.Avalonia.Enums
								.ButtonEnum
								.Ok, MsBox.Avalonia.Enums.Icon.Success)
						.ShowWindowDialogAsync(wnd).Wait();
				else
				{
					AliasImportFailureDlg dlg = new()
					{
						AliasesSuccessfullyImported = ctxt.Entries.Count - iExistingAliasCnt,
						Errors = new System.Collections.Generic.Dictionary<string, System.Collections.Generic
							.IEnumerable<string>>()
						{
							[Rsrcs.strClipboardName] = estrErrors,
						},
					};

					dlg.ShowDialog(wnd).Wait();
				}
			}
		}
	}

	private static void OnDragOver(object? objSender, DragEventArgs args)
	{
		if(!args.Data.Contains(DataFormats.Files))
			args.DragEffects = DragDropEffects.None;
	}

	private void OnFilesDropped(object? objSender, DragEventArgs args)
	{
		if(!args.Data.Contains(DataFormats.Files))
			Import(args.Data.GetFiles()?.Select(fileCur => new System.IO.FileInfo(fileCur.Path.AbsolutePath))
				?? throw new System.InvalidProgramException("Somehow we have but don't have files")
			)?.Wait();
	}

	private void OnLoadingRowInGrid(object? objSender, Avalonia.Controls.DataGridRowEventArgs args)
	{
		args.Row.PointerPressed += OnPointerPressedOnRow;
		args.Row.PointerMoved += OnPointerMovedWithRow;
	}

	private void OnUnloadingRowInGrid(object? objSender, Avalonia.Controls.DataGridRowEventArgs args)
	{
		args.Row.PointerPressed -= OnPointerPressedOnRow;
		args.Row.PointerMoved -= OnPointerMovedWithRow;
	}
}