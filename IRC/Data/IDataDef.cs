namespace BestChat.IRC.Data;

public interface IDataDefBasic
{
	string Name
	{
		get;
	}
}

public interface IDataDef<ItemType> : IDataDefBasic
	where ItemType : Platform.DataAndExt.Obj<ItemType>
{
	event Platform.DataAndExt.Obj<ItemType>.DFieldChanged<string> evtNameChanged;
}