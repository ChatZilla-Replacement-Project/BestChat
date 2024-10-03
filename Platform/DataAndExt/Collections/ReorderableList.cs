// Ignore Spelling: Reorderable

using System.Linq;

namespace BestChat.Platform.DataAndExt.Collections;

public class ReorderableList<ValueType> : System.Collections.Generic.LinkedList<ValueType>, IReadOnlyLinkedList<ValueType>
{
	#region Constructors & Destructors
		public ReorderableList()
		{
		}

		public ReorderableList(in System.Collections.Generic.IEnumerable<ValueType> copyThis) : base(copyThis)
		{
		}
	#endregion

	#region Delegates
		public delegate void DCtntsChanged(in ReorderableList<ValueType> rlSender, in System.Collections.Generic
			.IReadOnlyCollection<ValueType>? eNewEntries = null, in System.Collections.Generic.IReadOnlyCollection<System
			.Tuple<int, ValueType>>? eRemovedEntries = null, in System.Collections.Generic.IReadOnlyCollection<System
			.Tuple<int, int, ValueType>>? eRelocatedItems = null);
	#endregion

	#region Events
		public event DCtntsChanged? evtCtntsChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper types
	#endregion

	#region Members
	#endregion

	#region Properties
		public bool IsEmpty
			=> Count == 0;
	#endregion

	#region Methods
		public new System.Collections.Generic.LinkedListNode<ValueType> AddFirst(ValueType itemNew)
		{
			System.Collections.Generic.LinkedListNode<ValueType> llnodeRetVal = base.AddFirst(itemNew);

			evtCtntsChanged?.Invoke(this, [itemNew,]);

			return llnodeRetVal;
		}

		public new void AddFirst(System.Collections.Generic.LinkedListNode<ValueType> llnitemNew)
		{
			base.AddFirst(llnitemNew);

			evtCtntsChanged?.Invoke(this, [llnitemNew.Value,]);
		}

		public new System.Collections.Generic.LinkedListNode<ValueType> AddLast(ValueType itemNew)
		{
			System.Collections.Generic.LinkedListNode<ValueType> llnodeRetVal = base.AddLast(itemNew);

			evtCtntsChanged?.Invoke(this, [itemNew,]);

			return llnodeRetVal;
		}

		public new void AddLast(System.Collections.Generic.LinkedListNode<ValueType> llnitemNew)
		{
			base.AddLast(llnitemNew);

			evtCtntsChanged?.Invoke(this, [llnitemNew.Value,]);
		}

		public new System.Collections.Generic.LinkedListNode<ValueType> AddBefore(System.Collections.Generic
			.LinkedListNode<ValueType> llnitemExisting, ValueType itemNew)
		{
			System.Collections.Generic.LinkedListNode<ValueType> llnodeRetVal = base.AddBefore(llnitemExisting, itemNew);

			evtCtntsChanged?.Invoke(this, [itemNew,]);

			return llnodeRetVal;
		}

		public new void AddBefore(System.Collections.Generic.LinkedListNode<ValueType> llnitemExisting, System.Collections
			.Generic.LinkedListNode<ValueType> llnitemNew)
		{
			base.AddBefore(llnitemExisting, llnitemNew);

			evtCtntsChanged?.Invoke(this, [llnitemNew.Value,]);
		}

		public new System.Collections.Generic.LinkedListNode<ValueType> AddAfter(System.Collections.Generic
			.LinkedListNode<ValueType> llnitemExisting, ValueType itemNew)
		{
			System.Collections.Generic.LinkedListNode<ValueType> llnodeRetVal = base.AddAfter(llnitemExisting, itemNew);

			evtCtntsChanged?.Invoke(this, [itemNew,]);

			return llnodeRetVal;
		}

		public new bool Remove(ValueType itemToBeRemoved)
		{
			int iIndexOfItemBeingRemoved = this.ToList().IndexOf(itemToBeRemoved);

			bool bRetVal = base.Remove(itemToBeRemoved);

			evtCtntsChanged?.Invoke(this, eRemovedEntries: [new(iIndexOfItemBeingRemoved, itemToBeRemoved),]);

			return bRetVal;
		}

		public new void Remove(System.Collections.Generic.LinkedListNode<ValueType> llnitemToBeRemoved)
		{
			int iIndexOfItemBeingRemoved = this.ToList().IndexOf(llnitemToBeRemoved.Value);

			base.Remove(llnitemToBeRemoved);

			evtCtntsChanged?.Invoke(this, eRemovedEntries: [new(iIndexOfItemBeingRemoved, llnitemToBeRemoved
				.Value),]);
		}

		public new bool RemoveFirst()
			=> First is not null && Remove(First.Value);

		public new bool RemoveLast()
			=> Last is not null && Remove(Last.Value);

		public bool MoveEntryUp(ValueType entryToMove)
		{
			System.Collections.Generic.LinkedListNode<ValueType>? llnodeBeingMoved = Find(entryToMove);
			if(llnodeBeingMoved is null)
				throw new System.InvalidOperationException($"The entry {entryToMove} was not found in the list.");

			System.Collections.Generic.LinkedListNode<ValueType>? llnodePrev = llnodeBeingMoved.Previous;

			if(llnodePrev == null)
				return false;

			int iOldIndex = this.ToList().IndexOf(entryToMove);

			System.Tuple<int, int, ValueType> change = new(iOldIndex, iOldIndex - 1, entryToMove);

			Remove(llnodeBeingMoved);

			AddBefore(llnodePrev, llnodeBeingMoved);

			evtCtntsChanged?.Invoke(this, eRelocatedItems: [change, ]);

			return true;
		}

		public bool MoveEntryDown(ValueType entryToMove)
		{
			System.Collections.Generic.LinkedListNode<ValueType>? llnodeBeingMoved = Find(entryToMove);
			if(llnodeBeingMoved is null)
				throw new System.InvalidOperationException($"The entry {entryToMove} was not found in the list.");

			System.Collections.Generic.LinkedListNode<ValueType>? llnodeNext = llnodeBeingMoved.Next;

			if(llnodeNext == null)
				return false;


			int iOldIndex = this.ToList().IndexOf(entryToMove);

			System.Tuple<int, int, ValueType> change = new(iOldIndex, iOldIndex + 1, entryToMove);

			Remove(llnodeBeingMoved);

			AddAfter(llnodeNext, llnodeBeingMoved);

			evtCtntsChanged?.Invoke(this, eRelocatedItems: [change, ]);

			return true;
		}

		public bool MoveEntryToTop(ValueType entryToMove)
		{
			System.Collections.Generic.LinkedListNode<ValueType>? llnodeBeingMoved = Find(entryToMove);
			if(llnodeBeingMoved == null)
				return false;


			int iOldIndex = this.ToList().IndexOf(entryToMove);

			System.Tuple<int, int, ValueType> change = new(iOldIndex, 0, entryToMove);

			Remove(entryToMove);

			AddFirst(entryToMove);

			evtCtntsChanged?.Invoke(this, eRelocatedItems: [change, ]);

			return true;
		}

		public bool MoveEntryToBottom(ValueType entryToMove)
		{
			System.Collections.Generic.LinkedListNode<ValueType>? llnodeBeingMoved = Find(entryToMove);
			if(llnodeBeingMoved == null)
				return false;


			int iOldIndex = this.ToList().IndexOf(entryToMove);

			System.Tuple<int, int, ValueType> change = new(iOldIndex, 0, entryToMove);

			Remove(entryToMove);

			AddLast(entryToMove);

			evtCtntsChanged?.Invoke(this, eRelocatedItems: [change, ]);

			return true;
		}
	#endregion

	public System.Collections.Generic.LinkedListNode<ValueType>? Find(System.Func<ValueType, bool> funcPredicate)
	{
		if(First is not null)
			for(System.Collections.Generic.LinkedListNode<ValueType> llnodeCur = First; llnodeCur?.Next != null ; llnodeCur =
					llnodeCur.Next)
				if(funcPredicate(llnodeCur.Value))
					return llnodeCur;

		return null;
	}

	public System.Collections.Generic.LinkedListNode<ValueType>? FindLast(System.Func<ValueType, bool> funcPredicate)
	{
		if(Last is not null)
			for(System.Collections.Generic.LinkedListNode<ValueType> llnodeCur = Last; llnodeCur?.Previous != null ; llnodeCur
					= llnodeCur.Previous)
				if(funcPredicate(llnodeCur.Value))
					return llnodeCur;

		return null;
	}
}