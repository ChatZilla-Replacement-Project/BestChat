namespace	BestChat.Platform.DataAndExt.Collections;

public class ObservableCollection<ElementType> : System.Collections.ObjectModel.ObservableCollection<ElementType>
{
	public void	RemoveRange(in int	index, in int count)
	{
		CheckReentrancy();
		System.Collections.Generic.List<ElementType> items = (System.Collections.Generic.List<ElementType>)Items;
		items.RemoveRange(index, count);
		OnReset();
	}

	public void	InsertRange(in int	index, in System.Collections.Generic.IEnumerable<ElementType> collection)
	{
		CheckReentrancy();
		System.Collections.Generic.List<ElementType> items = (System.Collections.Generic.List<ElementType>)Items;
		items.InsertRange(index, collection);
		OnReset();
	}

	private	void OnReset()
	{
		FirePropertyChanged(nameof(Count));
		FirePropertyChanged(nameof(Items));
		OnCollectionChanged(new	System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections
			.Specialized.NotifyCollectionChangedAction.Reset));
	}

	private	void FirePropertyChanged(string	strPropName) =>	OnPropertyChanged(new	System.ComponentModel
		.PropertyChangedEventArgs(strPropName));
}