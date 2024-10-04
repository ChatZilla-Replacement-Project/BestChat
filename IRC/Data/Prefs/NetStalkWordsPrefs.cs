using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public class NetStalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr, IStalkWordsPrefs
{
	#region Constructors & Deconstructors
		public NetStalkWordsPrefs(NetPrefsBase mgrParent, GlobalStalkWordsPrefs inheritedSettings) :
			base(mgrParent, "Stalk words", PrefsRsrcs.strNetStalkWordsTitle, PrefsRsrcs.strNetStalkWordsDesc)
		{
			this.mgrParent = mgrParent;
			this.inheritedSettings = inheritedSettings;

			System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord,
				IReadOnlyOneStalkWord>> defInheritedEntries = inheritedSettings.Entries.Values.Select(iswCur
					=> new NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur));
			mapAllInheritanceOverridesByCtnts = new(
				this,
				"Stalk words from global settings",
				PrefsRsrcs.strNetStalkWordsInheritedTitle,
				PrefsRsrcs.strNetStalkWordsInheritedDesc,
				defInheritedEntries,
				KeyObtainer,
				(inherited,
						evth)
					=> inherited.evtKeyOfInheritedItemChanged += mapOverrideHandlers[evth] = (in
							NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord> inherited, in string
							strOldCtnts, in string _)
						=> evth(strOldCtnts, inherited),
				(inherited,
						evth)
					=>
				{
					inherited.evtKeyOfInheritedItemChanged -= mapOverrideHandlers[evth];

					mapOverrideHandlers.Remove(evth);
				}
			);

			addedStalkWords = new(
				this,
				"Lists more stalk words specific to this network",
				PrefsRsrcs.strNetStalkWordsAdditionalTitle,
				PrefsRsrcs.strNetStalkWordsAdditionalDesc,
				[],
				KeyObtainer,
				(swEntry,
						evth)
					=> swEntry.evtCtntsChanged += mapAddedStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord _, in string
							strNewCtnts, in string _)
						=> evth(strNewCtnts, swEntry),
				(swEntry,
						evth)
					=>
				{
					swEntry.evtCtntsChanged -= mapAddedStalkWordHandlers[evth];

					mapAddedStalkWordHandlers.Remove(evth);
				}
			);
		}

		public NetStalkWordsPrefs(NetPrefsBase mgrParent, DTO.NetStalkWordsDTO dto,GlobalStalkWordsPrefs inheritedSettings)
			: base(mgrParent, "Stalk words", PrefsRsrcs.strNetStalkWordsTitle, PrefsRsrcs.strNetStalkWordsDesc)
		{
			this.mgrParent = mgrParent;
			this.inheritedSettings = inheritedSettings;


			System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord,
				IReadOnlyOneStalkWord>> defInheritedEntries = inheritedSettings.Entries.Values.Select(iswCur
					=> new NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur));
			System.Collections.Generic.IEnumerable<NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord,
				IReadOnlyOneStalkWord>> enabledAltNicks = inheritedSettings.Entries.Values.Select(iswCur
					=> new NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur, dto
						.DisabledInheritedStalkWords?.Contains(iswCur.guid) ?? false)
				);
			mapAllInheritanceOverridesByCtnts = new(
				this,
				"Stalk words from global settings",
				PrefsRsrcs.strNetStalkWordsInheritedTitle,
				PrefsRsrcs.strNetStalkWordsInheritedDesc,
				defInheritedEntries,
				enabledAltNicks,
				KeyObtainer,
				(
						inherited,
						evth)
					=> inherited.evtKeyOfInheritedItemChanged += mapOverrideHandlers[evth] = (in
							NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord> _, in string
							strOldCtnts, in string _)
						=> evth(strOldCtnts, inherited),
				(inherited,
						evth)
					=>
				{
					inherited.evtKeyOfInheritedItemChanged -= mapOverrideHandlers[evth];

					mapOverrideHandlers.Remove(evth);
				}
			);

			addedStalkWords = new(
				this,
				"Lists more stalk words specific to this network",
				PrefsRsrcs.strNetStalkWordsAdditionalTitle,
				PrefsRsrcs.strNetStalkWordsAdditionalDesc,
				[],
				dto?.AddedStalkWords?.Select(dswCur
					=> new GlobalStalkWordsOneStalkWord(dswCur, this)
				) ?? [],
				KeyObtainer,
				(
						swEntry,
						evth)
					=> swEntry.evtCtntsChanged += mapAddedStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord _, in string strNewCtnts, in string _)
						=> evth(strNewCtnts, swEntry),
				(swEntry,
						evth)
					=>
				{
					swEntry.evtCtntsChanged -= mapAddedStalkWordHandlers[evth];

					mapAddedStalkWordHandlers.Remove(evth);
				}
			);
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

		public readonly GlobalStalkWordsPrefs inheritedSettings;


		private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string,
			NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>>
			mapAllInheritanceOverridesByCtnts;

		private readonly System.Collections.Generic.Dictionary<System.Action<string,
			NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>>,
			NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.DFieldChanged<string>>
			mapOverrideHandlers = [];

		private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalStalkWordsOneStalkWord>
			addedStalkWords;

		private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalStalkWordsOneStalkWord>,
			GlobalStalkWordsOneStalkWord.DFieldChanged<string>> mapAddedStalkWordHandlers = [];
	#endregion

	#region Properties
		public Platform.DataAndExt.Prefs.MappedObjListItem<string,
			NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>> AllInheritanceOverridesByCtnts
				=> mapAllInheritanceOverridesByCtnts;

		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalStalkWordsOneStalkWord>
			AddedStalkWords
			=> addedStalkWords;

		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, GlobalStalkWordsOneStalkWord> Entries
			=> addedStalkWords;
	#endregion

	#region Methods
		public DTO.NetStalkWordsDTO ToDTO()
			=> new(
				(
					from NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord> iswCur
						in mapAllInheritanceOverridesByCtnts.Values
					where !iswCur.Status
					select iswCur.inheritedItem.guid
				).ToArray(),
				addedStalkWords.Values.Select(swCur
					=> new DTO.GlobalStalkWordsOneStalkWordDTO(swCur.guid, swCur.Ctnts)
				).ToArray()
			);

		private static string KeyObtainer(NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord,
				IReadOnlyOneStalkWord> iswCur)
			=> iswCur.InheritedItem.Ctnts;

		private static string KeyObtainer(GlobalStalkWordsOneStalkWord swCur)
			=> swCur.Ctnts;
	#endregion

	#region Event Handlers
	#endregion
}