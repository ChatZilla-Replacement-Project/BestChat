// Ignore Spelling: Prefs DTO Conf

namespace BestChat.Platform.DataAndExt.Prefs.DTO;

public abstract record PrefsDTO(PrefsDTO.GlobalDTO Global)
{
	public abstract record GlobalDTO(GlobalDTO.GeneralDTO General, GlobalDTO.AppearanceDTO Appearance, GlobalDTO.PluginsDTO Plugins)
	{
		public abstract record AppearanceDTO(AppearanceDTO.ConfModeDTO ConfMode, AppearanceDTO.TimeStampDTO TimeStamp,
			AppearanceDTO.UserListDTO UserList)
		{
			public record ConfModeDTO(bool ConfModeEnabled = false, int UserLimitBeforeTrigger = 150, bool ActionsCollapsed = false, bool
				MsgsCollapsed = false);

			public record TimeStampDTO(bool Show = true, string Fmt = "G");

			public record UserListDTO(PaneLocations Loc = PaneLocations.left, WaysToShowUserModes HowToShowModes = WaysToShowUserModes.symbols,
				bool SortByMode = true);
		}

		public abstract AppearanceDTO Appearance
		{
			get;
		}

		public record PluginsDTO(PluginsDTO.ExtDTO Ext)
		{
			public record ExtDTO(ExtDTO.WhereToLookDTO WhereToLook, ExtDTO.ScriptEntryDTO[]? Scripts = null, ExtDTO.ProgramEntryDTO[]? Programs
				= null)
			{
				public record WhereToLookDTO(string[]? Paths = null, bool IncludeSysPaths = true);

				public record ScriptEntryDTO(string FileNameExtOrMask, string? ProgramNeeded, string ParamsToPass, bool Enabled);

				public record ProgramEntryDTO(string Name, string Program, string ParamsToPass, bool Enabled);
			}
		}

		public record GeneralDTO(GeneralDTO.ConnDTO Conn)
		{
			public record ConnDTO(bool IsIndentEnabled = true, bool IsAutoReconnectEnabled = true, bool
				IsRejoinAfterKickEnabled = true, string CharEncoding = "UTF-8", bool IsUnlimitedAttemptsOn =
				true, int MaxAttempts = 1, string? DefQuitMsg = null)
			{
			}
		}
	}

	public abstract GlobalDTO Global
	{
		get;
	}
}