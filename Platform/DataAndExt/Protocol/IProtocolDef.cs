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

	bool GuiRecommended
	{
		get;
	}
}