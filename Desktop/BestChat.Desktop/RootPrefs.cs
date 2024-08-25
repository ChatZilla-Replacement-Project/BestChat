// Ignore Spelling: Prefs

namespace BestChat.Desktop
{
	public class RootPrefs : Platform.DataAndExt.Prefs.Prefs<RootPrefs.GlobalPrefs, RootPrefs.GlobalPrefs.AppearancePrefs>
	{
		#region Constructors & Deconstructors
			private RootPrefs()
			{
				global = new(this);
			}

			private RootPrefs(PrefsDTO.RootDTO dto)
			{
				global = new(this, dto.Global);
			}

			static RootPrefs() => instance = new();
		#endregion

		#region Delegates
		#endregion

		#region Events
		#endregion

		#region Constants
		#endregion

		#region Helper Types
			public new class GlobalPrefs : Platform.DataAndExt.Prefs.Prefs<GlobalPrefs, GlobalPrefs.AppearancePrefs>.GlobalPrefs
			{
				#region Constructors & Deconstructors
					internal GlobalPrefs(RootPrefs mgrParent) :
						base(mgrParent)
						=> appearance = new(this);

					internal GlobalPrefs(RootPrefs mgrParent, PrefsDTO.RootDTO.GlobalDTO dto) :
						base(mgrParent, dto)
						=> appearance = new(this, dto.Appearance);
				#endregion

				#region Delegates
				#endregion

				#region Events
				#endregion

				#region Constants
				#endregion

				#region Helper Types
					public class AppearancePrefs : Platform.DataAndExt.Prefs.Prefs<GlobalPrefs, AppearancePrefs>.GlobalPrefs.AppearancePrefs
					{
						#region Constructors & Deconstructors
							internal AppearancePrefs(GlobalPrefs mgrParent) :
								base(mgrParent)
								=> fonts = new(this);

							internal AppearancePrefs(GlobalPrefs mgrParent, PrefsDTO.RootDTO.GlobalDTO.AppearanceDTO dto) :
								base(mgrParent, dto)
								=> fonts = new(this, dto.Fonts);
						#endregion

						#region Delegates
						#endregion

						#region Events
						#endregion

						#region Constants
						#endregion

						#region Helper Types
							public class FontPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
							{
								#region Constructors & Deconstructors
									internal FontPrefs(AppearancePrefs mgrParent) :
										base(mgrParent, "Fonts", Rsrcs.strGlobalAppearanceFontTitle, Rsrcs.strGlobalAppearanceFontDesc)
									{
										appFonts = new(this, "Fonts for the Application's main windows", Rsrcs
											.strGlobalAppearanceAppFontsTitle, Rsrcs.strGlboalAppearanceAppFontsDesc);

										viewFonts = new(this, "Fonts for views provided by a protocol.", Rsrcs
											.strGlobalAppearanceFontBlockPairOverriddenValTitle, Rsrcs.strGlobalAppearanceFontBlockPairOverriddenValDesc);
									}

									internal FontPrefs(AppearancePrefs mgrParent, PrefsDTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO dto) :
										base(mgrParent, "Fonts", Rsrcs.strGlobalAppearanceFontTitle, Rsrcs.strGlobalAppearanceFontDesc)
									{
										appFonts = new(this, "Fonts for the Application's main windows", Rsrcs
											.strGlobalAppearanceAppFontsTitle, Rsrcs.strGlboalAppearanceAppFontsDesc, dto.AppFontData);

										viewFonts = new(this, "Fonts for views provided by a protocol.", Rsrcs
											.strGlobalAppearanceFontBlockPairOverriddenValTitle, Rsrcs.strGlobalAppearanceFontBlockPairOverriddenValDesc, dto
											.ViewFontData);
									}
								#endregion

								#region Delegates
								#endregion

								#region Events
									public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
								#endregion

								#region Constants
								#endregion

								#region Helper Types
									public class OneFontBlockPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
									{
										#region Constructors & Deconstructors
											internal OneFontBlockPrefs(in FontPrefs mgrParent, in string strName, in string strLocalizedName, in string
													strLocalizedDesc) :
												base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
											{
												normalFontFamily = new(this, "Normal Font Family", Rsrcs
													.strGlobalAppearanceNormalFontFamilyTitle, Rsrcs.strGlobalAppearanceNormalFontFamilyDesc);

												fixedWidthFontFamily = new(this, "Fixed Width Font Family", Rsrcs
													.strGlobalAppearanceFixedWidthFontFamilyTitle, Rsrcs.strGlobalAppearanceFixedWidthFontFamilyDesc);

												size = new(this, "Size of the fonts used", Rsrcs.strGlobalAppearanceFontSizeTitle, Rsrcs
													.strGlobalAppearanceFontSizeDesc, 12);

												weight = new(this, "Weight (boldness) of the fonts used", Rsrcs
													.strGlobalAppearanceFontWeightTitle, Rsrcs.strGlobalAppearanceFontWeightDesc, Avalonia.Media.FontWeight.Normal);
											}

											internal OneFontBlockPrefs(in FontPrefs mgrParent, in string strName, in string strLocalizedName, in string
													strLocalizedDesc, PrefsDTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO.OneFontBlockDTO dto) :
												base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
											{
												normalFontFamily = new(this, "Normal Font Family", Rsrcs
													.strGlobalAppearanceNormalFontFamilyTitle, Rsrcs.strGlobalAppearanceNormalFontFamilyDesc, dto.NormalFamily);

												fixedWidthFontFamily = new(this, "Fixed Width Font Family", Rsrcs
													.strGlobalAppearanceFixedWidthFontFamilyTitle, Rsrcs.strGlobalAppearanceFixedWidthFontFamilyDesc);

												size = new(this, "Size of the fonts used", Rsrcs.strGlobalAppearanceFontSizeTitle, Rsrcs
													.strGlobalAppearanceFontSizeDesc, dto.Size, 12);

												weight = new(this, "Weight (boldness) of the fonts used", Rsrcs
													.strGlobalAppearanceFontWeightTitle, Rsrcs.strGlobalAppearanceFontWeightDesc, dto.Weight, Avalonia.Media
													.FontWeight.Normal);
											}
										#endregion

										#region Delegates
										#endregion

										#region Events
											public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
										#endregion

										#region Constants
										#endregion

										#region Helper Types
											public class InfoPairPrefs<FieldType> : Platform.DataAndExt.Prefs.AbstractChildMgr
											{
												#region Constructors & Deconstructors
													internal InfoPairPrefs(in OneFontBlockPrefs mgrParent, in string strName, in string strLocalizedName, in string
															strLocalizedDesc) :
														base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
													{
														isThemeOverridden = new(this, "Is Theme Overridden", Rsrcs
															.strGlobalAppearanceFontBlockPairIsOverriddenTitle, Rsrcs.strGlobalAppearanceFontBlockPairIsOverriddenDesc,
															false);

														overridenVal = new(this, "Override value", Rsrcs
															.strGlobalAppearanceFontBlockPairOverriddenValTitle, Rsrcs.strGlobalAppearanceFontBlockPairOverriddenValDesc,
															default);
													}

													internal InfoPairPrefs(in OneFontBlockPrefs mgrParent, in string strName, in string strLocalizedName, in string
															strLocalizedDesc, in PrefsDTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO.OneFontBlockDTO.InfoPairDTO<FieldType>
															dto) :
														base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
													{
														isThemeOverridden = new(this, "Is Theme Overridden", Rsrcs
															.strGlobalAppearanceFontBlockPairIsOverriddenTitle, Rsrcs.strGlobalAppearanceFontBlockPairIsOverriddenDesc,
															false, dto.IsOverridden);

														overridenVal = new(this, "Override value", Rsrcs
															.strGlobalAppearanceFontBlockPairOverriddenValTitle, Rsrcs.strGlobalAppearanceFontBlockPairOverriddenValDesc,
															default, dto.OverriddenVal);
													}

													internal InfoPairPrefs(in OneFontBlockPrefs mgrParent, in string strName, in string strLocalizedName, in string
															strLocalizedDesc, FieldType def) :
														base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
													{
														this.def = def;

														isThemeOverridden = new(this, "Is Theme Overridden", Rsrcs
															.strGlobalAppearanceFontBlockPairIsOverriddenTitle, Rsrcs.strGlobalAppearanceFontBlockPairIsOverriddenDesc,
															false);

														overridenVal = new(this, "Override value", Rsrcs
															.strGlobalAppearanceFontBlockPairOverriddenValTitle, Rsrcs.strGlobalAppearanceFontBlockPairOverriddenValDesc,
															default);
													}

													internal InfoPairPrefs(in OneFontBlockPrefs mgrParent, in string strName, in string strLocalizedName, in string
															strLocalizedDesc, in PrefsDTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO.OneFontBlockDTO.InfoPairDTO<FieldType>
															dto, FieldType def) :
														base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
													{
														this.def = def;

														isThemeOverridden = new(this, "Is Theme Overridden", Rsrcs
															.strGlobalAppearanceFontBlockPairIsOverriddenTitle, Rsrcs.strGlobalAppearanceFontBlockPairIsOverriddenDesc,
															false, dto.IsOverridden);

														overridenVal = new(this, "Override value", Rsrcs
															.strGlobalAppearanceFontBlockPairOverriddenValTitle, Rsrcs.strGlobalAppearanceFontBlockPairOverriddenValDesc,
															default, dto.OverriddenVal);
													}
												#endregion

												#region Delegates
												#endregion

												#region Events
													public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
												#endregion

												#region Constants
												#endregion

												#region Helper Types
												#endregion

												#region Members
													private readonly Platform.DataAndExt.Prefs.Item<bool> isThemeOverridden;

													private readonly Platform.DataAndExt.Prefs.Item<FieldType?> overridenVal;

													private FieldType? def = default;
												#endregion

												#region Properties
													public Platform.DataAndExt.Prefs.Item<bool> IsThemeOverridden => isThemeOverridden;

													public Platform.DataAndExt.Prefs.Item<FieldType?> OverriddenVal => overridenVal;
												#endregion

												#region Methods
												#endregion

												#region Event Handlers
												#endregion
											}
										#endregion

										#region Members
											private readonly InfoPairPrefs<Avalonia.Media.FontFamily> normalFontFamily;

											private readonly InfoPairPrefs<Avalonia.Media.FontFamily> fixedWidthFontFamily;

											private readonly InfoPairPrefs<double> size;

											private readonly InfoPairPrefs<Avalonia.Media.FontWeight> weight;
										#endregion

										#region Properties
											public InfoPairPrefs<Avalonia.Media.FontFamily> NormalFontFamily => normalFontFamily;

											public InfoPairPrefs<Avalonia.Media.FontFamily> FixedWidthFontFamily => fixedWidthFontFamily;

											public InfoPairPrefs<double> Size => size;

											public InfoPairPrefs<Avalonia.Media.FontWeight> Weight;
										#endregion

										#region Methods
										#endregion

										#region Event Handlers
										#endregion
									}
								#endregion

								#region Members
									private readonly OneFontBlockPrefs appFonts;

									private readonly OneFontBlockPrefs viewFonts;
								#endregion

								#region Properties
									public OneFontBlockPrefs AppFonts => appFonts;

									public OneFontBlockPrefs ViewFonts => viewFonts;
								#endregion

								#region Methods
								#endregion

								#region Event Handlers
								#endregion
							}
						#endregion

						#region Members
							private readonly FontPrefs fonts;
						#endregion

						#region Properties
							public FontPrefs Fonts => fonts;
						#endregion

						#region Methods
						#endregion

						#region Event Handlers
						#endregion
					}
				#endregion

				#region Members
					private readonly AppearancePrefs appearance;
				#endregion

				#region Properties
					public override AppearancePrefs Appearance => appearance;
				#endregion

				#region Methods
				#endregion

				#region Event Handlers
				#endregion
			}
		#endregion

		#region Members
			public static readonly RootPrefs instance;

			public readonly GlobalPrefs global;
		#endregion

		#region Properties
			public static RootPrefs Instance
				=> instance;

			public override GlobalPrefs Global => global;
		#endregion

		#region Methods
		#endregion

		#region Event Handlers
		#endregion
	}
}