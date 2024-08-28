// Ignore Spelling: aet Unban Opped Deopped Dehalf

namespace BestChat.IRC.Data.Events.Types;

[System.ComponentModel.ImmutableObject(true)]
public class ActionEventType : Platform.DataAndExt.Conversations.AbstractEventType<ActionEventType.Types>
{
	#region Constructors & Deconstructors
		private ActionEventType(in Types type, in string strDescOfVal) :
			base(type, strDescOfVal)
		{
		}
	#endregion

	#region Constants
		public static readonly ActionEventType aetKickOfUser = new(Types.kickOfUser, Rsrcs.strActionEventTypeKickOfUserDesc);

		public static readonly ActionEventType aetBanOfUser = new(Types.banOfUser, Rsrcs.strActionEventTypeBanOfUserDesc);

		public static readonly ActionEventType aetUnbanOfUser = new(Types.unbanOfUser, Rsrcs.strActionEventTypeUnbanOfUserDesc);

		public static readonly ActionEventType aetQuietOfUser = new(Types.quietOfUser, Rsrcs.strActionEventTypeQuietOfUserDesc);

		public static readonly ActionEventType aetUnquietOfUser = new(Types .unquietOfUser, Rsrcs
			.strActionEventTypeUnquietOfUserDesc);

		public static readonly ActionEventType aetSelfJoin = new(Types.selfJoin, Rsrcs.strActionEventTypeSelfJoinDesc);

		public static readonly ActionEventType aetJoin = new(Types.join, Rsrcs.strActionEventTypeJoinDesc);

		public static readonly ActionEventType aetQuit = new(Types.quit, Rsrcs.strActionEventTypeQuitDesc);

		public static readonly ActionEventType aetTopicChanged = new(Types.topicChanged, Rsrcs.strActionEventTypeTopicChangedDesc);

		public static readonly ActionEventType aetChanModeAdded = new(Types.chanModeAdded, Rsrcs
			.strActionEventTypeChanModeAddedDesc);

		public static readonly ActionEventType aetChanModeRemoved = new(Types.chanModeRemoved, Rsrcs
			.strActionEventTypeChanModeRemovedDesc);

		public static readonly ActionEventType aetChanModeAddedAndRemoved = new(Types.chanModeAddedAndRemoved, Rsrcs
			.strActionEventTypeChanModeAddedAndRemovedDesc);

		public static readonly ActionEventType aetUserVoiced = new(Types.userVoiced, Rsrcs.strActionEventTypeUserVoicedDesc);

		public static readonly ActionEventType aetUserDevoiced = new(Types.userDevoiced, Rsrcs.strActionEventTypeUserDevoicedDesc);

		public static readonly ActionEventType aetUserOpped = new(Types.userOpped, Rsrcs.strActionEventTypeUserOppedDesc);

		public static readonly ActionEventType aetUserDeopped = new(Types.userDeopped, Rsrcs.strActionEventTypeUserDeoppedDesc);

		public static readonly ActionEventType aetUserHalfOpped = new(Types.userHalfOpped, Rsrcs
			.strActionEventTypeUserHalfOppedDesc);

		public static readonly ActionEventType aetUserDehalfOpped = new(Types.userDehalfOpped, Rsrcs
			.strActionEventTypeUserDehalfOppedDesc);

		public static readonly ActionEventType aetNickOfUserChanged = new(Types.nickOfUserChanged, Rsrcs
			.strActionEventTypeNickOfUserChangedDesc);
	#endregion

	#region Helper Types
		public enum Types
		{
			kickOfUser,
			banOfUser,
			unbanOfUser,
			quietOfUser,
			unquietOfUser,
			selfJoin,
			join,
			quit,
			part,
			topicChanged,
			chanModeAdded,
			chanModeRemoved,
			chanModeAddedAndRemoved,
			userVoiced,
			userDevoiced,
			userOpped,
			userDeopped,
			userHalfOpped,
			userDehalfOpped,
			nickOfUserChanged,
			userModeChanged
		}
	#endregion

	#region Properties
		public static ActionEventType KickOfUser
			=> aetKickOfUser;

		public static ActionEventType BanOfUser
			=> aetBanOfUser;

		public static ActionEventType UnbanOfUser
			=> aetUnbanOfUser;

		public static ActionEventType QuietOfUser
			=> aetQuietOfUser;

		public static ActionEventType UnquietOfUser
			=> aetUnquietOfUser;

		public static ActionEventType SelfJoin
			=> aetSelfJoin;

		public static ActionEventType Join
			=> aetJoin;

		public static ActionEventType Quit
			=> aetQuit;

		public static ActionEventType TopicChanged
			=> aetTopicChanged;

		public static ActionEventType ChanModeAdded
			=> aetChanModeAdded;

		public static ActionEventType ChanModeRemoved
			=> aetChanModeRemoved;

		public static ActionEventType ChanModeAddedAndRemoved
			=> aetChanModeAddedAndRemoved;

		public static ActionEventType UserVoiced
			=> aetUserVoiced;

		public static ActionEventType UserDevoiced
			=> aetUserDevoiced;

		public static ActionEventType UserOpped
			=> aetUserOpped;

		public static ActionEventType UserDeopped
			=> aetUserDeopped;

		public static ActionEventType UserHalfOpped = aetUserHalfOpped;

		public static ActionEventType UserDehalfOpped
			=> aetUserDehalfOpped;

		public static ActionEventType NickOfUserChanged
			=> aetNickOfUserChanged;
	#endregion
}