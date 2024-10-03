namespace BestChat.IRC.Data.Prefs
{
public class GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt
	.Obj<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>>, GlobalAliasesPrefs<GlobalPrefsType, GlobalDtoType>
	.IReadOnlyOneAlias, Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs
	.IKeyChanged<GlobalAliasesOneAlias<GlobalPrefsType, GlobalDtoType>, string>
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
{
	#region Constructors & Deconstructors
	public GlobalAliasesOneAlias(in string strName, in string strCmd, in GlobalAliasesPrefs<GlobalPrefsType, GlobalDtoType> parent)
	{
		this.strName = strName;
		this.strCmd = strCmd;

		this.parent = parent;
	}

	public GlobalAliasesOneAlias(in DTO.IrcDTO<GlobalDtoType>.GlobalDTO.OneAliasDTO dto, in GlobalAliasesPrefs<GlobalPrefsType,
		GlobalDtoType> parent) :
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

	public event Prefs<GlobalPrefsType, GlobalDtoType>.NetPrefs.IKeyChanged<GlobalAliasesOneAlias<GlobalPrefsType,
		GlobalDtoType>, string>.DKeyChanged? evtKeyChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
	public readonly GlobalAliasesPrefs<GlobalPrefsType, GlobalDtoType> parent;

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
}