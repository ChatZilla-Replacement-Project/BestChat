// Ignore Spelling: Prefs Conf Ctrl

namespace BestChat.Platform.UI.Desktop.Prefs.DTO;

// ReSharper disable once InconsistentNaming
internal record RootDTO
(
	RootDTO.GlobalDTO Global
) : DataAndExt.Prefs.DTO.PrefsDTO
{
	// ReSharper disable once InconsistentNaming
	public new record GlobalDTO
	(
		GlobalDTO.AppearanceDTO Appearance,
		GlobalDTO.PluginsDTO Plugins,
		GlobalDTO.CompositionDTO Composition
	) : DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO(Plugins)
	{
		// ReSharper disable once InconsistentNaming
		public new record AppearanceDTO
		(
			AppearanceDTO.ConfModeDTO ConfMode,
			AppearanceDTO.TimeStampDTO TimeStamp,
			AppearanceDTO.UserListDTO UserList,
			AppearanceDTO.FontDTO Fonts,
			AppearanceDTO.EmojiDTO Emoji,
			AppearanceDTO.AnimationDTO Animation,
			AppearanceDTO.MsgGroupsDTO MsgGroups,
			bool HyphenateLongWords,
			bool RecognizeLinks,
			bool DisplayCtrlChars,
			bool UseTypographicalQuotes
		) : DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO.AppearanceDTO(ConfMode, TimeStamp, MsgGroups)
		{
			// ReSharper disable once InconsistentNaming
			public record FontDTO
			(
				FontDTO.OneFontBlockDTO AppFontData,
				FontDTO.OneFontBlockDTO ViewFontData
			) : DataAndExt.Prefs.AbstractMgr.AbstractDTO("Global/Appearance/Fonts")
			{
				// ReSharper disable once InconsistentNaming
				public record OneFontBlockDTO
				(
					OneFontBlockDTO.InfoPairDTO<Avalonia.Media.FontFamily> NormalFamily,
					OneFontBlockDTO.InfoPairDTO<Avalonia.Media.FontFamily> FixedFontFamily,
					OneFontBlockDTO.InfoPairDTO<double> Size,
					OneFontBlockDTO.InfoPairDTO<Avalonia.Media.FontWeight> Weight
				) : DataAndExt.Prefs.AbstractMgr.AbstractDTO("Global/Appearance/Fonts/OneFontBlock")
				{
					// ReSharper disable once InconsistentNaming
					public record InfoPairDTO<FieldType>
					(
						bool IsOverridden,
						FieldType? OverriddenVal
					) : DataAndExt.Prefs.AbstractMgr.AbstractDTO("Global/Appearance/Fonts/OneFontBlock/InfoPair");
				}
			}

			// ReSharper disable once InconsistentNaming
			public record EmojiDTO
			(
				GlobalAppearanceEmojiPrefs.SendingEmojiOpts SendingEmoji,
				GlobalAppearanceEmojiPrefs.SendingEmoticonsOpts SendingEmoticons,
				GlobalAppearanceEmojiPrefs.DisplayingEmojiOpts DisplayingEmoji,
				GlobalAppearanceEmojiPrefs.DisplayingEmoticonsOpts DisplayingEmoticons,
				bool MakeEmojiOnlyPostsBigger,
				GlobalAppearanceEmojiPrefs.EmojiAnimationOpts EmojiAnimation
			);

			// ReSharper disable once InconsistentNaming
			public record AnimationDTO
			(
				// ReSharper disable once InconsistentNaming
				GlobalAppearanceAnimationPrefs.GifAnimationOpts GIFs,
				GlobalAppearanceAnimationPrefs.GifAnimationOpts Avatars,
				bool ResumeOnMouseOver
			) : DataAndExt.Prefs.AbstractMgr.AbstractDTO("Global/Appearance/Animation");

			// ReSharper disable once InconsistentNaming
			public record UserListDTO
			(
				GlobalAppearanceUserListPrefs.UserListPaneLocs Loc = GlobalAppearanceUserListPrefs.UserListPaneLocs.left,
				GlobalAppearanceUserListPrefs.WaysToShowUserModes HowToShowModes = GlobalAppearanceUserListPrefs
					.WaysToShowUserModes.symbols,
				GlobalAppearanceUserListPrefs.SortOrders SortByMode = GlobalAppearanceUserListPrefs.SortOrders.nameOnly
			) : DataAndExt.Prefs.AbstractMgr.AbstractDTO("Global/Appearance/UserList");

			public FontDTO Fonts
			{
				get;

				private init;
			} = Fonts;

			public EmojiDTO Emoji
			{
				get;

				private init;
			} = Emoji;

			public AnimationDTO Animation
			{
				get;

				private init;
			} = Animation;

			public UserListDTO UserList
			{
				get;

				private init;
			} = UserList;

			public bool HyphenateLongWords
			{
				get;

				private init;
			} = HyphenateLongWords;

			public bool RecognizeLinks
			{
				get;

				private init;
			} = RecognizeLinks;

			public bool DisplayCtrlChars
			{
				get;

				private init;
			} = DisplayCtrlChars;

			public bool UseTypographicalQuotes
			{
				get;

				private init;
			} = UseTypographicalQuotes;
		}

		public record CompositionDTO
		(
			bool UseTypeographicalQuotes,
			bool TreatDblDashAsMDash,
			bool TreatThreePeriodsAsEllipsis,
			bool EnableEmojiShortCuts,
			bool EnableEntityShortCuts
		);

		public CompositionDTO Composition
		{
			get;

			private init;
		} = Composition;

		public AppearanceDTO Appearance
		{
			get;

			private init;
		} = Appearance;

		public override DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO.AppearanceDTO BaseAppearance
			=> Appearance;
	}

	public override DataAndExt.Prefs.DTO.PrefsDTO.GlobalDTO BaseGlobal
		=> Global;
}