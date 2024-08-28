// Ignore Spelling: dt

namespace BestChat.IRC.Data.Events;

[System.ComponentModel.ImmutableObject(true)]
public abstract class PostEventInfo : Platform.DataAndExt.Conversations.AbstractEventInfo<Types.PostEventType, Types.PostEventType.Types>,
	Platform.DataAndExt.Conversations.IMsgEventInfo
{
	#region Constructors & Deconstructors
		protected internal PostEventInfo(in Types.PostEventType type, in System.DateTime? dtWhenItHappened = null) :
			base(type, dtWhenItHappened ?? System.DateTime.Now)
		{
		}
	#endregion

	#region Helper Types
		public class SayPostEventInfo : PostEventInfo
		{
			internal SayPostEventInfo(in string strNickOfSender, in string strMsg, in System.DateTime? dtWhenItHappened = null) :
				base(Types.PostEventType.Say, dtWhenItHappened)
			{
				this.strNickOfSender = strNickOfSender;
				this.strMsg = strMsg;
			}

			public readonly string strNickOfSender;

			public readonly string strMsg;

			public override string Sender
				=> strNickOfSender;

			public override object SenderIcon
				=> strNickOfSender[0];

			public override string DescForEvt
				=> strMsg;
		}

		public class MePostEventInfo : PostEventInfo
		{
			internal MePostEventInfo(in string strNickOfSender, in string strMsg, in System.DateTime? dtWhenItHappened = null) :
				base(Types.PostEventType.Me, dtWhenItHappened)
			{
				this.strNickOfSender = strNickOfSender;
				this.strMsg = strMsg;
			}

			public readonly string strNickOfSender;

			public readonly string strMsg;

			public override string Sender
				=> strNickOfSender;

			public override object SenderIcon
				=> strNickOfSender[0];

			public override string DescForEvt
				=> strMsg;
		}
	#endregion

	#region Properties
		public abstract string Sender
		{
			get;
		}

		public abstract object SenderIcon
		{
			get;
		}
	#endregion
}