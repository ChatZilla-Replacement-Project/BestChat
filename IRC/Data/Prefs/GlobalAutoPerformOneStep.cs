namespace BestChat.IRC.Data.Prefs
{
public class GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt
	.Obj<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>>, IReadOnlyOneStep, Prefs<GlobalPrefsType,
		GlobalDtoType>.NetPrefs.IKeyChanged<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, string>
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
{
	#region Constructors & Deconstructors
	public GlobalAutoPerformOneStep(in string strWhatToDo, in System.Guid guid = default) :
		base(guid)
		=> this.strWhatToDo = strWhatToDo;

	public GlobalAutoPerformOneStep(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.AutoPerformDTO.OneStepDTO dto) :
		base(dto.GUID)
		=> strWhatToDo = dto.WhatToDo;
	#endregion

	#region Delegates
	#endregion

	#region Events
	public event DFieldChanged<string>? evtWhatToDoChanged;

	public event Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.IKeyChanged<GlobalAutoPerformOneStep<GlobalPrefsType, GlobalDtoType>, string>.DKeyChanged? evtKeyChanged;
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
}