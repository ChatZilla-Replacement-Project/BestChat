using System.Linq;

namespace BestChat.IRC.Data.Prefs
{
public class GlobalAutoPerformPrefs<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
{
	#region Constructors & Deconstructors
	public GlobalAutoPerformPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
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

	public GlobalAutoPerformPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO
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
				=> aliasCur.ToDTO()
			).ToArray();
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
}