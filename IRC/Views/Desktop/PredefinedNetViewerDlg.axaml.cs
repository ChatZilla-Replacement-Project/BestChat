namespace BestChat.IRC.Views.Desktop;

using Platform.DataAndExt.Ext;

public partial class PredefinedNetViewerDlg : Avalonia.Controls.Window
{
	public PredefinedNetViewerDlg()
		=> InitializeIfNeeded();

	public Data.Defs.NetServerInfo.Editable? ServerInfoCtxt
	{
		get => DataContext as Data.Defs.NetServerInfo.Editable;

		set
		{
			DataContext = value;

			if(value != null)
				Title = Rsrcs.strViewingNetTitleFmt.Fmt(value.Domain, value.eunetParent.Name);
		}
	}

	private void OnCloseClicked(Avalonia.Controls.Button btnSender, Avalonia.Interactivity.RoutedEventArgs e)
		=> Close();
}