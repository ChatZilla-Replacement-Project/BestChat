namespace BestChat.Platform.DataAndExt.Protocol;

public interface IProtocolDef
{
	string Name
	{
		get;
	}

	string LocalizedName
	{
		get;
	}

	string LocalizedDesc
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

	Conversations.IGroupViewOrConversation? TopLevelViewGroupOrConversation
	{
		get;
	}

	bool GuiRecommended
	{
		get;
	}
}