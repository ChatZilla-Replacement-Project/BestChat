namespace BestChat.Platform.UI.Desktop.Prefs.Pages;

public partial class GlobalAppearanceUserListPage : AbstractVisualPrefsTabCtrl
{
	public GlobalAppearanceUserListPage()
		=> InitializeComponent();


	private GlobalAppearanceUserListPrefs? ctxt;


	public GlobalAppearanceUserListPrefs? Ctxt
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
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalAppearancePage");

		if(!ctxt.IsEditMode)
			throw new System.InvalidOperationException("The context is not editing");

		base.OnInitialized();
	}


	private void OnResetLocClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Loc.ResetValToDef();

	private void OnResetHowToShowModesClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.HowToShowModes.ResetValToDef();

	private void OnResetSortOrderClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.SortByMode.ResetValToDef();
}