namespace BestChat.IRC.Data.Prefs;

public class NetInheritedItemEnabledStatus<InheritedType, ReadOnlyInterfaceType> : Platform.DataAndExt
	.Obj<NetInheritedItemEnabledStatus<InheritedType, ReadOnlyInterfaceType>>
	where InheritedType : IKeyChanged<InheritedType, string>, ReadOnlyInterfaceType
{
	public NetInheritedItemEnabledStatus(InheritedType inheritedItem, bool bStatus = true)
	{
		this.inheritedItem = inheritedItem;
		this.bStatus = bStatus;

		this.inheritedItem.evtKeyChanged += (in InheritedType changed, in string strOldKey, in string strNewKey) =>
			evtKeyOfInheritedItemChanged?.Invoke(this, strOldKey, strNewKey);
	}

	public readonly InheritedType inheritedItem;
	private bool bStatus;

	public ReadOnlyInterfaceType InheritedItem => inheritedItem;

	public bool Status
	{
		get => bStatus;

		set
		{
			if(bStatus != value)
			{
				bStatus = value;

				MakeDirty();

				FireStatusChanged();
			}
		}
	}

	public event DFieldChanged<bool>? evtStatusChanged;

	public event DFieldChanged<string>? evtKeyOfInheritedItemChanged;

	private void FireStatusChanged()
	{
		FirePropChanged(nameof(Status));

		evtStatusChanged?.Invoke(this, !bStatus, bStatus);
	}

	public static implicit operator ReadOnlyInterfaceType(NetInheritedItemEnabledStatus<InheritedType,
		ReadOnlyInterfaceType> val)
		=> val.InheritedItem;
}