namespace BestChat.Platform.UI.Desktop.Prefs;

public class GlobalAppearancePrefs : DataAndExt.Prefs.GlobalAppearancePrefsBase
{
	#region Constructors & Deconstructors
		internal GlobalAppearancePrefs(GlobalPrefs mgrParent) :
			base(mgrParent)
		{
			fonts = new(this);
			emoji = new(this);
			animation = new(this);
			userList = new((DataAndExt.Prefs.AbstractMgr)this);
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

		internal GlobalAppearancePrefs(GlobalPrefs mgrParent, DTO.RootDTO.GlobalDTO.AppearanceDTO dto) :
			base(mgrParent, dto)
		{
			fonts = new(this, dto.Fonts);
			emoji = new(this, dto.Emoji);
			animation = new(this, dto.Animation);
			userList = new(this, dto.UserList);
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
	#endregion

	#region Members
		private readonly GlobalAppearanceFontPrefs fonts;

		private readonly GlobalAppearanceEmojiPrefs emoji;

		private readonly GlobalAppearanceAnimationPrefs animation;

		private readonly GlobalAppearanceUserListPrefs userList;

		private readonly DataAndExt.Prefs.Item<bool> hyphenateLongWords;

		private readonly DataAndExt.Prefs.Item<bool> recognizeLinks;

		private readonly DataAndExt.Prefs.Item<bool> displayCtrlChars;

		private readonly DataAndExt.Prefs.Item<bool> useTypographicalQuotes;
	#endregion

	#region Properties
		public GlobalAppearanceFontPrefs Fonts
			=> fonts;

		public GlobalAppearanceEmojiPrefs Emoji
			=> emoji;

		public GlobalAppearanceAnimationPrefs Animation
			=> animation;

		public GlobalAppearanceUserListPrefs UserList
			=> userList;

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
				userList.ToDTO(),
				fonts.ToDTO(),
				emoji.ToDTO(),
  			animation.ToDTO(),
        MsgGroups.ToDTO(),
				hyphenateLongWords.CurVal,
				recognizeLinks.CurVal,
				displayCtrlChars.CurVal,
				useTypographicalQuotes.CurVal
			);
	#endregion

	#region Event Handlers
	#endregion
}