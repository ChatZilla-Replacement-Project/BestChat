namespace BestChat.IRC.Data.Events.Types;

[System.ComponentModel.ImmutableObject(true)]
public class NoticeEventType : Platform.DataAndExt.Conversations.AbstractEventType<NoticeEventType.Types>
{
	#region Constructors & Deconstructors
		private NoticeEventType(in Types type, in string strDescOfVal) :
			base(type, strDescOfVal)
		{
		}
	#endregion

	#region Constants
		public static readonly NoticeEventType noteInfo = new(Types.info, Rsrcs.strNoticeEventTypeInfoName);

		public static readonly NoticeEventType noteConnectAttemptInProgress = new(Types.info, Rsrcs
			.strNoticeEventTypeConnectionAttemptInProgressDesc);
	#endregion

	#region Helper Types
		public enum Types
		{
			info,
			connectAttemptInProgress
		}
	#endregion

	#region Properties
		public static NoticeEventType Info
			=> noteInfo;

		public static NoticeEventType ConnectAttemptInProgress
			=> noteConnectAttemptInProgress;
	#endregion
}