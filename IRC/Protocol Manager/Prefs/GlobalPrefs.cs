// Ignore Spelling: Prefs dto Mirc Tele

namespace BestChat.IRC.ProtocolMgr.Prefs;

public class GlobalPrefs : Data.Prefs.GlobalPrefs<DTO.GlobalDTO>
{
	#region Constructors & Deconstructors
		public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent)
			=> fmt = new(this);

		public GlobalPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.GlobalDTO dto)
			: base(mgrParent, dto)
			=> fmt =new(this, dto.Fmt);
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
		private readonly GlobalFmtPrefs fmt;
	#endregion

	#region Properties
		public GlobalFmtPrefs Fmt
			=> fmt;
	#endregion

	#region Methods
		public override DTO.GlobalDTO ToDTO()
			=> new(
				AutoPerform.ToDTO(),
				DCC.ToDTO(),
				Conn.ToDTO(),
				fmt.ToDTO(),
				Aliases.ToDTO(),
				AltNicks.ToDTO(),
				StalkWords.ToDTO()
			);
	#endregion

	#region Event Handlers
	#endregion
}