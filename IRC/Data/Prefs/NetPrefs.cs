namespace BestChat.IRC.Data.Prefs;

public class NetPrefs<GlobalPrefsType, GlobalDtoType> : NetPrefsBase
	where GlobalPrefsType : GlobalPrefs<GlobalDtoType>
	where GlobalDtoType : DTO.GlobalDTO
{
	#region Constructors & Deconstructors
		public NetPrefs(Prefs<GlobalPrefsType, GlobalDtoType> mgrParent, Defs.Net netOwner) :
			base(mgrParent, "IRC", PrefsRsrcs.strNetTitle, PrefsRsrcs.strNetTitle, netOwner)
		{
			if(Prefs<GlobalPrefsType, GlobalDtoType>.Instance is null)
				throw new System.InvalidOperationException("No global object.  Was it initalized?");

			dcc = new(this);
			autoPeform = new(this, Prefs<GlobalPrefsType, GlobalDtoType>.Instance.Global.AutoPerform);
			conn = new(this);
			aliases = new(this, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.Aliases);
			altNicks = new(this, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.AltNicks);
			notifyWhenOnline = new(this);
			stalkWords = new(this, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.StalkWords);
			knownChans = new(this);
		}

		public NetPrefs(Prefs<GlobalPrefsType, GlobalDtoType> mgrParent, DTO.NetDTO dto) :
			base(mgrParent, "IRC", PrefsRsrcs.strNetTitle, PrefsRsrcs.strNetTitle, (Defs.Net?)Defs.Net.AllInstancesByGUID[dto
				.OwnerNet] ?? throw new System.InvalidOperationException("While loading IRC preferences, found preferences for"
				+ $" {dto.OwnerNet}, but we can't find that network."))
		{
			if(Prefs<GlobalPrefsType, GlobalDtoType>.Instance is null)
				throw new System.InvalidOperationException("No global object.  Was it initalized?");

			dcc = new(this, dto.DCC);
			autoPeform = new(this, dto.AutoPerform, Prefs<GlobalPrefsType, GlobalDtoType>.Instance.Global
				.AutoPerform);
			conn = new(this, dto.Conn);
			aliases = new(this, dto.Aliases, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.Aliases);
			altNicks = new(this, dto.AltNicks, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global.AltNicks);
			notifyWhenOnline = new(this, dto.NotifyWhenOnline);
			stalkWords = new(this, dto.StalkWords, Prefs<GlobalPrefsType, GlobalDtoType>.Instance!.Global
				.StalkWords);
			knownChans = new(this, dto.KnownChans);
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
		private readonly NetDccPrefs dcc;

		private readonly NetAutoPerformPrefs autoPeform;

		private readonly NetConnPrefs conn;

		private readonly NetAliasesPrefs aliases;

		private readonly NetAltNicksPrefs altNicks;

		private readonly NetStalkWordsPrefs stalkWords;

		private readonly NetNotifyWhenOnlinePrefs notifyWhenOnline;

		private readonly NetChanListPrefs knownChans;
	#endregion

	#region Properties
		public override NetDccPrefs DCC
			=> dcc;

		public override NetAutoPerformPrefs AutoPerform
			=> autoPeform;

		public override NetConnPrefs Conn
			=> conn;

		public override NetAliasesPrefs Aliases
			=> aliases;

		public override NetAltNicksPrefs AltNicks
			=> altNicks;

		public override NetStalkWordsPrefs StalkWords
			=> stalkWords;

		public override NetNotifyWhenOnlinePrefs NotifyWhenOnline
			=> notifyWhenOnline;

		public override NetChanListPrefs KnownChans
			=> knownChans;

		public override bool CanBeRemoved
			=> true;
	#endregion

	#region Methods
		public override DTO.NetDTO ToDTO()
			=> new(
				OwnerNet.guid,
				dcc.ToDTO(),
				autoPeform.ToDTO(),
				conn.ToDTO(),
				aliases.ToDTO(),
				altNicks.ToDTO(),
				stalkWords.ToDTO(),
				notifyWhenOnline.ToDTO(),
				knownChans.ToDTO()
			);
	#endregion

	#region Event Handlers
	#endregion
}