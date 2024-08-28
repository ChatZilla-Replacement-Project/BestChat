// Ignore Spelling: Prefs DTO Conf

namespace BestChat.Platform.DataAndExt.Prefs.DTO;

public abstract record PrefsDTO
(
) : AbstractMgr.AbstractDTO("IRC")
{
	public abstract record GlobalDTO
	(
		//GlobalDTO.GeneralDTO General,
		GlobalDTO.PluginsDTO Plugins
	) : AbstractMgr.AbstractDTO("Global")
	{
		public abstract record AppearanceDTO
		(
			AppearanceDTO.ConfModeDTO ConfMode,
			AppearanceDTO.TimeStampDTO TimeStamp,
			AppearanceDTO.UserListDTO UserList
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
				string Fmt = "G"
			) : AbstractMgr.AbstractDTO("Global/Appearance/TimeStamp");

			public record UserListDTO
			(
				PaneLocations Loc = PaneLocations.left,
				WaysToShowUserModes HowToShowModes = WaysToShowUserModes.symbols,
				bool SortByMode = true
			) : AbstractMgr.AbstractDTO("Global/Appearance/UserList");
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
				public record WhereToLookDTO(string[]? Paths = null, bool IncludeSysPaths = true);

				public record ScriptEntryDTO(string FileNameExtOrMask, string? ProgramNeeded, string ParamsToPass, bool Enabled);

				public record ProgramEntryDTO(string Name, string Program, string ParamsToPass, bool Enabled);
			}
		}

		public record GeneralDTO
		(
			GeneralDTO.ConnDTO Conn
		)
		{
			public record ConnDTO
			(
				bool IsIdentEnabled = true,
				bool IsAutoReconnectEnabled = true,
				bool IsRejoinAfterKickEnabled = true,
				string CharEncoding = "UTF-8",
				bool IsUnlimitedAttemptsOn = true,
				int MaxAttempts = 1,
				string? DefQuitMsg = null
			)
			{
			}
		}
	}

	public abstract GlobalDTO BaseGlobal
	{
		get;
	}
}