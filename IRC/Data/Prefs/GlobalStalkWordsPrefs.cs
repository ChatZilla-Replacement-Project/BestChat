using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public class GlobalStalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr, IStalkWordsPrefs
{
	#region Constructors & Deconstructors
		public GlobalStalkWordsPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent, "Stalk Words", PrefsRsrcs.strGlobalStalkWordsTitle, PrefsRsrcs
				.strGlobalStalkWordsDesc)
			=> entries = new(
				this,
				"Entries",
				PrefsRsrcs.strGlobalStalkWordsTitle,
				PrefsRsrcs.strGlobalStalkWordsDesc,
				[],
				val
					=> val.Ctnts,
				(swEntry,
						evth)
					=> swEntry.evtCtntsChanged += mapStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord swEntry, in string
							strOldCtnts, in string _)
						=> evth(strOldCtnts, swEntry),
				(swEntry,
						evth)
					=>
				{
					swEntry.evtCtntsChanged -= mapStalkWordHandlers[evth];

					mapStalkWordHandlers.Remove(evth);
				}
			);

		public GlobalStalkWordsPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO
				.GlobalStalkWordsOneStalkWordDTO[]? dto)
			: base(mgrParent, "Stalk Words", PrefsRsrcs.strGlobalStalkWordsTitle, PrefsRsrcs
				.strGlobalStalkWordsDesc)
			=> entries = new(
				this,
				"Entries",
				PrefsRsrcs.strGlobalStalkWordsTitle,
				PrefsRsrcs.strGlobalStalkWordsDesc,
				[],
				dto?.Select(dswCUr
					=> new GlobalStalkWordsOneStalkWord(dswCUr, this))
				?? [],
				val
					=> val.Ctnts,
				(swEntry,
						evth)
					=> swEntry.evtCtntsChanged += mapStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord swEntry, in string
							strOldCtnts, in string _)
						=> evth(strOldCtnts, swEntry),
				(swEntry,
						evth)
					=>
				{
					swEntry.evtCtntsChanged -= mapStalkWordHandlers[evth];

					mapStalkWordHandlers.Remove(evth);
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
		private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalStalkWordsOneStalkWord> entries;

		private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalStalkWordsOneStalkWord>, Platform.DataAndExt
			.Obj<GlobalStalkWordsOneStalkWord>.DFieldChanged<string>> mapStalkWordHandlers = [];
	#endregion

	#region Properties
		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalStalkWordsOneStalkWord> Entries
			=> entries;
	#endregion

	#region Methods
		public DTO.GlobalStalkWordsOneStalkWordDTO[]? ToDTO()
			=> entries.Values.Select(swCur =>
				swCur.ToDTO()
			).ToArray();
	#endregion

	#region Event Handlers
	#endregion
}

public interface IStalkWordsPrefs
{
	Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalStalkWordsOneStalkWord> Entries
	{
		get;
	}
}