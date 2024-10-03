// Ignore Spelling: Prefs evt Dcc dto Ip Ctnts istep Chans

using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public abstract class Prefs<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalPrefsType : GlobalPrefs<GlobalDtoType>
	where GlobalDtoType : DTO.GlobalDTO
{
	#region Constructors & Deconstructors
		protected Prefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) : base(mgrParent, "IRC",
			PrefsRsrcs.strIrcRootTitle, PrefsRsrcs.strIrcRootDesc)
		{
			instance = this;

			listNetworks = [];
		}

		protected Prefs(Platform.DataAndExt.Prefs.AbstractChildMgr mgrParent, DTO.IrcDTO<GlobalDtoType> dto) :
			base(mgrParent, "IRC", PrefsRsrcs.strIrcRootTitle, PrefsRsrcs.strIrcRootDesc)
		{
			instance = this;

			listNetworks = dto.Networks is null
				? []
				: [.. dto.Networks.Select(netdtoCur => new NetPrefs<GlobalPrefsType, GlobalDtoType>(this,
					netdtoCur))];
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
		private static Prefs<GlobalPrefsType, GlobalDtoType>? instance = null;

		private readonly System.Collections.Generic.List<NetPrefs<GlobalPrefsType, GlobalDtoType>> listNetworks;
	#endregion

	#region Properties
		public static Prefs<GlobalPrefsType, GlobalDtoType>? Instance
			=> instance;

		public abstract GlobalPrefsType Global
		{
			get;
		}

		public System.Collections.Generic.IReadOnlyList<NetPrefs<GlobalPrefsType, GlobalDtoType>> Networks
			=> listNetworks;
	#endregion

	#region Methods
		public abstract DTO.IrcDTO<GlobalDtoType> ToDTO();
	#endregion

	#region Event Handlers
	#endregion
}