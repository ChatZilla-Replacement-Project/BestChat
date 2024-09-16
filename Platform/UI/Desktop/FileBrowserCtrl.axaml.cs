// Ignore Spelling: Ctrls Ctrl Loc evt

namespace BestChat.Platform.UI.Desktop;

/// <summary>
/// Interaction logic for FileBrowserCtrl.xaml
/// </summary>
public partial class FileBrowserCtrl : Avalonia.Controls.UserControl, System.ComponentModel.INotifyPropertyChanged
{
	#region Constructors & Deconstructors
		public FileBrowserCtrl()
			=> InitializeIfNeeded();
	#endregion

	#region Events
		public new event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

		public event System.Action<FileBrowserCtrl, System.IO.FileInfo?, System.IO.FileInfo>? evtLocSpecifiedChanged;
	#endregion
	
	#region Constants
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification
			= "Due to naming standards that are inherited")]
		public static readonly Avalonia.DirectProperty<FileBrowserCtrl, string> DlgTitleProperty = Avalonia
			.AvaloniaProperty.RegisterDirect<FileBrowserCtrl, string>(
				nameof(DlgTitle),
				sender
					=> sender.DlgTitle,
				(sender, strNewDlgTitle)
					=> sender.DlgTitle = strNewDlgTitle,
				Rsrcs.strDefFileBrowserTitle
			);

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification
			= "Due to naming standards that are inherited")]
		public static readonly Avalonia.DirectProperty<FileBrowserCtrl, System.IO.FileInfo?> LocSpecifiedProperty
			= Avalonia.AvaloniaProperty.RegisterDirect<FileBrowserCtrl, System.IO.FileInfo?>(
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

		public System.IO.FileInfo? LocSpecified
		{
			get;
			set;
		}
	#endregion

	#region Event Handlers
		private async void OnBrowseBtnClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity
			.RoutedEventArgs e)
		{
			Avalonia.Controls.TopLevel? tp = Avalonia.Controls.TopLevel.GetTopLevel(this);
		
			if(tp != null)
			{
				System.Collections.Generic.IReadOnlyList<Avalonia.Platform.Storage.IStorageFile> files = await tp
					.StorageProvider.OpenFilePickerAsync(new()
				{
					Title = DlgTitle,
					AllowMultiple = false,
					FileTypeFilter = [
						new("SSL Certificate")
						{
							MimeTypes =
							[
								// TODO: Determine the correct mime types.
							],
							Patterns =
							[
								"*.cert",
							],
							// TODO: Determine the values for the Apple identifiers.
						},
						new("All Files")
						{
							Patterns =
							[
								"*.*"
							]
						}
					],
				});

				if(files.Count > 0)
				{
					System.IO.FileInfo? fileOldLocSpecified = LocSpecified;

					LocSpecified = new(files[0].Path.AbsolutePath);

					evtLocSpecifiedChanged?.Invoke(this, fileOldLocSpecified, LocSpecified);
					PropertyChanged?.Invoke(this, new(nameof(LocSpecified)));
				}
			}
		}
	#endregion
}