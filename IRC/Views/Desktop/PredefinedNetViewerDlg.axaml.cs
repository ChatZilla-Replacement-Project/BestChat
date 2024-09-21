namespace BestChat.IRC.Views.Desktop;

using Platform.DataAndExt.Ext;

public partial class PredefinedNetViewerDlg : Avalonia.Controls.Window
{
	public PredefinedNetViewerDlg()
		=> InitializeIfNeeded();

	public Data.Defs.PredefinedNet? NetCtxt
	{
		get => DataContext as Data.Defs.PredefinedNet;

		set
		{
			DataContext = value;

			if(value != null)
				Title = Rsrcs.strViewingNetTitleFmt.Fmt(value.Name);
		}
	}

	private void OnCloseClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
		=> Close();

	private void OnVisitHomepageClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs e)
	{
		using System.Diagnostics.Process process = new System.Diagnostics.Process();
		process.StartInfo = new System.Diagnostics.ProcessStartInfo()
			{
				UseShellExecute = true, FileName = NetCtxt!.HomePage!.AbsolutePath,
			};
		process.Start();
	}
}