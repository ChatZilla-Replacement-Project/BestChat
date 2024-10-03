namespace BestChat.IRC.Data.Prefs;

public class GlobalAutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalAutoPerformPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
			: base(mgrParent, "Auto-perform", PrefsRsrcs.strGlobalAutoPerformTitle, PrefsRsrcs
				.strGlobalAutoPerformDesc)
		{
			whenStartingBestChat = new(this, "When Starting Best Chat", PrefsRsrcs
				.strGlobalAutoPerformWhenStartingBestChatTitle, PrefsRsrcs.strGlobalAutoPerformWhenStartingBestChatDesc);
			whenJoiningNet = new(this, "When Joining a Network", PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningNetTitle, PrefsRsrcs.strGlobalAutoPerformWhenJoiningNetDesc);
			whenJoiningChan = new(this, "When Joining a Channel", PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningChanTitle, PrefsRsrcs.strGlobalAutoPerformWhenJoiningChanDesc);
			whenOpeningUserChat = new(this, "When Opening a User Chat", PrefsRsrcs
				.strGlobalAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs.strGlobalAutoPerformWhenOpeningUserChatDesc);
		}

		public GlobalAutoPerformPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.GlobalAutoPerformDTO dto) :
			base(mgrParent, "Auto-perform", PrefsRsrcs.strGlobalAutoPerformTitle, PrefsRsrcs.strGlobalAutoPerformDesc)
		{
			whenStartingBestChat = new(this, "When Starting Best Chat", PrefsRsrcs
				.strGlobalAutoPerformWhenStartingBestChatTitle, PrefsRsrcs.strGlobalAutoPerformWhenStartingBestChatDesc, dto
				.WhenStartingBestChat);
			whenJoiningNet = new(this, "When Joining a Network", PrefsRsrcs.strGlobalAutoPerformWhenJoiningNetTitle,
				PrefsRsrcs.strGlobalAutoPerformWhenJoiningNetTitle, dto.WhenJoiningNet);
			whenJoiningChan = new(this, "When Joining a Channel", PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningChanTitle, PrefsRsrcs.strGlobalAutoPerformWhenJoiningChanDesc, dto
				.WhenJoiningChan);
			whenOpeningUserChat = new(this, "When Opening a User Chat", PrefsRsrcs
				.strGlobalAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs.strGlobalAutoPerformWhenOpeningUserChatDesc, dto
				.WhenOpeningUserChat);
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
		private readonly GlobalAutoPerformOnEvtPrefs whenStartingBestChat;

		private readonly GlobalAutoPerformOnEvtPrefs whenJoiningNet;

		private readonly GlobalAutoPerformOnEvtPrefs whenJoiningChan;

		private readonly GlobalAutoPerformOnEvtPrefs whenOpeningUserChat;
	#endregion

	#region Properties
		public GlobalAutoPerformOnEvtPrefs WhenStartingBestChat
			=> whenStartingBestChat;

		public GlobalAutoPerformOnEvtPrefs WhenJoiningNet
			=> whenJoiningNet;

		public GlobalAutoPerformOnEvtPrefs WhenJoiningChan
			=> whenJoiningChan;

		public GlobalAutoPerformOnEvtPrefs WhenOpeningUserChat
			=> whenOpeningUserChat;
	#endregion

	#region Methods
		public DTO.GlobalAutoPerformDTO ToDTO()
			=> new(
				whenStartingBestChat.ToDTO(),
				whenJoiningNet.ToDTO(),
				whenJoiningChan.ToDTO(),
				whenOpeningUserChat.ToDTO()
			);
	#endregion

	#region Event Handlers
	#endregion
}