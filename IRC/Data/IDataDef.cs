namespace BestChat.IRC.Data;

public interface IDataDefBasic<ItemType>
{
	string Name
	{
		get;
	}
}

public interface IDataDef<ItemType> : IDataDefBasic<ItemType>
	where ItemType : Platform.DataAndExt.Obj<ItemType>
{
	event Platform.DataAndExt.Obj<ItemType>.DFieldChanged<string> evtNameChanged;
}