namespace BestChat.Platform.UI.Desktop.Prefs
{
public class GlobalCompositionPrefs : DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
	public GlobalCompositionPrefs(GlobalPrefs mgrParent) :
		base(mgrParent, "Composition", Rsrcs.strGlobalCompositionTitle, Rsrcs
			.strGlobalCompositionDesc)
	{
		useTypographicalQuotes = new(this, "Use Typographical Quotes", Rsrcs
			.strGlobalCompositionUseTypographicalQuotesTitle, Rsrcs
			.strGlobalCompositionUseTypographicalQuotesDesc, false);
		treatDblDashAsMDash = new(this, "Treat Double Dash as MDash", Rsrcs
			.strGlobalCompositionTreatDblDashAsMDashTitle, Rsrcs
			.strGlobalCompositionTreatDblDashAsMDashDesc, true);
		treatThreePeriodsAsEllipsis = new(this, "Treat Three Periods as Ellipsis",
			Rsrcs.strGlobalCompositionTreatThreePeriodsAsEllipsisTitle, Rsrcs
				.strGlobalCompositionTreatThreePeriodsAsEllipsisDesc, true);
		enableEmojiShortCuts = new(this, "Enable Emoji Short Cuts", Rsrcs
			.strGlobalCompositionEnableEmojiShortCutsTitle, Rsrcs
			.strGlobalCompositionEnableEmojiShortCutsDesc, true);
		enableEntityShortCuts = new(this, "Enable Entity Short Cuts", Rsrcs
			.strGlobalCompositionEnableEntityShortCutsTitle, Rsrcs
			.strGlobalCompositionEnableEntityShortCutsDesc, true);
	}

	internal GlobalCompositionPrefs(GlobalPrefs mgrParent, DTO.RootDTO.GlobalDTO.CompositionDTO dto) :
		base(mgrParent, "Composition", Rsrcs.strGlobalCompositionTitle, Rsrcs
			.strGlobalCompositionDesc)
	{
		useTypographicalQuotes = new(this, "Use Typographical Quotes", Rsrcs
			.strGlobalCompositionUseTypographicalQuotesTitle, Rsrcs
			.strGlobalCompositionUseTypographicalQuotesDesc, false, dto.UseTypeographicalQuotes);
		treatDblDashAsMDash = new(this, "Treat Double Dash as MDash", Rsrcs
			.strGlobalCompositionTreatDblDashAsMDashTitle, Rsrcs
			.strGlobalCompositionTreatDblDashAsMDashDesc, true, dto.TreatDblDashAsMDash);
		treatThreePeriodsAsEllipsis = new(this, "Treat Three Periods as Ellipsis",
			Rsrcs.strGlobalCompositionTreatThreePeriodsAsEllipsisTitle, Rsrcs
				.strGlobalCompositionTreatThreePeriodsAsEllipsisDesc, true, dto
				.TreatThreePeriodsAsEllipsis);
		enableEmojiShortCuts = new(this, "Enable Emoji Short Cuts", Rsrcs
			.strGlobalCompositionEnableEmojiShortCutsTitle, Rsrcs
			.strGlobalCompositionEnableEmojiShortCutsDesc, true, dto.EnableEmojiShortCuts);
		enableEntityShortCuts = new(this, "Enable Entity Short Cuts", Rsrcs
			.strGlobalCompositionEnableEntityShortCutsTitle, Rsrcs
			.strGlobalCompositionEnableEntityShortCutsDesc, true, dto.EnableEntityShortCuts);
	}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
	private readonly DataAndExt.Prefs.Item<bool> useTypographicalQuotes;

	private readonly DataAndExt.Prefs.Item<bool> treatDblDashAsMDash;

	private readonly DataAndExt.Prefs.Item<bool> treatThreePeriodsAsEllipsis;

	private readonly DataAndExt.Prefs.Item<bool> enableEmojiShortCuts;

	private readonly DataAndExt.Prefs.Item<bool> enableEntityShortCuts;
	#endregion

	#region Properties
	public DataAndExt.Prefs.Item<bool> UseTypographicalQuotes
		=> useTypographicalQuotes;

	public DataAndExt.Prefs.Item<bool> TreatDblDashAsMDash
		=> treatDblDashAsMDash;

	public DataAndExt.Prefs.Item<bool> TreatThreePeriodsAsEllipsis
		=> treatThreePeriodsAsEllipsis;

	public DataAndExt.Prefs.Item<bool> EnableEmojiShortCuts
		=> enableEmojiShortCuts;

	public DataAndExt.Prefs.Item<bool> EnableEntityShortCuts
		=> enableEntityShortCuts;
	#endregion

	#region Methods
	internal DTO.RootDTO.GlobalDTO.CompositionDTO ToDTO()
		=> new(
			useTypographicalQuotes.CurVal,
			treatDblDashAsMDash.CurVal,
			treatThreePeriodsAsEllipsis.CurVal,
			enableEmojiShortCuts.CurVal,
			enableEntityShortCuts.CurVal
		);
	#endregion

	#region Event Handlers
	#endregion
}
}