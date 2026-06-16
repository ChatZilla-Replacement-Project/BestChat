namespace BestChat.Platform.DataAndExt.Conversations
{
	public interface IGroup : IGroupViewOrConversation, TreeData.IChildOwner
	{
		public System.Collections.Generic.IReadOnlyDictionary<string, IGroupViewOrConversation> ChildrenByName
		{
			get;
		}

		public System.Collections.Generic.IEnumerable<IGroupViewOrConversation> UnsortedChildren
		{
			get;
		}
	}
}