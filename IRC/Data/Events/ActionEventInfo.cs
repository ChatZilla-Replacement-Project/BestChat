// Ignore Spelling: dt ech Dehalf Deopped Evt Opped Unban

namespace BestChat.IRC.Data.Events;

using Platform.DataAndExt.Ext;

[System.ComponentModel.ImmutableObject(true)]
public abstract class ActionEventInfo : Platform.DataAndExt.Conversations.AbstractEventInfo<Types.ActionEventType,
	Types.ActionEventType.Types>
{
	#region Constructors & Deconstructors
		protected internal ActionEventInfo(in Types.ActionEventType type, in System.DateTime? dtWhenItHappened = null) :
			base(type, dtWhenItHappened ?? System.DateTime.Now)
		{
		}
	#endregion

	#region Helper Types
		public class KickOfUserActionEventInfo : ActionEventInfo
		{
			internal KickOfUserActionEventInfo(in string strUserThatWasKicked, in string strWhoDidTheKicking, in System.DateTime? dtWhenItHappened
					= null) :
				base(Types.ActionEventType.KickOfUser, dtWhenItHappened)
			{
				this.strUserThatWasKicked = strUserThatWasKicked;
				this.strWhoDidTheKicking = strWhoDidTheKicking;
			}

			public readonly string strUserThatWasKicked;

			public readonly string strWhoDidTheKicking;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeKickOfUserDesc.Fmt(strUserThatWasKicked, strWhoDidTheKicking);
		}

		public class BanOfUserActionEventInfo : ActionEventInfo
		{
			internal BanOfUserActionEventInfo(in string strUserThatWasBanned, in string strWhoDidTheBanning, in System.DateTime? dtWhenItHappened
					= null) :
				base(Types.ActionEventType.BanOfUser, dtWhenItHappened)
			{
				this.strUserThatWasBanned = strUserThatWasBanned;
				this.strWhoDidTheBanning = strWhoDidTheBanning;
			}

			public readonly string strUserThatWasBanned;

			public readonly string strWhoDidTheBanning;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeBanOfUserDesc.Fmt(strUserThatWasBanned, strWhoDidTheBanning);
		}

		public class UnbanOfUserActionEventInfo : ActionEventInfo
		{
			internal UnbanOfUserActionEventInfo(in string strUserThatWasUnbanned, in string strWhoDidTheUnbanning, in System.DateTime?
					dtWhenItHappened = null) :
				base(Types.ActionEventType.UnbanOfUser, dtWhenItHappened)
			{
				this.strUserThatWasUnbanned = strUserThatWasUnbanned;
				this.strWhoDidTheUnbanning = strWhoDidTheUnbanning;
			}

			public readonly string strUserThatWasUnbanned;

			public readonly string strWhoDidTheUnbanning;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeUnbanOfUserDesc.Fmt(strUserThatWasUnbanned, strWhoDidTheUnbanning);
		}

		public class QuietOfUserActionEventInfo : ActionEventInfo
		{
			internal QuietOfUserActionEventInfo(in string strUserThatWasQuieted, in string strWhoDidTheQuieting, in System.DateTime?
					dtWhenItHappened = null) :
				base(Types.ActionEventType.QuietOfUser, dtWhenItHappened)
			{
				this.strUserThatWasQuieted = strUserThatWasQuieted;
				this.strWhoDidTheQuieting = strWhoDidTheQuieting;
			}

			public readonly string strUserThatWasQuieted;

			public readonly string strWhoDidTheQuieting;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeQuietOfUserDesc.Fmt(strUserThatWasQuieted, strWhoDidTheQuieting);
		}

		public class UnquietOfUserActionEventInfo : ActionEventInfo
		{
			internal UnquietOfUserActionEventInfo(in string strUserThatWasUnquieted, in string strWhoDidTheUnquieting, in System.DateTime?
					dtWhenItHappened = null) :
				base(Types.ActionEventType.UnquietOfUser, dtWhenItHappened)
			{
				this.strUserThatWasUnquieted = strUserThatWasUnquieted;
				this.strWhoDidTheUnquieting = strWhoDidTheUnquieting;
			}

			public readonly string strUserThatWasUnquieted;

			public readonly string strWhoDidTheUnquieting;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeUnquietOfUserDesc.Fmt(strUserThatWasUnquieted, strWhoDidTheUnquieting);
		}

		public class SelfJoinActionEventInfo : ActionEventInfo
		{
			internal SelfJoinActionEventInfo(in string strProperNameOfChanTheUserJoined, in System.DateTime? dtWhenItHappened = null) :
				base(Types.ActionEventType.Join, dtWhenItHappened)
				=> this.strProperNameOfChanTheUserJoined = strProperNameOfChanTheUserJoined;

			public readonly string strProperNameOfChanTheUserJoined;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeSelfJoinDesc.Fmt(strProperNameOfChanTheUserJoined);
		}

		public class JoinActionEventInfo : ActionEventInfo
		{
			internal JoinActionEventInfo(in string strUserThatJoined, in System.DateTime? dtWhenItHappened = null) :
				base(Types.ActionEventType.Join, dtWhenItHappened)
				=> this.strUserThatJoined = strUserThatJoined;

			public readonly string strUserThatJoined;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeJoinDesc.Fmt(strUserThatJoined);
		}

		public class QuitActionEventInfo : ActionEventInfo
		{
			internal QuitActionEventInfo(in string strUserThatQuit, in System.DateTime? dtWhenItHappened = null) :
				base(Types.ActionEventType.Quit, dtWhenItHappened)
				=> this.strUserThatQuit = strUserThatQuit;

			public readonly string strUserThatQuit;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeQuitDesc.Fmt(strUserThatQuit);
		}

		public class TopicChangeActionEventInfo : ActionEventInfo
		{
			internal TopicChangeActionEventInfo(in string strChanProperName, in string strNewTopic, in string strTopicSetByUser, in System.DateTime
					dtSetAt, in System.DateTime? dtWhenItHappened = null) :
				base(Types.ActionEventType.TopicChanged, dtWhenItHappened)
			{
				this.strChanProperName = strChanProperName;
				this.strNewTopic = strNewTopic;
				this.strTopicSetByUser = strTopicSetByUser;
				this.dtSetAt = dtSetAt;
			}

			public readonly string strChanProperName;

			public readonly string strNewTopic;

			public readonly string strTopicSetByUser;

			public readonly System.DateTime dtSetAt;

			// TODO: Once preferences are more functional, ask them for how to format the date.
			public override string DescForEvt
				=> Rsrcs.strActionEventTypeTopicChangedDesc.Fmt(strChanProperName, strNewTopic, strTopicSetByUser, dtSetAt);
		}

		public class ChanModeAddedActionEventInfo : ActionEventInfo
		{
			internal ChanModeAddedActionEventInfo(in string strChanProperName, in System.Collections.Generic.IEnumerable<char> echModesAdded, in
					System.DateTime? dtWhenItHappened = null) :
				base(Types.ActionEventType.ChanModeAdded, dtWhenItHappened)
			{
				this.strChanProperName = strChanProperName;
				this.echModesAdded = echModesAdded;
			}

			public readonly string strChanProperName;

			public readonly System.Collections.Generic.IEnumerable<char> echModesAdded;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeChanModeAddedDesc.Fmt(strChanProperName, string.Join(", ", echModesAdded));
		}

		public class ChanModeRemovedActionEventInfo : ActionEventInfo
		{
			internal ChanModeRemovedActionEventInfo(in string strChanProperName, in System.Collections.Generic.IEnumerable<char> echModesRemoved, in
					System.DateTime? dtWhenItHappened = null) :
				base(Types.ActionEventType.ChanModeRemoved, dtWhenItHappened)
			{
				this.strChanProperName = strChanProperName;
				this.echModesRemoved = echModesRemoved;
			}

			public readonly string strChanProperName;

			public readonly System.Collections.Generic.IEnumerable<char> echModesRemoved;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeChanModeRemovedDesc.Fmt(strChanProperName, string.Join(", ", echModesRemoved));
		}

		public class ChanModeAddedAndRemovedActionEventInfo : ActionEventInfo
		{
			internal ChanModeAddedAndRemovedActionEventInfo(in string strChanProperName, in System.Collections.Generic.IEnumerable<char>
					echModesAdded, in System.Collections.Generic.IEnumerable<char> echModesRemoved, in System.DateTime? dtWhenItHappened = null) :
				base(Types.ActionEventType.ChanModeAddedAndRemoved, dtWhenItHappened)
			{
				this.strChanProperName = strChanProperName;
				this.echModesAdded = echModesAdded;
				this.echModesRemoved = echModesRemoved;
			}

			public readonly string strChanProperName;

			public readonly System.Collections.Generic.IEnumerable<char> echModesAdded;

			public readonly System.Collections.Generic.IEnumerable<char> echModesRemoved;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeChanModeAddedAndRemovedDesc.Fmt(strChanProperName, string.Join(", ", echModesAdded), string
					.Join(", ", echModesRemoved));
		}

		public class UserVoicedActionEventInfo : ActionEventInfo
		{
			internal UserVoicedActionEventInfo(in string strUserGettingVoice, in string strUserIssuingVoice, in System.DateTime? dtWhenItHappened =
					null) :
				base(Types.ActionEventType.UserVoiced, dtWhenItHappened)
			{
				this.strUserGettingVoice = strUserGettingVoice;
				this.strUserIssuingVoice = strUserIssuingVoice;
			}

			public readonly string strUserGettingVoice;

			public readonly string strUserIssuingVoice;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeUserVoicedDesc.Fmt(strUserGettingVoice, strUserIssuingVoice);
		}

		public class UserDevoicedActionEventInfo : ActionEventInfo
		{
			internal UserDevoicedActionEventInfo(in string strUserLosingVoice, in string strUserRevokingVoice, in System.DateTime? dtWhenItHappened
					= null) :
				base(Types.ActionEventType.UserDevoiced, dtWhenItHappened)
			{
				this.strUserLosingVoice = strUserLosingVoice;
				this.strUserRevokingVoice = strUserRevokingVoice;
			}

			public readonly string strUserLosingVoice;

			public readonly string strUserRevokingVoice;

			public override string DescForEvt => Rsrcs.strActionEventTypeUserDevoicedDesc
				.Fmt(strUserLosingVoice, strUserRevokingVoice);
		}

		public class UserOppedActionEventInfo : ActionEventInfo
		{
			internal UserOppedActionEventInfo(in string strUserGettingOp, in string strUserIssuingOp, in System.DateTime? dtWhenItHappened = null)
				: base(Types.ActionEventType.UserOpped, dtWhenItHappened)
			{
				this.strUserGettingOp = strUserGettingOp;
				this.strUserIssuingOp = strUserIssuingOp;
			}

			public readonly string strUserGettingOp;

			public readonly string strUserIssuingOp;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeUserOppedDesc.Fmt(strUserGettingOp, strUserIssuingOp);
		}

		public class UserDeoppedActionEventInfo : ActionEventInfo
		{
			internal UserDeoppedActionEventInfo(in string strUserLosingOp, in string strUserRevokingOp, in System.DateTime? dtWhenItHappened =
					null) :
				base(Types.ActionEventType.UserDeopped, dtWhenItHappened)
			{
				this.strUserLosingOp = strUserLosingOp;
				this.strUserRevokingOp = strUserRevokingOp;
			}

			public readonly string strUserLosingOp;

			public readonly string strUserRevokingOp;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeUserDeoppedDesc.Fmt(strUserLosingOp, strUserRevokingOp);
		}

		public class UserHalfOppedActionEventInfo : ActionEventInfo
		{
			internal UserHalfOppedActionEventInfo(in string strUserGettingHalfOp, in string strUserIssuingHalfOp, in System.DateTime?
					dtWhenItHappened = null) :
				base(Types.ActionEventType.UserHalfOpped, dtWhenItHappened)
			{
				this.strUserGettingHalfOp = strUserGettingHalfOp;
				this.strUserIssuingHalfOp = strUserIssuingHalfOp;
			}

			public readonly string strUserGettingHalfOp;

			public readonly string strUserIssuingHalfOp;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeUserOppedDesc.Fmt(strUserGettingHalfOp, strUserIssuingHalfOp);
		}

		public class UserDehalfOppedActionEvtInfo : ActionEventInfo
		{
			internal UserDehalfOppedActionEvtInfo(in string strUserLosingHalfOp, in string strUserRevokingHalfOp, in System.DateTime?
					dtWhenItHappened = null) :
				base(Types.ActionEventType.UserDehalfOpped, dtWhenItHappened)
			{
				this.strUserLosingHalfOp = strUserLosingHalfOp;
				this.strUserRevokingHalfOp = strUserRevokingHalfOp;
			}

			public readonly string strUserLosingHalfOp;

			public readonly string strUserRevokingHalfOp;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeUserDehalfOppedDesc.Fmt(strUserLosingHalfOp, strUserRevokingHalfOp);
		}

		public class NickOfUserChangedActionEvtInfo : ActionEventInfo
		{
			internal NickOfUserChangedActionEvtInfo(in string strOldNick, in string strNewNick, in System.DateTime? dtWhenItHappened = null) :
				base(Types.ActionEventType.NickOfUserChanged,dtWhenItHappened)
			{
				this.strOldNick = strOldNick;
				this.strNewNick = strNewNick;
			}

			public readonly string strOldNick;

			public readonly string strNewNick;

			public override string DescForEvt
				=> Rsrcs.strActionEventTypeNickOfUserChangedDesc.Fmt(strOldNick, strNewNick);
		}
	#endregion
}