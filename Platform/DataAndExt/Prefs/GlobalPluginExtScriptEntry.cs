namespace BestChat.Platform.DataAndExt.Prefs;

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
		public event DFieldChanged<string>? evtFileNameExtOrMaskChanged;

		public event DFieldChanged<System.IO.FileInfo?>? evtProgramNeeededChanged;

		public event DFieldChanged<string>? evtParamsToPassChanged;

		public event DBoolFieldChanged? evtEnabledChanged;
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
					string strOldFileNameOrMask = strFileNameExtOrMask;

					strFileNameExtOrMask = value;

					MakeDirty();

					FireFileNameOrMaskChanged(strOldFileNameOrMask);
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
					System.IO.FileInfo? fileOldProgramNeeded = fileProgramNeeded;

					fileProgramNeeded = value;

					MakeDirty();

					FireProgramNeededChanged(fileOldProgramNeeded);
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
					string strOldParamsToPass = strParamsToPass;

					strParamsToPass = value;

					MakeDirty();

					FireParamsToPassChanged(strOldParamsToPass);
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

					MakeDirty();

					FireEnabledChanged();
				}
			}
		}
	#endregion

	#region Methods
		private void FireFileNameOrMaskChanged(in string strOldFileNameOrMask)
		{
			FirePropChanged(nameof(FileNameExtOrMask));

			evtFileNameExtOrMaskChanged?.Invoke(this, strOldFileNameOrMask, strFileNameExtOrMask);
		}

		private void FireProgramNeededChanged(in System.IO.FileInfo? fileOldProgramNeeded)
		{
			FirePropChanged(nameof(ProgramNeeded));

			evtProgramNeeededChanged?.Invoke(this, fileOldProgramNeeded, fileProgramNeeded);
		}

		private void FireParamsToPassChanged(in string strOldParamsToPass)
		{
			FirePropChanged(nameof(ParamsToPass));

			evtParamsToPassChanged?.Invoke(this, strOldParamsToPass, strOldParamsToPass);
		}

		private void FireEnabledChanged()
		{
			FirePropChanged(nameof(Enabled));

			evtEnabledChanged?.Invoke(this, bEnabled);
		}

		public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ScriptEntryDTO ToDTO()
			=> new(
				strFileNameExtOrMask,
				fileProgramNeeded,
				strParamsToPass,
				bEnabled
			);
	#endregion

	#region Event Handlers
	#endregion
}