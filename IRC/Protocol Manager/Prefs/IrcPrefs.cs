// Ignore Spelling: Prefs

using System.Linq;
using BestChat.IRC.Data.Prefs.DTO;

namespace BestChat.IRC.ProtocolMgr.Prefs;

public class IrcPrefs : Data.Prefs.Prefs<GlobalPrefs, DTO.GlobalDTO>
{
	#region Constructors & Deconstructors
		public IrcPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent)
		{
			instance = this;

			global = new(this);
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
		private static IrcPrefs? instance = null;

		private readonly Prefs.GlobalPrefs global;
	#endregion

	#region Properties
		public static IrcPrefs? Instance
			=> instance;

		public override Prefs.GlobalPrefs Global
			=> global;
	#endregion

	#region Methods
		public static IrcPrefs InitInstance(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
			=> instance ??= InitInstance(mgrParent);
	#endregion

	#region Event Handlers
		public override IrcDTO<DTO.GlobalDTO> ToDTO()
			=> new DTO.IrcDTO(Global.ToDTO(), Networks.Select(netCur => netCur.ToDTO()).ToArray());
	#endregion
}