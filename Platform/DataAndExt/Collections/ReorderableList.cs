// Ignore Spelling: Reorderable

namespace BestChat.Platform.DataAndExt.Collections;

public class ReorderableList<ValueType> : System.Collections.Generic.LinkedList<ValueType>
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
	#endregion

	#region Events
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
		public bool MoveEntryUp(ValueType entryToMove)
		{
			System.Collections.Generic.LinkedListNode<ValueType>? llnodeBeingMoved = Find(entryToMove);
			if(llnodeBeingMoved == null)
				return false;

			System.Collections.Generic.LinkedListNode<ValueType>? llnodePrev = llnodeBeingMoved.Previous;

			if(llnodePrev == null)
				return false;


			Remove(llnodeBeingMoved);

			AddBefore(llnodePrev, llnodeBeingMoved);

			return true;
		}

		public bool MoveEntryDown(ValueType entryToMove)
		{
			System.Collections.Generic.LinkedListNode<ValueType>? llnodeBeingMoved = Find(entryToMove);
			if(llnodeBeingMoved == null)
				return false;

			System.Collections.Generic.LinkedListNode<ValueType>? llnodeNext = llnodeBeingMoved.Next;

			if(llnodeNext == null)
				return false;


			Remove(llnodeBeingMoved);

			AddAfter(llnodeNext, llnodeBeingMoved);

			return true;
		}

		public bool MoveEntryToTop(ValueType entryToMove)
		{
			System.Collections.Generic.LinkedListNode<ValueType>? llnodeBeingMoved = Find(entryToMove);
			if(llnodeBeingMoved == null)
				return false;

			Remove(entryToMove);

			AddFirst(entryToMove);

			return true;
		}

		public bool MoveEntryToBottom(ValueType entryToMove)
		{
			System.Collections.Generic.LinkedListNode<ValueType>? llnodeBeingMoved = Find(entryToMove);
			if(llnodeBeingMoved == null)
				return false;

			Remove(entryToMove);

			AddLast(entryToMove);

			return true;
		}
	#endregion
}