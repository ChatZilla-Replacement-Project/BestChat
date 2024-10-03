namespace BestChat.Platform.UI.Desktop.Prefs;

public class GlobalAppearanceFontsOneFontBlockPrefs : DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		internal GlobalAppearanceFontsOneFontBlockPrefs(in GlobalAppearanceFontPrefs mgrParent, in string strName, in string
				strLocalizedName, in string strLocalizedDesc) :
			base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
		{
			normalFontFamily = new(this, "Normal Font Family", UiDesktopRsrcs
				.strGlobalAppearanceNormalFontFamilyTitle, UiDesktopRsrcs
				.strGlobalAppearanceNormalFontFamilyDesc);

			fixedWidthFontFamily = new(this, "Fixed Width Font Family",
				UiDesktopRsrcs.strGlobalAppearanceFixedWidthFontFamilyTitle, UiDesktopRsrcs
				.strGlobalAppearanceFixedWidthFontFamilyDesc);

			size = new(this, "Size of the fonts used", UiDesktopRsrcs
				.strGlobalAppearanceFontSizeTitle, UiDesktopRsrcs.strGlobalAppearanceFontSizeDesc, 12);

			weight = new(this, "Weight (boldness) of the fonts used",
				UiDesktopRsrcs.strGlobalAppearanceFontWeightTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontWeightDesc, Avalonia.Media.FontWeight.Normal);
		}

		internal GlobalAppearanceFontsOneFontBlockPrefs(in GlobalAppearanceFontPrefs mgrParent, in string strName, in string
				strLocalizedName, in string strLocalizedDesc, DTO.RootDTO.GlobalDTO
				.AppearanceDTO.FontDTO.OneFontBlockDTO dto) :
			base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
		{
			normalFontFamily = new(this, "Normal Font Family", UiDesktopRsrcs
				.strGlobalAppearanceNormalFontFamilyTitle, UiDesktopRsrcs
				.strGlobalAppearanceNormalFontFamilyDesc, dto.NormalFamily);

			fixedWidthFontFamily = new(this, "Fixed Width Font Family",
				UiDesktopRsrcs.strGlobalAppearanceFixedWidthFontFamilyTitle, UiDesktopRsrcs
				.strGlobalAppearanceFixedWidthFontFamilyDesc);

			size = new(this, "Size of the fonts used", UiDesktopRsrcs
				.strGlobalAppearanceFontSizeTitle, UiDesktopRsrcs.strGlobalAppearanceFontSizeDesc, dto
				.Size, 12);

			weight = new(this, "Weight (boldness) of the fonts used",
				UiDesktopRsrcs.strGlobalAppearanceFontWeightTitle, UiDesktopRsrcs
				.strGlobalAppearanceFontWeightDesc, dto.Weight, Avalonia.Media.FontWeight.Normal);
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
		private readonly GlobalAppearanceFontsOneFontBlockInfoPairPrefs<Avalonia.Media.FontFamily> normalFontFamily;

		private readonly GlobalAppearanceFontsOneFontBlockInfoPairPrefs<Avalonia.Media.FontFamily> fixedWidthFontFamily;

		private readonly GlobalAppearanceFontsOneFontBlockInfoPairPrefs<double> size;

		private readonly GlobalAppearanceFontsOneFontBlockInfoPairPrefs<Avalonia.Media.FontWeight> weight;
	#endregion

	#region Properties
		public GlobalAppearanceFontsOneFontBlockInfoPairPrefs<Avalonia.Media.FontFamily> NormalFontFamily
			=> normalFontFamily;

		public GlobalAppearanceFontsOneFontBlockInfoPairPrefs<Avalonia.Media.FontFamily> FixedWidthFontFamily
			=> fixedWidthFontFamily;

		public GlobalAppearanceFontsOneFontBlockInfoPairPrefs<double> Size
			=> size;

		public GlobalAppearanceFontsOneFontBlockInfoPairPrefs<Avalonia.Media.FontWeight> Weight
			=> weight;
	#endregion

	#region Methods
		internal DTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO.OneFontBlockDTO
			ToDTO()
				=> new(normalFontFamily.ToDTO(), fixedWidthFontFamily.ToDTO(), size.ToDTO(),
					weight.ToDTO());
	#endregion

	#region Event Handlers
	#endregion
}