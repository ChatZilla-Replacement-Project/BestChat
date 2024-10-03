namespace BestChat.IRC.Data.Prefs;

public class NetDccPrefs : GlobalDccPrefs
{
	#region Constructors & Deconstructors
		public NetDccPrefs(NetPrefsBase mgrParent) :
			base(mgrParent)
			=> @override = new(mgrParent, "Override", PrefsRsrcs
				.strNetDccOverrideTitle, PrefsRsrcs.strNetDccOverrideDesc, false);

		public NetDccPrefs(NetPrefsBase mgrParent, DTO.NetDccDTO dto) :
			base(mgrParent, dto)
			=> @override = new(mgrParent, "Override", PrefsRsrcs
				.strNetDccOverrideTitle, PrefsRsrcs.strNetDccOverrideDesc, false, dto.Override);
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
		private readonly Platform.DataAndExt.Prefs.Item<bool> @override;
	#endregion

	#region Properties
		public Platform.DataAndExt.Prefs.Item<bool> Override
			=> @override;
	#endregion

	#region Methods
		public override DTO.NetDccDTO ToDTO()
			=> new(@override.CurVal, Enabled.CurVal, GetIpFromServer.CurVal, DownloadsFolder.CurVal,
				[.. Ports]);
	#endregion

	#region Event Handlers
	#endregion
}