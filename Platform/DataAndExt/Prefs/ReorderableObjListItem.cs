// Ignore Spelling: Prefs Reorderable

namespace BestChat.Platform.DataAndExt.Prefs;

public class ReorderableObjListItem<TypeOfElement> : ReorderableListItem<TypeOfElement>
	where TypeOfElement : Obj<TypeOfElement>
{
	public ReorderableObjListItem(in AbstractMgr mgrParent, in string strItemName, in string
			strLocalizedName, in string strLocalizedLongDesc, System.Collections.Generic
			.IEnumerable<TypeOfElement> def) :
		base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, def)
	{
	}

	public ReorderableObjListItem(in AbstractMgr mgrParent, in string strItemName, in string
			strLocalizedName, in string strLocalizedLongDesc, System.Collections.Generic
			.IEnumerable<TypeOfElement> def, System.Collections.Generic.IEnumerable<TypeOfElement> val)
		: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc, def)
	{
	}

	protected override void OnNewEntry(TypeOfElement itemNew)
		=> itemNew.evtDirtyChanged += OnItemDirtyChanged;

	protected override void OnEntryRemoved(TypeOfElement itemDeleted)
		=> itemDeleted.evtDirtyChanged -= OnItemDirtyChanged;

	private void OnItemDirtyChanged(in TypeOfElement objSender, in bool bIsNowDirty)
	{
		if(bIsNowDirty)
			MakeDirty();
	}
}