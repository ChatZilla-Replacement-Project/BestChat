// Ignore Spelling: prefs Conf dto Ungrouped Msgs Loc

namespace BestChat.Platform.DataAndExt.Prefs;

public abstract class PrefsBase : AbstractMgr
{
}

public abstract class Prefs<GlobalPrefsType, AppearancePrefsType> : PrefsBase
	where GlobalPrefsType : GlobalPrefsBase<GlobalPrefsType, AppearancePrefsType>
	where AppearancePrefsType : GlobalAppearancePrefsBase
{
	#region Constructors & Deconstructors
		protected Prefs()
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
		private readonly System.Collections.Generic.SortedDictionary<string, AbstractChildMgr> mapMgrsForProtocolsByName =
			[];

		private static System.IO.FileInfo? fileOurSettings = null;
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
			fileOurSettings ??= new(System.IO.Path.Combine(dirLocalDataLoc.FullName,
				"settings.json"));

			return fileOurSettings.Exists
				? System.Text.Json.JsonSerializer.Deserialize<MainDtoType>(fileOurSettings.OpenRead(),
					jsoStandard)
				: null;
		}

		protected abstract DTO.PrefsDTO ToDTO();

		public void Save()
		{
			if(fileOurSettings == null)
				throw new System.InvalidProgramException("The settings file wasn't set before " +
					"attempting to save preferences.");

			using(System.IO.FileStream stream = fileOurSettings.OpenWrite())
				System.Text.Json.JsonSerializer.Serialize(stream, ToDTO(), jsoStandard);

			Protocol.MgrBase.Instance.TellAllProtocolsToSave();
		}
	#endregion

	#region Event Handlers
	#endregion
}