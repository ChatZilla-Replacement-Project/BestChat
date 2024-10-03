namespace BestChat.Platform.DataAndExt.Prefs;

public class GlobalPluginExtProgramEntry : Obj<GlobalPluginExtProgramEntry>
{
	#region Constructors & Deconstructors
		public GlobalPluginExtProgramEntry(in string strName, in System.IO.FileInfo fileProgram, in
			string strParamsToPass, in bool bEnabled)
		{
			this.strName = strName;
			this.fileProgram = fileProgram;
			this.strParamsToPass = strParamsToPass;
			this.bEnabled = bEnabled;
		}

		internal GlobalPluginExtProgramEntry(in DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ProgramEntryDTO
			dto)
		{
			strName = dto.Name;
			fileProgram = dto.Program;
			strParamsToPass = dto.ParamsToPass;
			bEnabled = dto.Enabled;
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public event DFieldChanged<string>? evtNameChanged;

		public event DFieldChanged<System.IO.FileInfo>? evtProgramChanged;

		public event DFieldChanged<string>? evtParamsToPass;

		public event DBoolFieldChanged? evtEnabledChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	#endregion

	#region Members
		private string strName;

		private System.IO.FileInfo fileProgram;

		private string strParamsToPass;

		private bool bEnabled;
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

					MakeDirty();

					FireNameChanged(strOldName);
				}
			}
		}

		public System.IO.FileInfo Program
		{
			get => fileProgram;

			set
			{
				if(fileProgram != value)
				{
					System.IO.FileInfo fileOldProgram = fileProgram;

					fileProgram = value;

					MakeDirty();

					FireProgramChanged(fileOldProgram);
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
		private void FireNameChanged(in string strOldName)
		{
			FirePropChanged(nameof(Name));

			evtNameChanged?.Invoke(this, strOldName, strName);
		}

		private void FireProgramChanged(in System.IO.FileInfo fileOldProgram)
		{
			FirePropChanged(nameof(Program));

			evtProgramChanged?.Invoke(this, fileOldProgram, fileProgram);
		}

		private void FireParamsToPassChanged(in string strOldParamsToPass)
		{
			FirePropChanged(nameof(ParamsToPass));

			evtParamsToPass?.Invoke(this, strOldParamsToPass, strParamsToPass);
		}

		private void FireEnabledChanged()
		{
			FirePropChanged(nameof(Enabled));

			evtEnabledChanged?.Invoke(this, bEnabled);
		}

		public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ProgramEntryDTO ToDTO()
			=> new(strName, fileProgram, strParamsToPass, bEnabled);
	#endregion

	#region Event Handlers
	#endregion
}