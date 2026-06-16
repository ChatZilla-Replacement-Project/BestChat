using Avalonia.Interactivity;

namespace BestChat.Platform.UI.Desktop.Prefs.Pages;

public partial class GlobalAppearanceFontsOneFontBlockPage : AbstractVisualPrefsTabCtrl
{
	public GlobalAppearanceFontsOneFontBlockPage()
		=> InitializeComponent();


	private GlobalAppearanceFontsOneFontBlockPrefs? ctxt;


	public GlobalAppearanceFontsOneFontBlockPrefs? Ctxt
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
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalAppearanceFontsOneFontBlockPage");

		if(!ctxt.IsEditMode)
			throw new System.InvalidOperationException("The context is not editing");

		base.OnInitialized();
	}


	private void OnResetNormalFontClicked(object? sender, RoutedEventArgs e)
	{
		ctxt?.NormalFontFamily.IsThemeOverridden.ResetValToDef();
		ctxt?.NormalFontFamily.OverriddenVal.ResetValToDef();
	}

	private void OnResetFixedWidthFontClicked(object? sender, RoutedEventArgs e)
	{
		ctxt?.FixedWidthFontFamily.IsThemeOverridden.ResetValToDef();
		ctxt?.FixedWidthFontFamily.OverriddenVal.ResetValToDef();
	}

	private void OnResetFontSizeClicked(object? sender, RoutedEventArgs e)
	{
		ctxt?.Size.IsThemeOverridden.ResetValToDef();
		ctxt?.Size.OverriddenVal.ResetValToDef();
	}

	private void OnResetFontWeightClicked(object? sender, RoutedEventArgs e)
	{
		ctxt?.Weight.IsThemeOverridden.ResetValToDef();
		ctxt?.Weight.OverriddenVal.ResetValToDef();
	}
}