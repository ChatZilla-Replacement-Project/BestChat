using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public class NetNotifyWhenOnlinePrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public NetNotifyWhenOnlinePrefs(in NetPrefsBase mgrParent) :
			base(mgrParent, "Notify When Online", PrefsRsrcs.strNetNotifyWhenOnlineTitle,
				PrefsRsrcs.strNetNotifyWhenOnlineDesc)
			=> entries = new(
				this,
				"Notify when online",
				PrefsRsrcs.strNetNotifyTitle,
				PrefsRsrcs.strNetNotifyDesc,
				[],
				KeyObtainer,
				(
						notifyEntry,
						evth)
					=> notifyEntry.evtWhatToFollowChanged += mapNotifyHandlers[evth] = (
							in NotifyWhenOnlineOneNotify notifySender, in string strVal,
							in string _)
						=> evth(strVal, notifyEntry),
				(
						notifyEntry,
						evth)
					=>
						{
							notifyEntry.evtWhatToFollowChanged -= mapNotifyHandlers[evth];

							mapNotifyHandlers.Remove(evth);
						}
			);

		public NetNotifyWhenOnlinePrefs(in NetPrefsBase mgrParent, in string[]? dto) :
			base(mgrParent, "Notify When Online", PrefsRsrcs.strNetNotifyWhenOnlineTitle,
				PrefsRsrcs.strNetNotifyWhenOnlineDesc)
			=> entries = new(
				this,
				"Notify when online",
				PrefsRsrcs.strNetNotifyTitle,
				PrefsRsrcs.strNetNotifyDesc,
				[],
				dto?.Select(strCurNotifyWhatToFollow
					=> new NotifyWhenOnlineOneNotify(strCurNotifyWhatToFollow, this)
				) ?? [],
				KeyObtainer,
				(
						notifyEntry,
						evth)
					=> notifyEntry.evtWhatToFollowChanged += mapNotifyHandlers[evth] = (
							in NotifyWhenOnlineOneNotify notifySender, in string strVal,
							in string _)
						=> evth(strVal, notifyEntry),
				(
						notifyEntry,
						evth)
					=>
						{
							notifyEntry.evtWhatToFollowChanged -= mapNotifyHandlers[evth];

							mapNotifyHandlers.Remove(evth);
						}
			);
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
		private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, NotifyWhenOnlineOneNotify> entries;
	#endregion

	#region Properties
		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, NotifyWhenOnlineOneNotify> Entries
			=> entries;

		private readonly
			System.Collections.Generic.Dictionary<System.Action<string, NotifyWhenOnlineOneNotify>, Platform.DataAndExt
				.Obj<NotifyWhenOnlineOneNotify>.DFieldChanged<string>> mapNotifyHandlers = [];
	#endregion

	#region Methods
		public string[]? ToDTO()
			=> [..
				entries.Values.Select(notifyCur
					=> notifyCur.WhatToFollow),
			];

		private static string KeyObtainer(NotifyWhenOnlineOneNotify notifyVal)
			=> notifyVal.WhatToFollow;
	#endregion

	#region Event Handlers
	#endregion
}