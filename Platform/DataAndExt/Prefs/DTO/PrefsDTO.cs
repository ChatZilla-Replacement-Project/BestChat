// Ignore Spelling: Prefs DTO Conf

namespace BestChat.Platform.DataAndExt.Prefs.DTO;

public abstract record PrefsDTO
(
) : AbstractMgr.AbstractDTO("IRC")
{
	public abstract record GlobalDTO
	(
		GlobalDTO.PluginsDTO Plugins
	) : AbstractMgr.AbstractDTO("Global")
	{
		public abstract record AppearanceDTO
		(
			AppearanceDTO.ConfModeDTO ConfMode,
			AppearanceDTO.TimeStampDTO TimeStamp,
			AppearanceDTO.UserListDTO UserList,
			AppearanceDTO.MsgGroupsDTO MsgGroups
		) : AbstractMgr.AbstractDTO("Global/Appearance")
		{
			public record ConfModeDTO
			(
				bool ConfModeEnabled = false,
				int UserLimitBeforeTrigger = 150,
				bool ActionsCollapsed = false,
				bool MsgsCollapsed = false
			) : AbstractMgr.AbstractDTO("Global/Appearance/ConfMode");

			public record TimeStampDTO
			(
				bool Show = true,
				string Fmt = "G",
				string? KeyOverride = null,
				PrefsBase.GlobalPrefs.TimeStampPrefs.HowOftenToRepeatOpts HowOftenToRepeat = PrefsBase.GlobalPrefs
					.TimeStampPrefs.HowOftenToRepeatOpts.everyThirtySeconds
			) : AbstractMgr.AbstractDTO(KeyOverride ?? "Global/Appearance/TimeStamp");

			public record UserListDTO
			(
				PaneLocations Loc = PaneLocations.left,
				WaysToShowUserModes HowToShowModes = WaysToShowUserModes.symbols,
				bool SortByMode = true
			) : AbstractMgr.AbstractDTO("Global/Appearance/UserList");

			public record MsgGroupsDTO
			(
				bool Enabled,
				bool LimitMsgsPerGroup,
				int MaxMsgsPerGroup,
				System.TimeSpan? HowLongToWaitBeforeStartingNewGroup = null
			);
		}

		public abstract AppearanceDTO BaseAppearance
		{
			get;
		}

		public record PluginsDTO
		(
			PluginsDTO.ExtDTO Ext
		) : AbstractMgr.AbstractDTO("Global/Plugins")
		{
			public record ExtDTO 
			(
				ExtDTO.WhereToLookDTO WhereToLook,
				ExtDTO.ScriptEntryDTO[]? Scripts = null,
				ExtDTO.ProgramEntryDTO[]? Programs = null
			) : AbstractMgr.AbstractDTO("Global/Plugins/Ext")
			{
				public record WhereToLookDTO(System.IO.DirectoryInfo[]? Paths = null, bool IncludeSysPaths =
					true);

				public record ScriptEntryDTO(string FileNameExtOrMask, System.IO.FileInfo? ProgramNeeded,
					string ParamsToPass, bool Enabled);

				public record ProgramEntryDTO(string Name, System.IO.FileInfo Program, string ParamsToPass,
					bool Enabled);
			}
		}
	}

	public abstract GlobalDTO BaseGlobal
	{
		get;
	}
}