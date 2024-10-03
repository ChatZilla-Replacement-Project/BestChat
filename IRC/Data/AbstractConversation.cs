namespace BestChat.IRC.Data;

public abstract class AbstractConversation : Platform.DataAndExt.Conversations.AbstractConversation
{
	protected AbstractConversation(in string strName, in string strLongDesc) :
		base(strName, strLongDesc)
	{
	}

	public virtual string? Topic
	{
		get => null;

		[ System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "ValueParameterNotUsed") ]
		set
		{
		}
	}


	public virtual bool IsTopicLocked
		=> true;

	public virtual bool IsSecret
		=> false;

	public virtual bool IsPrivate
		=> false;

	public virtual bool IsModerated
		=> false;

	public virtual bool AreColorsStripped
		=> false;

	public virtual bool AreOutsideMsgProhibited
		=> false;

	public virtual bool IsInviteOnly
		=> false;

	public virtual bool IsKeywordRequired
		=> false;
}