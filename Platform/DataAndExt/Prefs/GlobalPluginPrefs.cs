using System.Linq;

namespace BestChat.Platform.DataAndExt.Prefs
{
public class GlobalPluginPrefs : AbstractChildMgr
{
	#region Constructors & Deconstructors
	public GlobalPluginPrefs(AbstractMgr mgrParent) : base(mgrParent, "Plugins", Rsrcs
		.strGlobalPluginsTitle, Rsrcs.strGlobalPluginsDesc)
	{
		ext = new(this);
	}

	internal GlobalPluginPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.PluginsDTO dto) :
		base(mgrParent, "Plugins", Rsrcs.strGlobalPluginsTitle, Rsrcs
			.strGlobalPluginsDesc)
	{
		ext = new(this, dto.Ext);
	}
	#endregion

	#region Delegates
	#endregion

	#region Events
	#endregion

	#region Constants
	#endregion

	#region Helper Types
	public class ExtPrefs : AbstractChildMgr
	{
		#region Constructors & Deconstructors
		public ExtPrefs(AbstractMgr mgrParent, System.Collections.Generic
			.IEnumerable<ScriptEntry>? scripts = null, System.Collections.Generic
			.IEnumerable<ProgramEntry>? programs = null) :
			base(mgrParent, "External", Rsrcs.strGlobalPluginsExtTitle,
				Rsrcs.strGlobalPluginsDesc)
		{
			whereToLook = new(this);
			howToRunScripts = new(this);
			if(scripts != null)
			{
				this.scripts.AddRange(scripts);
				foreach(ScriptEntry curScript in scripts)
					curScript.evtDirtyChanged += OnScriptDirtyChanged;
			}

			if(programs != null)
			{
				this.programs.AddRange(programs);
				foreach(ProgramEntry curProgram in programs)
					curProgram.evtDirtyChanged += OnProgramDirtyChanged;
			}
		}

		internal ExtPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO dto)
			: base(mgrParent, "External", Rsrcs.strGlobalPluginsExtTitle, Rsrcs
				.strGlobalPluginsDesc)
		{
			whereToLook = new(this, dto.WhereToLook);
			howToRunScripts = new(this);

			if(dto.Scripts is System.Collections.Generic.IEnumerable<DTO.PrefsDTO.GlobalDTO
				.PluginsDTO.ExtDTO.ScriptEntryDTO> scripts)
			{
				this.scripts.AddRange(scripts.Select(curScript => new
					ScriptEntry(curScript)));
				foreach(ScriptEntry curScript in this.scripts)
					curScript.evtDirtyChanged += OnScriptDirtyChanged;
			}

			if(dto.Programs is System.Collections.Generic.IEnumerable<DTO.PrefsDTO.GlobalDTO
				.PluginsDTO.ExtDTO.ProgramEntryDTO> programs)
			{
				this.programs.AddRange(dto.Programs.Select(curProgram => new
					ProgramEntry(curProgram)));
				foreach(ProgramEntry curProgram in this.programs)
					curProgram.evtDirtyChanged += OnProgramDirtyChanged;
			}
		}
		#endregion

		#region Delegates
		#endregion

		#region Events
		#endregion

		#region Constants
		#endregion

		#region Helper Types
		public class WhereToLookPrefs : AbstractChildMgr
		{
			#region Constructors & Deconstructors
			public WhereToLookPrefs(AbstractMgr mgrParent) :
				base(mgrParent, "Where To Look", Rsrcs
					.strGlobalPluginsExtWhereToLookTitle, Rsrcs
					.strGlobalPluginsExtWhereToLookDesc)
			{
				paths = new(this, "Paths", Rsrcs
					.strGlobalPluginsExtWhereToLookPathsTitle, Rsrcs
					.strGlobalPluginsExtWhereToLookPathsDesc, []);

				includeSysPath = new(this, "Include Your System Path " +
					"Environment Variable in the Search", Rsrcs
						.strGlobalPluginsExtWhereToLookIncludeSysPathTitle, Rsrcs
						.strGlobalPluginsExtWhereToLookIncludeSysPathDesc, true);
			}

			internal WhereToLookPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.PluginsDTO
				.ExtDTO.WhereToLookDTO dto) :
				base(mgrParent, "Where To Look", Rsrcs
					.strGlobalPluginsExtWhereToLookTitle,Rsrcs.strGlobalPluginsExtWhereToLookDesc)
			{
				paths = new(this, "Paths", Rsrcs
						.strGlobalPluginsExtWhereToLookPathsTitle, Rsrcs
						.strGlobalPluginsExtWhereToLookPathsDesc, [], dto.Paths ??
					[]);


				includeSysPath = new(this, "Include Your System Path " +
					"Environment Variable in the Search", Rsrcs
						.strGlobalPluginsExtWhereToLookIncludeSysPathTitle, Rsrcs
						.strGlobalPluginsExtWhereToLookIncludeSysPathDesc, dto.IncludeSysPaths);
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
			private readonly ReorderableListItem<System.IO.DirectoryInfo> paths;

			private readonly Item<bool> includeSysPath;
			#endregion

			#region Properties
			public ReorderableListItem<System.IO.DirectoryInfo> Paths
				=> paths;

			public Item<bool> IncludeSysPath
				=> includeSysPath;
			#endregion

			#region Methods
			public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.WhereToLookDTO ToDTO()
				=> new([.. paths], includeSysPath.CurVal);
			#endregion

			#region Event Handlers
			#endregion
		}

		public class HowToRunScriptsPrefs : AbstractChildMgr
		{
			#region Constructors & Deconstructors
			public HowToRunScriptsPrefs(AbstractMgr mgrParent) :
				base(mgrParent, "How to run them", Rsrcs
					.strGlobalPluginsHowToRunThemTitle, Rsrcs.strGlobalPluginsHowToRunThemDesc)
			{
				groupedByWhatRunsThem = new(this);

				ungrouped = new(this);
			}
			#endregion

			#region Delegates
			#endregion

			#region Events
			#endregion

			#region Constants
			#endregion

			#region Helper Types
			public class GroupedByWhatRunsThemPrefs : AbstractChildMgr
			{
				#region Constructors & Deconstructors
				public GroupedByWhatRunsThemPrefs(AbstractMgr mgrParent) : base(mgrParent, "Grouped by what runs them", Rsrcs
					.strGlobalPluginsHowToRunThemGroupedByWhatRunsThemTitle, Rsrcs
					.strGlobalPluginsHowToRunThemGroupedByWhatRunsThemDesc)
				{
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
				#endregion

				#region Properties
				#endregion

				#region Methods
				#endregion

				#region Event Handlers
				#endregion
			}

			public class UngroupedPrefs : AbstractChildMgr
			{
				#region Constructors & Deconstructors
				public UngroupedPrefs(AbstractMgr mgrParent)
					: base(mgrParent, "Ungrouped", Rsrcs.strGlobalPluginsHowToRunThemTitle, Rsrcs
						.strGlobalPluginsHowToRunThemDesc)
				{
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
				#endregion

				#region Properties
				#endregion

				#region Methods
				#endregion

				#region Event Handlers
				#endregion
			}
			#endregion

			#region Members
			private readonly GroupedByWhatRunsThemPrefs groupedByWhatRunsThem;

			private readonly UngroupedPrefs ungrouped;
			#endregion

			#region Properties
			public GroupedByWhatRunsThemPrefs GroupedByWhatRunsThem
				=> groupedByWhatRunsThem;

			public UngroupedPrefs Ungrouped
				=> ungrouped;
			#endregion

			#region Methods
			#endregion

			#region Event Handlers
			#endregion
		}

		public class ScriptEntry : Obj<ScriptEntry>
		{
			#region Constructors & Deconstructors
			public ScriptEntry(in string strFileNameExtOrMask, in System.IO.FileInfo?
				fileProgramNeeded, in string strParamsToPass, in bool bEnabled)
			{
				this.strFileNameExtOrMask = strFileNameExtOrMask;
				this.fileProgramNeeded = fileProgramNeeded;
				this.strParamsToPass = strParamsToPass;
				this.bEnabled = bEnabled;
			}

			internal ScriptEntry(in DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ScriptEntryDTO
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

		public class ProgramEntry : Obj<ProgramEntry>
		{
			#region Constructors & Deconstructors
			public ProgramEntry(in string strName, in System.IO.FileInfo fileProgram, in
				string strParamsToPass, in bool bEnabled)
			{
				this.strName = strName;
				this.fileProgram = fileProgram;
				this.strParamsToPass = strParamsToPass;
				this.bEnabled = bEnabled;
			}

			internal ProgramEntry(in DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ProgramEntryDTO
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
		#endregion

		#region Members
		private readonly WhereToLookPrefs whereToLook;

		private readonly HowToRunScriptsPrefs howToRunScripts;

		private readonly System.Collections.Generic.List<ScriptEntry> scripts =
			[];

		private readonly System.Collections.Generic.List<ProgramEntry> programs =
			[];
		#endregion

		#region Properties
		public WhereToLookPrefs WhereToLook
			=> whereToLook;

		public HowToRunScriptsPrefs HowToRunThem
			=> howToRunScripts;
		#endregion

		#region Methods
		public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO ToDTO()
			=> new(
				whereToLook.ToDTO(),
				scripts.Select(scriptCur
					=> scriptCur.ToDTO()).ToArray(),
				programs.Select(progCur
					=> progCur.ToDTO()).ToArray()
			);
		#endregion

		#region Event Handlers
		private void OnScriptDirtyChanged(in ScriptEntry scriptSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}

		private void OnProgramDirtyChanged(in ProgramEntry progSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}
		#endregion
	}
	#endregion

	#region Members
	private readonly ExtPrefs ext;
	#endregion

	#region Properties
	public ExtPrefs Ext
		=> ext;
	#endregion

	#region Methods
	public DTO.PrefsDTO.GlobalDTO.PluginsDTO ToDTO()
		=> new(ext.ToDTO());
	#endregion

	#region Event Handlers
	#endregion
}
}