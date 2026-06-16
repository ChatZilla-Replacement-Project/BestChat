namespace BestChat.Platform.UI.Desktop.Prefs.Pages;

public partial class GlobalNotificationsPage : AbstractVisualPrefsTabCtrl
{
	public GlobalNotificationsPage()
		=> InitializeComponent();


	private GlobalNotificationsPrefs? ctxt;


	public GlobalNotificationsPrefs? Ctxt
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
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalNotificationsPage");

		if(!ctxt.IsEditMode)
			throw new System.InvalidOperationException("This GlobalNotificationsPrefs object isn't in EditMode mode");

		base.OnInitialized();
	}


	private void OnResetMethod(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Method.ResetValToDef();

	private void OnResetEnableHistory(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.EnableHistory.ResetValToDef();

	private void OnResetPlaySound(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.PlaySound.ResetValToDef();

	private void OnResetSoundToPlay(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.SoundToPlay.ResetValToDef();

	private void OnResetKeepVisibleFor(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.KeepVisibleFor.ResetValToDef();
}