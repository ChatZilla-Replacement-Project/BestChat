﻿namespace BestChat.IRC.Data.Prefs
{
public class NetTimeStampPrefs : Platform.DataAndExt.Prefs.GlobalAppearanceTimeStampPrefs
{
	#region Constructors & Deconstructors
	public NetTimeStampPrefs(NetPrefsBase mgrParent) :
		base(mgrParent)
		=> @override = new(mgrParent, "Override", PrefsRsrcs.strNetTimeStampOverrideNetTitle, PrefsRsrcs
			.strNetTimeStampOverrideNetDesc,false);

	public NetTimeStampPrefs(NetPrefsBase mgrParent, DTO.NetTimeStampDTO dto) :
		base(mgrParent, dto)
		=> @override = new(mgrParent, "Override", PrefsRsrcs.strNetTimeStampOverrideNetTitle, PrefsRsrcs
			.strNetTimeStampOverrideNetDesc, false, dto.Override);
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
	public override DTO.NetTimeStampDTO ToDTO()
		=> new(@override.CurVal, Show.CurVal, Fmt.CurVal);
	#endregion

	#region Event Handlers
	#endregion
}
}