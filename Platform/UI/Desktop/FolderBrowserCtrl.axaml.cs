// Ignore Spelling: Ctrls Ctrl Loc evt

namespace BestChat.Platform.UI.Desktop;

/// <summary>
/// Interaction logic for FolderBrowserCtrl.xaml
/// </summary>
public partial class FolderBrowserCtrl : Avalonia.Controls.UserControl, System.ComponentModel
	.INotifyPropertyChanged
{
	#region Constructors & Deconstructors
		public FolderBrowserCtrl()
			=> InitializeIfNeeded();
	#endregion

	#region Events
		public new event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

		public event System.Action<FolderBrowserCtrl, System.IO.DirectoryInfo?, System.IO.DirectoryInfo>?
			evtLocSpecifiedChanged;
	#endregion

	#region Constants
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification
			= "Due to naming standards that are inherited")]
		public static readonly Avalonia.DirectProperty<FolderBrowserCtrl, string> DlgTitleProperty = Avalonia
			.AvaloniaProperty.RegisterDirect<FolderBrowserCtrl, string>(
				nameof(DlgTitle),
				sender
					=> sender.DlgTitle,
				(sender, strNewDlgTitle)
					=> sender.DlgTitle = strNewDlgTitle,
				Rsrcs.strDefFileBrowserTitle
			);

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification
			= "Due to naming standards that are inherited")]
		public static readonly Avalonia.DirectProperty<FolderBrowserCtrl, System.IO.DirectoryInfo?>
			LocSpecifiedProperty = Avalonia.AvaloniaProperty.RegisterDirect<FolderBrowserCtrl, System.IO.DirectoryInfo?>(
				nameof(LocSpecified),
				sender => sender.LocSpecified,
				(sender, dirNewVal) => sender.LocSpecified = dirNewVal,
				null
			);
	#endregion

	#region Properties
		public string DlgTitle
		{
			get;
			set;
		} = Rsrcs.strDefFileBrowserTitle;

		private System.IO.DirectoryInfo? LocSpecified
		{
			get;
			set;
		}
	#endregion

	#region Event Handlers
		private async void OnBrowseBtnClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			Avalonia.Controls.TopLevel? tp = Avalonia.Controls.TopLevel.GetTopLevel(this);

			if(tp != null)
			{
				System.Collections.Generic.IReadOnlyList<Avalonia.Platform.Storage.IStorageFolder> folders = await tp
					.StorageProvider.OpenFolderPickerAsync(new()
				{
					AllowMultiple = false,
					Title = DlgTitle,
				});

				if(folders.Count > 0)
				{
					System.IO.DirectoryInfo? fileOldLocSpecified = LocSpecified;

					LocSpecified = new(folders[0].Path.AbsolutePath);

					evtLocSpecifiedChanged?.Invoke(this, fileOldLocSpecified, LocSpecified);
					PropertyChanged?.Invoke(this, new(nameof(LocSpecified)));
				}
			}
		}
	#endregion
}