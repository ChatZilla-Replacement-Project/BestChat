namespace BestChat.IRC.Data.Prefs;

public class NetAutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public NetAutoPerformPrefs(NetPrefsBase mgrParent, GlobalAutoPerformPrefs inheritedSettings) :
			base(mgrParent, "Auto-perform", PrefsRsrcs.strNetAutoPerformTitle, PrefsRsrcs
				.strNetAutoPerformDesc)
		{
			this.mgrParent = mgrParent;

			whenJoiningNet = new(this, "When joining this network", PrefsRsrcs
				.strNetAutoPerformWhenJoiningNetTitle, PrefsRsrcs.strNetAutoPerformWhenJoiningNetDesc, inheritedSettings
				.WhenJoiningNet);
			whenJoiningChan = new(this, "When joining any channel on this network",
				PrefsRsrcs.strNetAutoPerformWhenJoiningChanTitle, PrefsRsrcs
					.strNetAutoPerformWhenJoiningChanDesc, inheritedSettings.WhenJoiningChan);
			whenOpeningUserChat = new(this, "When opening chat with any user on this" +
				" network", PrefsRsrcs.strNetAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
					.strNetAutoPerformWhenOpeningUserChatDesc, inheritedSettings.WhenOpeningUserChat);
		}

		public NetAutoPerformPrefs(NetPrefsBase mgrParent, DTO.NetAutoPerformDTO dto, GlobalAutoPerformPrefs inheritedSettings)
			: base(mgrParent, "Auto-perform", PrefsRsrcs.strNetAutoPerformTitle, PrefsRsrcs
				.strNetAutoPerformDesc)
		{
			this.mgrParent = mgrParent;

			whenJoiningNet = new(this, "When joining this network", PrefsRsrcs
					.strNetAutoPerformWhenJoiningNetTitle, PrefsRsrcs.strNetAutoPerformWhenJoiningNetDesc,
				dto.WhenJoiningNet, inheritedSettings.WhenJoiningNet);
			whenJoiningChan = new(this, "When joining any channel on this network",
				PrefsRsrcs.strNetAutoPerformWhenJoiningChanTitle, PrefsRsrcs
					.strNetAutoPerformWhenJoiningChanDesc, dto.WhenJoiningChan, inheritedSettings.WhenJoiningChan);
			whenOpeningUserChat = new(this, "When opening chat with any user on this" +
				" network", PrefsRsrcs.strNetAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
					.strNetAutoPerformWhenOpeningUserChatDesc, dto.WhenOpeningUserChat, inheritedSettings.WhenOpeningUserChat);
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
		public new readonly NetPrefsBase mgrParent;

		private readonly NetAutoPerformOnEvtPrefs whenJoiningNet;

		private readonly NetAutoPerformOnEvtPrefs whenJoiningChan;

		private readonly NetAutoPerformOnEvtPrefs whenOpeningUserChat;
	#endregion

	#region Properties
		public NetAutoPerformOnEvtPrefs WhenJoiningNet
			=> whenJoiningNet;

		public NetAutoPerformOnEvtPrefs WhenJoiningChan
			=> whenJoiningChan;

		public NetAutoPerformOnEvtPrefs WhenOpeningUserChat
			=> whenOpeningUserChat;
	#endregion

	#region Methods
		public DTO.NetAutoPerformDTO ToDTO()
			=> new(
				whenJoiningNet.ToDTO(),
				whenJoiningChan.ToDTO(),
				whenOpeningUserChat.ToDTO()
			);
	#endregion

	#region Event Handlers
	#endregion
}