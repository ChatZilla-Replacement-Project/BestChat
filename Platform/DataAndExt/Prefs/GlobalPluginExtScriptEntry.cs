namespace BestChat.Platform.DataAndExt.Prefs
{
public class GlobalPluginExtScriptEntry : Obj<GlobalPluginExtScriptEntry>
{
	#region Constructors & Deconstructors
	public GlobalPluginExtScriptEntry(in string strFileNameExtOrMask, in System.IO.FileInfo?
		fileProgramNeeded, in string strParamsToPass, in bool bEnabled)
	{
		this.strFileNameExtOrMask = strFileNameExtOrMask;
		this.fileProgramNeeded = fileProgramNeeded;
		this.strParamsToPass = strParamsToPass;
		this.bEnabled = bEnabled;
	}

	internal GlobalPluginExtScriptEntry(in DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ScriptEntryDTO
		dto)
	{
		strFileNameExtOrMask = dto.FileNameExtOrMask;
		fileProgramNeeded = dto.ProgramNeeded ?? null;
		strParamsToPass = dto.ParamsToPass;
		bEnabled = dto.Enabled;
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
	private string strFileNameExtOrMask;

	private System.IO.FileInfo? fileProgramNeeded;

	private string strParamsToPass;

	private bool bEnabled;
	#endregion

	#region Properties
	public string FileNameExtOrMask
	{
		get => strFileNameExtOrMask;

		set
		{
			if(strFileNameExtOrMask != value)
			{
				strFileNameExtOrMask = value;

				FirePropChanged(nameof(FileNameExtOrMask));

				MakeDirty();
			}
		}
	}

	public System.IO.FileInfo? ProgramNeeded
	{
		get => fileProgramNeeded;

		set
		{
			if(fileProgramNeeded != value)
			{
				fileProgramNeeded = value;

				FirePropChanged(nameof(ProgramNeeded));

				MakeDirty();
			}
		}
	}

	public string ParamsToPass
	{
		get => strParamsToPass;

		set
		{
			if(strParamsToPass != value)
			{
				strParamsToPass = value;

				FirePropChanged(nameof(ParamsToPass));

				MakeDirty();
			}
		}
	}

	public bool Enabled
	{
		get => bEnabled;

		set
		{
			if(bEnabled != value)
			{
				bEnabled = value;

				FirePropChanged(nameof(Enabled));

				MakeDirty();
			}
		}
	}
	#endregion

	#region Methods
	public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ScriptEntryDTO ToDTO()
		=> new(strFileNameExtOrMask, fileProgramNeeded, strParamsToPass,
			bEnabled);
	#endregion

	#region Event Handlers
	#endregion
}
}