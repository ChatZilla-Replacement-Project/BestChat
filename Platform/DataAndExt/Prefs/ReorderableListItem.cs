// Ignore Spelling: Reorderable evt

using System.Linq;

namespace BestChat.Platform.DataAndExt.Prefs;

public class ReorderableListItem<TypeOfElement> : ItemBase, System.Collections.Specialized
	.INotifyCollectionChanged, System.Collections.Generic.ICollection<TypeOfElement>,
	System.Collections.Generic.IReadOnlyCollection<TypeOfElement>
{
	#region Constructors & Deconstructors
	/* ReSharper disable once InconsistentNaming */
	public ReorderableListItem(in AbstractMgr mgrParent, /* ReSharper disable once InconsistentNaming */ in string
		strItemName, in string strLocalizedName,  /* ReSharper disable once InconsistentNaming */ in string
		strLocalizedLongDesc, System.Collections.Generic.IEnumerable<TypeOfElement> def) :
			base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			Def = def;
			rlistEntries = new(def);

			rlistEntries.evtCtntsChanged += OnCtntsOfInnerListChanged;

			DefTester = TestCurValForDef;
		}

		public ReorderableListItem(in AbstractMgr mgrParent, /* ReSharper disable once InconsistentNaming */ in string
				strItemName, in string strLocalizedName, /* ReSharper disable once InconsistentNaming */ in string
				strLocalizedLongDesc, System.Collections.Generic.IEnumerable<TypeOfElement> def, System.Collections.Generic
				.IEnumerable<TypeOfElement> val) :
			base(mgrParent, strItemName, strLocalizedName, strLocalizedLongDesc)
		{
			Def = def;
			rlistEntries = new(val);

			DefTester = TestCurValForDef;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event System.Collections.Specialized.NotifyCollectionChangedEventHandler?
			CollectionChanged;

		public event DCollectionFieldChanged<System.Collections.Generic.IReadOnlyCollection<TypeOfElement>, TypeOfElement>?
			evtEntriesChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private readonly Collections.ReorderableList<TypeOfElement> rlistEntries;

		private TypeOfElement[]? backedUpVal = null;
	#endregion

	#region Properties
		public System.Collections.Generic.IEnumerable<TypeOfElement> Def
		{
			get;

			private init;
		}

		public System.Func<System.Collections.Generic.IEnumerable<TypeOfElement>, bool> DefTester
		{
			get;

			set;
		}

		public System.Action? ResetToDefMethod
		{
			get;

			init;
		} = null;

		public override string ValAsText
			=> string.Join(',', rlistEntries);

		public int Count
			=> rlistEntries.Count;

		public bool IsEmpty
			=> rlistEntries.IsEmpty;

		public bool IsReadOnly
			=> false;

		public override bool IsDefaulted
			=> rlistEntries.SequenceEqual(Def);

		public override bool IsReadyToEdit
			=> backedUpVal is
				{
					Length: > 0,
				};
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
			evtEntriesChanged?.Invoke(this, this, [itemNew, ]);
		}

		public void Prepend(TypeOfElement itemNew)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(rlistEntries.Contains(itemNew))
				return;

			rlistEntries.AddFirst(itemNew);

			OnNewEntry(itemNew);

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Add));
			evtEntriesChanged?.Invoke(this, this, [itemNew, ]);

		}

		public void Add(TypeOfElement itemNew)
			=> Append(itemNew);

		public void AddBefore(TypeOfElement itemExisting, TypeOfElement itemNew)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(rlistEntries.Contains(itemNew))
				return;

			System.Collections.Generic.LinkedListNode<TypeOfElement> llnitemExisting = rlistEntries.Find(itemExisting) ??
				throw new System.InvalidProgramException("Can't find the item specified in this list.");

			rlistEntries.AddBefore(llnitemExisting, itemNew);

			OnNewEntry(itemNew);

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized.NotifyCollectionChangedAction.Add));
			evtEntriesChanged?.Invoke(this, this, [itemNew,]);
		}

		public void AddAfter(TypeOfElement itemExisting, TypeOfElement itemNew)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(rlistEntries.Contains(itemNew))
				return;

			System.Collections.Generic.LinkedListNode<TypeOfElement> llnitemExisting = rlistEntries.Find(itemExisting) ??
				throw new System.InvalidProgramException("Can't find the item specified in this list.");

			rlistEntries.AddAfter(llnitemExisting, itemNew);

			OnNewEntry(itemNew);

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized.NotifyCollectionChangedAction.Add));
			evtEntriesChanged?.Invoke(this, this, [itemNew,]);
		}

		public void MoveEntryUp(TypeOfElement entryToMove)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!rlistEntries.MoveEntryUp(entryToMove))
				return;

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized.NotifyCollectionChangedAction.Add));
		}

		public void MoveEntryDown(TypeOfElement entryToMove)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!rlistEntries.MoveEntryDown(entryToMove))
				return;

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Add));
		}

		public void MoveEntryToTop(TypeOfElement entryToMove)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!rlistEntries.MoveEntryToTop(entryToMove))
				return;

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Add));
		}

		public void MoveEntryToBottom(TypeOfElement entryToMove)
		{
			if(!IsReadyToEdit)
				throw new EditingException(EditingException.WhenPossibilities.notReadyToEdit);

			if(!rlistEntries.MoveEntryToBottom(entryToMove))
				return;

			CollectionChanged?.Invoke(this, new(System.Collections.Specialized
				.NotifyCollectionChangedAction.Add));
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

		private bool TestCurValForDef(System.Collections.Generic.IEnumerable<TypeOfElement> entries)
			=> entries.SequenceEqual(Def);

		public void ResetValToDef()
		{
			if(ResetToDefMethod == null)
			{
				foreach(TypeOfElement entryCur in rlistEntries)
					OnEntryRemoved(entryCur);

				rlistEntries.Clear();

				foreach(TypeOfElement entryCur in Def)
					Add(entryCur);
			}
			else
				ResetToDefMethod();
		}
	#endregion

	#region Event Handlers
		private void OnCtntsOfInnerListChanged(in Collections.ReorderableList<TypeOfElement> sender, in System.Collections
				.Generic.IReadOnlyCollection<TypeOfElement>? newEntries, in System.Collections.Generic
				.IReadOnlyCollection<System.Tuple<int, TypeOfElement>>? removedEntries, in System.Collections.Generic
				.IReadOnlyCollection<System.Tuple<int, int, TypeOfElement>>? changedEntries)
			=> evtEntriesChanged?.Invoke
			(
				this,
				rlistEntries,
				newEntries,
				removedEntries?.Select(tupleCur
					=> tupleCur.Item2),
				changedEntries?.Select(tupleCur
					=> tupleCur.Item3)
			);
		#endregion
}