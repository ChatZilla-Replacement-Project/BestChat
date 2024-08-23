namespace BestChat.Platform.DataAndExt
{
	public abstract class TreeData
	{
		#region Constructors & Deconstructors
			protected TreeData(in IItemInfo itemUs)
			{
				this.itemUs = itemUs;
				LocalizedName = itemUs.LocalizedName;
				LocalizedLongDesc = itemUs.LocalizedLongDesc;
				IChildOwner? ownerOfChildren = itemUs is IChildOwner owner ? (IChildOwner)itemUs : null;
				Icon = itemUs.Icon;

				if(ownerOfChildren != null)
				{
					ownerOfChildren.CollectionChanged += OnSrcCollectionChanged;

					foreach(IItemInfo itemCur in ownerOfChildren.Children)
					{
						itemCur.evtDieing += OnChildDieing;

						ocChildren.Add(MakeChild(itemCur));
					}
				}
			}
		#endregion

		#region Delegates
		#endregion

		#region Events
		#endregion

		#region Constants
		#endregion

		#region Helper Types
			public interface IItemInfo : Dieable.IDieable
			{
				string LocalizedName
				{
					get;
				}

				string LocalizedLongDesc
				{
					get;
				}

				string Icon
				{
					get;
				}
			}

			public interface IChildOwner : IItemInfo, System.Collections.Specialized.INotifyCollectionChanged
			{
				System.Collections.Generic.IEnumerable<IItemInfo> Children
				{
					get;
				}
			}
		#endregion

		#region Members
			public readonly IItemInfo itemUs;

			private readonly System.Collections.Generic.Dictionary<Dieable.IDieable, TreeData> mapDieableToTreeDataInstance =
				[];

			private readonly System.Collections.ObjectModel.ObservableCollection<TreeData> ocChildren = [];
		#endregion

		#region Properties
			public abstract string LocalizedName
			{
				get;

				init;
			}

			public abstract string LocalizedLongDesc
			{
				get;

				init;
			}

			public System.Collections.Specialized.INotifyCollectionChanged Children
				=> ocChildren;

			public abstract string Icon
			{
				get;

				init;
			}
		#endregion

		#region Methods
			protected abstract TreeData MakeChild(in IItemInfo itemCurChild);
		#endregion

		#region Event Handlers
			private void OnSrcCollectionChanged(object? objSender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
				=> throw new System.NotImplementedException();
	
			private void OnChildDieing(Dieable.IDieable dieing)
			{
				if(mapDieableToTreeDataInstance.TryGetValue(dieing, out TreeData? value))
					ocChildren.Remove(value);

				mapDieableToTreeDataInstance.Remove(dieing);
			}
		#endregion
	}
}