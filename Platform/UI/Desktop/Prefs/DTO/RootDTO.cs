// Ignore Spelling: Prefs Conf Ctrl

namespace BestChat.Platform.UI.Desktop.Prefs.DTO;

internal record RootDTO
(
	RootDTO.GlobalDTO Global
) : DataAndExt.Prefs.DTO.PrefsDTO
{
	public new record GlobalDTO : DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO
	{
		public GlobalDTO
		(
			in AppearanceDTO appearance,
			in PluginsDTO plugins,
			in CompositionDTO composition
		) :
			base(plugins)
		{
			Appearance = appearance;
			Composition = composition;
		}

		public new record AppearanceDTO : DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO.AppearanceDTO
		{
			public AppearanceDTO
			(
				ConfModeDTO confMode,
				TimeStampDTO timeStamp,
				UserListDTO userList,
				FontDTO fonts,
				EmojiDTO emoji,
				AnimationDTO animation,
				bool bHyphenateLongWords,
				bool bRecognizeLinks,
				bool bDisplayCtrlChars,
				bool bUseTypographicalQuotes,
				MsgGroupsDTO msgGroups
			) : base(confMode, timeStamp, userList, msgGroups)
			{
				Fonts = fonts;
				Emoji = emoji;
				Animation = animation;
				HyphenateLongWords = bHyphenateLongWords;
				RecognizeLinks = bRecognizeLinks;
				DisplayCtrlChars = bDisplayCtrlChars;
				UseTypographicalQuotes = bUseTypographicalQuotes;
			}

			public record FontDTO
			(
				FontDTO.OneFontBlockDTO AppFontData,
				FontDTO.OneFontBlockDTO ViewFontData
			)
			{
				public record OneFontBlockDTO
				(
					OneFontBlockDTO.InfoPairDTO<Avalonia.Media.FontFamily> NormalFamily,
					OneFontBlockDTO.InfoPairDTO<Avalonia.Media.FontFamily> FixedFontFamily,
					OneFontBlockDTO.InfoPairDTO<double> Size,
					OneFontBlockDTO.InfoPairDTO<Avalonia.Media.FontWeight> Weight
				) : DataAndExt.Prefs.AbstractMgr.AbstractDTO("Global/Appearance/Fonts")
				{
					public record InfoPairDTO<FieldType>
					(
						bool IsOverridden,
						FieldType? OverriddenVal
					);
				}
			}

			public record EmojiDTO
			(
				RootPrefs.GlobalPrefs.AppearancePrefs.EmojiPrefs.SendingEmojiOpts SendingEmoji,
				RootPrefs.GlobalPrefs.AppearancePrefs.EmojiPrefs.SendingEmoticonsOpts SendingEmoticons,
				RootPrefs.GlobalPrefs.AppearancePrefs.EmojiPrefs.DisplayingEmojiOpts DisplayingEmoji,
				RootPrefs.GlobalPrefs.AppearancePrefs.EmojiPrefs.DisplayingEmoticonsOpts DisplayingEmoticons,
				bool MakeEmojiOnlyPostsBigger,
				RootPrefs.GlobalPrefs.AppearancePrefs.EmojiPrefs.EmojiAnimationOpts EmojiAnimation
			);

			public record AnimationDTO
			(
				RootPrefs.GlobalPrefs.AppearancePrefs.AnimationPrefs.GifAnimationOpts GIFs,
				RootPrefs.GlobalPrefs.AppearancePrefs.AnimationPrefs.GifAnimationOpts Avatars,
				bool ResumeOnMouseOver
			);

			public FontDTO Fonts
			{
				get;

				private init;
			}

			public EmojiDTO Emoji
			{
				get;

				private init;
			}

			public AnimationDTO Animation
			{
				get;

				private init;
			}

			public bool HyphenateLongWords
			{
				get;

				private init;
			}

			public bool RecognizeLinks
			{
				get;

				private init;
			}

			public bool DisplayCtrlChars
			{
				get;

				private init;
			}

			public bool UseTypographicalQuotes
			{
				get;

				private init;
			}
		}

		public record CompositionDTO
		(
			bool UseTypeographicalQuotes,
			bool TreatDblDashAsMDash,
			bool TreatThreePeriodsAsEllipsis,
			bool EnableEmojiShortCuts,
			bool EnableEntityShortCuts
		);

		public AppearanceDTO Appearance
		{
			get;

			init;
		}

		public CompositionDTO Composition
		{
			get;

			private init;
		}

		public override DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO.AppearanceDTO BaseAppearance
			=> Appearance;
	}

	public override DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO BaseGlobal => Global;
}