// Ignore Spelling: evt

namespace BestChat.IRC.Data;

using Platform.DataAndExt.Ext;

public class Chan : AbstractConversation, Platform.DataAndExt.TreeData.IItemInfo
{
	#region Constructors & Deconstructors
		public Chan(in ActiveNetwork anetOwner, in string strName) :
			base(strName, Rsrcs.strChanNameDescForTree.Fmt(strName, anetOwner.unetDef.Name))
		{
			mapAllChanByName[strName] = this;

			this.anetOwner = anetOwner;
			this.strName = strName;

			#if DemoMode
				// TODO: Get the real modes for the channel on this network
				foreach(Defs.ChanMode cmodeCur in anetOwner.unetDef.ChanModesByModeChar.Values)
				{
					Defs.TwoWayMode modeNew = new(cmodeCur, Defs.BoolModeState.off);

					mapModesOnChan[cmodeCur.ModeChar] = modeNew;

					modeNew.evtStateChanged += OnStateOfModeChanged;

					foreach(Defs.Mode<Defs.BoolModeState, Defs.BoolModeStates>.Param mpCur in modeNew.AllParamsByName.Values)
						mpCur.evtValChanged += OnValOfModeParamChanged;

					if(cmodeCur.smt.HasValue)
						mapModesWithStdTypes[cmodeCur.smt.Value] = modeNew;
				}

				// TODO: Replace this with the real topic
				strTopic = strName;


				System.Text.Json.JsonDocument doc = System.Text.Json.JsonDocument.Parse(client.GetStreamAsync(strName
					switch
					{
						"##space"
							=> "https://github.com/ChatZilla-Replacement-Project/JSON-Data/blob/main/Sample%20Data/" +
								"Sample%20%23%23space%20log.json",
						"#best-chat"
							=> "https://github.com/ChatZilla-Replacement-Project/JSON-Data/blob/main/Sample%20Data/" +
								"Sample%20%23best-chat%20log.json",
						_
							=> throw new System.Exception("Unexpected channel name"),
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
												.GetProperty("NewTopic").GetString() ?? throw new System.Exception("Topic " +
												$"change record from the sample log for {ProperName} is missing the required NewTopic " +
												"field"), elementCur.GetProperty("SetBy").GetString() ?? throw new System
												.Exception($"Topic change record from the sample log for {ProperName} is missing " +
												"the required SetBy field"), elementCur.GetProperty("SetAt").GetDateTime()));

											break;

										case nameof(Events.Types.ActionEventType.Types.userOpped):
											RecordEvent(new Events.ActionEventInfo.UserOppedActionEventInfo(elementCur
												.GetProperty("WhoGotOp").GetString() ?? throw new System.Exception("user got op"
												+ " record from sample channel log is missing the require WhoGotOp field"), elementCur
												.GetProperty("WhoIssuedOp").GetString() ?? throw new System.Exception("Op "
												+ "issued record from sample channel log data is missing the required WhoIssuedOp " +
												"field")));

											break;

										default:
											throw new System.Exception("Unknown event notice subtype in network log sample data");
									}

									break;

								case "post":
									switch(elementCur.GetProperty("SubType").GetString())
									{
										case nameof(Events.Types.PostEventType.Types.say):
											RecordEvent(new Events.PostEventInfo.SayPostEventInfo(elementCur
												.GetProperty("NickOfSender").GetString() ?? throw new System.Exception("Record "
												+ "of /say in channel log is missing the required NickOfSender field"), elementCur
												.GetProperty("Msg").GetString() ?? throw new System.Exception("Required"
												+ " Msg field is missing from /say record in sample channel log data")));

											break;

										case nameof(Events.Types.PostEventType.Types.me):
											RecordEvent(new Events.PostEventInfo.MePostEventInfo(elementCur
												.GetProperty("NickOfSender").GetString() ?? throw new System
												.Exception("Record of /me in channel log is missing the required NickOfSender " +
												"field"), elementCur.GetProperty("Msg").GetString() ?? throw new System
												.Exception("Required Msg field is missing from /me record in sample channel log " +
												"data")));

											break;

										default:
											throw new System.Exception("Unknown event notice subtype in network log sample data");
									}

									break;

								case "notice":
									switch(elementCur.GetProperty("SubType").GetString())
									{
										case nameof(Events.Types.NoticeEventType.Types.info):
											RecordEvent(new Events.NoticeEventInfo
												.InfoNoticeEventInfo(elementCur.GetProperty(nameof(Events.ActionEventInfo.DescForEvt))
												.GetString() ?? throw new System.Exception("info event from channel logged sample " +
												"data is missing the required DescForEvent field.")));

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

		~Chan()
		{
			evtDieing?.Invoke(this);

			mapAllChanByName.Remove(strName);
		}
	#endregion

	#region Delegates
		public delegate void DStrFieldChanged(in Chan chanSender, in string strOldTopic, in string strNewTopic);
	#endregion

	#region Events
		public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

		public override event System.Action<Platform.DataAndExt.Dieable.IDieable>? evtDieing;

		public event DStrFieldChanged? evtTopicChanged;
	#endregion

	#region Constants
		public const char chPrefix = '#';

		public static readonly string strSafePrefix = System.Net.WebUtility.UrlEncode($"{chPrefix}") ?? throw new
			System.InvalidProgramException("Unexpected failure to initialize strSafePrefix");
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


		private static readonly System.Collections.Generic.SortedDictionary<string, Chan> mapAllChanByName =
			[];


		public readonly ActiveNetwork anetOwner;

		public readonly string strName;

		private string strTopic;

		private readonly System.Collections.Generic.SortedDictionary<char, Defs.TwoWayMode> mapModesOnChan =
			[];

		// TODO: We need a way to update this map when the remote user's nick changes.
		private readonly System.Collections.Generic.SortedDictionary<string, RemoteUser> mapChanMembersByNick =
			[];

		private readonly System.Collections.Generic.Dictionary<Defs.ChanMode.StdModeTypes, Defs.TwoWayMode>
			mapModesWithStdTypes = [];
	#endregion

	#region Properties
		public static System.Collections.Generic.IReadOnlyDictionary<string, Chan> AllChanByName
			=> mapAllChanByName;


		public ActiveNetwork Owner
			=> anetOwner;

		public override string ProperName
			=> strName.StartsWith(chPrefix) ? strName : $"{chPrefix}{strName}";

		public override string SafeName
			=> System.Net.WebUtility.UrlEncode(ProperName);

		public override string Path
			=> $"{anetOwner.unetDef.Name}/{Rsrcs.strOneChannel}/{strName}";

		public override string LocalizedName
			=> ProperName;

		public override string Icon
			=> "🪟";

		public override string? Topic
		{
			get => strTopic;

			set
			{
				if(strTopic != null)
				{
					string strOldTopic = strTopic;

					strTopic = value;

					FireTopicChanged(strOldTopic);

					// TODO: Send any changes to the server.
				}
			}
		}

		public override bool IsTopicLocked
			=> mapModesWithStdTypes.ContainsKey(Defs.ChanMode.StdModeTypes.TopicLock) && mapModesWithStdTypes[Defs
				.ChanMode.StdModeTypes.TopicLock].StateAsBool;

		public override bool IsSecret
			=> mapModesWithStdTypes.ContainsKey(Defs.ChanMode.StdModeTypes.Secret) && mapModesWithStdTypes[Defs.ChanMode
				.StdModeTypes.Secret].StateAsBool;

		public override bool IsPrivate
			=> mapModesWithStdTypes.ContainsKey(Defs.ChanMode.StdModeTypes.Private) && mapModesWithStdTypes[Defs.ChanMode
				.StdModeTypes.Private].StateAsBool;

		public override bool IsModerated
			=> mapModesWithStdTypes.ContainsKey(Defs.ChanMode.StdModeTypes.Moderated) && mapModesWithStdTypes[Defs
				.ChanMode.StdModeTypes.Moderated].StateAsBool;

		public override bool AreColorsStripped
			=> mapModesWithStdTypes.ContainsKey(Defs.ChanMode.StdModeTypes.ColorStrip) && mapModesWithStdTypes[Defs
				.ChanMode.StdModeTypes.ColorStrip].StateAsBool;

		public override bool AreOutsideMsgProhibited
			=> mapModesWithStdTypes.ContainsKey(Defs.ChanMode.StdModeTypes.NoOutsideMsg) && mapModesWithStdTypes[Defs
				.ChanMode.StdModeTypes.NoOutsideMsg].StateAsBool;

		public override bool IsInviteOnly => mapModesWithStdTypes.ContainsKey(Defs.ChanMode.StdModeTypes.InviteOnly) &&
			mapModesWithStdTypes[Defs.ChanMode.StdModeTypes.InviteOnly].StateAsBool;

		public override bool IsKeywordRequired
			=> mapModesWithStdTypes.ContainsKey(Defs.ChanMode.StdModeTypes.Keyword) && mapModesWithStdTypes[Defs.ChanMode
				.StdModeTypes.Keyword].StateAsBool;


		public override Platform.DataAndExt.Conversations.IViewOrConversation.Types Type
			=> Platform.DataAndExt.Conversations.IViewOrConversation.Types.channelOrRoom;


		public System.Collections.Generic.IReadOnlyDictionary<char, Defs.TwoWayMode> AllModesOnChan
			=> mapModesOnChan;

		public System.Collections.Generic.IReadOnlyDictionary<string, RemoteUser> AllChanMembersByNick
			=> mapChanMembersByNick;
	#endregion

	#region Methods
		public override string? ToString()
			=> strName;

		protected override void FirePropChanged(in string strPropName)
			=> PropertyChanged?.Invoke(this, new(strPropName));

		private void FireTopicChanged(string strOldTopic)
		{
			FirePropChanged(nameof(Topic));

			evtTopicChanged?.Invoke(this, strOldTopic, strTopic);
		}
	#endregion

	#region Event Handlers
		private void OnStateOfModeChanged(in Defs.Mode<Defs.BoolModeState, Defs.BoolModeStates> modeSender, in Defs.BoolModeState stateOld, in
			Defs.BoolModeState stateNew)
		{
			// TODO: Attempt to change the mode on the network
		}

		private void OnValOfModeParamChanged(in Defs.Mode<Defs.BoolModeState, Defs.BoolModeStates>.Param mpSender, in object? objOldVal, in
			object? objNewVal)
		{
			if(objNewVal != null)
			{
				// TODO: Attempt to change the mode on the network
			}
		}
	#endregion
}