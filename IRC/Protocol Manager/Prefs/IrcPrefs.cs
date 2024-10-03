// Ignore Spelling: Prefs

using System.Linq;

namespace BestChat.IRC.ProtocolMgr.Prefs;

public class IrcPrefs : Data.Prefs.Prefs<GlobalPrefs, DTO.GlobalDTO>
{
	#region Constructors & Deconstructors
		private IrcPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
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
		private static IrcPrefs? instance;

		private readonly GlobalPrefs global;
	#endregion

	#region Properties
		public new static IrcPrefs? Instance
			=> instance;

		public override GlobalPrefs Global
			=> global;
	#endregion

	#region Methods
		public static IrcPrefs InitInstance(Platform.DataAndExt.Prefs.AbstractMgr mgrParent)
			=> instance ??= new(mgrParent);

		public override DTO.IrcDTO ToDTO()
			=> new(
				Global.ToDTO(),
				Networks.Select(netCur
						=> netCur.ToDTO()
				).ToArray()
			);
	#endregion

	#region Event Handlers
	#endregion
}