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

	protected override void OnNewEntry(EntryType entryNew)
		=> entryNew.evtDirtyChanged += OnEntryDirtyChanged;

	protected override void OnEntryRemoved(EntryType entryDeleted)
		=> entryDeleted.evtDirtyChanged -= OnEntryDirtyChanged;

	private void OnEntryDirtyChanged(in EntryType objSender, in bool bIsNowDirty)
	{
		if(bIsNowDirty)
			MakeDirty();
	}
}