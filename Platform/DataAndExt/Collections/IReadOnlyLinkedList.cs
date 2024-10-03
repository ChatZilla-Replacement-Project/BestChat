namespace BestChat.Platform.DataAndExt.Collections;

public interface IReadOnlyLinkedList<ValueType> : System.Collections.Generic.IReadOnlyCollection<ValueType>
{
	System.Collections.Generic.LinkedListNode<ValueType>? First
	{
		get;
	}

	System.Collections.Generic.LinkedListNode<ValueType>? Last
	{
		get;
	}

	bool Contains(ValueType value);

	void CopyTo(ValueType[] array, int iArrayIndex);

	System.Collections.Generic.LinkedListNode<ValueType>? Find(ValueType value);

	System.Collections.Generic.LinkedListNode<ValueType>? Find(System.Func<ValueType, bool> funcPredicate);

	System.Collections.Generic.LinkedListNode<ValueType>? FindLast(ValueType value);

	System.Collections.Generic.LinkedListNode<ValueType>? FindLast(System.Func<ValueType, bool> funcPredicate);
}