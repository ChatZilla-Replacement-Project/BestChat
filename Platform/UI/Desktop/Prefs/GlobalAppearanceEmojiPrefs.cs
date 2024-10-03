namespace BestChat.Platform.UI.Desktop.Prefs;

public class GlobalAppearanceEmojiPrefs : DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalAppearanceEmojiPrefs(GlobalAppearancePrefs mgrParent) :
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

		internal GlobalAppearanceEmojiPrefs(GlobalAppearancePrefs mgrParent, DTO.RootDTO.GlobalDTO.AppearanceDTO
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
				"clients.", typeof(GlobalAppearanceEmojiPrefs))]
			leaveAlone,

			[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmojiSendingConvertToEmoticons),
				"Convert to emotions", nameof(Rsrcs
				.strEmojiSendingConvertToEmoticonsToolTip), "Best Chat will convert " +
				"emoji you enter into emoticons.  This is safer as not all clients can correctly " +
				"handle emoji.", typeof(GlobalAppearanceEmojiPrefs))]
			convertToEmoticons,
		}

		public enum SendingEmoticonsOpts : byte
		{
			[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmoticonSendingLeaveAlone),
				"Leave Alone", nameof(Rsrcs.strEmoticonSendingLeaveAloneToolTip),
				"Not all clients handle emoji correctly.  This option ensure emoticons" +
				" go out as plain ASCII which they should be able to handle just fine.",
				typeof(GlobalAppearanceEmojiPrefs))]
			leaveAlone,

			[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmoticonSendingConvertToEmoji),
				"Convert to Emoji", nameof(Rsrcs.strEmoticonSendingConvertToEmojiToolTip),
				"Causes Best Chat to send out the emoticons you type as emoji.",
				typeof(GlobalAppearanceEmojiPrefs))]
			convertToEmoji,
		}

		public enum DisplayingEmojiOpts : byte
		{
			[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmojiDisplayingLeaveAlone),
				"Leave Alone", nameof(Rsrcs.strEmojiDisplayingLeaveAloneToolTip),
				"If you select this, Best Chat will display any emoji as received.  " +
				"Regardless, you'll be able to move your mouse over the emoji to see what was actually"
				+ " received and what you can enter to get the same emoji.", typeof(GlobalAppearanceEmojiPrefs))]
			leaveAlone,

			[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strEmojiDisplayingAsEmoticons),
				"Display as plain text emoticons", nameof(Rsrcs
				.strEmojiDisplayingAsEmoticonsToolTiop), "If you select this, " +
				"Best Chat will display any emoji you receive as though you received plain text emoji."
				+ "  Regardless, you'll be about to hover your mouse over the emoticon shown and see "
				+ "what actually arrived and how to send that emoji back.", typeof(GlobalAppearanceEmojiPrefs))]
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