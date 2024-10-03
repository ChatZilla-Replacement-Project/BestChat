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
				GlobalAppearanceTimeStampPrefs.HowOftenToRepeatOpts HowOftenToRepeat =
					GlobalAppearanceTimeStampPrefs.HowOftenToRepeatOpts.everyThirtySeconds
			) : AbstractMgr.AbstractDTO(KeyOverride ?? "Global/Appearance/TimeStamp");

			// ReSharper disable once InconsistentNaming
			public record MsgGroupsDTO
			(
				bool Enabled,
				bool LimitMsgsPerGroup,
				int MaxMsgsPerGroup,
				System.TimeSpan? HowLongToWaitBeforeStartingNewGroup = null
			) : DataAndExt.Prefs.AbstractMgr.AbstractDTO("Global/Appearance/MsgGroups");
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
				public record WhereToLookDTO
				(
					System.IO.DirectoryInfo[]? Paths = null,
					bool IncludeSysPaths = true
				) : AbstractMgr.AbstractDTO("Global/Plugins/Ext/WhereToLook");

				public record ScriptEntryDTO
				(
					string FileNameExtOrMask,
					System.IO.FileInfo? ProgramNeeded,
					string ParamsToPass,
					bool Enabled
				) : AbstractMgr.AbstractDTO("Global/Plugins/Ext/Script");

				public record ProgramEntryDTO
				(
					string Name,
					System.IO.FileInfo Program,
					string ParamsToPass,
					bool Enabled
				) : AbstractMgr.AbstractDTO("Global/Plugins/Ext/Program");
			}
		}
	}

	public abstract GlobalDTO BaseGlobal
	{
		get;
	}
}