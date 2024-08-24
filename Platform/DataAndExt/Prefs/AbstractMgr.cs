// Ignore Spelling: Prefs

using System.Linq;

namespace BestChat.Platform.DataAndExt.Prefs;

public abstract class AbstractMgr : Obj<AbstractMgr>
{
	#region Constructors & Deconstructors
		protected AbstractMgr()
		{
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public abstract class Editable
		{
			protected Editable(AbstractMgr original)
			{
				this.original = original;


				mapItemsByName = [];
				foreach(ItemBase itemCur in original.ItemsByName)
					mapItemsByName[itemCur.strItemName] = itemCur.EditableMaker(itemCur);

				mapChildMgrByName = [];
				foreach(AbstractChildMgr cmgrCur in original.ChildMgrByName)
					mapChildMgrByName[cmgrCur.Name] = AbstractChildMgr.Editable.Make(cmgrCur);
			}

			public readonly AbstractMgr original;

			public void Save()
			{
				foreach(ItemBase.IEditable eitemCur in mapItemsByName.Values)
					eitemCur.Save();

				foreach(AbstractChildMgr.Editable ecmgrCur in mapChildMgrByName.Values)
					ecmgrCur.Save();
			}

			private readonly System.Collections.Generic.SortedDictionary<string, ItemBase.IEditable> mapItemsByName =
				[];

			private readonly System.Collections.Generic.SortedDictionary<string, AbstractChildMgr.Editable> mapChildMgrByName =
				[];

			public System.Collections.Generic.IReadOnlyCollection<ItemBase.IEditable> ItemsByName
				=> mapItemsByName.Values;

			public System.Collections.Generic.IReadOnlyCollection<AbstractChildMgr.Editable> ChildMgrByName
				=> mapChildMgrByName.Values;
		}
	#endregion

	#region Members
		private readonly System.Collections.Generic.SortedDictionary<string, ItemBase> mapItemsByName = [];

		private readonly System.Collections.Generic.SortedDictionary<string, AbstractChildMgr> mapChildMgrByName =
			[];
	#endregion

	#region Properties
		public System.Collections.Generic.IReadOnlyCollection<ItemBase> ItemsByName
			=> mapItemsByName.Values;

		public System.Collections.Generic.IReadOnlyCollection<AbstractChildMgr> ChildMgrByName
			=> mapChildMgrByName.Values;
	#endregion

	#region Methods
		internal void Add<ItemType>(in Item<ItemType> itemNew)
		{
			mapItemsByName[itemNew.LocalizedName] = itemNew;

			itemNew.evtDirtyChanged += OnChildItemDirtyChanged;
		}

		internal void Add(in AbstractChildMgr cmgrNew)
		{
			mapChildMgrByName[cmgrNew.LocalizedName] = cmgrNew;

			cmgrNew.evtDirtyChanged += OnChildMgrDirtyChanged;
		}

		public System.Collections.Generic.IReadOnlyDictionary<string, object> ToTupleList()
		{
			System.Collections.Generic.SortedDictionary<string, object> mapFieldsByName = [];

			foreach(ItemBase itemCur in mapItemsByName.Values)
				mapFieldsByName[itemCur.ItemName] = itemCur.ValAsText;

			foreach(AbstractChildMgr cmgrCur in mapChildMgrByName.Values)
				mapFieldsByName[cmgrCur.Name] = cmgrCur.ToTupleList();

			return mapFieldsByName;
		}
	#endregion

	#region Event Handlers
		private void OnChildItemDirtyChanged(in ItemBase objSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}

		private void OnChildMgrDirtyChanged(in AbstractMgr objSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}
	#endregion
}