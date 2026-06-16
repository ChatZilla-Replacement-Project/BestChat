namespace BestChat.Platform.UI.Desktop.Prefs;

public class GlobalNotificationsPrefs : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	public GlobalNotificationsPrefs(GlobalPrefs cmgrParent) :
		base(cmgrParent, "Notifications", Rsrcs.strGlobalNotificationsTitle, Rsrcs.strGlobalNotificationsDesc)
	{
		method = new(this, @"Method", Rsrcs.strGlobalNotificationMethod, Rsrcs.strGlobalNotificationMethodDesc,
			NotificationMethods.popupWnd);
		enableHistory = new(this, @"Enable History", Rsrcs.strGlobalNotificationsEnableHistory, Rsrcs
			.strGlobalNotificationsEnableHistoryDesc, true);
		playSound = new(this, @"Play sound during notifications", Rsrcs.strGlobalNotificationsPlaySound, Rsrcs
			.strGlobalNotificationsPlaySoundDesc, true);
		soundToPlay = new(this, @"What sound should be played?", Rsrcs.strGlobalNotificationsPlaySound, Rsrcs
			.strGlobalNotificationsPlaySoundDesc, null);
		keepVisibleFor = new(this, @"Keep notification visible for this many seconds.", Rsrcs
			.strGlobalNotificationKeepVisibleFor, Rsrcs.strGlobalNotificationsKeepVisibleForDesc, 10, iMinVal: 1);
	}

	internal GlobalNotificationsPrefs(GlobalPrefs cmgrParent, DTO.RootDTO.GlobalDTO.NotificationsDTO dto) :
		base(cmgrParent, "Notifications", Rsrcs.strGlobalNotificationsTitle, Rsrcs.strGlobalNotificationsDesc)
	{
		method = new(this, "Method", Rsrcs.strGlobalNotificationMethod, Rsrcs.strGlobalNotificationMethodDesc,
			NotificationMethods.popupWnd, dto.Method);
		enableHistory = new(this, "Enable History", Rsrcs.strGlobalNotificationsEnableHistory, Rsrcs
			.strGlobalNotificationsEnableHistoryDesc, true, dto.EnableNotifications);
		playSound = new(this, @"Play sound during notifications", Rsrcs.strGlobalNotificationsPlaySound, Rsrcs
			.strGlobalNotificationsPlaySoundDesc, true, dto.PlaySound);
		soundToPlay = new(this, @"What sound should be played?", Rsrcs.strGlobalNotificationsPlaySound, Rsrcs
			.strGlobalNotificationsPlaySoundDesc, null, dto.SoundToPlay);
		keepVisibleFor = new(this, @"Keep notification visible for this many seconds.", Rsrcs
			.strGlobalNotificationKeepVisibleFor, Rsrcs.strGlobalNotificationsKeepVisibleForDesc, 10, dto
			.KeepVisibleFor, iMinVal: 1);
	}


	private readonly DataAndExt.Prefs.Item<NotificationMethods> method;

	private readonly DataAndExt.Prefs.Item<bool> enableHistory;

	private readonly DataAndExt.Prefs.Item<bool> playSound;

	private readonly DataAndExt.Prefs.Item<System.IO.FileInfo?> soundToPlay;

	private readonly DataAndExt.Prefs.IntItem keepVisibleFor;


	public DataAndExt.Prefs.Item<NotificationMethods> Method
		=> method;

	public DataAndExt.Prefs.Item<bool> EnableHistory
		=> enableHistory;

	public DataAndExt.Prefs.Item<bool> PlaySound
		=> playSound;

	public DataAndExt.Prefs.Item<System.IO.FileInfo?> SoundToPlay
		=> soundToPlay;

	public DataAndExt.Prefs.IntItem KeepVisibleFor
		=> keepVisibleFor;


	internal DTO.RootDTO.GlobalDTO.NotificationsDTO ToDTO()
		=> new(
			method.CurVal,
			enableHistory.CurVal,
			playSound.CurVal,
			soundToPlay.CurVal,
			keepVisibleFor.CurVal
		);
}