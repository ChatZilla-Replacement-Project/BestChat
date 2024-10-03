namespace BestChat.IRC.Data.Prefs
{
public class GlobalConnPrefs<GlobalPrefsType, GlobalDtoType> : Platform.DataAndExt.Prefs.AbstractChildMgr
	where GlobalPrefsType : GlobalPrefs<GlobalPrefsType, GlobalDtoType>
	where GlobalDtoType : DTO.IrcDTO<GlobalDtoType>.GlobalDTO
{
	#region Constructors & Deconstructors
	public GlobalConnPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent) :
		base(mgrParent,"Connection", PrefsRsrcs.strGlobalConnTitle, PrefsRsrcs.strGlobalConnDesc)
	{
		enableIndent = new(this, "Enable Ident", PrefsRsrcs
			.strGlobalConnEnableIdentTitle, PrefsRsrcs.strGlobalConnEnableIdentDesc, true);

		autoReconnect = new(this, "Auto Reconnect", PrefsRsrcs
				.strGlobalConnAutoReconnectTitle, PrefsRsrcs.strGlobalConnAutoReconnectDesc,
			true);

		rejoinAfterKick = new(this, "Rejoin After Kick",
			PrefsRsrcs.strGlobalConnRejoinAfterKickTitle, PrefsRsrcs
				.strGlobalConnRejoinAfterKickDesc, true);

		characterEncoding = new(this, "Character Encoding", PrefsRsrcs
				.strGlobalConnCharEncodingTitle, PrefsRsrcs.strGlobalConnCharEncodingDesc,
			"UTF-8");

		unlimitedAttempts = new(this, "Unlimited Reconnection Attempts",
			PrefsRsrcs.strGlobalConnUnlimitedAttemptsTitle, PrefsRsrcs
				.strGlobalConnUnlimitedAttemptsDesc, true);

		maxAttempts = new(this, "Maximum Attempts to Reconnect", PrefsRsrcs
				.strGlobalConnMaxAttemptsTitle, PrefsRsrcs.strGlobalConnMaxAttemptsDesc, 1,
			iMinVal: 1);

		defQuitMsg = new(this, "Default Quit message", PrefsRsrcs
			.strGlobalConnDefQuitMsgTitle, PrefsRsrcs.strGlobalConnDefQuitMsgDesc, PrefsRsrcs
			.strDefQuitMsg);
	}

	internal GlobalConnPrefs(Platform.DataAndExt.Prefs.AbstractMgr mgrParent, DTO.IrcDTO<GlobalDtoType>
		.GlobalDTO.ConnDTO dto) :
		base(mgrParent, "Connection", PrefsRsrcs.strGlobalConnTitle, PrefsRsrcs.strGlobalConnDesc)
	{
		enableIndent = new(this, "Enable Ident", PrefsRsrcs
			.strGlobalConnEnableIdentTitle, PrefsRsrcs.strGlobalConnEnableIdentDesc, true, dto
			.IsIdentEnabled);

		autoReconnect = new(this, "Auto Reconnect", PrefsRsrcs
				.strGlobalConnAutoReconnectTitle, PrefsRsrcs.strGlobalConnAutoReconnectDesc, true,
			dto.IsAutoReconnectEnabled);

		rejoinAfterKick = new(this, "Rejoin After Kick", PrefsRsrcs
				.strGlobalConnRejoinAfterKickTitle, PrefsRsrcs.strGlobalConnRejoinAfterKickDesc,
			true, dto.IsRejoinAfterKickEnabled);

		characterEncoding = new(this, "Character Encoding", PrefsRsrcs
				.strGlobalConnCharEncodingTitle, PrefsRsrcs.strGlobalConnCharEncodingDesc,
			"UTF-8", dto.CharEncoding);

		unlimitedAttempts = new(this, "Unlimited Reconnection Attempts",
			PrefsRsrcs.strGlobalConnUnlimitedAttemptsTitle, PrefsRsrcs
				.strGlobalConnUnlimitedAttemptsDesc, true, dto.IsUnlimitedAttemptsOn);

		maxAttempts = new(this, "Maximum Attempts to Reconnect", PrefsRsrcs
				.strGlobalConnMaxAttemptsTitle, PrefsRsrcs.strGlobalConnMaxAttemptsDesc, 1,
			iMinVal: 1, iCurVal: dto.MaxAttempts);

		defQuitMsg = new(this, "Default Quit message", PrefsRsrcs
			.strGlobalConnDefQuitMsgTitle, PrefsRsrcs.strGlobalConnDefQuitMsgDesc, PrefsRsrcs
			.strDefQuitMsg, dto.DefQuitMsg ?? PrefsRsrcs.strDefQuitMsg);
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
	private readonly Platform.DataAndExt.Prefs.Item<bool> enableIndent;
	private readonly Platform.DataAndExt.Prefs.Item<bool> autoReconnect;
	private readonly Platform.DataAndExt.Prefs.Item<bool> rejoinAfterKick;
	private readonly Platform.DataAndExt.Prefs.Item<string> characterEncoding;
	private readonly Platform.DataAndExt.Prefs.Item<bool> unlimitedAttempts;
	private readonly Platform.DataAndExt.Prefs.IntItem maxAttempts;
	private readonly Platform.DataAndExt.Prefs.Item<string> defQuitMsg;
	// TODO: Add proxy once we know what we're doing with that.
	#endregion

	#region Properties
	public Platform.DataAndExt.Prefs.Item<bool> EnableIdent
		=> enableIndent;

	public Platform.DataAndExt.Prefs.Item<bool> AutoReconnect
		=> autoReconnect;

	public Platform.DataAndExt.Prefs.Item<bool> RejoinAfterKick
		=> rejoinAfterKick;

	public Platform.DataAndExt.Prefs.Item<string> CharEncoding
		=> characterEncoding;

	public Platform.DataAndExt.Prefs.Item<bool> UnlimitedAttempts
		=> unlimitedAttempts;

	public Platform.DataAndExt.Prefs.IntItem MaxAttempts
		=> maxAttempts;

	public Platform.DataAndExt.Prefs.Item<string> DefQuitMsg
		=> defQuitMsg;
	#endregion

	#region Methods
	public virtual DTO.IrcDTO<GlobalDtoType>.GlobalDTO.ConnDTO ToDTO()
		=> new(
			enableIndent.CurVal,
			autoReconnect.CurVal,
			rejoinAfterKick.CurVal,
			characterEncoding.CurVal,
			unlimitedAttempts.CurVal,
			maxAttempts.CurVal,
			defQuitMsg.CurVal);
	#endregion

	#region Event Handlers
	#endregion
}
}