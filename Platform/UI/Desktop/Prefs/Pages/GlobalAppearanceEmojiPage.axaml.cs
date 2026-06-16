namespace BestChat.Platform.UI.Desktop.Prefs.Pages;

public partial class GlobalAppearanceEmojiPage : AbstractVisualPrefsTabCtrl
{
	public GlobalAppearanceEmojiPage()
		=> InitializeComponent();


	private GlobalAppearanceEmojiPrefs? ctxt;


	public GlobalAppearanceEmojiPrefs? Ctxt
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
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalAppearanceEmojiPage");

		if(!ctxt.IsEditMode)
			throw new System.InvalidOperationException("The context is not editing");

		base.OnInitialized();
	}


	private void OnResetSendingEmojiClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.SendingEmoji.ResetValToDef();

	private void OnResetSendingEmoticonsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.SendingEmoticons.ResetValToDef();

	private void OnResetDisplayingEmojiClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.DisplayingEmoji.ResetValToDef();

	private void OnResetDisplayingEmoticonsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.DisplayingEmoticons.ResetValToDef();

	private void OnResetMakeEmojiOnlyPostsBiggerClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.MakeEmojiOnlyPostsBigger.ResetValToDef();

	private void OnResetEmojiAnimationClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.EmojiAnimation.ResetValToDef();
}