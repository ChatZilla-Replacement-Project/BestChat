namespace BestChat.IRC.Data.Prefs;

public class GlobalAltNicksOneAltNickEditable : GlobalAltNicksOneAltNick
{
	public GlobalAltNicksOneAltNickEditable(GlobalAltNicksOneAltNick nickOriginalAltNick) :
		base(nickOriginalAltNick.NickToUse, nickOriginalAltNick.mgrParent)
		=> this.nickOriginalAltNick = nickOriginalAltNick;

	public readonly GlobalAltNicksOneAltNick nickOriginalAltNick;

	public GlobalAltNicksOneAltNick OriginalNick
		=> nickOriginalAltNick;

	public void Save()
		=> nickOriginalAltNick.SaveFrom(this);

	public bool WereChangesMade
	{
		get;

		private set;
	}

	public new string NickToUse
	{
		get => base.NickToUse;

		set
		{
			if(base.NickToUse != value)
			{
				base.NickToUse = value;

				WereChangesMade = true;

				FirePropChanged(nameof(WereChangesMade));
			}
		}
	}

	public bool HasErrors
		=> false; // TODO: Come up with an actual value

	public bool IsValid
		=> !HasErrors;
}