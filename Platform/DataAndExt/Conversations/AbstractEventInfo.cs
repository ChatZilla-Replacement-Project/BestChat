// Ignore Spelling: ei dt Evt

namespace BestChat.Platform.DataAndExt.Conversations;

public interface IRawEventInfo
{
}

public interface IEventInfo : IRawEventInfo
{
	public string DescForEvt
	{
		get;
	}

	public System.DateTime WhenItHappened
	{
		get;
	}
}

public interface IRawMsgEventInfo : IRawEventInfo
{
	public string Sender
	{
		get;
	}

	public object SenderIcon
	{
		get;
	}
}

public interface IMsgEventInfo : IEventInfo, IRawMsgEventInfo
{
}

public class GroupInfo : IRawMsgEventInfo, System.Collections.Generic.IReadOnlyList<IEventInfo>
{
	public GroupInfo(in string strNickOfSender, in object objSenderIcon, params IEventInfo[] entries)
	{
		this.strNickOfSender = strNickOfSender;
		this.objSenderIcon = objSenderIcon;

		listEntries = entries.Length > 0 ? new(entries) : [];
	}

	public readonly string strNickOfSender;

	public readonly object objSenderIcon;

	private readonly System.Collections.ObjectModel.ObservableCollection<IEventInfo> listEntries;

	public IEventInfo this[int iIndex]
		=> listEntries[iIndex];

	public string Sender
		=> strNickOfSender;

	public object SenderIcon
		=> objSenderIcon;

	public int Count
		=> listEntries.Count;

	public System.Collections.Generic.IEnumerator<IEventInfo> GetEnumerator()
		=> listEntries.GetEnumerator();

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		=> listEntries.GetEnumerator();

	public void RecordEvent(IEventInfo eiNewEntry)
		=> listEntries.Add(eiNewEntry);
}

[System.ComponentModel.ImmutableObject(true)]
public abstract class AbstractEventInfo<EventType, EnumForEventType> : IEventInfo
	where EnumForEventType : struct, System.Enum
	where EventType : AbstractEventType<EnumForEventType>
{
	#region Constructors & Deconstructors
		protected AbstractEventInfo(in EventType type, in System.DateTime dtWhenItHappened)
		{
			this.type = type;
			this.dtWhenItHappened = dtWhenItHappened;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		public readonly EventType type;

		public readonly System.DateTime dtWhenItHappened;
	#endregion

	#region Properties
		public EventType Type
			=> type;

		public abstract string DescForEvt
		{
			get;
		}

		public System.DateTime WhenItHappened
			=> dtWhenItHappened;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}