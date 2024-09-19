// Ignore Spelling: evt Prefs

namespace BestChat.Platform.DataAndExt.Prefs;

public class MappedObjListItem<KeyType, EntryType> : MappedListItem<KeyType, EntryType>
	where EntryType : Obj<EntryType>
	where KeyType : notnull
{
	public MappedObjListItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
			string strLocalizedLongDesc, System.Collections.Generic.IEnumerable<EntryType> def, in System
			.Func<EntryType, KeyType> funcKeyObtainer, bool bSort = false)
		: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, def, funcKeyObtainer, bSort
			? new System.Collections.Generic.SortedDictionary<KeyType, EntryType>()
			: new System.Collections.Generic.Dictionary<KeyType, EntryType>())
	{
	}

	public MappedObjListItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
			string strLocalizedLongDesc, System.Collections.Generic.IEnumerable<EntryType> def, in System.Collections
			.Generic.IEnumerable<EntryType> val, in System.Func<EntryType, KeyType> funcKeyObtainer, bool bSort = false)
		: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, def, val, funcKeyObtainer, bSort
			? new System.Collections.Generic.SortedDictionary<KeyType, EntryType>()
			: new System.Collections.Generic.Dictionary<KeyType, EntryType>())
	{
	}
	
	private readonly System.Collections.Generic.Dictionary<System.Guid, EntryType> mapGuidToEntry =
		[];

	public System.Collections.Generic.IEnumerable<System.Guid> GuidKeys
		=> ((System.Collections.Generic.IReadOnlyDictionary<System.Guid, EntryType>)mapGuidToEntry).Keys;

	public override bool ContainsKey(System.Guid guidKey)
		=> mapGuidToEntry.ContainsKey(guidKey);

	public override void Clear()
	{
		base.Clear();

		mapGuidToEntry.Clear();
	}

	protected override void OnNewEntry(EntryType entryNew)
	{
		entryNew.evtDirtyChanged += OnEntryDirtyChanged;
		mapGuidToEntry[entryNew.guid] = entryNew;
	}

	protected override void OnEntryRemoved(EntryType entryDeleted)
	{
		entryDeleted.evtDirtyChanged -= OnEntryDirtyChanged;
		mapGuidToEntry.Remove(entryDeleted.guid);
	}

	private void OnEntryDirtyChanged(in EntryType objSender, in bool bIsNowDirty)
	{
		if(bIsNowDirty)
			MakeDirty();
	}

	public EntryType this[System.Guid guidKey]
		=> ((System.Collections.Generic.IReadOnlyDictionary<System.Guid, EntryType>)mapGuidToEntry)[guidKey];

	public void CopyTo(System.Collections.Generic.KeyValuePair<System.Guid, EntryType>[] array, int
			iArrayIndex)
		=> ((System.Collections.ICollection)mapGuidToEntry).CopyTo(array, iArrayIndex);
}