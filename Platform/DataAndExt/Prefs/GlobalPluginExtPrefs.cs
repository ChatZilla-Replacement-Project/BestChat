using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BestChat.Platform.DataAndExt.Prefs;

public class GlobalPluginExtPrefs : AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalPluginExtPrefs(AbstractMgr mgrParent, System.Collections.Generic
			.IEnumerable<GlobalPluginExtScriptEntry>? scripts = null, System.Collections.Generic
			.IEnumerable<GlobalPluginExtProgramEntry>? programs = null) :
			base(mgrParent, "External", Rsrcs.strGlobalPluginsExtTitle, Rsrcs.strGlobalPluginsDesc)
		{
			whereToLook = new(this);
			globalPluginExtHowToRunScripts = new(this);
			this.scripts = new(
				this,
				"Scripts",
				Rsrcs.strGlobalPluginExtScriptsTitle,
				Rsrcs.strGlobalPluginExtScriptsDesc,
				[],
				scripts is null
					? []
					: [.. scripts,],
				scriptCur
					=> scriptCur.FileNameExtOrMask,
				(scriptEntry, evth)
					=> scriptEntry.evtFileNameExtOrMaskChanged += mapScriptChangeSubscribers[evth] = (in
							GlobalPluginExtScriptEntry scriptEntry, in string strOldFileNameExtOrMask, in string _)
						=> evth(strOldFileNameExtOrMask, scriptEntry),
				(scriptEntry, evth)
					=>
						{
							scriptEntry.evtFileNameExtOrMaskChanged -= mapScriptChangeSubscribers[evth];

							mapScriptChangeSubscribers.Remove(evth);
						}
			);
			this.programs = new(
				this,
				"Programs",
				Rsrcs.strGlobalPluginExtProgramsTitle,
				Rsrcs.strGlobalPluginExtProgramsDesc,
				[],
				programs is null
					? []
					: [.. programs,],
				programCur
					=> programCur.Name,
				(programEntry, evth)
					=> programEntry.evtNameChanged += mapProgramChangeSubscribers[evth] = (in GlobalPluginExtProgramEntry
							scriptEntry, in string strOldName, in string _)
							=> evth(strOldName, scriptEntry),
				(programEntry, evth)
					=>
						{
							programEntry.evtNameChanged -= mapProgramChangeSubscribers[evth];

							mapProgramChangeSubscribers.Remove(evth);
						}
			);
		}

		internal GlobalPluginExtPrefs(AbstractMgr mgrParent, DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO dto)
			: base(mgrParent, "External", Rsrcs.strGlobalPluginsExtTitle, Rsrcs.strGlobalPluginsDesc)
		{
			whereToLook = new(this, dto.WhereToLook);
			globalPluginExtHowToRunScripts = new(this);
			scripts = new(
				this,
				"Scripts",
				Rsrcs.strGlobalPluginExtScriptsTitle,
				Rsrcs.strGlobalPluginExtScriptsDesc,
				[],
				dto.Scripts is null
					? []
					: [.. dto.Scripts.Select(dscriptCur
						=> new GlobalPluginExtScriptEntry(dscriptCur)),],
				scriptCur
					=> scriptCur.FileNameExtOrMask,
				(scriptEntry, evth)
					=> scriptEntry.evtFileNameExtOrMaskChanged += mapScriptChangeSubscribers[evth] = (in
						GlobalPluginExtScriptEntry scriptEntry, in string strOldFileNameExtOrMask, in string
						strNewFileNameExtOrMask)
							=> evth(strOldFileNameExtOrMask, scriptEntry),
						(scriptEntry,
								evth)
							=>
								{
									scriptEntry.evtFileNameExtOrMaskChanged -= mapScriptChangeSubscribers[evth];

									mapScriptChangeSubscribers.Remove(evth);
								}
			);
			programs = new(
				this,
				"Programs",
				Rsrcs.strGlobalPluginExtProgramsTitle,
				Rsrcs.strGlobalPluginExtProgramsDesc,
				[],
				dto.Programs is null
					? []
					: [.. dto.Programs.Select(programCur
						=> new GlobalPluginExtProgramEntry(programCur)),],
				programCur
					=> programCur.Program.FullName,
				(programEntry, evth)
					=> programEntry.evtNameChanged += mapProgramChangeSubscribers[evth] = (in GlobalPluginExtProgramEntry
						programEntry, in string strOldName, in string strNewName)
							=> evth(strOldName, programEntry),
				(programEntry,
						evth)
					=>
						{
							programEntry.evtNameChanged -= mapProgramChangeSubscribers[evth];

							mapProgramChangeSubscribers.Remove(evth);
						}
			);
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
		private readonly GlobalPluginExtWhereToLookPrefs whereToLook;

		private readonly GlobalPluginExtHowToRunScriptsPrefs globalPluginExtHowToRunScripts;

		private readonly MappedListItem<string, GlobalPluginExtScriptEntry> scripts;

		private readonly MappedListItem<string, GlobalPluginExtProgramEntry> programs;

		private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalPluginExtScriptEntry>,
			Obj<GlobalPluginExtScriptEntry>.DFieldChanged<string>> mapScriptChangeSubscribers = [];

		private readonly System.Collections.Generic.Dictionary<System.Action<string, GlobalPluginExtProgramEntry>,
			Obj<GlobalPluginExtProgramEntry>.DFieldChanged<string>> mapProgramChangeSubscribers = [];
	#endregion

	#region Properties
		public GlobalPluginExtWhereToLookPrefs WhereToLook
			=> whereToLook;

		public GlobalPluginExtHowToRunScriptsPrefs GlobalPluginExtHowToRunThem
			=> globalPluginExtHowToRunScripts;

		public MappedListItem<string, GlobalPluginExtScriptEntry> Scripts
			=> scripts;

		public MappedListItem<string, GlobalPluginExtProgramEntry> Programs
			=> programs;
	#endregion

	#region Methods
		public DTO.PrefsDTO.GlobalDTO.PluginsDTO.ExtDTO ToDTO()
			=> new(
				whereToLook.ToDTO(),
				scripts.Values.Select(scriptCur
					=> scriptCur.ToDTO()
				).ToArray(),
				programs.Values.Select(progCur
					=> progCur.ToDTO()
				).ToArray()
			);
	#endregion

	#region Event Handlers
		private void OnScriptDirtyChanged(in GlobalPluginExtScriptEntry scriptSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}

		private void OnProgramDirtyChanged(in GlobalPluginExtProgramEntry progSender, in bool bIsNowDirty)
		{
			if(bIsNowDirty)
				MakeDirty();
		}
	#endregion
}