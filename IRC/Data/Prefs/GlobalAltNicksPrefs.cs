using System.Linq;

namespace BestChat.IRC.Data.Prefs;

public class GlobalAltNicksPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	#region Constructors & Deconstructors
		public GlobalAltNicksPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
			base(mgrParent, "Alternate Nicks", PrefsRsrcs.strGlobalAltNicksTitle, PrefsRsrcs
				.strGlobalAltNicksDesc)
			=> entries = new(this, "Entries", PrefsRsrcs
				.strGlobalAltNicksTitle, PrefsRsrcs.strGlobalAltNicksDesc, []);

		public GlobalAltNicksPrefs(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in DTO.GlobalOneAltNickDTO[]? dto):
			base(mgrParent, "Alternate Nicks", PrefsRsrcs.strGlobalAltNicksTitle, PrefsRsrcs
				.strGlobalAltNicksDesc)
			=> entries = new(
				this,
				"Entries",
				PrefsRsrcs.strGlobalAltNicksTitle,
				PrefsRsrcs.strGlobalAltNicksDesc,
				[],
				dto?.Select(danickCur
					=> new GlobalAltNicksOneAltNick(danickCur, this)
				) ?? []
			);
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
		private readonly Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAltNicksOneAltNick> entries;
	#endregion

	#region Properties
		public Platform.DataAndExt.Prefs.ReorderableListItem<GlobalAltNicksOneAltNick> Entries
			=> entries;
	#endregion

	#region Methods
		public DTO.GlobalOneAltNickDTO[]? ToDTO()
			=> entries.Select(aliasCur
				=> aliasCur.ToDTO()
			).ToArray();
	#endregion

	#region Event Handlers
	#endregion
}