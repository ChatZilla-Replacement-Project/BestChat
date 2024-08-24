namespace BestChat.IRC.Data.Events.Types;

[System.ComponentModel.ImmutableObject(true)]
public class PostEventType : Platform.DataAndExt.Conversations.AbstractEventType<PostEventType.Types>
{
	#region Constructors & Deconstructors
		private PostEventType(in Types type, in string strDescOfVal) :
			base(type, strDescOfVal)
		{
		}
	#endregion

	#region Constants
		public static readonly PostEventType petSay = new(Types.say, Rsrcs.strPostActionSayDesc);

		public static readonly PostEventType petMe = new(Types.me, Rsrcs.strPostActionMeDesc);
	#endregion

	#region Helper Types
		public enum Types
		{
			say,
			me
		}
	#endregion

	#region Properties
		public static PostEventType Say
			=> petSay;

		public static PostEventType Me
			=> petMe;
	#endregion
}