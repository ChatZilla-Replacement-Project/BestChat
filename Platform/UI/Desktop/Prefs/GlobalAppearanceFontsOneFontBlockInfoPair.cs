namespace BestChat.Platform.UI.Desktop.Prefs;

public class GlobalAppearanceFontsOneFontBlockInfoPairPrefs<FieldType> : DataAndExt.Prefs
	.AbstractChildMgr
{
	#region Constructors & Deconstructors
		internal GlobalAppearanceFontsOneFontBlockInfoPairPrefs(in GlobalAppearanceFontsOneFontBlockPrefs mgrParent, in string strName, in string
				strLocalizedName, in string strLocalizedDesc) :
			base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
		{
			isThemeOverridden = new(this, "Is Theme Overridden", UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairIsOverriddenTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairIsOverriddenDesc, false);

			overridenVal = new(this, "Override value", UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairOverriddenValTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairOverriddenValDesc, default);
		}

		internal GlobalAppearanceFontsOneFontBlockInfoPairPrefs(in GlobalAppearanceFontsOneFontBlockPrefs mgrParent, in string strName, in string
				strLocalizedName, in string strLocalizedDesc, in DTO.RootDTO.GlobalDTO.AppearanceDTO
				.FontDTO.OneFontBlockDTO.InfoPairDTO<FieldType> dto) :
			base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
		{
			isThemeOverridden = new(this, "Is Theme Overridden", UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairIsOverriddenTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairIsOverriddenDesc, false, dto.IsOverridden);

			overridenVal = new(this, "Override value", UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairOverriddenValTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairOverriddenValDesc, default, dto.OverriddenVal);
		}

		internal GlobalAppearanceFontsOneFontBlockInfoPairPrefs(in GlobalAppearanceFontsOneFontBlockPrefs mgrParent, in string strName, in string
				strLocalizedName, in string strLocalizedDesc, FieldType def) :
			base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
		{
			this.def = def;

			isThemeOverridden = new(this, "Is Theme Overridden", UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairIsOverriddenTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairIsOverriddenDesc, false);

			overridenVal = new(this, "Override value", UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairOverriddenValTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairOverriddenValDesc, default);
		}

		internal GlobalAppearanceFontsOneFontBlockInfoPairPrefs(in GlobalAppearanceFontsOneFontBlockPrefs mgrParent, in string strName, in string
				strLocalizedName, in string strLocalizedDesc, in DTO.RootDTO.GlobalDTO.AppearanceDTO
				.FontDTO.OneFontBlockDTO.InfoPairDTO<FieldType> dto, FieldType def) :
			base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
		{
			this.def = def;

			isThemeOverridden = new(this, "Is Theme Overridden", UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairIsOverriddenTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairIsOverriddenDesc, false, dto.IsOverridden);

			overridenVal = new(this, "Override value", UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairOverriddenValTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontBlockPairOverriddenValDesc, default, dto.OverriddenVal);
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
		private readonly DataAndExt.Prefs.Item<bool> isThemeOverridden;

		private readonly DataAndExt.Prefs.Item<FieldType?> overridenVal;

		private readonly FieldType? def = default;
	#endregion

	#region Properties
		public DataAndExt.Prefs.Item<bool> IsThemeOverridden
			=> isThemeOverridden;

		public DataAndExt.Prefs.Item<FieldType?> OverriddenVal
			=> overridenVal;
	#endregion

	#region Methods
		internal DTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO.OneFontBlockDTO
			.InfoPairDTO<FieldType> ToDTO()
				=> new(isThemeOverridden.CurVal, overridenVal.CurVal);
	#endregion

	#region Event Handlers
	#endregion
}