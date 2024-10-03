namespace BestChat.IRC.Data.Prefs
{
public class NetConnPrefs : GlobalConnPrefs
{
	#region Constructors & Deconstructors
	public NetConnPrefs(NetPrefsBase mgrParent) :
		base(mgrParent)
		=> @override = new(mgrParent, "Override", PrefsRsrcs
			.strNetConnOverrideTitle, PrefsRsrcs.strNetConnOverrideDesc, false);

	public NetConnPrefs(NetPrefsBase mgrParent, DTO.NetConnDTO dto) :
		base(mgrParent)
		=> @override = new(mgrParent, "Override", PrefsRsrcs
			.strNetConnOverrideTitle, PrefsRsrcs.strNetConnOverrideDesc, false, dto.Override);
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
	public override DTO.NetConnDTO ToDTO()
		=> new(
			@override.CurVal,
			EnableIdent.CurVal,
			AutoReconnect.CurVal,
			RejoinAfterKick.CurVal,
			CharEncoding.CurVal,
			UnlimitedAttempts.CurVal,
			MaxAttempts.CurVal,
			DefQuitMsg.CurVal
		);
	#endregion

	#region Event Handlers
	#endregion
}
}