namespace BestChat.Platform.UI.Desktop.Prefs.Pages;

public partial class GlobalPage : AbstractVisualPrefsTabCtrl
{
	public GlobalPage()
		=> InitializeComponent();


	private GlobalPrefs? ctxt;


	public GlobalPrefs? Ctxt
	{
		get => ctxt;

		set
		{
			if(ctxt != value)
				DataContext = ctxt = value;
		}
	}

	public override System.Collections.Generic.IEnumerable<System.Type> HandlesChildMgrsOfType
		=>
			[
				typeof(GlobalCompositionPrefs),
			];


	protected override void OnInitialized()
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalAppearancePage");

		if(!ctxt.IsEditMode)
			throw new System.InvalidOperationException("The context is not editing");

		base.OnInitialized();
	}


	private void OnResetCompositionUseTypographicalQuotesClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Composition.UseTypographicalQuotes.ResetValToDef();

	private void OnResetCompositionTreatDblDashAsMDashClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Composition.TreatDblDashAsMDash.ResetValToDef();

	private void OnResetCompositionTreatThreePeriodsAsEllipsisClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Composition.TreatThreePeriodsAsEllipsis.ResetValToDef();

	private void OnCompositionEnableEmojiShortCutsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Composition.EnableEmojiShortCuts.ResetValToDef();

	private void OnResetCompositionEnableEntityShortCutsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Composition.EnableEntityShortCuts.ResetValToDef();
}