namespace BestChat.IRC.ProtocolMgr.Prefs.Pages;

public partial class NetConnPage : Platform.UI.Desktop.Prefs.AbstractVisualPrefsTabCtrl
{
	public NetConnPage()
		=> InitializeComponent();


	private Data.Prefs.NetConnPrefs? ctxt;

	public Data.Prefs.NetConnPrefs? Ctxt
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
		if(ctxt != null)
			throw new System.InvalidOperationException("Set Ctxt before showing the connection preference page");

		base.OnInitialized();
	}


	private void OnResetEnableIdentClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.EnableIdent.ResetValToDef();

	private void OnResetAutoReconnectClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.AutoReconnect.ResetValToDef();

	private void OnResetRejoinAfterKickClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.RejoinAfterKick.ResetValToDef();

	private void OnResetCharEncoding(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.CharEncoding.ResetValToDef();

	private void OnResetUnlimitedAttempts(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.UnlimitedAttempts.ResetValToDef();

	private void OnResetMaxAttempts(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.MaxAttempts.ResetValToDef();

	private void OnResetDefQuitMsg(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.DefQuitMsg.ResetValToDef();
}