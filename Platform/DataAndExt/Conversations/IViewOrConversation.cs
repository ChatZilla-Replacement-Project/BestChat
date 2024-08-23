namespace BestChat.Platform.DataAndExt.Conversations;

public interface IViewOrConversation : IGroupViewOrConversation
{
	public enum Types
	{
		channelOrRoom,
		virtualGroup,
		group,
		user,
		client,
	}

	public Types Type
	{
		get;
	}
}