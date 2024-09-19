// Ignore Spelling: Reorderable evt

using System.Linq;

namespace BestChat.Platform.DataAndExt.Prefs;

public class ReorderableListItem<TypeOfElement> : ItemBase, System.Collections.Specialized
	.INotifyCollectionChanged, System.Collections.Generic.ICollection<TypeOfElement>,
	System.Collections.Generic.IReadOnlyCollection<TypeOfElement>
{
	#region Constructors & Deconstructors
		public ReorderableListItem(in AbstractMgr mgrParent, in string strItemName, in string
				strLocalizedName, in string strLocalizedLongDesc, System.Collections.Generic
				.IEnumerable<TypeOfElement> def) :
			base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			this.def = def;
			rlistEntries = new(def);
		}

		public ReorderableListItem(in AbstractMgr mgrParent, in string strItemName, in string
				strLocalizedName, in string strLocalizedLongDesc, System.Collections.Generic
				.IEnumerable<TypeOfElement> def, System.Collections.Generic.IEnumerable<TypeOfElement> val)
			: base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			this.def = def;
			rlistEntries = new(val);
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event System.Collections.Specialized.NotifyCollectionChangedEventHandler?
			CollectionChanged;

		public event DCollectionFieldChanged<System.Collections.Generic
			.IReadOnlyCollection<TypeOfElement>>? evtEntriesChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly System.Collections.Generic.IEnumerable<TypeOfElement> def;

		private readonly Collections.ReorderableList<TypeOfElement> rlistEntries;

		private TypeOfElement[]? backedUpVal = null;
	#endregion

	#region Properties
		public override string ValAsText
			=> string.Join(',', rlistEntries);

		public int Count
			=> rlistEntries.Count;

		public bool IsEmpty
			=> rlistEntries.IsEmpty;

		public bool IsReadOnly
			=> false;

		public override bool IsDefaulted
			=> rlistEntries.SequenceEqual(def);

		public override bool IsReadyToEdit
			=> backedUpVal != null && backedUpVal.Length > 0;
	#endregion

	#region Methods
		public void Append(TypeOfElement itemNew)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(rlistEntries.Contains(itemNew))
				return;

			rlistEntries.AddLast(itemNew);

			OnNewEntry(itemNew);

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Add));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.add);
		}

		public void Add(TypeOfElement itemNew)
			=> Append(itemNew);

		public void MoveEntryUp(TypeOfElement entryToMove)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!rlistEntries.MoveEntryUp(entryToMove))
				return;

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Add));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.add);
		}

		public void MoveEntryDown(TypeOfElement entryToMove)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!rlistEntries.MoveEntryDown(entryToMove))
				return;

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Add));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.add);
		}

		public void MoveEntryToTop(TypeOfElement entryToMove)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!rlistEntries.MoveEntryToTop(entryToMove))
				return;

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Add));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.add);
		}

		public void MoveEntryToBottom(TypeOfElement entryToMove)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!rlistEntries.MoveEntryToBottom(entryToMove))
				return;

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Add));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.add);
		}

		public void Clear()
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!rlistEntries.IsEmpty)
				return;

			foreach(TypeOfElement entryCur in rlistEntries)
				OnEntryRemoved(entryCur);

			rlistEntries.Clear();

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Remove));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.removed);
		}

		public bool Contains(TypeOfElement itemToLookFor)
			=> rlistEntries.Contains(itemToLookFor);

		public void CopyTo(TypeOfElement[] array, int iArrayIndex)
			=> rlistEntries.CopyTo(array, iArrayIndex);

		public bool Remove(TypeOfElement itemToRemove)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!rlistEntries.Remove(itemToRemove))
				return false;

			OnEntryRemoved(itemToRemove);

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Remove));
			evtEntriesChanged?.Invoke(this, this, CollectionChangeType.removed);

			return true;
		}

		public System.Collections.Generic.IEnumerator<TypeOfElement> GetEnumerator()
			=> rlistEntries.GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> rlistEntries.GetEnumerator();

		protected virtual void OnNewEntry(TypeOfElement itemNew)
		{
		}

		protected virtual void OnEntryRemoved(TypeOfElement itemDeleted)
		{
		}

		internal override void PrepareForEdit()
		{
			if(!mgrParent.IsEditMode)
				throw new System.InvalidProgramException("Parent isn't editable");

			if(IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.preparingForEdit);

			backedUpVal = new TypeOfElement[rlistEntries.Count];
			rlistEntries.CopyTo(backedUpVal, 0);
		}

		internal override void SaveEdits()
		{
			bool bChangesWereMade = backedUpVal != null && rlistEntries.SequenceEqual(backedUpVal);
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

			rlistEntries.Clear();
			foreach(TypeOfElement itemCur in backedUpVal)
				rlistEntries.AddLast(itemCur);

			backedUpVal = null;
		}
	#endregion

	#region Event Handlers
	#endregion
}