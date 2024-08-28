// Ignore Spelling: prefs Conf dto Ungrouped Msgs Loc

using System.Linq;

namespace BestChat.Platform.DataAndExt.Prefs;

public abstract class Prefs<GlobalPrefsType, AppearancePrefsType> : AbstractMgr
	where GlobalPrefsType : Prefs<GlobalPrefsType, AppearancePrefsType>.GlobalPrefs
{
	#region Constructors & Deconstructors
		protected Prefs()
		{
		}
	#endregion

	#region Delegates
	#endregion

	#region Events
		public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
	#endregion

	#region Constants
	#endregion

	#region Helper Types
		public abstract class GlobalPrefs : AbstractChildMgr
		{
			#region Constructors & Deconstructors
				protected GlobalPrefs(AbstractMgr mgrParent) :
					base(mgrParent, "Global", Rsrcs.strGlobalName, Rsrcs.strGlobalNameToolTipText)
				{
					plugin = new(this);
				}

				protected GlobalPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO dto) :
					base(mgrParent, "Global", Rsrcs.strGlobalName, Rsrcs.strGlobalNameToolTipText)
				{
					plugin = new(this, dto.Plugins);
				}
			#endregion

			#region Delegates
			#endregion

			#region Events
				public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
			#endregion

			#region Constants
			#endregion

			#region Helper Types
				public abstract class AppearancePrefs : AbstractChildMgr
				{
					#region Constructors & Deconstructors
						protected AppearancePrefs(AbstractMgr mgrParent) : base(mgrParent, "Appearance", Rsrcs.strGlobalAppearancePageTitle, Rsrcs
							.strGlobalAppearancePageDesc)
						{
							confMode = new(this);
							timestamp = new(this);
							userlist = new(this);
						}

						protected AppearancePrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.AppearanceDTO dto) :
							base(mgrParent, "Appearance", Rsrcs.strGlobalAppearancePageTitle, Rsrcs.strGlobalAppearancePageDesc)
						{
							confMode = new(this, dto.ConfMode);
							timestamp = new(this, dto.TimeStamp);
							userlist = new(this, dto.UserList);
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
						public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public class ConfModePrefs : AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public ConfModePrefs(in AbstractMgr mgrParent) : base(mgrParent, "Conference Mode", Rsrcs
									.strGlobalAppearanceConfModeTitle, Rsrcs.strGlobalAppearanceConfModeDesc)
								{
									confModeEnabled = new(this, "Conference Mode Enabled?", Rsrcs.strGlobalAppearanceConfModeEnabledTitle,
										Rsrcs.strGlobalAppearanceConfModeEnabledDesc, false);
									userLimitBeforeTrigger = new(this, "User Limit Before Trigger", Rsrcs
										.strGlobalAppearanceConfModeLimitTitle, Rsrcs.strGlobalAppearanceConfModeLimitDesc, 150, iMinVal: 2);
									actionsCollapsed = new(this, "Collapse Actions When Collapsing Messages", Rsrcs
										.strGlobalAppearanceConfModeCollapseActionsTitle, Rsrcs.strGlobalAppearanceConfModeCollapseActionsDesc, false);
									msgsCollapsed = new(this, "Collapse Messages?", Rsrcs.strGlobalAppearanceConfModeCollapseMsgsTitle,
										Rsrcs.strGlobalAppearanceConfModeCollapseMsgsDesc, false);
								}

								internal ConfModePrefs(in AbstractMgr mgrParent, in DTO.PrefsDTO.GlobalDTO.AppearanceDTO.ConfModeDTO dto) :
									base(mgrParent, "Conference Mode", Rsrcs.strGlobalAppearanceConfModeTitle, Rsrcs.strGlobalAppearanceConfModeDesc)
								{
									confModeEnabled = new(this, "Conference Mode Enabled?", Rsrcs.strGlobalAppearanceConfModeEnabledTitle,
										Rsrcs.strGlobalAppearanceConfModeEnabledDesc, false, dto.ConfModeEnabled);
									userLimitBeforeTrigger = new(this, "User Limit Before Trigger", Rsrcs
										.strGlobalAppearanceConfModeLimitTitle,
										Rsrcs.strGlobalAppearanceConfModeLimitDesc, 150, iMinVal: 2, iCurVal: dto.UserLimitBeforeTrigger);
									actionsCollapsed = new(this, "Collapse Actions When Collapsing Messages", Rsrcs
										.strGlobalAppearanceConfModeCollapseActionsTitle, Rsrcs.strGlobalAppearanceConfModeCollapseActionsDesc, false, dto
										.ActionsCollapsed);
									msgsCollapsed = new(this, "Collapse Messages?", Rsrcs.strGlobalAppearanceConfModeCollapseMsgsTitle,
										Rsrcs.strGlobalAppearanceConfModeCollapseMsgsDesc, false, dto.MsgsCollapsed);
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								private readonly Item<bool> confModeEnabled;

								private readonly IntItem userLimitBeforeTrigger;

								private readonly Item<bool> actionsCollapsed;

								private readonly Item<bool> msgsCollapsed;
							#endregion

							#region Properties
								public Item<bool> ConfModeEnabled
									=> confModeEnabled;

								public IntItem UserLimitBeforeTrigger
									=> userLimitBeforeTrigger;

								public Item<bool> ActionsCollapsed
									=> actionsCollapsed;

								public Item<bool> MsgsCollapsed
									=> msgsCollapsed;
							#endregion

							#region Methods
								public DTO.PrefsDTO.GlobalDTO.AppearanceDTO.ConfModeDTO ToDTO()
									=> new(confModeEnabled.CurVal, userLimitBeforeTrigger.CurVal, actionsCollapsed.CurVal, msgsCollapsed.CurVal);
							#endregion

							#region Event Handlers
							#endregion
						}

						public class TimeStampPrefs : AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public TimeStampPrefs(in AbstractMgr mgrParent) :
									base(mgrParent, "Time Stamp", Rsrcs.strGlobalAppearanceTimeStampTitle, Rsrcs.strGlobalAppearanceTimeStampDesc)
								{
									show = new(this, "Show the time stamp", Rsrcs.strGlobalAppearanceTimeStampShowTitle, Rsrcs
										.strGlobalAppearanceTimeStampShowDesc, true);
									fmt = new(this, "Format", Rsrcs.strGlobalAppearanceTimeStampFmtTitle, Rsrcs
										.strGlobalAppearanceTimeStampFmtDesc, "G");
								}

								internal TimeStampPrefs(in AbstractMgr mgrParent, in DTO.PrefsDTO.GlobalDTO.AppearanceDTO.TimeStampDTO dto) :
									base(mgrParent, "Time Stamp", Rsrcs.strGlobalAppearanceTimeStampTitle, Rsrcs.strGlobalAppearanceTimeStampDesc)
								{
									show = new(this, "Show the time stamp", Rsrcs.strGlobalAppearanceTimeStampShowTitle, Rsrcs
										.strGlobalAppearanceTimeStampShowDesc, true, dto.Show);
									fmt = new(this, "Format", Rsrcs.strGlobalAppearanceTimeStampFmtTitle, Rsrcs
										.strGlobalAppearanceTimeStampFmtDesc, "G", dto.Fmt);
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								private readonly Item<bool> show;

								private readonly Item<string> fmt;
							#endregion

							#region Properties
								public Item<bool> Show
									=> show;

								public Item<string> Fmt
									=> fmt;
							#endregion

							#region Methods
								public DTO.PrefsDTO.GlobalDTO.AppearanceDTO.TimeStampDTO ToDTO()
									=> new(show.CurVal, Fmt.CurVal);
							#endregion

							#region Event Handlers
								private void OnItemDirtyChanged(ItemBase objSender, bool bIsNowDirty) => MakeDirty();
							#endregion
						}

						public class UserListPrefs : AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public UserListPrefs(AbstractMgr mgrParent) :
									base(mgrParent, "Time Stamp", Rsrcs.strGlobalAppearanceUserListLocTitle, Rsrcs.strGlobalAppearanceUserListLocDesc)
								{
									location = new(this, "Location", Rsrcs.strGlobalAppearanceUserListLocTitle, Rsrcs
										.strGlobalAppearanceUserListLocTitle, PaneLocations.left);
									howToShowModes = new(this, "Ways to show modes", Rsrcs
										.strGlobalAppearanceUserListWaysToShowModesTitle, Rsrcs.strGlobalAppearanceUserListWaysToShowModesDesc,
										WaysToShowUserModes.symbols);
									sortByMode = new(this, "Sort by mode", Rsrcs.strGlobalAppearanceUserListSortByModeTitle, Rsrcs
										.strGlobalAppearanceUserListSortByModeDesc, true);
								}

								internal UserListPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.AppearanceDTO.UserListDTO dto) :
									base(mgrParent, "Time Stamp", Rsrcs.strGlobalAppearanceUserListLocTitle, Rsrcs.strGlobalAppearanceUserListLocDesc)
								{
									location = new(this, "Location", Rsrcs.strGlobalAppearanceUserListLocTitle, Rsrcs
										.strGlobalAppearanceUserListLocTitle, PaneLocations.left, dto.Loc);
									howToShowModes = new(this, "Ways to show modes", Rsrcs
										.strGlobalAppearanceUserListWaysToShowModesTitle, Rsrcs.strGlobalAppearanceUserListWaysToShowModesDesc,
										WaysToShowUserModes.symbols, dto.HowToShowModes);
									sortByMode = new(this, "Sort by mode", Rsrcs.strGlobalAppearanceUserListSortByModeTitle, Rsrcs
										.strGlobalAppearanceUserListSortByModeDesc, true, dto.SortByMode);
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
							#endregion

							#region Members
								private readonly Item<PaneLocations> location;

								private readonly Item<WaysToShowUserModes> howToShowModes;

								private readonly Item<bool> sortByMode;
							#endregion

							#region Properties
								public Item<PaneLocations> Loc
									=> location;

								public Item<WaysToShowUserModes> HowToShowModes
									=> howToShowModes;

								public Item<bool> SortByMode
									=> sortByMode;
							#endregion

							#region Methods
								private void FirePropChanged(string strWhichProp)
									=> PropertyChanged?.Invoke(this, new(strWhichProp));

								public DTO.PrefsDTO.GlobalDTO.AppearanceDTO.UserListDTO ToDTO()
									=> new(location.CurVal, howToShowModes.CurVal, sortByMode.CurVal);
							#endregion

							#region Event Handlers
							#endregion
						}
					#endregion

					#region Members
						private readonly ConfModePrefs confMode;

						private readonly TimeStampPrefs timestamp;

						private readonly UserListPrefs userlist;
					#endregion

					#region Properties
						public ConfModePrefs ConfMode => confMode;

						public TimeStampPrefs TimeStamp => timestamp;

						public UserListPrefs UserList => userlist;
					#endregion

					#region Methods
					#endregion

					#region Event Handlers
					#endregion
				}

				public class PluginPrefs : AbstractChildMgr
				{
					#region Constructors & Deconstructors
						public PluginPrefs(AbstractMgr mgrParent) : base(mgrParent, "Plugins", Rsrcs
							.strGlobalPluginsTitle, Rsrcs.strGlobalPluginsDesc)
						{
							ext = new(this);
						}

						internal PluginPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.PluginsDTO dto) : base(mgrParent,
							"Plugins", Rsrcs.strGlobalPluginsTitle, Rsrcs.strGlobalPluginsDesc)
						{
							ext = new(this, dto.Ext);
						}
					#endregion

					#region Delegates
					#endregion

					#region Events
						public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
					#endregion

					#region Constants
					#endregion

					#region Helper Types
						public class ExtPrefs : AbstractChildMgr
						{
							#region Constructors & Deconstructors
								public ExtPrefs(AbstractMgr mgrParent, System.Collections.Generic.IEnumerable<ScriptEntry>? scripts =
									null, System.Collections.Generic.IEnumerable<ProgramEntry>? programs = null) :
									base(mgrParent, "External", Rsrcs.strGlobalPluginsExtTitle, Rsrcs.strGlobalPluginsDesc)
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

								internal ExtPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO dto) :
									base(mgrParent, "External", Rsrcs.strGlobalPluginsExtTitle, Rsrcs.strGlobalPluginsDesc)
								{
									whereToLook = new(this, dto.WhereToLook);
									howToRunScripts = new(this);

									if(dto.Scripts is System.Collections.Generic.IEnumerable<DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ScriptEntryDTO>
										scripts)
									{
										this.scripts.AddRange(scripts.Select(curScript => new ScriptEntry(curScript)));
										foreach(ScriptEntry curScript in this.scripts)
											curScript.evtDirtyChanged += OnScriptDirtyChanged;
									}

									if(dto.Programs is System.Collections.Generic.IEnumerable<DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ProgramEntryDTO>
										programs)
									{
										this.programs.AddRange(dto.Programs.Select(curProgram => new ProgramEntry(curProgram)));
										foreach(ProgramEntry curProgram in this.programs)
											curProgram.evtDirtyChanged += OnProgramDirtyChanged;
									}
								}
							#endregion

							#region Delegates
							#endregion

							#region Events
								public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
							#endregion

							#region Constants
							#endregion

							#region Helper Types
								public class WhereToLookPrefs : AbstractChildMgr
								{
									#region Constructors & Deconstructors
										public WhereToLookPrefs(AbstractMgr mgrParent) :
											base(mgrParent, "Where To Look", Rsrcs.strGlobalPluginsExtWhereToLookTitle, Rsrcs
												.strGlobalPluginsExtWhereToLookDesc)
										{
											llistPaths = [];

											includeSysPath = new(this, "Include Your System Path Environment Variable in the Search", Rsrcs
												.strGlobalPluginsExtWhereToLookIncludeSysPathTitle, Rsrcs.strGlobalPluginsExtWhereToLookIncludeSysPathDesc, true);
										}

										internal WhereToLookPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.WhereToLookDTO dto) :
											base(mgrParent, "Where To Look", Rsrcs.strGlobalPluginsExtWhereToLookTitle,Rsrcs
												.strGlobalPluginsExtWhereToLookDesc)
										{
											llistPaths = new(dto.Paths is string[] paths ? paths : []);

											includeSysPath = new(this, "Include Your System Path Environment Variable in the Search",
												Rsrcs.strGlobalPluginsExtWhereToLookIncludeSysPathTitle, Rsrcs.strGlobalPluginsExtWhereToLookIncludeSysPathDesc, dto
												.IncludeSysPaths);
										}
									#endregion

									#region Delegates
									#endregion

									#region Events
										public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
									#endregion

									#region Constants
									#endregion

									#region Helper Types
									#endregion

									#region Members
										private readonly System.Collections.Generic.LinkedList<string> llistPaths;

										private readonly Item<bool> includeSysPath;
									#endregion

									#region Properties
										public System.Collections.Generic.IEnumerable<string> Paths
											=> llistPaths;

										public Item<bool> IncludeSysPath
											=> includeSysPath;
									#endregion

									#region Methods
										public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.WhereToLookDTO ToDTO()
											=> new([.. llistPaths], includeSysPath.CurVal);
									#endregion

									#region Event Handlers
									#endregion
								}

								public class HowToRunScriptsPrefs : AbstractChildMgr
								{
									#region Constructors & Deconstructors
										public HowToRunScriptsPrefs(AbstractMgr mgrParent) :
											base(mgrParent, "How to run them", Rsrcs.strGlobalPluginsHowToRunThemTitle, Rsrcs
												.strGlobalPluginsHowToRunThemDesc)
										{
											groupedByWhatRunsThem = new(this);

											ungrouped = new(this);
										}
									#endregion

									#region Delegates
									#endregion

									#region Events
										public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
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
												public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
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
												public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
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
										public ScriptEntry(in string strFileNameExtOrMask, in System.IO.FileInfo? fileProgramNeeded, in string strParamsToPass,
											in bool bEnabled)
										{
											this.strFileNameExtOrMask = strFileNameExtOrMask;
											this.fileProgramNeeded = fileProgramNeeded;
											this.strParamsToPass = strParamsToPass;
											this.bEnabled = bEnabled;
										}

										internal ScriptEntry(in DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ScriptEntryDTO dto)
										{
											strFileNameExtOrMask = dto.FileNameExtOrMask;
											fileProgramNeeded = dto.ProgramNeeded is string strProgramNeeded ? new(strProgramNeeded) : null;
											strParamsToPass = dto.ParamsToPass;
											bEnabled = dto.Enabled;
										}
									#endregion

									#region Delegates
									#endregion

									#region Events
										public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
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
										private void FirePropChanged(string strNameOfChangedProp) => PropertyChanged?.Invoke(this, new
											(strNameOfChangedProp));

										public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ScriptEntryDTO ToDTO()
											=> new(strFileNameExtOrMask, fileProgramNeeded?.FullName, strParamsToPass, bEnabled);
									#endregion

									#region Event Handlers
									#endregion
								}

								public class ProgramEntry : Obj<ProgramEntry>
								{
									#region Constructors & Deconstructors
										public ProgramEntry(in string strName, in System.IO.FileInfo fileProgram, in string strParamsToPass, in bool
											bEnabled)
										{
											this.strName = strName;
											this.fileProgram = fileProgram;
											this.strParamsToPass = strParamsToPass;
											this.bEnabled = bEnabled;
										}

										internal ProgramEntry(in DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ProgramEntryDTO dto)
										{
											strName = dto.Name;
											fileProgram = new(dto.Program);
											strParamsToPass = dto.ParamsToPass;
											bEnabled = dto.Enabled;
										}
									#endregion

									#region Delegates
									#endregion

									#region Events
										public override event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
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
										private void FirePropChanged(string strNameOfChangedProp) => PropertyChanged?.Invoke(this, new
											(strNameOfChangedProp));

										public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO.ProgramEntryDTO ToDTO()
											=> new(strName, fileProgram.FullName, strParamsToPass, bEnabled);
									#endregion

									#region Event Handlers
									#endregion
								}
							#endregion

							#region Members
								private readonly WhereToLookPrefs whereToLook;

								private readonly HowToRunScriptsPrefs howToRunScripts;

								private readonly System.Collections.Generic.List<ScriptEntry> scripts = [];

								private readonly System.Collections.Generic.List<ProgramEntry> programs = [];
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
						public ExtPrefs Ext => ext;
			#endregion

					#region Methods
						public DTO.PrefsDTO.GlobalDTO.PluginsDTO ToDTO()
							=> new(ext.ToDTO());
					#endregion

					#region Event Handlers
					#endregion
				}
			#endregion

			#region Members
				private readonly PluginPrefs plugin;
			#endregion

			#region Properties
				public abstract AppearancePrefsType Appearance
				{
					get;
				}

				public PluginPrefs Plugins
					=> plugin;
			#endregion

			#region Methods
			#endregion

			#region Event Handlers
			#endregion
		}
	#endregion

	#region Members
		private readonly System.Collections.Generic.SortedDictionary<string, AbstractChildMgr> mapMgrsForProtolsByName =
			[];

		private static System.IO.FileInfo? fileOurSettings = null;

		private static System.Xml.Serialization.XmlSerializer? serializer;
	#endregion

	#region Properties
		public abstract GlobalPrefsType Global
		{
			get;
		}
	#endregion

	#region Methods
		protected static MainDtoType? Load<MainDtoType>(in System.IO.DirectoryInfo dirLocalDataLoc)
			where MainDtoType : DTO.PrefsDTO
		{
			fileOurSettings ??= new(System.IO.Path.Combine(dirLocalDataLoc.FullName, "settings.json"));

			return fileOurSettings.Exists
				? System.Text.Json.JsonSerializer.Deserialize<MainDtoType>(fileOurSettings.OpenRead(), jsoStandard)
				: null;
		}

		protected abstract DTO.PrefsDTO ToDTO();

		public void Save(in System.IO.DirectoryInfo dirLocalDataLoc)
		{
			if(fileOurSettings == null)
				throw new System.InvalidProgramException("The settings file wasn't set before attempting to save preferences.");

			using(System.IO.FileStream stream = fileOurSettings.OpenWrite())
				System.Text.Json.JsonSerializer.Serialize(stream, ToDTO(), jsoStandard);

			Protocol.MgrBase.Instance.TellAllProtocolsToSave(dirLocalDataLoc);
		}
	#endregion

	#region Event Handlers
	#endregion
}