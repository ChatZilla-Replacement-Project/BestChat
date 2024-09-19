// Ignore Spelling: Prefs Loc metadata cmgr Prot

using System.Linq;

namespace BestChat.Platform.UI.Desktop.Prefs;

public class RootPrefs : DataAndExt.Prefs.Prefs<RootPrefs.GlobalPrefs, RootPrefs
	.GlobalPrefs.AppearancePrefs>
{
	#region Constructors & Deconstructors
		private RootPrefs(Avalonia.Controls.Window wndMain, System.IO.DirectoryInfo dirDataLoc)
		{
			this.wndMain = wndMain;
			global = new(this);


			ProtocolGuiMgr mgr = ProtocolGuiMgr.Init(dirDataLoc, AskUserIfTheyWantToEnableNewProtocol);

			foreach(ProtocolGuiMgr.IProtocolGuiDef? iprotCur in mgr.AllEnabledProtocols)
				if(iprotCur != null && iprotCur.RootPrefForProtocol != null)
					RegisterNewProtMgr(iprotCur.RootPrefForProtocol);
		}

		private RootPrefs(Avalonia.Controls.Window wndMain, System.IO.DirectoryInfo dirDataLoc, DTO.RootDTO dto)
		{
			this.wndMain = wndMain;
			global = new(this, dto.Global);


			ProtocolGuiMgr mgr = ProtocolGuiMgr.Init(dirDataLoc, AskUserIfTheyWantToEnableNewProtocol);

			foreach(ProtocolGuiMgr.IProtocolGuiDef? iprotCur in mgr.AllEnabledProtocols)
				if(iprotCur != null && iprotCur.RootPrefForProtocol != null)
					RegisterNewProtMgr(iprotCur.RootPrefForProtocol);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public new class GlobalPrefs : DataAndExt.Prefs.Prefs<GlobalPrefs, GlobalPrefs
			.AppearancePrefs>.GlobalPrefs
		{
			#region Constructors & Deconstructors
				internal GlobalPrefs(RootPrefs mgrParent) :
					base(mgrParent)
					=> appearance = new(this);

				internal GlobalPrefs(RootPrefs mgrParent, DTO.RootDTO.GlobalDTO dto) :
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
				public new class AppearancePrefs : DataAndExt.Prefs.Prefs<GlobalPrefs,
					AppearancePrefs>.GlobalPrefs.AppearancePrefs
				{
					#region Constructors & Deconstructors
						internal AppearancePrefs(GlobalPrefs mgrParent) :
							base(mgrParent)
							=> fonts = new(this);

						internal AppearancePrefs(GlobalPrefs mgrParent, DTO.RootDTO.GlobalDTO.AppearanceDTO dto) :
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
						public class FontPrefs : DataAndExt.Prefs.AbstractChildMgr
						{
							#region Constructors & Deconstructors
								internal FontPrefs(AppearancePrefs mgrParent) :
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

								internal FontPrefs(AppearancePrefs mgrParent, DTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO dto) :
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
								public class OneFontBlockPrefs : DataAndExt.Prefs.AbstractChildMgr
								{
									#region Constructors & Deconstructors
										internal OneFontBlockPrefs(in FontPrefs mgrParent, in string strName, in string
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

										internal OneFontBlockPrefs(in FontPrefs mgrParent, in string strName, in string
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
										public class InfoPairPrefs<FieldType> : DataAndExt.Prefs
											.AbstractChildMgr
										{
											#region Constructors & Deconstructors
												internal InfoPairPrefs(in OneFontBlockPrefs mgrParent, in string strName, in string
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

												internal InfoPairPrefs(in OneFontBlockPrefs mgrParent, in string strName, in string
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

												internal InfoPairPrefs(in OneFontBlockPrefs mgrParent, in string strName, in string
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

												internal InfoPairPrefs(in OneFontBlockPrefs mgrParent, in string strName, in string
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
											internal DTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO.OneFontBlockDTO.InfoPairDTO<FieldType>
												ToDTO()
													=> new(isThemeOverridden.CurVal, overridenVal.CurVal);
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
										public InfoPairPrefs<Avalonia.Media.FontFamily> NormalFontFamily
											=> normalFontFamily;

										public InfoPairPrefs<Avalonia.Media.FontFamily> FixedWidthFontFamily
											=> fixedWidthFontFamily;

										public InfoPairPrefs<double> Size
											=> size;

										public InfoPairPrefs<Avalonia.Media.FontWeight> Weight
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
							#endregion

							#region Members
							private readonly OneFontBlockPrefs appFonts;

							private readonly OneFontBlockPrefs viewFonts;
							#endregion

							#region Properties
								public OneFontBlockPrefs AppFonts
									=> appFonts;

								public OneFontBlockPrefs ViewFonts
									=> viewFonts;
							#endregion

							#region Methods
								internal DTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO ToDTO()
									=> new(appFonts.ToDTO(), viewFonts.ToDTO());
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						private readonly FontPrefs fonts;
					#endregion

					#region Properties
						public FontPrefs Fonts
							=> fonts;
					#endregion

					#region Methods
						internal DTO.RootDTO.GlobalDTO.AppearanceDTO ToDTO()
							=> new(ConfMode.ToDTO(), TimeStamp.ToDTO(), UserList.ToDTO(), fonts.ToDTO());
					#endregion

					#region Event Handlers
					#endregion
				}
			#endregion

			#region Members
				private readonly AppearancePrefs appearance;
			#endregion

			#region Properties
				public override AppearancePrefs Appearance
					=> appearance;
			#endregion

			#region Methods
				internal DTO.RootDTO.GlobalDTO ToDTO()
					=> new(appearance.ToDTO(), Plugins.ToDTO());
			#endregion

			#region Event Handlers
			#endregion
		}
	#endregion

	#region Members
		private static RootPrefs? instance = null;

		public readonly Avalonia.Controls.Window wndMain;

		public readonly GlobalPrefs global;
	#endregion

	#region Properties
		public static bool IsReady
			=> instance != null;

		public static RootPrefs Instance
			=> instance ?? throw new System.InvalidProgramException("Call BestChat.Desktop.RootPrefs.Load " +
				"before accessing the instance");

		public override GlobalPrefs Global
			=> global;
	#endregion

	#region Methods
		public static void Load(Avalonia.Controls.Window wndMain, System.IO.DirectoryInfo dirDataLoc)
		{
			if(instance != null)
				instance = Load<DTO.RootDTO>(dirDataLoc) is DTO.RootDTO dto
					? new(wndMain, dirDataLoc, dto)
					: new(wndMain, dirDataLoc);
		}

		protected override DataAndExt.Prefs.DTO.PrefsDTO ToDTO()
			=> new DTO.RootDTO(global.ToDTO());

		public bool AskUserIfTheyWantToEnableNewProtocol(DataAndExt.Protocol.Mgr<ProtocolGuiMgr.IProtocolGuiDef>
				.ProtocolMetaData metadataForProtocol)
			=> MsBox.Avalonia.MessageBoxManager.GetMessageBoxCustom(new()
			{
				ButtonDefinitions =
					[
						new()
							{
								Name = UiDesktopRsrcs.strQuestionNo,
								IsDefault = true,
								IsCancel = true,
							},
							new()
							{
								Name = UiDesktopRsrcs.strQuestionYes,
							},
					],
				ContentTitle = UiDesktopRsrcs.strPermNeededToEnableProtocolCaption,
				ContentMessage = UiDesktopRsrcs.strPermNeededToEnableProtocolQuestion,
				Icon = MsBox.Avalonia.Enums.Icon.Warning,
				WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner,
				CanResize = false,
				SizeToContent = Avalonia.Controls.SizeToContent.WidthAndHeight,
				ShowInCenter = true,
				FontFamily = Instance.Global.Appearance.Fonts.AppFonts.NormalFontFamily.OverriddenVal.CurVal,
			}).ShowWindowDialogAsync(wndMain).Result == UiDesktopRsrcs.strQuestionYes;

		public void RegisterNewProtMgr(DataAndExt.Prefs.AbstractChildMgr cmgrForProt)
			=> Add(cmgrForProt);
	#endregion

	#region Event Handlers
	#endregion
}