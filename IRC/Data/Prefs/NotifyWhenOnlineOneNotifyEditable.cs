namespace BestChat.IRC.Data.Prefs;

public partial class NotifyWhenOnlineOneNotifyEditable : NotifyWhenOnlineOneNotify, System.ComponentModel
	.INotifyDataErrorInfo
{
	public NotifyWhenOnlineOneNotifyEditable(in NotifyWhenOnlineOneNotify notifyOriginal) :
		base(notifyOriginal, notifyOriginal.cmgrParent)
		=> this.notifyOriginal = notifyOriginal;

	public event System.EventHandler<System.ComponentModel.DataErrorsChangedEventArgs>? ErrorsChanged;

	public readonly NotifyWhenOnlineOneNotify notifyOriginal;

	public NotifyWhenOnlineOneNotify Original
		=> notifyOriginal;

	public new string WhatToFollow
	{
		get => base.WhatToFollow;

		set
		{
			if(base.WhatToFollow != value)
			{
				base.WhatToFollow = value;

				WereChangesMade = true;

				ErrorsChanged?.Invoke(this, new(nameof(WhatToFollow)));
			}
		}
	}

	public bool WereChangesMade
	{
		get;

		set;
	}

	public bool HasErrors
		=> base.WhatToFollow != "" || notifyOriginal.cmgrParent.Entries.ContainsKey(base.WhatToFollow) && !WereChangesMade
			|| GetValidNickTester().IsMatch(base.WhatToFollow);

	public bool IsValid
		=> !HasErrors;

	public System.Collections.IEnumerable GetErrors(string? _)
		=> base.WhatToFollow == ""
			? (new[]
			{
				Rsrcs.strWhatToFollowIsBlank,
			})
			: notifyOriginal.cmgrParent.Entries.ContainsKey(base.WhatToFollow) && !WereChangesMade
				? (new[]
				{
					Rsrcs.strWhatToFollowIsBlank,
				})
				: GetValidNickTester().IsMatch(base.WhatToFollow)
					? (new[]
					{
						Rsrcs.strWhatToFollowNotValidNick,
					})
					: (System.Collections.IEnumerable)System.Array.Empty<string>();

	public void Save()
		=> notifyOriginal.SaveFrom(this);

	[System.Text.RegularExpressions.GeneratedRegex(@"[\s!@#$%^\&\*\(\)-=+\[\]\\\{\}\|;:'"",<\.>/\?]")]
	private static partial System.Text.RegularExpressions.Regex GetValidNickTester();
}