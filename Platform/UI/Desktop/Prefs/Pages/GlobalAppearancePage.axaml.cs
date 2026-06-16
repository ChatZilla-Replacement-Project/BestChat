namespace BestChat.Platform.UI.Desktop.Prefs.Pages;

public partial class GlobalAppearancePage : AbstractVisualPrefsTabCtrl
{
	public GlobalAppearancePage()
		=> InitializeComponent();


	private GlobalAppearancePrefs? ctxt;


	public GlobalAppearancePrefs? Ctxt
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
				typeof(DataAndExt.Prefs.GlobalAppearanceConfModePrefs),
				typeof(DataAndExt.Prefs.GlobalAppearanceTimeStampPrefs),
				typeof(DataAndExt.Prefs.GlobalAppearanceMsgGroupsPrefs),
				typeof(GlobalAppearanceAnimationPrefs)
			];


	protected override void OnInitialized()
	{
		if(ctxt is null)
			throw new System.InvalidOperationException("Set Ctxt before showing a GlobalAppearancePage");

		if(!ctxt.IsEditMode)
			throw new System.InvalidOperationException("The context is not editing");

		base.OnInitialized();
	}


	private void OnResetConfModeClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.ConfMode.ConfModeEnabled.ResetValToDef();

	private void OnResetConfModeUserLimitBeforeTriggerClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.ConfMode.UserLimitBeforeTrigger.ResetValToDef();

	private void OnResetConfModeCollapseMsgsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.ConfMode.MsgsCollapsed.ResetValToDef();

	private void OnResetConfModeCollapseActionsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.ConfMode.ActionsCollapsed.ResetValToDef();

	private void OnResetTimeStampsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.TimeStamp.Show.ResetValToDef();

	private void OnResetTimeStampInlineFmtClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.TimeStamp.InlineFmt.ResetValToDef();

	private void OnResetTimeStampExpandedFmtClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.TimeStamp.ExpandedFmt.ResetValToDef();

	private void OnResetTimeStampHowOftenToRepeatClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.TimeStamp.HowOftenToRepeat.ResetValToDef();

	private void OnResetMsgGroupsEnabledClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.MsgGroups.Enabled.ResetValToDef();

	private void OnResetMsgGroupsHowLongToWaitBeforeStartingNewGroupClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.MsgGroups.HowLongToWaitBeforeStartingNewGroup.ResetValToDef();

	private void OnResetMsgGroupsMaxMsgsPerGroupClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.MsgGroups.MaxMsgsPerGroup.ResetValToDef();

	private void OnResetAnimationGIFsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Animation.GIFs.ResetValToDef();

	private void OnResetAnimationAvatarsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Animation.Avatars.ResetValToDef();

	private void OnResetAnimationResumeOnMouseOverClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.Animation.ResumeOnMouseOver.ResetValToDef();

	private void ResetHyphenateLongWordsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.HyphenateLongWords.ResetValToDef();

	private void OnResetRecognizeLinksClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.RecognizeLinks.ResetValToDef();

	private void OnResetDisplayCtrlCharsClicked(object? objSender, Avalonia.Interactivity.RoutedEventArgs args)
		=> ctxt?.DisplayCtrlChars.ResetValToDef();
}