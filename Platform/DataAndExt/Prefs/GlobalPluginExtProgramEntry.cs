namespace BestChat.Platform.DataAndExt.Prefs;

public class GlobalPluginsExtProgramEntry : Obj<GlobalPluginsExtProgramEntry>
{
	#region Constructors & Deconstructors
		public GlobalPluginsExtProgramEntry(in string strName, in System.IO.FileInfo fileProgram, in
			string strParamsToPass, in bool bEnabled)
		{
			this.strName = strName;
			this.fileProgram = fileProgram;
			this.strParamsToPass = strParamsToPass;
			this.bEnabled = bEnabled;
		}

		internal GlobalPluginsExtProgramEntry(in DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ProgramEntryDTO
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
					strName = value;

					FirePropChanged(nameof(Name));

					MakeDirty();
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
					fileProgram = value;

					FirePropChanged(nameof(Program));

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
		public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ProgramEntryDTO ToDTO()
			=> new(strName, fileProgram, strParamsToPass, bEnabled);
	#endregion

	#region Event Handlers
	#endregion
}