// Ignore Spelling: Prefs evt

using System.Linq;
using BestChat.Platform.DataAndExt.Ext;

namespace BestChat.Platform.DataAndExt.Prefs;

public class CollectionItem<TypeOfElement> : ItemBase, System.Collections.Specialized
	.INotifyCollectionChanged, System.Collections.Generic.ISet<TypeOfElement>, System
	.Collections.Generic.IReadOnlySet<TypeOfElement>
{
	#region Constructors & Deconstructors
		public CollectionItem(in AbstractMgr mgrParent, in string strItemName, in string
				strLocalizedName, in string strLocalizedLongDesc, System.Collections.Generic
				.IEnumerable<TypeOfElement> def) :
			base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			this.def = def;
			hsEntries = new(def);
		}

		public CollectionItem(in AbstractMgr mgrParent, in string strItemName, in string
				strLocalizedName, in string strLocalizedLongDesc, System.Collections.Generic
				.IEnumerable<TypeOfElement> def, System.Collections.Generic.IEnumerable<TypeOfElement> val)
			: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			this.def = def;
			hsEntries = new(val);
		}
	#endregion

	#region Events
		public event System.Collections.Specialized.NotifyCollectionChangedEventHandler?
			CollectionChanged;

		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlySet<TypeOfElement>>?
			evtEntriesChanged;
	#endregion

	#region Members
		public readonly System.Collections.Generic.IEnumerable<TypeOfElement> def;

		private readonly System.Collections.Generic.HashSet<TypeOfElement> hsEntries;

		private TypeOfElement[]? backedUpVal = null;
	#endregion

	#region Properties
		public override string ValAsText
			=> string.Join(',', hsEntries);

		public System.Collections.Generic.IEnumerable<TypeOfElement> Def
			=> def;

		public int Count
			=> hsEntries.Count;

		public bool IsEmpty
			=> hsEntries.Count == 0;

		public bool IsReadOnly
			=> false;

		public override bool IsDefaulted
			=> hsEntries.SetEquals(def);

		public override bool IsReadyToEdit
			=> backedUpVal != null && backedUpVal.Length > 0;
	#endregion

	#region Methods
		public bool AddNewEntry(TypeOfElement itemNew)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(hsEntries.Contains(itemNew))
				return false;

			if(!hsEntries.Add(itemNew))
				return false;

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Add));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.add);

			return true;
		}

		public bool RemoveEntry(TypeOfElement itemToBeRemoved)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!hsEntries.Contains(itemToBeRemoved))
				return false;

			if(!hsEntries.Remove(itemToBeRemoved))
				return false;

			MakeDirty();

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Remove));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.removed);

			return true;
		}

		public bool Add(TypeOfElement item)
			=> AddNewEntry(item);

		public void ExceptWith(System.Collections.Generic.IEnumerable<TypeOfElement> other)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			System.Collections.Generic.HashSet<TypeOfElement> hsOld = hsEntries;

			hsEntries.ExceptWith(other);

			if(!hsOld.SetEquals(hsEntries))
			{
				CollectionChanged?.Invoke(this, new(System.Collections.Specialized
					.NotifyCollectionChangedAction.Remove));
				evtEntriesChanged?.Invoke(this, this, CollectionChangeType.removed);
			}
		}

		public void IntersectWith(System.Collections.Generic.IEnumerable<TypeOfElement> other)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			System.Collections.Generic.HashSet<TypeOfElement> hsOld = hsEntries;

			hsEntries.IntersectWith(other);

			if(!hsOld.SetEquals(hsEntries))
			{
				CollectionChanged?.Invoke(this, new(System.Collections.Specialized
					.NotifyCollectionChangedAction.Remove));
				evtEntriesChanged?.Invoke(this, this, CollectionChangeType.removed);
			}
		}

		public bool IsProperSubsetOf(System.Collections.Generic.IEnumerable<TypeOfElement> other)
			=> hsEntries.IsProperSubsetOf(other);

		public bool IsProperSupersetOf(System.Collections.Generic.IEnumerable<TypeOfElement> other)
			=> hsEntries.IsProperSupersetOf(other);

		public bool IsSubsetOf(System.Collections.Generic.IEnumerable<TypeOfElement> other)
			=> hsEntries.IsSubsetOf(other);

		public bool IsSupersetOf(System.Collections.Generic.IEnumerable<TypeOfElement> other)
			=> hsEntries.IsSupersetOf(other);

		public bool Overlaps(System.Collections.Generic.IEnumerable<TypeOfElement> other)
			=> hsEntries.Overlaps(other);

		public bool SetEquals(System.Collections.Generic.IEnumerable<TypeOfElement> other)
			=> hsEntries.SetEquals(other);

		public void SymmetricExceptWith(System.Collections.Generic.IEnumerable<TypeOfElement> other)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			System.Collections.Generic.HashSet<TypeOfElement> hsOld = hsEntries;

			hsEntries.SymmetricExceptWith(other);

			if(!hsOld.SetEquals(hsEntries))
			{
				CollectionChanged?.Invoke(this, new(System.Collections.Specialized
					.NotifyCollectionChangedAction.Remove));
				evtEntriesChanged?.Invoke(this, this, CollectionChangeType.removed);
			}
		}

		public void UnionWith(System.Collections.Generic.IEnumerable<TypeOfElement> other)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			System.Collections.Generic.HashSet<TypeOfElement> hsOld = hsEntries;

			hsEntries.UnionWith(other);

			if(!hsOld.SetEquals(hsEntries))
			{
				CollectionChanged?.Invoke(this, new(System.Collections.Specialized
					.NotifyCollectionChangedAction.Remove));
				evtEntriesChanged?.Invoke(this, this, CollectionChangeType.removed);
			}
		}

		void System.Collections.Generic.ICollection<TypeOfElement>.Add(TypeOfElement item)
			=> Add(item);

		public void Clear()
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(hsEntries.Count == 0)
				return;

			hsEntries.Clear();

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Remove));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.removed);
		}

		public bool Contains(TypeOfElement item)
			=> hsEntries.Contains(item);

		public void CopyTo(TypeOfElement[] array, int iArrayIndex)
			=> hsEntries.CopyTo(array, iArrayIndex);

		public bool Remove(TypeOfElement item)
			=> RemoveEntry(item);

		public System.Collections.Generic.IEnumerator<TypeOfElement> GetEnumerator()
			=> hsEntries.GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> hsEntries.GetEnumerator();

		internal override void PrepareForEdit()
		{
			if(!mgrParent.IsEditMode)
				throw new System.InvalidProgramException("Parent isn't editable");

			if(IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.preparingForEdit);

			backedUpVal = new TypeOfElement[hsEntries.Count];
			hsEntries.CopyTo(backedUpVal, 0);
		}

		internal override void SaveEdits()
		{
			
			bool bChangesWereMade = backedUpVal != null && hsEntries.SetEquals(backedUpVal);
			if(!bChangesWereMade)
				return;

			backedUpVal = null;

			MakeDirty();

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Reset));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.other);
		}

		internal override void RevertEdits()
		{
			if(!mgrParent.IsEditMode)
				throw new System.InvalidProgramException("Parent not editing");

			if(backedUpVal == null || backedUpVal.Length == 0)
				throw new EditingException(EditingException.WhenPossibilities.reverting);

			hsEntries.Clear();
			hsEntries.UnionWith(backedUpVal);

			backedUpVal = null;
		}
	#endregion
}