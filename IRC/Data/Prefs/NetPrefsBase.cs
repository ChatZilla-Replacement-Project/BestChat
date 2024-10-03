namespace BestChat.IRC.Data.Prefs;

public abstract class NetPrefsBase : Platform.DataAndExt.Prefs.AbstractChildMgr
{
	protected NetPrefsBase(in Platform.DataAndExt.Prefs.AbstractMgr mgrParent, in string strName, in string
			strLocalizedName, in string strLocalizedLongDesc, Defs.Net netOwner) :
		base(in mgrParent, in strName, in strLocalizedName, in strLocalizedLongDesc)
		=> OwnerNet = netOwner;

	public abstract DTO.NetDTO ToDTO();

	public Defs.Net OwnerNet
	{
		get;

		private init;
	}

	public abstract NetTimeStampPrefs TimeStamps
	{
		get;
	}

	public abstract NetDccPrefs DCC
	{
		get;
	}

	public abstract NetAutoPerformPrefs AutoPerform
	{
		get;
	}

	public abstract NetConnPrefs Conn
	{
		get;
	}

	public abstract NetAliasesPrefs Aliases
	{
		get;
	}

	public abstract NetAltNicksPrefs AltNicks
	{
		get;
	}

	public abstract NetStalkWordsPrefs StalkWords
	{
		get;
	}

	public abstract NetNotifyWhenOnlinePrefs NotifyWhenOnline
	{
		get;
	}

	public abstract NetChanListPrefs KnownChans
	{
		get;
	}
}