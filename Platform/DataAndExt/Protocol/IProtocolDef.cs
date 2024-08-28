namespace BestChat.Platform.DataAndExt.Protocol;

public interface IProtocolDef
{
	string Name
	{
		get;
	}

	string Publisher
	{
		get;
	}

	System.Uri Homepage
	{
		get;
	}

	System.Uri PublisherHomepage
	{
		get;
	}

	Prefs.AbstractChildMgr? RootPrefForProtocol
	{
		get;
	}

	Conversations.IGroupViewOrConversation? TopLevelViewGroupOrConversation
	{
		get;
	}

	void SaveAllData(in System.IO.DirectoryInfo dirDataLoc)
	{
	}

	bool GuiRecommended
	{
		get;
	}
}