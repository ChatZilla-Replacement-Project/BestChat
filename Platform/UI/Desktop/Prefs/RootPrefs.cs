// Ignore Spelling: Prefs Loc metadata cmgr Prot dto Emoticons Ctrl Msgs

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
				if(iprotCur != null && iprotCur.TopLevelPrefsMgr != null)
					RegisterNewProtMgr(iprotCur.TopLevelPrefsMgr);
		}

		private RootPrefs(Avalonia.Controls.Window wndMain, System.IO.DirectoryInfo dirDataLoc, DTO.RootDTO dto)
		{
			this.wndMain = wndMain;
			global = new(this, dto.Global);


			ProtocolGuiMgr mgr = ProtocolGuiMgr.Init(dirDataLoc, AskUserIfTheyWantToEnableNewProtocol);

			foreach(ProtocolGuiMgr.IProtocolGuiDef? iprotCur in mgr.AllEnabledProtocols)
				if(iprotCur != null && iprotCur.TopLevelPrefsMgr != null)
					RegisterNewProtMgr(iprotCur.TopLevelPrefsMgr);
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
				{
					appearance = new(this);
					composition = new(this);
				}

				internal GlobalPrefs(RootPrefs mgrParent, DTO.RootDTO.GlobalDTO dto) :
					base(mgrParent, dto)
				{
					appearance = new(this, dto.Appearance);
					composition = new(this, dto.Composition);
				}
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
						{
							fonts = new(this);
							emoji = new(this);
							animation = new(this);
							hyphenateLongWords = new(this, "Hyphenate Long Words", Rsrcs
								.strGlobalAppearanceHyphenateLongWordsTitle, Rsrcs
								.strGlobalAppearanceHyphenateLongWordsDesc, true);
							recognizeLinks = new(this, "Recognize Links", Rsrcs
								.strGlobalAppearanceRecognizeLinksTitle, Rsrcs.strGlobalAppearanceRecognizeLinksDesc,
								false);
							displayCtrlChars = new(this, "Display Control Characters", Rsrcs
								.strGlobalAppearanceDisplayCtrlCharsTitle, Rsrcs.strGlobalAppearanceDisplayCtrlCharsDesc,
								true);
							useTypographicalQuotes = new(this, "Use Typographical Quotes", Rsrcs
								.strGlobalAppearanceUseTypographicalQuotesTitle, Rsrcs
								.strGlobalAppearanceUseTypographicalQuotesDesc, true);
						}

						internal AppearancePrefs(GlobalPrefs mgrParent, DTO.RootDTO.GlobalDTO.AppearanceDTO dto) :
							base(mgrParent, dto)
						{
							fonts = new(this, dto.Fonts);
							emoji = new(this, dto.Emoji);
							animation = new(this, dto.Animation);
							hyphenateLongWords = new(this, "Hyphenate Long Words", Rsrcs
								.strGlobalAppearanceHyphenateLongWordsTitle, Rsrcs
								.strGlobalAppearanceHyphenateLongWordsDesc, true, dto.HyphenateLongWords);
							recognizeLinks = new(this, "Recognize Links", Rsrcs
								.strGlobalAppearanceRecognizeLinksTitle, Rsrcs.strGlobalAppearanceRecognizeLinksDesc,
								false, dto.RecognizeLinks);
							displayCtrlChars = new(this, "Display Control Characters", Rsrcs
								.strGlobalAppearanceDisplayCtrlCharsTitle, Rsrcs.strGlobalAppearanceDisplayCtrlCharsDesc,
								true, dto.DisplayCtrlChars);
							useTypographicalQuotes = new(this, "Use Typographical Quotes", Rsrcs
								.strGlobalAppearanceUseTypographicalQuotesTitle, Rsrcs
								.strGlobalAppearanceUseTypographicalQuotesDesc, true, dto.UseTypographicalQuotes);
						}
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
											internal DTO.RootDTO.GlobalDTO.AppearanceDTO.FontDTO.OneFontBlockDTO
												.InfoPairDTO<FieldType> ToDTO()
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

						public class EmojiPrefs : DataAndExt.Prefs.AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public EmojiPrefs(AppearancePrefs mgrParent) :
									base(mgrParent, "Emoji", Rsrcs.strGlobalAppearanceEmojiTitle, Rsrcs
										.strGlobalAppearanceEmojiDesc)
								{
									sendingEmoji = new(this, "Sending Emoji", Rsrcs
										.strGlobalAppearanceEmojiSendingTitle, Rsrcs.strGlobalAppearanceEmojiSendingDesc,
										SendingEmojiOpts.leaveAlone);
									sendingEmoticons = new(this, "Sending Emoticons", Rsrcs
										.strGlobalAppearanceEmoticonsSendingTitle, Rsrcs
										.strGlobalAppearanceEmoticonsSendingTitle, SendingEmoticonsOpts.leaveAlone);
									displayingEmoji = new(this, "Displaying Emoji", Rsrcs
										.strGlobalAppearanceEmojiDisplayingTitle, Rsrcs.strGlobalAppearanceEmojiDisplayingDesc,
										DisplayingEmojiOpts.leaveAlone);
									displayingEmoticons = new(this, "Displaying Emoticons",
										Rsrcs.strGlobalAppearanceEmoticonsDisplayDesc, Rsrcs
										.strGlobalAppearanceEmoticonsDisplayTitle, DisplayingEmoticonsOpts.displayAsEmoji);
									makeEmojiOnlyPostsBigger = new(this, "Make Emoji Only Posts Bigger",
										Rsrcs.strGlobalAppearanceMakeEmojiOnlyPostsBiggerTitle, Rsrcs
										.strGlobalAppearanceMakeEmojiOnlyPostsBiggerDesc, true);
									emojiAnimation = new(this, "Emoji Animation", Rsrcs
										.strGlobalAppearanceEmojiAnimationTitle, Rsrcs.strGlobalAppearanceEmojiAnimationDesc,
										EmojiAnimationOpts.neverAnimate);
								}

								internal EmojiPrefs(AppearancePrefs mgrParent, DTO.RootDTO.GlobalDTO.AppearanceDTO
									.EmojiDTO dto) :
									base(mgrParent, "Emoji", Rsrcs.strGlobalAppearanceEmojiTitle, Rsrcs
										.strGlobalAppearanceEmojiDesc)
								{
									sendingEmoji = new(this, "Sending Emoji", Rsrcs
										.strGlobalAppearanceEmojiSendingTitle, Rsrcs.strGlobalAppearanceEmojiSendingDesc,
										SendingEmojiOpts.leaveAlone, dto.SendingEmoji);
									sendingEmoticons = new(this, "Sending Emoticons", Rsrcs
										.strGlobalAppearanceEmoticonsSendingTitle, Rsrcs
										.strGlobalAppearanceEmoticonsSendingTitle, SendingEmoticonsOpts.leaveAlone, dto
										.SendingEmoticons);
									displayingEmoji = new(this, "Displaying Emoji", Rsrcs
										.strGlobalAppearanceEmojiDisplayingTitle, Rsrcs.strGlobalAppearanceEmojiDisplayingDesc,
										DisplayingEmojiOpts.leaveAlone, dto.DisplayingEmoji);
									displayingEmoticons = new(this, "Displaying Emoticons",
										Rsrcs.strGlobalAppearanceEmoticonsDisplayDesc, Rsrcs
										.strGlobalAppearanceEmoticonsDisplayTitle, DisplayingEmoticonsOpts.displayAsEmoji, dto
										.DisplayingEmoticons);
									makeEmojiOnlyPostsBigger = new(this, "Make Emoji Only Posts Bigger",
										Rsrcs.strGlobalAppearanceMakeEmojiOnlyPostsBiggerTitle, Rsrcs
										.strGlobalAppearanceMakeEmojiOnlyPostsBiggerDesc, true, dto
										.MakeEmojiOnlyPostsBigger);
									emojiAnimation = new(this, "Emoji Animation", Rsrcs
										.strGlobalAppearanceEmojiAnimationTitle, Rsrcs.strGlobalAppearanceEmojiAnimationDesc,
										EmojiAnimationOpts.neverAnimate, dto.EmojiAnimation);
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
							#endregion

							#region Constants
							#endregion

							#region Helper Types
								public enum SendingEmojiOpts : byte
								{
									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmojiSendingLeaveAlone), "Leave" +
										" Alone", nameof(Rsrcs.strEmojiSendingLeaveAloneToolTip), "Best Chat " +
										"will send any emoji you enter as is.  Emoji might not be handled correctly by some " +
										"clients.", typeof(EmojiPrefs))]
									leaveAlone,

									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmojiSendingConvertToEmoticons),
										"Convert to emotions", nameof(Rsrcs
										.strEmojiSendingConvertToEmoticonsToolTip), "Best Chat will convert " +
										"emoji you enter into emoticons.  This is safer as not all clients can correctly " +
										"handle emoji.", typeof(EmojiPrefs))]
									convertToEmoticons,
								}

								public enum SendingEmoticonsOpts : byte
								{
									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmoticonSendingLeaveAlone),
										"Leave Alone", nameof(Rsrcs.strEmoticonSendingLeaveAloneToolTip),
										"Not all clients handle emoji correctly.  This option ensure emoticons" +
										" go out as plain ASCII which they should be able to handle just fine.",
										typeof(EmojiPrefs))]
									leaveAlone,

									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmoticonSendingConvertToEmoji),
										"Convert to Emoji", nameof(Rsrcs.strEmoticonSendingConvertToEmojiToolTip),
										"Causes Best Chat to send out the emoticons you type as emoji.",
										typeof(EmojiPrefs))]
									convertToEmoji,
								}

								public enum DisplayingEmojiOpts : byte
								{
									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmojiDisplayingLeaveAlone),
										"Leave Alone", nameof(Rsrcs.strEmojiDisplayingLeaveAloneToolTip),
										"If you select this, Best Chat will display any emoji as received.  " +
										"Regardless, you'll be able to move your mouse over the emoji to see what was actually"
										+ " received and what you can enter to get the same emoji.", typeof(EmojiPrefs))]
									leaveAlone,

									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmojiDisplayingAsEmoticons),
										"Display as plain text emoticons", nameof(Rsrcs
										.strEmojiDisplayingAsEmoticonsToolTiop), "If you select this, " +
										"Best Chat will display any emoji you receive as though you received plain text emoji."
										+ "  Regardless, you'll be about to hover your mouse over the emoticon shown and see "
										+ "what actually arrived and how to send that emoji back.", typeof(EmojiPrefs))]
									displayAsEmoticons,
								}

								public enum DisplayingEmoticonsOpts : byte
								{
									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmoticonDisplayingDisplayAsEmoji),
										"Leave Alone", nameof(Rsrcs.strEmoticonDisplayingAsEmojiToolTip),
										"If you select this, Best Chat will never convert emoticons into emoji" +
										" for you.  However, if you hover over the emoticon, you'll see a graphical depiction "
										+ "of it.  Depending on the emoji display option below, your theme will select an " +
										"animation to play.", typeof(EmojiAnimationOpts))]
									leaveAlone,

									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmoticonDisplayingDisplayAsEmoji),
										"Display as Emoji", nameof(Rsrcs.strEmoticonDisplayingAsEmojiToolTip),
										"Causes Best Chat to act as though an emoji were sent instead of an " +
										"emoticon.  Hover your mouse over the emoji to see what was actually sent and how to " +
										"sent that emoji back.  The emoji display option can allow that emoji to be animated " +
										"in which case your theme chooses the animation.", typeof(EmojiAnimationOpts))]
									displayAsEmoji,
								}

								public enum EmojiAnimationOpts : byte
								{
									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmojiAnimateNever), "Never " +
										"Animate Emoji", nameof(Rsrcs.strEmojiAnimateNeverToolTip), "Selecting this will " +
										"prevent animated emoji.  Some people find them distracting.",
										typeof(EmojiAnimationOpts))]
									neverAnimate,

									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmojiAnimateEndlessly),
										"Animate All Emoji", nameof(Rsrcs.strEmojiAnimateEndlesslyToolTip), "Causes "
										+ "all emoji to be animated endlessly.  Your theme will choose the animation.",
										typeof(EmojiAnimationOpts))]
									playAnimationsEndlessly,

									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmojiAnimateLimit), "Limit Emoji " +
										"Animation", nameof(Rsrcs.strEmojiAnimateLimitToolTip), "This is a cross between the " +
										"other two.  The emoji will animate, but stop after a couple loops through the " +
										"animation.  Your theme selects the animation.", typeof(EmojiAnimationOpts))]
									playLimitAnimations,
								}
							#endregion

							#region Members
								private readonly DataAndExt.Prefs.Item<SendingEmojiOpts> sendingEmoji;

								private readonly DataAndExt.Prefs.Item<SendingEmoticonsOpts> sendingEmoticons;

								private readonly DataAndExt.Prefs.Item<DisplayingEmojiOpts> displayingEmoji;

								private readonly DataAndExt.Prefs.Item<DisplayingEmoticonsOpts> displayingEmoticons;

								private readonly DataAndExt.Prefs.Item<bool> makeEmojiOnlyPostsBigger;

								private readonly DataAndExt.Prefs.Item<EmojiAnimationOpts> emojiAnimation;
							#endregion

							#region Properties
								public DataAndExt.Prefs.Item<SendingEmojiOpts> SendingEmoji
									=> sendingEmoji;

								public DataAndExt.Prefs.Item<SendingEmoticonsOpts> SendingEmoticons
									=> sendingEmoticons;

								public DataAndExt.Prefs.Item<DisplayingEmojiOpts> DisplayingEmoji
									=> displayingEmoji;

								public DataAndExt.Prefs.Item<DisplayingEmoticonsOpts> DisplayingEmoticons
									=> displayingEmoticons;

								public DataAndExt.Prefs.Item<bool> MakeEmojiOnlyPostsBigger
									=> makeEmojiOnlyPostsBigger;

								public DataAndExt.Prefs.Item<EmojiAnimationOpts> EmojiAnimation
									=> emojiAnimation;
							#endregion

							#region Methods
								internal DTO.RootDTO.GlobalDTO.AppearanceDTO.EmojiDTO ToDTO()
									=> new(
										sendingEmoji.CurVal,
										sendingEmoticons.CurVal,
										displayingEmoji.CurVal,
										displayingEmoticons.CurVal,
										makeEmojiOnlyPostsBigger.CurVal,
										emojiAnimation.CurVal
									);
							#endregion

							#region Event Handlers
							#endregion
						}

						public class AnimationPrefs : DataAndExt.Prefs.AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public AnimationPrefs(AppearancePrefs mgrParent) :
									base(mgrParent, "Animation", Rsrcs.strGlobalAppearanceAnimationTitle, Rsrcs
										.strGlobalAppearanceAnimationDesc)
								{
									gifs = new(this, "GIF", Rsrcs
										.strGlobalAppearanceAnimationGifTitle, Rsrcs.strGlobalAppearanceAnimationGifDesc,
										GifAnimationOpts.once);
									avatars = new(this, "Avatars", Rsrcs
										.strGlobalAppearanceAnimationAvatarsTitle, Rsrcs
										.strGlobalAppearanceAnimationAvatarsDesc, GifAnimationOpts.neverPlay);
									resumeOnMouseOver = new(this, "Resume on Mouse Over", Rsrcs
										.strGlobalAppearanceAnimationResumeOnMouseOverTitle, Rsrcs
										.strGlobalAppearanceAnimationResumeOnMouseOverDesc, true);
								}

								internal AnimationPrefs(AppearancePrefs mgrParent, DTO.RootDTO.GlobalDTO.AppearanceDTO
										.AnimationDTO dto) :
									base(mgrParent, "Animation", Rsrcs.strGlobalAppearanceAnimationTitle, Rsrcs
										.strGlobalAppearanceAnimationDesc)
								{
									gifs = new(this, "GIF", Rsrcs
										.strGlobalAppearanceAnimationGifTitle, Rsrcs.strGlobalAppearanceAnimationGifDesc,
										GifAnimationOpts.once, GifAnimationOpts.once);
									avatars = new(this, "Avatars", Rsrcs
										.strGlobalAppearanceAnimationAvatarsTitle, Rsrcs
										.strGlobalAppearanceAnimationAvatarsDesc, GifAnimationOpts.neverPlay, dto.Avatars);
									resumeOnMouseOver = new(this, "Resume on Mouse Over", Rsrcs
										.strGlobalAppearanceAnimationResumeOnMouseOverTitle, Rsrcs
										.strGlobalAppearanceAnimationResumeOnMouseOverDesc, true, dto.ResumeOnMouseOver);
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
							#endregion

							#region Constants
							#endregion

							#region Helper Types
								public enum GifAnimationOpts
								{
									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strGifAnimationNever), "Never",
										nameof(Rsrcs.strGifAnimationNeverToolTip), "Prevents GIFs from being" +
										" animated.  This has no effect on any other animations.  However, an option below " +
										"starts the animation if you mouse over the object.", typeof(GifAnimationOpts))]
									neverPlay,

									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strGifAnimationOnce), "Once",
										nameof(Rsrcs.strGifAnimationOnceToolTip), "Plays the GIF animation once, " +
										"but only once.", typeof(GifAnimationOpts))]
									once,

									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strGifAnimationTwice), "Twice",
										nameof(Rsrcs.strGifAnimationTwiceToolTip), "Plays the GIF animation twice " +
										"and then stops the animation.", typeof(GifAnimationOpts))]
									twice,

									[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strGifAnimationContinuously),
										"Continuously", nameof(Rsrcs.strGifAnimationContinuouslyToolTip),
										"Plays the GIF animation endlessly.", typeof(GifAnimationOpts))]
									continuously,
								}
							#endregion

							#region Members
								private readonly DataAndExt.Prefs.Item<GifAnimationOpts> gifs;

								private readonly DataAndExt.Prefs.Item<GifAnimationOpts> avatars;

								private readonly DataAndExt.Prefs.Item<bool> resumeOnMouseOver;
							#endregion

							#region Properties
								public DataAndExt.Prefs.Item<GifAnimationOpts> GIFs
									=> gifs;

								public DataAndExt.Prefs.Item<GifAnimationOpts> Avatars
									=> avatars;

								public DataAndExt.Prefs.Item<bool> ResumeOnMouseOver
									=> resumeOnMouseOver;
							#endregion

							#region Methods
								internal DTO.RootDTO.GlobalDTO.AppearanceDTO.AnimationDTO ToDTO()
									=> new(
										gifs.CurVal,
										avatars.CurVal,
										resumeOnMouseOver.CurVal
									);
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						private readonly FontPrefs fonts;

						private readonly EmojiPrefs emoji;

						private readonly AnimationPrefs animation;

						private readonly DataAndExt.Prefs.Item<bool> hyphenateLongWords;

						private readonly DataAndExt.Prefs.Item<bool> recognizeLinks;

						private readonly DataAndExt.Prefs.Item<bool> displayCtrlChars;

						private readonly DataAndExt.Prefs.Item<bool> useTypographicalQuotes;
					#endregion

					#region Properties
						public FontPrefs Fonts
							=> fonts;

						public EmojiPrefs Emoji
							=> emoji;

						public AnimationPrefs Animation
							=> animation;

						public DataAndExt.Prefs.Item<bool> HyphenateLongWords
							=> hyphenateLongWords;

						public DataAndExt.Prefs.Item<bool> RecognizeLinks
							=> recognizeLinks;

						public DataAndExt.Prefs.Item<bool> DisplayCtrlChars
							=> displayCtrlChars;

						public DataAndExt.Prefs.Item<bool> UseTypographicalQuotes
							=> useTypographicalQuotes;
					#endregion

					#region Methods
						internal DTO.RootDTO.GlobalDTO.AppearanceDTO ToDTO()
							=> new(
								ConfMode.ToDTO(),
								TimeStamp.ToDTO(),
								UserList.ToDTO(),
								fonts.ToDTO(),
								emoji.ToDTO(),
								animation.ToDTO(),
								hyphenateLongWords.CurVal,
								recognizeLinks.CurVal,
								displayCtrlChars.CurVal,
								useTypographicalQuotes.CurVal,
								MsgGroups.ToDTO()
							);
					#endregion

					#region Event Handlers
					#endregion
				}

				public class CompositionPrefs : DataAndExt.Prefs.AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public CompositionPrefs(GlobalPrefs mgrParent) :
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

						internal CompositionPrefs(GlobalPrefs mgrParent, DTO.RootDTO.GlobalDTO.CompositionDTO dto) :
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
			#endregion

			#region Members
				private readonly AppearancePrefs appearance;

				private readonly CompositionPrefs composition;
			#endregion

			#region Properties
				public override AppearancePrefs Appearance
					=> appearance;

				public CompositionPrefs Composition
					=> composition;
			#endregion

			#region Methods
				internal DTO.RootDTO.GlobalDTO ToDTO()
					=> new(
						appearance.ToDTO(),
						Plugins.ToDTO(),
						composition.ToDTO()
					);
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