namespace BestChat.Platform.UI.Desktop.Prefs;

public enum NotificationMethods
{
	[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strNotificationMethodOff), "Off", nameof(Rsrcs.strNotificationMethodOffDesc),
		@"Best Chat won’t display any notifications.  You’ll need to check yourself.  However, notifications will appear " +
		@"in the notifications view.", typeof(NotificationMethods))]
	off,

	[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strNotificationMethodPopupWnd), "Popup Window", nameof(Rsrcs
		.strNotificationMethodPopupWndDesc), @"Best Chat will use one of its own windows to display the notification.  You’ll"
		+ @" need to rely on Best Chat’s notification view to see history.", typeof(NotificationMethods))]
	popupWnd,

	[DataAndExt.Attr.LocalizedDesc(nameof(Rsrcs.strNotificationMethodOsBased), "OS based", nameof(Rsrcs
		.strNotificationMethodOsBasedDesc), @"Best Chat will send notifications to your OS's notification system.  This may "
		+ @"mean those notifications will be mixed in with notifications from other applications.  You may or may not "
		+ @"prefer that.", typeof(NotificationMethods))]
	osBased,
}