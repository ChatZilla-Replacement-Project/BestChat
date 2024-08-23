namespace BestChat.Platform.DataAndExt.Conversations;

public interface IGroupViewOrConversation : TreeData.IItemInfo
{
	string Name
	{
		get;
	}

	string ProperName
	{
		get;
	}

	string SafeName
	{
		get;
	}

	string LongDesc
	{
		get;
	}

	string Path
	{
		get;
	}
}