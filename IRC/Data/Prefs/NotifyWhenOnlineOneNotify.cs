namespace BestChat.IRC.Data.Prefs;

public class NotifyWhenOnlineOneNotify : Platform.DataAndExt.Obj<NotifyWhenOnlineOneNotify>
{
	public NotifyWhenOnlineOneNotify(in string strWhatToFollow, in NetNotifyWhenOnlinePrefs cmgrParent)
	{
		this.strWhatToFollow = strWhatToFollow;
		this.cmgrParent = cmgrParent;
	}

	public NotifyWhenOnlineOneNotify(in NotifyWhenOnlineOneNotify notifyCopyThis, in NetNotifyWhenOnlinePrefs cmgrParent)
	{
		strWhatToFollow = notifyCopyThis.strWhatToFollow;
		this.cmgrParent = cmgrParent;
	}


	public event DFieldChanged<string>? evtWhatToFollowChanged;


	public readonly NetNotifyWhenOnlinePrefs cmgrParent;

	private string strWhatToFollow;


	public string WhatToFollow
	{
		get => strWhatToFollow;

		set
		{
			if(strWhatToFollow != value)
			{
				string strOldWhatToFollow = strWhatToFollow;

				strWhatToFollow = value;

				MakeDirty();

				FireWhatToFollowChanged(strOldWhatToFollow);
			}
		}
	}

	private void FireWhatToFollowChanged(in string strOldWhatToFollow)
	{
		FirePropChanged(strOldWhatToFollow);

		evtWhatToFollowChanged?.Invoke(this, strOldWhatToFollow, strWhatToFollow);
	}

	public NotifyWhenOnlineOneNotifyEditable MakeEditable()
		=> new(this);

	public void SaveFrom(in NotifyWhenOnlineOneNotifyEditable enotify)
		=> WhatToFollow = enotify.WhatToFollow;
}