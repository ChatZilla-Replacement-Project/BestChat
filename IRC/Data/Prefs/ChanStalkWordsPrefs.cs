using System.Linq;

namespace BestChat.IRC.Data.Prefs
{
public class ChanStalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
	public ChanStalkWordsPrefs(ChanPrefs mgrParent, NetStalkWordsPrefs inheritedSettings) :
		base(mgrParent, "Stalk words for this channel", PrefsRsrcs
			.strNetChanStalkWordsInheritedTitle, PrefsRsrcs.strNetChanStalkWordsInheritedDesc)
	{
		this.inheritedSettings = inheritedSettings;

		System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>> listDefEnabledAliases = inheritedSettings
			.AllInheritanceOverridesByCtnts.Values.Select(iswCur
				=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur.inheritedItem, iswCur.Status,
					ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>
						.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText, inheritedSettings.mgrParent
						.OwnerNet)
			).ToList();
		listDefEnabledAliases.AddRange(inheritedSettings.AddedStalkWords.Values.Select(iswCur
			=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur, true,
				ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.InheritedFromTypes.network, PrefsRsrcs.strAliasText,
				inheritedSettings.mgrParent.OwnerNet)));
		mapAllInheritanceOverridesByName = new(
			this,
			"Inherited aliases",
			PrefsRsrcs.strNetChanAutoPerformInheritedTitle,
			PrefsRsrcs.strNetChanAutoPerformInheritedDesc,
			listDefEnabledAliases,
			KeyObtainer,
			(inherited, evth)
				=> inherited.inheritedItem.evtCtntsChanged += mapOverrideHandlers[evth] = (in GlobalStalkWordsOneStalkWord _, in string strOldVal, in string _)
					=> evth(strOldVal, inherited),
			(inherited,
					evth)
				=>
			{
				inherited.inheritedItem.evtCtntsChanged -= mapOverrideHandlers[evth];

				mapOverrideHandlers.Remove(evth);
			}
		);

		addedStalkWords = new(
			this,
			"Additional Stalk Words",
			PrefsRsrcs.strNetChanStalkWordsAddedTitle,
			PrefsRsrcs.strNetChanStalkWordsAddedDesc,
			[],
			KeyObtainer,
			(inherited,
					evth)
				=> inherited.evtCtntsChanged += mapAddedStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord _, in string strOldVal, in string _)
					=> evth(strOldVal, inherited),
			(inherited,
					evth)
				=>
			{
				inherited.evtCtntsChanged -= mapAddedStalkWordHandlers[evth];

				mapAddedStalkWordHandlers.Remove(evth);
			},
			true
		);
	}

	public ChanStalkWordsPrefs(ChanPrefs mgrParent, DTO.NetStalkWordsDTO dto, NetStalkWordsPrefs inheritedSettings) :
		base(mgrParent, "Stalk words for this channel", PrefsRsrcs
			.strNetChanStalkWordsInheritedTitle, PrefsRsrcs.strNetChanStalkWordsInheritedDesc)
	{
		this.inheritedSettings = inheritedSettings;

		System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>> listDefEnabledStalkWords = inheritedSettings
			.AllInheritanceOverridesByCtnts.Values.Select(iswCur
				=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur.inheritedItem, iswCur.Status,
					ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.InheritedFromTypes.global, PrefsRsrcs
						.strStalkWordsText, inheritedSettings.mgrParent.OwnerNet)
			).ToList();
		listDefEnabledStalkWords.AddRange(inheritedSettings.AddedStalkWords.Values.Select(iswCur
			=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur, true,
				ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.InheritedFromTypes.network, PrefsRsrcs.strAliasText,
				inheritedSettings.mgrParent.OwnerNet)));
		System.Collections.Generic.List<ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>> listEnabledInheritedStalkWords =
			inheritedSettings.AllInheritanceOverridesByCtnts.Values.Select(iswCur
				=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur.inheritedItem, !dto?
						.DisabledInheritedStalkWords?.Contains(iswCur.inheritedItem.guid) ?? iswCur.Status,
					ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.InheritedFromTypes.global, PrefsRsrcs.strStalkWordsText,
					inheritedSettings.mgrParent.OwnerNet)
			).ToList();
		foreach(ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord> ialiasCur in listEnabledInheritedStalkWords)
			ialiasCur.evtDirtyChanged += (in NetInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord> _, in bool bNowDirty)
				=>
			{
				if(bNowDirty)
					MakeDirty();
			};
		listEnabledInheritedStalkWords.AddRange(inheritedSettings.AddedStalkWords.Values
			.Select(iswCur
				=> new ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>(iswCur, !dto?.DisabledInheritedStalkWords?
						.Contains(iswCur.guid) ?? true, ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>.InheritedFromTypes.network,
					PrefsRsrcs.strAliasText, inheritedSettings.mgrParent.OwnerNet)));
		mapAllInheritanceOverridesByName = new(
			this,
			"Inherited aliases",
			PrefsRsrcs.strNetChanAliasInheritedTitle,
			PrefsRsrcs.strNetChanAliasInheritedDesc,
			listDefEnabledStalkWords,
			listEnabledInheritedStalkWords,
			KeyObtainer,
			(inherited, evth)
				=> inherited.inheritedItem.evtCtntsChanged += mapOverrideHandlers[evth] = (in GlobalStalkWordsOneStalkWord _, in string strOldVal, in string _)
					=> evth(strOldVal, inherited),
			(inherited,
					evth)
				=>
			{
				inherited.inheritedItem.evtCtntsChanged -= mapOverrideHandlers[evth];

				mapOverrideHandlers.Remove(evth);
			}
		);

		addedStalkWords = new(
			this,
			"Additional Stalk Words",
			PrefsRsrcs.strNetChanStalkWordsAddedTitle,
			PrefsRsrcs.strNetChanStalkWordsAddedDesc,
			[],
			KeyObtainer,
			(inherited,
					evth)
				=> inherited.evtCtntsChanged += mapAddedStalkWordHandlers[evth] = (in GlobalStalkWordsOneStalkWord _, in string strOldVal, in string _)
					=> evth(strOldVal, inherited),
			(inherited,
					evth)
				=>
			{
				inherited.evtCtntsChanged -= mapAddedStalkWordHandlers[evth];

				mapAddedStalkWordHandlers.Remove(evth);
			},
			true);
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
	public readonly NetStalkWordsPrefs inheritedSettings;


	private readonly Platform.DataAndExt.Prefs.MappedListItem<string, ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>>
		mapAllInheritanceOverridesByName;

	private readonly System.Collections.Generic.Dictionary<System.Action<string,
			ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>>, GlobalStalkWordsOneStalkWord.DFieldChanged<string>>
		mapOverrideHandlers = [];

	private readonly Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalStalkWordsOneStalkWord> addedStalkWords;

	private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalStalkWordsOneStalkWord>, GlobalStalkWordsOneStalkWord.DFieldChanged<string>>
		mapAddedStalkWordHandlers = [];
	#endregion

	#region Properties
	public Platform.DataAndExt.Prefs.MappedListItem<string, ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord>>
		AllInheritanceOverridesByName
		=> mapAllInheritanceOverridesByName;


	public Platform.DataAndExt.Prefs.MappedObjListItem<string, GlobalStalkWordsOneStalkWord>
		AddedStalkWords
		=> addedStalkWords;
	#endregion

	#region Methods
	public DTO.NetStalkWordsDTO ToDTO()
		=> new(
			(from iswCur in mapAllInheritanceOverridesByName.Values
			where !iswCur.Status
			select iswCur.InheritedItem.GUID).ToArray()
		);

	private static string KeyObtainer(ChanInheritedItemEnabledStatus<GlobalStalkWordsOneStalkWord, IReadOnlyOneStalkWord> iswCur)
		=> iswCur.InheritedItem.Ctnts;

	private static string KeyObtainer(GlobalStalkWordsOneStalkWord swCur)
		=> swCur.Ctnts;
	#endregion

	#region Event Handlers
	#endregion
}
}