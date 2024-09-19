// Ignore Spelling: evt

namespace BestChat.IRC.Data;

using Platform.DataAndExt.Ext;

public class ConversationWithRemoteUser : AbstractConversation, Platform.DataAndExt.TreeData.IItemInfo
{
	#region Constructors & Deconstructors
		public ConversationWithRemoteUser(in ActiveNet anetOwner, in RemoteUser ru) :
			base(ru.CurNick, Rsrcs.strRemoteUserDescForTree.Fmt(ru.CurNick, anetOwner.Name))
		{
			if(ru == anetOwner.ru)
				throw new System.InvalidProgramException($"A ConversationWithRemoteUser was created, but it's parent, {anetOwner.Name}, " +
					$"happens to be the same remote user: {ru.CurNick}");

			this.anetOwner = anetOwner;
			this.ru = ru;

			#if DemoMode
				System.Text.Json.JsonDocument doc = System.Text.Json.JsonDocument.Parse(client.GetStreamAsync(ru.CurNick switch
					{
						"Peter" => "https://github.com/ChatZilla-Replacement-Project/JSON-Data/blob/main/Sample%20Data/" +
							"Sample%20conversation%20with%20Micheal%20log.json",
						"Micheal" => "https://github.com/ChatZilla-Replacement-Project/JSON-Data/blob/main/Sample%20Data/" +
							"Sample%20conversation%20with%20Peter%20log.json",
						_ => throw new System.Exception("Unexpected user name"),
					}).Result, jdo);

				if(doc.RootElement.ValueKind == System.Text.Json.JsonValueKind.Array)
					foreach(System.Text.Json.JsonElement elementCur in doc.RootElement.EnumerateArray())
						if(elementCur.ValueKind == System.Text.Json.JsonValueKind.Object)
							switch(elementCur.GetProperty(nameof(Type)).GetString())
							{
								case "action":
									switch(elementCur.GetProperty("SubType").GetString())
									{
										case nameof(Events.Types.ActionEventType.Types.selfJoin):
											RecordEvent(new Events.ActionEventInfo.SelfJoinActionEventInfo(anetOwner.Def.Name));

											break;

										case nameof(Events.Types.ActionEventType.Types.topicChanged):
											RecordEvent(new Events.ActionEventInfo.TopicChangeActionEventInfo(ProperName, elementCur
												.GetProperty("NewTopic").GetString() ?? throw new System.Exception("Topic change record from the " +
												$"sample log for {ProperName} is missing the required NewTopic field"), elementCur.GetProperty("SetBy")
												.GetString() ?? throw new System.Exception($"Topic change record from the sample log for {ProperName} is " +
												$"missing the required SetBy field"), elementCur.GetProperty("SetAt").GetDateTime()));

											break;

										case nameof(Events.Types.ActionEventType.Types.userOpped):
											RecordEvent(new Events.ActionEventInfo.UserOppedActionEventInfo(elementCur.GetProperty("WhoGotOp")
												.GetString() ?? throw new System.Exception("user got op record from sample channel log is missing the " +
												"required WhoGotOp field"), elementCur.GetProperty("WhoIssuedOp").GetString() ?? throw new System
												.Exception("Op issued record from sample channel log data is missing the required WhoIssuedOp field")));

											break;

										default:
											throw new System.Exception("Unknown event notice subtype in network log sample data");
									}

									break;

								case "post":
									switch(elementCur.GetProperty("SubType").GetString())
									{
										case nameof(Events.Types.PostEventType.Types.say):
											RecordEvent(new Events.PostEventInfo.SayPostEventInfo(elementCur.GetProperty("NickOfSender")
												.GetString() ?? throw new System.Exception("Record of /say in channel log is missing the required " +
												"NickOfSender field"), elementCur.GetProperty("Msg").GetString() ?? throw new System.Exception("Required"
												+ " Msg field is missing from /say record in sample channel log data")));

											break;

										case nameof(Events.Types.PostEventType.Types.me):
											RecordEvent(new Events.PostEventInfo.MePostEventInfo(elementCur.GetProperty("NickOfSender").GetString()
												?? throw new System.Exception("Record of /me in channel log is missing the required NickOfSender field"),
												elementCur.GetProperty("Msg").GetString() ?? throw new System.Exception("Required Msg field is missing " +
												"from /me record in sample channel log data")));

											break;

										default:
											throw new System.Exception("Unknown event notice subtype in network log sample data");
									}

									break;

								case "notice":
									switch(elementCur.GetProperty("SubType").GetString())
									{
										case nameof(Events.Types.NoticeEventType.Types.info):
											RecordEvent(new Events.NoticeEventInfo.InfoNoticeEventInfo(elementCur.GetProperty(nameof(Events.ActionEventInfo
												.DescForEvt)).GetString() ?? throw new System.Exception("info event from channel logged sample data is missing the "
												+ "required DescForEvent field.")));

											break;

										default:
											throw new System.Exception("Unknown event notice subtype in network log sample data");
									}
									break;

								default:
									throw new System.Exception("Unknown event type in network log sample data");
							}
			#else
			#endif
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public override event System.Action<Platform.DataAndExt.Dieable.IDieable>? evtDieing;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		#if DemoMode
			private static readonly System.Net.Http.HttpClient client = new();

			private static readonly System.Text.Json.JsonDocumentOptions jdo = new()
			{
				AllowTrailingCommas = true,
				CommentHandling = System.Text.Json.JsonCommentHandling.Skip,
			};
		#endif

		public readonly ActiveNet anetOwner;

		public readonly RemoteUser ru;
	#endregion

	#region Properties
		public override string ProperName => ru.CurNick;

		public override string SafeName => ru.CurNick;

		public override string Path => $"{anetOwner.Name}/{Rsrcs.strConversationGroupTypeRemoteUsers}/{
			ru.CurNick}";

		public override string LocalizedName => ru.CurNick;

		public override string Icon => "🧑";

		public override Platform.DataAndExt.Conversations.IViewOrConversation.Types Type => Platform.DataAndExt.Conversations
			.IViewOrConversation.Types.user;
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
	#endregion
}