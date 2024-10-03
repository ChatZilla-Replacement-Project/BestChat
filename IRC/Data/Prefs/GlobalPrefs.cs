using System.Linq;

namespace BestChat.IRC.Data.Prefs
{
public abstract class GlobalPrefs<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
{
	#region Constructors & Deconstructors
	public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
		base(mgrParent, "Global", PrefsRsrcs.strGlobalTitle, PrefsRsrcs.strGlobalDesc)
	{
		autoPerform = new(this);
		dcc = new(this);
		conn = new(this);
		aliases = new(this);
		altNicks = new(this);
		stalkWords = new(this);
	}

	public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, GlobalDtoType dto)
		: base(mgrParent, "Global", PrefsRsrcs.strGlobalTitle, PrefsRsrcs.strGlobalDesc)
	{
		autoPerform = new(this, dto.AutoPerform);
		dcc = new(this, dto.DCC);
		conn = new(this, dto.Conn);
		aliases = new(this, dto.Aliases);
		altNicks = new(this, dto.AltNicks);
		stalkWords = new(this, dto.StalkWords);
	}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	public class AliasesPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public AliasesPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string? strName = null, in
			string? strLocalizedName = null, in string? strLocalizedDesc = null) :
			base(mgrParent, strName ?? "Aliases", strLocalizedName ?? PrefsRsrcs.strGlobalAliasesTitle,
				strLocalizedDesc ?? PrefsRsrcs.strGlobalAliasesDesc)
			=> entries = new(
				this,
				"Entries",
				PrefsRsrcs.strGlobalAliasesTitle,
				PrefsRsrcs.strGlobalAliasesDesc,
				[],
				aliasCur
					=> aliasCur.Name,
				(aliasEntry,
						evth)
					=> aliasEntry.evtNameChanged += mapAliasHandlers[evth] = (in OneAlias aliasSender, in string strVal,
							in string _)
						=> evth(strVal, aliasEntry),
				(aliasEntry,
						evth)
					=>
				{
					aliasEntry.evtNameChanged -= mapAliasHandlers[evth];

					mapAliasHandlers.Remove(evth);
				}
			);

		public AliasesPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.IrcDTO<GlobalDtoType>
			.GlobalDTO.OneAliasDTO[]? dto, in string? strName = null, in string? strLocalizedName = null, in string?
			strLocalizedDesc = null) :
			base(mgrParent, strName ?? "Aliases", strLocalizedName ?? PrefsRsrcs.strGlobalAliasesTitle,
				strLocalizedDesc ?? PrefsRsrcs.strGlobalAliasesDesc)
			=> entries = new(
				this,
				"Entries",
				PrefsRsrcs.strGlobalAliasesTitle,
				PrefsRsrcs.strGlobalAliasesDesc,
				[],
				dto?.Select(daliasCur
					=> new OneAlias(daliasCur, this)) ?? [],
				aliasCur
					=> aliasCur.Name,
				(aliasEntry,
						evth)
					=> aliasEntry.evtNameChanged += mapAliasHandlers[evth] = (in OneAlias aliasSender, in string strVal,
							in string _)
						=> evth(strVal, aliasEntry),
				(aliasEntry,
						evth)
					=>
				{
					aliasEntry.evtNameChanged -= mapAliasHandlers[evth];

					mapAliasHandlers.Remove(evth);
				}
			);
		#endregion

		#region Delegates
		#endregion

		#region Events
		#endregion

		#region Constants
		#endregion

		#region Helper Types
		public interface IReadOnlyOneAlias
		{
			string Name
			{
				get;
			}

			string Cmd
			{
				get;
			}

			System.Guid GUID
			{
				get;
			}
		}

		public class OneAlias : Platform.DataAndExt.Obj<OneAlias>, IReadOnlyOneAlias, Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.IKeyChanged<OneAlias,
			string>
		{
			#region Constructors & Deconstructors
			public OneAlias(in string strName, in string strCmd, in AliasesPrefs parent)
			{
				this.strName = strName;
				this.strCmd = strCmd;

				this.parent = parent;
			}

			public OneAlias(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO dto, in AliasesPrefs parent) :
				base(dto.GUID)
			{
				strName = dto.Name;
				strCmd = dto.Cmd;

				this.parent = parent;
			}
			#endregion

			#region Delegates
			#endregion

			#region Events
			public event DFieldChanged<string>? evtNameChanged;

			public event DFieldChanged<string>? evtCmdChanged;

			public event Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.IKeyChanged<OneAlias, string>.DKeyChanged? evtKeyChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
			#endregion

			#region Members
			public readonly AliasesPrefs parent;

			private string strName;

			private string strCmd;
			#endregion

			#region Properties
			public string Name
			{
				get => strName;

				set
				{
					if(strName != value)
					{
						string strOldName = strName;

						strName = value;

						FireNameChanged(strOldName);

						MakeDirty();
					}
				}
			}

			public string Cmd
			{
				get => strCmd;

				set
				{
					if(strCmd != value)
					{
						string strOldCmd = strCmd;

						strCmd = value;

						FireCmdChanged(strOldCmd);

						MakeDirty();
					}
				}
			}
			#endregion

			#region Methods
			private void FireNameChanged(in string strOldName)
			{
				FirePropChanged(nameof(Name));

				evtNameChanged?.Invoke(this, strOldName, strName);
				evtKeyChanged?.Invoke(this, strOldName, strName);
			}

			private void FireCmdChanged(in string strOldCmd)
			{
				FirePropChanged(nameof(Cmd));

				evtCmdChanged?.Invoke(this, strOldCmd, strCmd);
			}

			public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO ToDTO()
				=> new(
					guid,
					strName,
					strCmd
				);
			#endregion

			#region Event Handlers
			#endregion
		}
		#endregion

		#region Members
		private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneAlias> entries;

		private readonly
			System.Collections.Generic.Dictionary<System.Action<string, OneAlias>, Platform.DataAndExt.Obj<OneAlias>
				.DFieldChanged<string>> mapAliasHandlers = [];
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneAlias> Entries
			=> entries;
		#endregion

		#region Methods
		public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO[]? ToDTO()
			=> entries.Values.Select(aliasCur
				=> aliasCur.ToDTO()).ToArray();
		#endregion

		#region Event Handlers
		#endregion
	}

	public class AutoPerformPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public AutoPerformPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
			: base(mgrParent, "Auto-perform", PrefsRsrcs.strGlobalAutoPerformTitle, PrefsRsrcs
				.strGlobalAutoPerformDesc)
		{
			whenStartingBestChat = new(this, "When Starting Best Chat", PrefsRsrcs
				.strGlobalAutoPerformWhenStartingBestChatTitle, PrefsRsrcs
				.strGlobalAutoPerformWhenStartingBestChatDesc);
			whenJoiningNet = new(this, "When Joining a Network", PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningNetTitle, PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningNetDesc);
			whenJoiningChan = new(this, "When Joining a Channel", PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningChanTitle, PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningChanDesc);
			whenOpeningUserChat = new(this, "When Opening a User Chat", PrefsRsrcs
				.strGlobalAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
				.strGlobalAutoPerformWhenOpeningUserChatDesc);
		}

		public AutoPerformPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO
			.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO dto) :
			base(mgrParent, "Auto-perform", PrefsRsrcs.strGlobalAutoPerformTitle, PrefsRsrcs.strGlobalAutoPerformDesc)
		{
			whenStartingBestChat = new(this, "When Starting Best Chat", PrefsRsrcs
				.strGlobalAutoPerformWhenStartingBestChatTitle, PrefsRsrcs
				.strGlobalAutoPerformWhenStartingBestChatDesc, dto.WhenStartingBestChat);
			whenJoiningNet = new(this, "When Joining a Network", PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningNetTitle, PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningNetTitle, dto.WhenJoiningNet);
			whenJoiningChan = new(this, "When Joining a Channel", PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningChanTitle, PrefsRsrcs
				.strGlobalAutoPerformWhenJoiningChanDesc, dto.WhenJoiningChan);
			whenOpeningUserChat = new(this, "When Opening a User Chat", PrefsRsrcs
				.strGlobalAutoPerformWhenOpeningUserChatTitle, PrefsRsrcs
				.strGlobalAutoPerformWhenOpeningUserChatDesc, dto.WhenOpeningUserChat);
		}
		#endregion

		#region Delegates
		#endregion

		#region Events
		#endregion

		#region Constants
		#endregion

		#region Helper Types
		public class OnEvtPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
		{
			#region Constructors & Deconstructors
			public OnEvtPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string strName, in string
				strLocalizedName, in string strLocalizedDesc) :
				base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
				=> steps = new(this, "Steps", strLocalizedName, strLocalizedDesc, []);

			public OnEvtPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string strName, in string
				strLocalizedName, in string strLocalizedDesc, in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO
				.OneStepDTO[]? dto) :
				base(mgrParent, strName, strLocalizedName, strLocalizedDesc)
				=> steps = new(
					this,
					"Steps",
					strLocalizedName,
					strLocalizedDesc,
					[],
					dto?.Select(dstep
						=> new OneStep(dstep)
					) ?? []);
			#endregion

			#region Delegates
			#endregion

			#region Events
			#endregion

			#region Constants
			#endregion

			#region Helper Types
			public interface IReadOnlyOneStep
			{
				System.Guid GUID
				{
					get;
				}

				string WhatToDo
				{
					get;
				}
			}

			public class OneStep : Platform.DataAndExt.Obj<OneStep>, IReadOnlyOneStep, Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs
				.IKeyChanged<OneStep, string>
			{
				#region Constructors & Deconstructors
				public OneStep(in string strWhatToDo, in System.Guid guid = default) :
					base(guid)
					=> this.strWhatToDo = strWhatToDo;

				public OneStep(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO dto) :
					base(dto.GUID)
					=> strWhatToDo = dto.WhatToDo;
				#endregion

				#region Delegates
				#endregion

				#region Events
				public event DFieldChanged<string>? evtWhatToDoChanged;

				public event Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.IKeyChanged<OneStep, string>.DKeyChanged? evtKeyChanged;
				#endregion

				#region Constants
				#endregion

				#region Helper Types
				#endregion

				#region Members
				private string strWhatToDo;
				#endregion

				#region Properties
				public string WhatToDo
				{
					get => strWhatToDo;

					set
					{
						if(strWhatToDo != value)
						{
							string strOldWhatToDo = strWhatToDo;

							strWhatToDo = value;

							MakeDirty();

							FireWhatToDoChanged(strOldWhatToDo);
						}
					}
				}
				#endregion

				#region Methods
				private void FireWhatToDoChanged(in string strOldWhatToDo)
				{
					FirePropChanged(nameof(strWhatToDo));

					evtWhatToDoChanged?.Invoke(this, strOldWhatToDo, strWhatToDo);
					evtKeyChanged?.Invoke(this, strOldWhatToDo, strWhatToDo);
				}

				public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO ToDTO()
					=> new(guid, strWhatToDo);
				#endregion

				#region Event Handlers
				#endregion
			}
			#endregion

			#region Members
			private readonly Platform.DataAndExt.Prefs.ReorderableListItem<OneStep> steps;
			#endregion

			#region Properties
			public Platform.DataAndExt.Prefs.ReorderableListItem<OneStep> Steps
				=> steps;
			#endregion

			#region Methods
			public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO[]? ToDTO()
				=> steps.Select(aliasCur
					=> aliasCur.ToDTO()).ToArray();
			#endregion

			#region Event Handlers
			#endregion
		}
		#endregion

		#region Members
		private readonly OnEvtPrefs whenStartingBestChat;

		private readonly OnEvtPrefs whenJoiningNet;

		private readonly OnEvtPrefs whenJoiningChan;

		private readonly OnEvtPrefs whenOpeningUserChat;
		#endregion

		#region Properties
		public OnEvtPrefs? WhenStartingBestChat
			=> whenStartingBestChat;

		public OnEvtPrefs WhenJoiningNet
			=> whenJoiningNet;

		public OnEvtPrefs WhenJoiningChan
			=> whenJoiningChan;

		public OnEvtPrefs WhenOpeningUserChat
			=> whenOpeningUserChat;
		#endregion

		#region Methods
		public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO ToDTO()
			=> new(
				whenStartingBestChat.ToDTO(),
				whenJoiningNet.ToDTO(),
				whenJoiningChan.ToDTO(),
				whenOpeningUserChat.ToDTO()
			);
		#endregion

		#region Event Handlers
		#endregion
	}

	public class AltNicksPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public AltNicksPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent, "Alternate Nicks", PrefsRsrcs.strGlobalAltNicksTitle, PrefsRsrcs
				.strGlobalAltNicksDesc)
			=> entries = new(this, "Entries", PrefsRsrcs
				.strGlobalAltNicksTitle, PrefsRsrcs.strGlobalAltNicksDesc, []);

		public AltNicksPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.IrcDTO<GlobalDtoType>
			.GlobalDTO.OneAltNickDTO[]? dto):
			base(mgrParent, "Alternate Nicks", PrefsRsrcs.strGlobalAltNicksTitle, PrefsRsrcs
				.strGlobalAltNicksDesc)
			=> entries = new(
				this,
				"Entries",
				PrefsRsrcs.strGlobalAltNicksTitle,
				PrefsRsrcs.strGlobalAltNicksDesc,
				[],
				dto?.Select(danickCur
					=> new OneAltNick(danickCur, this)
				) ?? []
			);
		#endregion

		#region Delegates
		#endregion

		#region Events
		#endregion

		#region Constants
		#endregion

		#region Helper Types
		public interface IReadOnlyOneAltNick
		{
			string NickToUse
			{
				get;
			}
		}

		public class OneAltNick : Platform.DataAndExt.Obj<OneAltNick>, IReadOnlyOneAltNick, Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.
			IKeyChanged<OneAltNick, string>
		{
			#region Constructors & Deconstructors
			public OneAltNick(in string strNickToUse, in AltNicksPrefs parent)
			{
				this.strNickToUse = strNickToUse;

				this.parent = parent;
			}

			public OneAltNick(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO dto, in
				AltNicksPrefs parent) :
				base(dto.GUID)
			{
				strNickToUse = dto.NickToUse;

				this.parent = parent;
			}
			#endregion

			#region Delegates
			#endregion

			#region Events
			public event DFieldChanged<string>? evtNickToUseChanged;

			public event Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.IKeyChanged<OneAltNick, string>.DKeyChanged? evtKeyChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
			#endregion

			#region Members
			public readonly AltNicksPrefs parent;

			private string strNickToUse;
			#endregion

			#region Properties
			public string NickToUse
			{
				get => strNickToUse;

				set
				{
					if(strNickToUse != value)
					{
						string strOldNickToUse = strNickToUse;

						strNickToUse = value;

						MakeDirty();

						FireCtntsChanged(strOldNickToUse);
					}
				}
			}
			#endregion

			#region Methods
			private void FireCtntsChanged(string strOldNickToUse)
			{
				FirePropChanged(nameof(NickToUse));

				evtNickToUseChanged?.Invoke(this, strOldNickToUse, strNickToUse);
				evtKeyChanged?.Invoke(this, strOldNickToUse, strNickToUse);
			}

			public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO ToDTO()
				=> new(guid, strNickToUse);
			#endregion

			#region Event Handlers
			#endregion
		}
		#endregion

		#region Members
		private readonly Platform.DataAndExt.Prefs.ReorderableListItem<OneAltNick> entries;
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.ReorderableListItem<OneAltNick> Entries
			=> entries;
		#endregion

		#region Methods
		public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO[]? ToDTO()
			=> entries.Select(aliasCur
				=> aliasCur.ToDTO()
			).ToArray();
		#endregion

		#region Event Handlers
		#endregion
	}

	public class StalkWordsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public StalkWordsPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent, "Stalk Words", PrefsRsrcs.strGlobalStalkWordsTitle, PrefsRsrcs
				.strGlobalStalkWordsDesc)
			=> entries = new(
				this,
				"Entries",
				PrefsRsrcs.strGlobalStalkWordsTitle,
				PrefsRsrcs.strGlobalStalkWordsDesc,
				[],
				val
					=> val.Ctnts,
				(swEntry,
						evth)
					=> swEntry.evtCtntsChanged += mapStalkWordHandlers[evth] = (in OneStalkWord swEntry, in string
							strOldCtnts, in string _)
						=> evth(strOldCtnts, swEntry),
				(swEntry,
						evth)
					=>
				{
					swEntry.evtCtntsChanged -= mapStalkWordHandlers[evth];

					mapStalkWordHandlers.Remove(evth);
				}
			);

		public StalkWordsPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.IrcDTO<GlobalDtoType>
			.GlobalDTO.OneStalkWordDTO[]? dto)
			: base(mgrParent, "Stalk Words", PrefsRsrcs.strGlobalStalkWordsTitle, PrefsRsrcs
				.strGlobalStalkWordsDesc)
			=> entries = new(
				this,
				"Entries",
				PrefsRsrcs.strGlobalStalkWordsTitle,
				PrefsRsrcs.strGlobalStalkWordsDesc,
				[],
				dto?.Select(dsto
					=> new OneStalkWord(dsto))
				?? [],
				val
					=> val.Ctnts,
				(swEntry,
						evth)
					=> swEntry.evtCtntsChanged += mapStalkWordHandlers[evth] = (in OneStalkWord swEntry, in string
							strOldCtnts, in string _)
						=> evth(strOldCtnts, swEntry),
				(swEntry,
						evth)
					=>
				{
					swEntry.evtCtntsChanged -= mapStalkWordHandlers[evth];

					mapStalkWordHandlers.Remove(evth);
				}
			);
		#endregion

		#region Delegates
		#endregion

		#region Events
		#endregion

		#region Constants
		#endregion

		#region Helper Types
		public interface IReadOnlyOneStalkWord
		{
			System.Guid GUID
			{
				get;
			}

			string Ctnts
			{
				get;
			}
		}

		public class OneStalkWord : Platform.DataAndExt.Obj<OneStalkWord>, IReadOnlyOneStalkWord, Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs
			.IKeyChanged<OneStalkWord, string>
		{
			#region Constructors & Deconstructors
			public OneStalkWord(in string strCtnts, System.Guid guid = default) :
				base(guid)
				=> this.strCtnts = strCtnts;

			public OneStalkWord(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO dto) :
				base(dto.GUID)
				=> strCtnts = dto.Ctnts;
			#endregion

			#region Delegates
			#endregion

			#region Events
			public event DFieldChanged<string>? evtCtntsChanged;

			public event Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.IKeyChanged<OneStalkWord, string>.DKeyChanged? evtKeyChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
			#endregion

			#region Members
			private string strCtnts;
			#endregion

			#region Properties
			public string Ctnts
			{
				get => strCtnts;

				set
				{
					if(strCtnts != value)
					{
						string strOldCtnts = strCtnts;

						strCtnts = value;

						MakeDirty();

						FireCtntsChanged(strOldCtnts);
					}
				}
			}
			#endregion

			#region Methods
			private void FireCtntsChanged(string strOldCtnts)
			{
				FirePropChanged(nameof(Ctnts));

				evtCtntsChanged?.Invoke(this, strOldCtnts, strCtnts);
				evtKeyChanged?.Invoke(this, strOldCtnts, strCtnts);
			}

			public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO ToDTO()
				=> new(
					guid,
					strCtnts
				);
			#endregion

			#region Event Handlers
			#endregion
		}
		#endregion

		#region Members
		private readonly Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneStalkWord> entries;

		private readonly System.Collections.Generic.Dictionary<System.Action<string, OneStalkWord>, Platform.DataAndExt.Obj<OneStalkWord>.DFieldChanged<string>> mapStalkWordHandlers = [];
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.MappedSortedListItem<string, OneStalkWord> Entries
			=> entries;
		#endregion

		#region Methods
		public DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneStalkWordDTO[]? ToDTO()
			=> entries.Values.Select(swCur =>
				swCur.ToDTO()
			).ToArray();
		#endregion

		#region Event Handlers
		#endregion
	}

	public class DccPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public DccPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
			: base(mgrParent, "DCC (Direct Client Chat)", PrefsRsrcs.strGlobalDccTitle, PrefsRsrcs
				.strGlobalDccTitle)
		{
			enabled = new(this, "Enabled", PrefsRsrcs.strGlobalDccEnabledTitle,
				PrefsRsrcs.strGlobalDccEnabledDesc, false);
			getIpFromServer = new(this, "Get IP From Server", PrefsRsrcs
					.strGlobalDccGetIpFromServerTitle, PrefsRsrcs.strGlobalDccGetIpFromServerDesc,
				false);
			downloadsFolder = new(this, "Downloads Folder", PrefsRsrcs
					.strGlobalDccDownloadsFolderTitle, PrefsRsrcs.strGlobalDccDownloadsFolderDesc,
				null);
			llistPorts = [];
		}

		public DccPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.IrcDTO<GlobalDtoType>
			.GlobalDTO.DccDTO dto) :
			base(mgrParent, "DCC (Direct Client Chat)", PrefsRsrcs.strGlobalDccTitle, PrefsRsrcs.strGlobalDccTitle)
		{
			enabled = new(this, "Enabled", PrefsRsrcs.strGlobalDccEnabledTitle,
				PrefsRsrcs.strGlobalDccEnabledDesc, dto.Enabled);
			getIpFromServer = new(this, "Get IP From Server", PrefsRsrcs
				.strGlobalDccGetIpFromServerTitle, PrefsRsrcs.strGlobalDccGetIpFromServerDesc, dto
				.GetLocalIpFromServer ?? false);
			downloadsFolder = new(this, "Downloads Folder", PrefsRsrcs
				.strGlobalDccDownloadsFolderTitle, PrefsRsrcs.strGlobalDccDownloadsFolderDesc, dto
				.DownloadsFolder);
			llistPorts = dto.Ports == null
				? []
				: new(dto.Ports);
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
		private readonly Platform.DataAndExt.Prefs.Item<bool> enabled;

		private readonly Platform.DataAndExt.Prefs.Item<bool> getIpFromServer;

		private readonly Platform.DataAndExt.Prefs.Item<System.IO.DirectoryInfo?> downloadsFolder;

		private readonly System.Collections.Generic.LinkedList<int> llistPorts;
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.Item<bool> Enabled
			=> enabled;

		public Platform.DataAndExt.Prefs.Item<bool> GetIpFromServer
			=> getIpFromServer;

		public Platform.DataAndExt.Prefs.Item<System.IO.DirectoryInfo?> DownloadsFolder
			=> downloadsFolder;

		public System.Collections.Generic.IReadOnlyCollection<int> Ports
			=> llistPorts;
		#endregion

		#region Methods
		public virtual DTO.IrcDTO<GlobalDtoType>.GlobalDTO.DccDTO ToDTO()
			=> new(
				enabled.CurVal,
				getIpFromServer.CurVal,
				downloadsFolder.CurVal,
				[.. llistPorts]
			);
		#endregion

		#region Event Handlers
		#endregion
	}

	public class ConnPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public ConnPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent,"Connection", PrefsRsrcs.strGlobalConnTitle, PrefsRsrcs.strGlobalConnDesc)
		{
			enableIndent = new(this, "Enable Ident", PrefsRsrcs
				.strGlobalConnEnableIdentTitle, PrefsRsrcs.strGlobalConnEnableIdentDesc, true);

			autoReconnect = new(this, "Auto Reconnect", PrefsRsrcs
					.strGlobalConnAutoReconnectTitle, PrefsRsrcs.strGlobalConnAutoReconnectDesc,
				true);

			rejoinAfterKick = new(this, "Rejoin After Kick",
				PrefsRsrcs.strGlobalConnRejoinAfterKickTitle, PrefsRsrcs
					.strGlobalConnRejoinAfterKickDesc, true);

			characterEncoding = new(this, "Character Encoding", PrefsRsrcs
					.strGlobalConnCharEncodingTitle, PrefsRsrcs.strGlobalConnCharEncodingDesc,
				"UTF-8");

			unlimitedAttempts = new(this, "Unlimited Reconnection Attempts",
				PrefsRsrcs.strGlobalConnUnlimitedAttemptsTitle, PrefsRsrcs
					.strGlobalConnUnlimitedAttemptsDesc, true);

			maxAttempts = new(this, "Maximum Attempts to Reconnect", PrefsRsrcs
					.strGlobalConnMaxAttemptsTitle, PrefsRsrcs.strGlobalConnMaxAttemptsDesc, 1,
				iMinVal: 1);

			defQuitMsg = new(this, "Default Quit message", PrefsRsrcs
				.strGlobalConnDefQuitMsgTitle, PrefsRsrcs.strGlobalConnDefQuitMsgDesc, PrefsRsrcs
				.strDefQuitMsg);
		}

		internal ConnPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.IrcDTO<GlobalDtoType>
			.GlobalDTO.ConnDTO dto) :
			base(mgrParent, "Connection", PrefsRsrcs.strGlobalConnTitle, PrefsRsrcs.strGlobalConnDesc)
		{
			enableIndent = new(this, "Enable Ident", PrefsRsrcs
				.strGlobalConnEnableIdentTitle, PrefsRsrcs.strGlobalConnEnableIdentDesc, true, dto
				.IsIdentEnabled);

			autoReconnect = new(this, "Auto Reconnect", PrefsRsrcs
					.strGlobalConnAutoReconnectTitle, PrefsRsrcs.strGlobalConnAutoReconnectDesc, true,
				dto.IsAutoReconnectEnabled);

			rejoinAfterKick = new(this, "Rejoin After Kick", PrefsRsrcs
					.strGlobalConnRejoinAfterKickTitle, PrefsRsrcs.strGlobalConnRejoinAfterKickDesc,
				true, dto.IsRejoinAfterKickEnabled);

			characterEncoding = new(this, "Character Encoding", PrefsRsrcs
					.strGlobalConnCharEncodingTitle, PrefsRsrcs.strGlobalConnCharEncodingDesc,
				"UTF-8", dto.CharEncoding);

			unlimitedAttempts = new(this, "Unlimited Reconnection Attempts",
				PrefsRsrcs.strGlobalConnUnlimitedAttemptsTitle, PrefsRsrcs
					.strGlobalConnUnlimitedAttemptsDesc, true, dto.IsUnlimitedAttemptsOn);

			maxAttempts = new(this, "Maximum Attempts to Reconnect", PrefsRsrcs
					.strGlobalConnMaxAttemptsTitle, PrefsRsrcs.strGlobalConnMaxAttemptsDesc, 1,
				iMinVal: 1, iCurVal: dto.MaxAttempts);

			defQuitMsg = new(this, "Default Quit message", PrefsRsrcs
				.strGlobalConnDefQuitMsgTitle, PrefsRsrcs.strGlobalConnDefQuitMsgDesc, PrefsRsrcs
				.strDefQuitMsg, dto.DefQuitMsg ?? PrefsRsrcs.strDefQuitMsg);
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
		private readonly Platform.DataAndExt.Prefs.Item<bool> enableIndent;
		private readonly Platform.DataAndExt.Prefs.Item<bool> autoReconnect;
		private readonly Platform.DataAndExt.Prefs.Item<bool> rejoinAfterKick;
		private readonly Platform.DataAndExt.Prefs.Item<string> characterEncoding;
		private readonly Platform.DataAndExt.Prefs.Item<bool> unlimitedAttempts;
		private readonly Platform.DataAndExt.Prefs.IntItem maxAttempts;
		private readonly Platform.DataAndExt.Prefs.Item<string> defQuitMsg;
		// TODO: Add proxy once we know what we're doing with that.
		#endregion

		#region Properties
		public Platform.DataAndExt.Prefs.Item<bool> EnableIdent
			=> enableIndent;

		public Platform.DataAndExt.Prefs.Item<bool> AutoReconnect
			=> autoReconnect;

		public Platform.DataAndExt.Prefs.Item<bool> RejoinAfterKick
			=> rejoinAfterKick;

		public Platform.DataAndExt.Prefs.Item<string> CharEncoding
			=> characterEncoding;

		public Platform.DataAndExt.Prefs.Item<bool> UnlimitedAttempts
			=> unlimitedAttempts;

		public Platform.DataAndExt.Prefs.IntItem MaxAttempts
			=> maxAttempts;

		public Platform.DataAndExt.Prefs.Item<string> DefQuitMsg
			=> defQuitMsg;
		#endregion

		#region Methods
		public virtual DTO.IrcDTO<GlobalDtoType>.GlobalDTO.ConnDTO ToDTO()
			=> new(
				enableIndent.CurVal,
				autoReconnect.CurVal,
				rejoinAfterKick.CurVal,
				characterEncoding.CurVal,
				unlimitedAttempts.CurVal,
				maxAttempts.CurVal,
				defQuitMsg.CurVal);
		#endregion

		#region Event Handlers
		#endregion
	}
	#endregion

	#region Members
	private readonly AutoPerformPrefs autoPerform;

	private readonly DccPrefs dcc;

	private readonly ConnPrefs conn;

	private readonly AliasesPrefs aliases;

	private readonly AltNicksPrefs altNicks;

	private readonly StalkWordsPrefs stalkWords;
	#endregion

	#region Properties
	public AutoPerformPrefs AutoPerform
		=> autoPerform;

	public DccPrefs DCC
		=> dcc;

	public ConnPrefs Conn
		=> conn;

	public AliasesPrefs Aliases
		=> aliases;

	public AltNicksPrefs AltNicks
		=> altNicks;

	public StalkWordsPrefs StalkWords
		=> stalkWords;
	#endregion

	#region Methods
	public abstract GlobalDtoType ToDTO();
	#endregion

	#region Event Handlers
	#endregion
}
}