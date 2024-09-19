// Ignore Spelling: Prefs

namespace BestChat.Platform.DataAndExt.Prefs;

public class MappedSortedListItem<KeyType, EntryType> : MappedListItem<KeyType, EntryType>
	where KeyType : notnull
	where EntryType : Obj<EntryType>
{
	public MappedSortedListItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
			string strLocalizedLongDesc, System.Collections.Generic.IEnumerable<EntryType> def, in System
			.Func<EntryType, KeyType> funcKeyObtainer)
		: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, def, funcKeyObtainer, new
			System.Collections.Generic.SortedDictionary<KeyType, EntryType>())
	{
	}

	public MappedSortedListItem(in AbstractMgr mgrParent, in string strItemName, in string strLocalizedName, in
			string strLocalizedLongDesc, System.Collections.Generic.IEnumerable<EntryType> def, in System.Collections
			.Generic.IEnumerable<EntryType> val, in System.Func<EntryType, KeyType> funcKeyObtainer)
		: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, def, val, funcKeyObtainer, new
			System.Collections.Generic.SortedDictionary<KeyType, EntryType>())
	{
	}
}