namespace BestChat.Platform.DataAndExt.Collections;

public interface IReadOnlyLinkList<ValueType> : System.Collections.Generic.IReadOnlyCollection<ValueType>
{
	ValueType First
	{
		get;
	}

	ValueType Last
	{
		get;
	}

	bool Contains(ValueType value);

	void CopyTo(ValueType[] array, int iArrayIndex);

	System.Collections.Generic.LinkedListNode<ValueType> Find(ValueType value);

	System.Collections.Generic.LinkedListNode<System.ValueType> Find(System.Func<ValueType, bool> funcPredicate);

	System.Collections.Generic.LinkedListNode<ValueType> FindLast(ValueType value);

	System.Collections.Generic.LinkedListNode<ValueType> FindLast(System.Func<ValueType, bool> funcPredicate);
}