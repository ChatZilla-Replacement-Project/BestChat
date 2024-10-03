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
				(_, _)
					=>
				{
				},
				(_, _)
					=>
				{
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
				dto ?? [],
				KeyObtainer,
				(_, _)
					=>
				{
				},
				(_, _)
					=>
				{
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
		private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, string> entries;
	#endregion

	#region Properties
		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, string> Entries
			=> entries;
	#endregion

	#region Methods
		public string[]? ToDTO()
			=> [.. entries.Values, ];

		private static string KeyObtainer(string strVal)
			=> strVal;
	#endregion

	#region Event Handlers
	#endregion
}