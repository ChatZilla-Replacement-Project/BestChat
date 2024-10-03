namespace BestChat.Platform.UI.Desktop.Prefs;

public class GlobalAppearanceFontPrefs : DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		internal GlobalAppearanceFontPrefs(GlobalAppearancePrefs mgrParent) :
			base(mgrParent, "Fonts", UiDesktopRsrcs.strGlobalAppearanceFontTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontDesc)
		{
			appFonts = new(this, "Fonts for the Application's main windows",
				UiDesktopRsrcs.strGlobalAppearanceAppFontsTitle, UiDesktopRsrcs
				.strGlboalAppearanceAppFontsDesc);

			viewFonts = new(this, "Fonts for views provided by a protocol.",
				UiDesktopRsrcs.strGlobalAppearanceFontBlockPairOverriddenValTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairOverriddenValDesc);
		}

		internal GlobalAppearanceFontPrefs(GlobalAppearancePrefs mgrParent, DTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO dto) :
			base(mgrParent, "Fonts", UiDesktopRsrcs.strGlobalAppearanceFontTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontDesc)
		{
			appFonts = new(this, "Fonts for the Application's main windows",
				UiDesktopRsrcs.strGlobalAppearanceAppFontsTitle, UiDesktopRsrcs
				.strGlboalAppearanceAppFontsDesc, dto.AppFontData);

			viewFonts = new(this, "Fonts for views provided by a protocol.",
				UiDesktopRsrcs.strGlobalAppearanceFontBlockPairOverriddenValTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairOverriddenValDesc, dto.ViewFontData);
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
		private readonly GlobalAppearanceFontsOneFontBlockPrefs appFonts;

		private readonly GlobalAppearanceFontsOneFontBlockPrefs viewFonts;
	#endregion

	#region Properties
		public GlobalAppearanceFontsOneFontBlockPrefs AppFonts
			=> appFonts;

		public GlobalAppearanceFontsOneFontBlockPrefs ViewFonts
			=> viewFonts;
	#endregion

	#region Methods
		internal DTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO ToDTO()
			=> new(appFonts.ToDTO(), viewFonts.ToDTO());
	#endregion

	#region Event Handlers
	#endregion
}