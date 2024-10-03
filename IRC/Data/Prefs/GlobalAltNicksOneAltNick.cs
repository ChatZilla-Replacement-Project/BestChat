namespace BestChat.IRC.Data.Prefs
{
public class GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt
	.Obj<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>>, IReadOnlyOneAltNick, Prefs<GlobalPrefsType,
		GlobalDtoType>.NetPrefs.IKeyChanged<GlobalAltNicksOneAltNick<GlobalPrefsType, GlobalDtoType>, string>
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
{
	#region Constructors & Deconstructors
	public GlobalAltNicksOneAltNick(in string strNickToUse, in GlobalAltNicksPrefs<GlobalPrefsType, GlobalDtoType> parent)
	{
		this.strNickToUse = strNickToUse;

		this.parent = parent;
	}

	public GlobalAltNicksOneAltNick(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAltNickDTO dto, in
		GlobalAltNicksPrefs<GlobalPrefsType, GlobalDtoType> parent) :
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

	public event Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.IKeyChanged<GlobalAltNicksOneAltNick, string>.DKeyChanged? evtKeyChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
	public readonly GlobalAltNicksPrefs<GlobalPrefsType, GlobalDtoType> parent;

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
}