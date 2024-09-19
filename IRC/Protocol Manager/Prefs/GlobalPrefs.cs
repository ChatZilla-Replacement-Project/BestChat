// Ignore Spelling: Prefs dto

namespace BestChat.IRC.ProtocolMgr.Prefs;

public class GlobalPrefs : Data.Prefs.Prefs<GlobalPrefs, DTO.GlobalDTO>.GlobalPrefs
{
	#region Constructors & Deconstructors
		public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent)
		{
		}

		public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.GlobalDTO dto)
			: base(mgrParent, dto)
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
		public class Fmt : Platform.DataAndExt.Prefs.AbstractChildMgr
		{
			#region Constructors & Deconstructors
				public Fmt(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
					base(mgrParent, "Format", Rsrcs.strPrefsGlobalFmtTitle, Rsrcs.strTranslatedProtDesc)
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
	#endregion

	#region Properties
	#endregion

	#region Methods
	#endregion

	#region Event Handlers
		public override DTO.GlobalDTO ToDTO()
			=> new DTO.GlobalDTO(
				AutoPerform.ToDTO(),
				DCC.ToDTO(),
				Conn.ToDTO(),
				Aliases.ToDTO(),
				AltNicks.ToDTO(),
				StalkWords.ToDTO()
			);
	#endregion
}